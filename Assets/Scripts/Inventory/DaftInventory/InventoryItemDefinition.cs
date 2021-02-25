using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectRefuge.Inventory
{
    [CreateAssetMenu(fileName = "NewInventoryItem", menuName = "ProjectRefuge/InventoryItem", order = 1)]
    [Serializable]
    public class InventoryItemDefinition : ScriptableObject
    {
        // Data
        public string itemName;        // Name of item
        public string description;     // Description of item
        public Sprite icon;            // Icon of item
    }
}

