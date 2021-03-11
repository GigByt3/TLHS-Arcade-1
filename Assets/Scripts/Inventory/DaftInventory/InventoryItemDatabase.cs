using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;


namespace ProjectRefuge.Inventory
{
    /// <summary>
    /// The InventoryItemDatabase 
    /// </summary>
    public static class InventoryItemDatabase
    {
        private const string ITEM_DATABASE_PATH = "ItemDatabase/";
        private const bool SHOULD_LOAD_IN_STATIC_CONSTRUCTOR = true;

        private static bool isItemDatabaseLoaded = false;
        public static bool IsItemDatabaseLoaded { get { return isItemDatabaseLoaded; } }

        private static Dictionary<string, InventoryItemDefinition> definitionsByName = new Dictionary<string, InventoryItemDefinition>();

        /// <summary>
        /// Static constructor loads all items from the item database folder
        /// into memory.
        /// </summary>
        static InventoryItemDatabase()
        {

            if (SHOULD_LOAD_IN_STATIC_CONSTRUCTOR)
            {
                LoadAssetsSynchronous();
            }

        }

        /// <summary>
        /// Synchronously loads all assets in the ITEM_DATABASE_PATH as items.
        /// </summary>
        private static void LoadAssetsSynchronous()
        {
            InventoryItemDefinition[] definitions = Resources.LoadAll<InventoryItemDefinition>(ITEM_DATABASE_PATH);

            foreach (InventoryItemDefinition definition in definitions)
            {
                definitionsByName.Add(definition.itemName, definition);
            }

            isItemDatabaseLoaded = true;
        }

        /// <summary>
        /// Returns the inventory item definition with the given name, or null if
        /// none such inventory item definition exists.
        /// </summary>
        public static InventoryItemDefinition GetDefinitionByName(string itemName)
        {
            if (!isItemDatabaseLoaded)
            {
                Debug.LogError("FAILED TO GET DEFINITION: DATABASE NOT LOADED.");
            }

            if (definitionsByName.ContainsKey(itemName))
            {
                return definitionsByName[itemName];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Returns an array containing all loaded definitions.
        /// </summary>
        /// <returns>All loaded definitions in the database.</returns>
        public static InventoryItemDefinition[] GetAllDefinitions()
        {
            InventoryItemDefinition[] definitions = new InventoryItemDefinition[definitionsByName.Count];

            int i = 0;
            foreach(KeyValuePair<string, InventoryItemDefinition> pair in definitionsByName)
            {
                definitions[i] = pair.Value;
                ++i;
            }

            return definitions;
        }

    }
}

