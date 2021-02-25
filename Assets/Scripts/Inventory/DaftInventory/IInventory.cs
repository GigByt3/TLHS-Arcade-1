using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRefuge.Inventory
{
    /// <summary>
    /// Defines a common interface for components that act like inventories.
    /// </summary>
    public interface IInventory
    {
        bool CanAddInventoryItem(InventoryItemDefinition definition, int qty);
        bool AddInventoryItem(InventoryItemDefinition definition, int qty);
        bool HasInventoryItem(InventoryItemDefinition definition);
        bool HasInventoryItem(InventoryItemDefinition defintion, int qty);
        bool CanRemoveInventoryItem(InventoryItemDefinition definition, int qty);
        bool RemoveInventoryItem(InventoryItemDefinition definition, int qty);
    }
}


