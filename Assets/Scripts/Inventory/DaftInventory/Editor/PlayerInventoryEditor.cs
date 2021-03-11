using System;
using System.Collections.Generic;

using UnityEditor;

namespace ProjectRefuge.Inventory
{
    /// <summary>
    /// PlayerInventoryEditor draws the inventory items in the inventory
    /// at the given time.
    /// </summary>
    [CustomEditor(typeof(PlayerInventory))]
    public class PlayerInventoryEditor : Editor
    {
        /// <summary>
        /// Drawn whenever the inspector is redrawn.
        /// </summary>
        public override void OnInspectorGUI()
        {
            PlayerInventory inventory = serializedObject.context as PlayerInventory;

            EditorGUILayout.LabelField("isDebug", serializedObject.FindProperty("isDebug").boolValue.ToString());

            if (inventory != null)
            {
                EditorGUILayout.LabelField("Items", EditorStyles.foldoutHeader);
                foreach(KeyValuePair<InventoryItemDefinition, int> pair in inventory.InternalInventory)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField(pair.Key.name);
                    EditorGUILayout.LabelField(pair.Value.ToString());

                    EditorGUILayout.EndHorizontal();
                }
            }

        }
    }
}
