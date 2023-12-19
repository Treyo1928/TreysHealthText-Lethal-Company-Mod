using UnityEngine;
using TMPro;
using HarmonyLib;

namespace TreysHealthText
{
    [HarmonyPatch(typeof(HUDManager), "UpdateHealthUI")]
    public static class HUDManager_UpdateHealthUI_Patch
    {
        private static TextMeshProUGUI healthText = null;

        static void Postfix(HUDManager __instance, int health)
        {
            if (healthText == null)
            {
                CreateHealthText(__instance, 100); // Initial health value
            }

            healthText.text = $"HP\n{health}";

            if (health <= 0)
            {
                GameObject.Destroy(healthText.gameObject);
                healthText = null;
            }
        }

        private static void CreateHealthText(HUDManager hudManager, int initialHealth)
        {
            // Find the 'TopLeftCorner' GameObject (parent of 'Self')
            GameObject topLeftCorner = GameObject.Find("Systems/UI/Canvas/IngamePlayerHUD/TopLeftCorner");

            if (topLeftCorner == null)
            {
                Plugin.PluginLogger.LogError("'TopLeftCorner' not found.");
                return;
            }

            // Create a new GameObject for the TextMeshPro element
            GameObject textObj = new GameObject("PlayerHealthText");
            textObj.transform.SetParent(topLeftCorner.transform, false); // Parent to 'TopLeftCorner'

            // Add and configure the TextMeshPro component
            healthText = textObj.AddComponent<TextMeshProUGUI>();

            // Clone properties from weightCounter (if available)
            if (hudManager.weightCounter != null)
            {
                TextMeshProUGUI weightText = hudManager.weightCounter;
                healthText.font = weightText.font;
                healthText.fontSize = weightText.fontSize;
                healthText.color = weightText.color;
                healthText.alignment = TextAlignmentOptions.Center;
                healthText.enableAutoSizing = weightText.enableAutoSizing;
                healthText.fontSizeMin = weightText.fontSizeMin;
                healthText.fontSizeMax = weightText.fontSizeMax;

                // Clone material properties
                if (weightText.fontMaterial != null)
                {
                    healthText.fontSharedMaterial = new Material(weightText.fontMaterial);
                }

                // Apply rotation from weightCounter's parent to healthText
                if (weightText.transform.parent != null)
                {
                    RectTransform weightCounterParentRect = weightText.transform.parent.GetComponent<RectTransform>();
                    if (weightCounterParentRect != null)
                    {
                        RectTransform healthTextRect = healthText.GetComponent<RectTransform>();
                        healthTextRect.localRotation = weightCounterParentRect.localRotation;
                    }
                }
            }
            else
            {
                // Set default properties if weightCounter is not available
                healthText.fontSize = 24; // Adjust as needed
                healthText.color = Color.red; // Default color
                healthText.alignment = TextAlignmentOptions.Center; // Center alignment
            }

            // Configure RectTransform for positioning
            RectTransform rectTransform = textObj.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 1); // Top left anchor
            rectTransform.anchorMax = new Vector2(0, 1); // Top left anchor
            rectTransform.pivot = new Vector2(0, 1); // Top left pivot
            rectTransform.anchoredPosition = new Vector2(-53, -95); // Adjust position as needed

            // Set initial health text
            healthText.text = $"HP\n{initialHealth}";
        }
    }
}
