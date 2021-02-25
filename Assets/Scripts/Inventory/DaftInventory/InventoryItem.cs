using System;
using UnityEngine;

namespace ProjectRefuge.Inventory
{
    /// <summary>
    /// Represents an inventory item in an inventory.
    /// </summary>
    [Serializable]
    public class InventoryItem
    {
        [SerializeField]
        private string inventoryItemName;
        public string InventoryItemName { get { return inventoryItemName; } }

        private InventoryItemDefinition definition;
        public InventoryItemDefinition Definition
        {
            get
            {
                // If null (IE deserialized,) retrieve the item from the database.
                if (definition == null)
                {
                    definition = InventoryItemDatabase.GetDefinitionByName(inventoryItemName);
                }

                return definition;
            }
        }

        [SerializeField]
        private int qty;
        public int Qty { get { return qty; } set { qty = value; } }

        /// <summary>
        /// Empty constructor for serialization.
        /// </summary>
        public InventoryItem() { }

        /// <summary>
        /// Standard constructor
        /// </summary>
        public InventoryItem(InventoryItemDefinition definition, int qty)
        {
            this.qty = qty;
            this.definition = definition;
            this.inventoryItemName = this.definition.itemName;
        }

    }

}
