using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace ProjectRefuge.Inventory
{
    /// <summary>
    /// Represents a player's inventory.
    /// </summary>
    public class PlayerInventory : MonoBehaviour, IInventory
    {

        #region Singleton Pattern

        // Singleton instance.
        private static PlayerInventory instance;
        public static PlayerInventory Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Awake is fired pre-initialization.
        /// </summary>
        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Cannot have two PlayerInventory in a game.");
            }

            instance = this;
        }

        /// <summary>
        /// Destroy is fired when the object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }

            // Also remove event listeners
            foreach(Action<InventoryItemDefinition, int> eventHandler in InventoryItemUpdated.GetInvocationList())
            {
                Debug.Log("Removed event handlers.");
                InventoryItemUpdated -= eventHandler;
            }
        }

        #endregion

        #region Events

        public event Action<InventoryItemDefinition, int> InventoryItemUpdated = delegate { };

        #endregion Events

        public Dictionary<InventoryItemDefinition, int> InternalInventory { get; private set; } = new Dictionary<InventoryItemDefinition, int>();

        #region Unity Methods

        // Debug fields.
        public bool isDebug = false;
        private Rect debugWindowRect = new Rect(0, 0, 200, 200);
        private Vector2 scrollRect = Vector2.zero;

        private InventoryItemDefinition[] definitions;

        /// <summary>
        /// Start is called at initialization.
        /// </summary>
        private void Start()
        {
            definitions = InventoryItemDatabase.GetAllDefinitions();
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F3))
            {
                isDebug = !isDebug;
            }
        }

        /// <summary>
        /// Draws the inventory debug window. 
        /// </summary>
        private void InventoryDebugWindow(int id)
        {

            GUILayout.Label("Items");

            foreach(KeyValuePair<InventoryItemDefinition, int> pair in InternalInventory)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label(pair.Key.itemName);
                GUILayout.Label(pair.Value.ToString());

                GUILayout.EndHorizontal();
            }

            // ----

            
            scrollRect = GUILayout.BeginScrollView(scrollRect);
            
            foreach(InventoryItemDefinition definition in definitions)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Label(definition.itemName);
                if (GUILayout.Button("+"))
                {
                    AddInventoryItem(definition, 1);
                }
                if (GUILayout.Button("-"))
                {
                    RemoveInventoryItem(definition, 1);
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();

            GUI.DragWindow();
        }

        /// <summary>
        /// OnGUI is called whenever the GUI is redrawn.
        /// </summary>
        private void OnGUI()
        {
            if (isDebug)
            {
                debugWindowRect = GUI.Window(2999, debugWindowRect, InventoryDebugWindow, "InventoryDebug");
                debugWindowRect = new Rect(
                    Mathf.Clamp(debugWindowRect.x, 0, Screen.width - debugWindowRect.width),
                    Mathf.Clamp(debugWindowRect.y, 0, Screen.height - debugWindowRect.height),
                    debugWindowRect.width,
                    debugWindowRect.height
                );
            }
        }

        #endregion

        #region Inventory Methods

        /// <summary>
        /// Adds the inventory item of the given definition to the inventory.
        /// </summary>
        /// <param name="definition">Definition of the item.</param>
        /// <param name="qty">Quantity of the item to add.</param>
        /// <returns></returns>
        public bool AddInventoryItem(InventoryItemDefinition definition, int qty)
        {
            if (CanAddInventoryItem(definition, qty))
            {
                if (InternalInventory.ContainsKey(definition))
                {
                    InternalInventory[definition] += qty;
                }
                else
                {
                    InternalInventory.Add(definition, qty);
                }

                InventoryItemUpdated?.Invoke(
                    definition,
                    Mathf.Clamp(
                        InternalInventory.ContainsKey(definition) ? InternalInventory[definition] : 0,
                        0,
                        int.MaxValue
                    )
                );

                return true;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Returns true if the inventory item can be added.
        /// </summary>
        public bool CanAddInventoryItem(InventoryItemDefinition definition, int qty)
        {
            return true;
        }

        /// <summary>
        /// Returns true if the item can be removed, false if there are not enough units
        /// of the item in the inventory to remove.
        /// </summary>
        /// <param name="definition">The item to remove.</param>
        /// <param name="qty">Amount of item to remove.</param>
        /// <returns></returns>
        public bool CanRemoveInventoryItem(InventoryItemDefinition definition, int qty)
        {
            return HasInventoryItem(definition);
        }

        /// <summary>
        /// Returns true if the inventory has at least one of the given item.
        /// </summary>
        /// <param name="definition">The item of which to check if there is at least one unit.</param>
        /// <returns>True if the inventory has at least one of the given item, false otherwise.</returns>
        public bool HasInventoryItem(InventoryItemDefinition definition)
        {
            return InternalInventory.ContainsKey(definition);
        }

        /// <summary>
        /// Returns true if the inventory has at least the given quantity of the given item.
        /// </summary>
        /// <param name="defintion">The item to check.</param>
        /// <param name="qty">At least this quantity required in order to return true.</param>
        /// <returns>True if the inventory has at least the given quantity of the item, false otherwise.</returns>
        public bool HasInventoryItem(InventoryItemDefinition defintion, int qty)
        {
            return InternalInventory.ContainsKey(defintion) && InternalInventory[defintion] >= qty;
        }

        /// <summary>
        /// Removes the given quantity of the given item from the inventory. Returns false
        /// if there are not enough items to remove.
        /// </summary>
        /// <param name="definition">The item to remove.</param>
        /// <param name="qty">The amount to remove.</param>
        /// <returns>True if successful, false if there were not enough of the item to remove.</returns>
        public bool RemoveInventoryItem(InventoryItemDefinition definition, int qty)
        {
            if (CanRemoveInventoryItem(definition, qty))
            {
                if (InternalInventory[definition] <= qty)
                {
                    InternalInventory.Remove(definition);
                }
                else
                {
                    InternalInventory[definition] -= qty;
                }

                // Fire event
                InventoryItemUpdated?.Invoke(
                    definition,
                    Mathf.Clamp(
                        InternalInventory.ContainsKey(definition) ? InternalInventory[definition] : 0,
                        0,
                        int.MaxValue
                    )
                );

                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
