using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace ProjectRefuge.Inventory.UI
{
    /// <summary>
    /// InventoryHoverPanelController controls the InventoryHoverPanel
    /// </summary>
    public class InventoryHoverPanelController : MonoBehaviour
    {
        private RectTransform thisRectTransform;

        private Text nameText;
        private Text descriptionText;
        private Image image;
        private Image panelImage;

        private float currentTransparency = 0;
        private float easingDirection = 0;

        public float transparencyChangeTime = 1f;

        /// <summary>
        /// Awake is called before the first frame update
        /// </summary>
        private void Awake()
        {
            thisRectTransform = GetComponent<RectTransform>();

            image = transform.Find("Image").GetComponent<Image>();
            nameText = transform.Find("NameText").GetComponent<Text>();
            descriptionText = transform.Find("DescriptionText").GetComponent<Text>();
            panelImage = GetComponent<Image>();

            SetTransparency(currentTransparency);
        }

        /// <summary>
        /// LateUpdate is called once per frame
        /// </summary>
        private void LateUpdate()
        {
            // Tooltip positioning.
            thisRectTransform.position = Input.mousePosition + new Vector3(thisRectTransform.rect.width / 2 + 4, thisRectTransform.rect.height / 2 + 4, -200);
            thisRectTransform.position = new Vector3(
                Mathf.Clamp(thisRectTransform.position.x, thisRectTransform.rect.width / 2, Screen.width - thisRectTransform.rect.width / 2),
                Mathf.Clamp(thisRectTransform.position.y, thisRectTransform.rect.height / 2, Screen.height - thisRectTransform.rect.height / 2),
                thisRectTransform.position.z
            );

            // Transparency
            currentTransparency = Mathf.MoveTowards(currentTransparency, easingDirection, (1 / transparencyChangeTime) * Time.deltaTime);
            if (currentTransparency == 0 && gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }

            SetTransparency(currentTransparency);
        }

        /// <summary>
        /// Sets the transparency of the hover panel.
        /// </summary>
        private void SetTransparency(float transparency)
        {
            nameText.color = new Color(nameText.color.r, nameText.color.g, nameText.color.b, currentTransparency);
            descriptionText.color = new Color(descriptionText.color.r, descriptionText.color.g, descriptionText.color.b, currentTransparency);
            image.color = new Color(image.color.r, image.color.g, image.color.b, currentTransparency);
            nameText.color = new Color(nameText.color.r, nameText.color.g, nameText.color.b, currentTransparency);
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, currentTransparency);
        }

        /// <summary>
        /// Sets the text and textures of the hover window to the text
        /// of the given definition.
        /// </summary>
        public void SetValuesForDefinition(InventoryItemDefinition definition)
        {
            nameText.text = definition.itemName;
            descriptionText.text = definition.description;
            image.sprite = definition.icon;
        }

        /// <summary>
        /// Eases the transparency of the window in or out depending on the 
        /// </summary>
        /// <param name="isIn"></param>
        public void EaseTransparency(bool isIn)
        {
            if (isIn)
            {
                gameObject.SetActive(true);
                easingDirection = 1;
            }
            else
            {
                easingDirection = 0;
            }
        }
    }
}

