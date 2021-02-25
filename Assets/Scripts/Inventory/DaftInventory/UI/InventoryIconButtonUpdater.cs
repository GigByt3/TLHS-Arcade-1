using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace ProjectRefuge.Inventory.UI
{
    /// <summary>
    /// InventoryIconButtonUpdater listens for events from the PlayerInventory
    /// and draws changes to the inventory onto the screen.
    /// </summary>
    public class InventoryIconButtonUpdater : MonoBehaviour
    {

        // Player inventory to listen to.
        private PlayerInventory playerInventory;

        [SerializeField]
        private GameObject inventoryItemButtonPrefab;

        [SerializeField]
        private List<InventoryIconButtonController> controllers = new List<InventoryIconButtonController>();

        /// <summary>
        /// OnEnable is called at initialization and when the updater is enabled.
        /// </summary>
        private void OnEnable()
        {
            playerInventory = PlayerInventory.Instance;
            playerInventory.InventoryItemUpdated += PlayerInventoryInventoryItemUpdated;
        }

        /// <summary>
        /// OnDisable is called at destruction and when the updater is disabled.
        /// </summary>
        private void OnDisable()
        {
            if (playerInventory != null)
            {
                playerInventory.InventoryItemUpdated -= PlayerInventoryInventoryItemUpdated;
            }
        }

        /// <summary>
        /// Fired whenever the inventory item of the given definition changes quantity within
        /// the inventory
        /// </summary>
        private void PlayerInventoryInventoryItemUpdated(InventoryItemDefinition item, int qty)
        {
            // Linear search to find controller for specific button.
            InventoryIconButtonController found = null;
            foreach (InventoryIconButtonController iconButtonController in controllers)
            {
                if (iconButtonController.itemDefinition == item)
                {
                    found = iconButtonController;
                    break;
                }
            }

            // Added from zero? Create!
            if (qty > 0 && found == null)
            {
                GameObject newGo = Instantiate(inventoryItemButtonPrefab) as GameObject;
                newGo.transform.SetParent(transform);
                newGo.SetActive(true);
                found = newGo.GetComponent<InventoryIconButtonController>();
                found.UpdateInternalData(item, qty);
                controllers.Add(found);
            }
            // Removed all of item? Delete!
            else if (qty <= 0 && found != null)
            {
                Destroy(found.gameObject);
                controllers.Remove(found);
            }
            // Just update if not in either case.
            else if (found != null)
            {
                found.UpdateInternalData(item, qty);
            }
        }
    }
}
