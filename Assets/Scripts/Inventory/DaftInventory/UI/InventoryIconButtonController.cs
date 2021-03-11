using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ProjectRefuge.Inventory.UI
{
    /// <summary>
    /// InventoryIconButtonController controls the buttons for each inventory item.
    /// </summary>
    public class InventoryIconButtonController : MonoBehaviour
    {

        // Inventory
        public InventoryItemDefinition itemDefinition;
        public int qty;

        // Hover pane reference.
        public InventoryHoverPanelController hoverPanelController;

        // UI elements
        [SerializeField]
        private Image itemIcon;
        private Text itemQtyText;
        private EventTrigger eventTrigger;

        // Animate Quantity
        private bool isRunning;
        public float inventoryQuantityAnimationDuration = 1f;
        private float baseTextSize = 0;
        public float fontSizeBiggerMax = 16f;
        private float t;
        public AnimationCurve inventoryQuantityAnimationCurve;

        /// <summary>
        /// Start is called at initialization.
        /// </summary>
        private void Awake()
        {
            //itemIcon = GetComponentInChildren<Image>();
            itemQtyText = GetComponentInChildren<Text>();
            baseTextSize = itemQtyText.fontSize;
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            if (isRunning)
            {
                t += Time.deltaTime;

                float fontSizeBiggerThisFrame = 
                    inventoryQuantityAnimationCurve.Evaluate(t / inventoryQuantityAnimationDuration) * 
                    (fontSizeBiggerMax - baseTextSize);
                itemQtyText.fontSize = Mathf.RoundToInt(baseTextSize + fontSizeBiggerThisFrame);
                itemQtyText.fontStyle = FontStyle.Bold;

                if (t >= inventoryQuantityAnimationDuration)
                {
                    isRunning = false;
                    itemQtyText.fontStyle = FontStyle.Normal;
                }
            }
        }

        /// <summary>
        /// Updates the data this inventory icon represents.
        /// </summary>
        public void UpdateInternalData(InventoryItemDefinition itemDefinition, int qty)
        {
            this.itemDefinition = itemDefinition;
            this.qty = qty;

            isRunning = true;
            t = 0;

            itemQtyText.text = this.qty.ToString();
            itemIcon.sprite = this.itemDefinition.icon;
        }

        /// <summary>
        /// Pushes this item's definition information to the hover panel controller.
        /// </summary>
        public void PushDataToHoverPanelController()
        {
            hoverPanelController.SetValuesForDefinition(itemDefinition);
        }

    }
}
