using UnityEngine;
using TMPro;
using HarmonyLib;
using System;

namespace TreysHealthText
{
    [HarmonyPatch(typeof(HUDManager), "UpdateHealthUI")]
    public static class HUDManager_UpdateHealthUI_Patch
    {
        public static TextMeshProUGUI healthText = null;

        static void Postfix(HUDManager __instance, int health)
        {
            Plugin.PluginLogger.LogInfo("UpdateHealthUI Patch Ran");
            if (healthText == null)
            {
                CreateHealthText(__instance, 100, health); // Initial health value
            }

            // Update the text based on the configuration
            string hpPosition = Plugin.HpTextPosition.Value;
            bool underline = Plugin.UnderlineTopLine.Value;

            UpdateHealthText(health, false);

            //Below code deletes the health text, but I don't think that's needed and actually causes lag on level load
            /*if (health <= 0)
            {
                GameObject.Destroy(healthText.gameObject);
                healthText = null;
            }*/
        }

        private static void UpdateHealthText(int health, bool rainbow)
        {
            // Update the text based on the configuration
            string hpPosition = Plugin.HpTextPosition.Value;
            bool underline = Plugin.UnderlineTopLine.Value;
            string labelName = Plugin.HPLabelName.Value;
            string topText;
            string bottomText;
            if (rainbow){
                // Somehow change color of text asset
                Plugin.PluginLogger.LogInfo("Updated Text Color");

            }
            if (hpPosition.Equals("Above"))
            {
                topText = labelName;
                bottomText = health.ToString();
            }
            else if (hpPosition.Equals("Below"))
            {
                topText = health.ToString();
                bottomText = labelName;
            }
            else if (hpPosition.Equals("Right"))
            {
                topText = health.ToString() + labelName;
                bottomText = "";
            }
            else
            {
                topText = labelName + health.ToString();
                bottomText = "";
            }
            if (underline)
            {
                topText = "<u>" + topText + "</u>";
            }
            healthText.text = topText + "\n" + bottomText;
        }

        private static void CreateHealthText(HUDManager hudManager, int initialHealth, int health)
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
                healthText.color = ParseColor(Plugin.TextColor.Value);
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

            //Move the text to a good-looking position
            int XOffset = Plugin.XOffset.Value;
            int YOffset = Plugin.YOffset.Value;
            rectTransform.anchoredPosition = new Vector2(-53 + XOffset, -95 + YOffset);

            UpdateHealthText(health, false);
        }
        private static Color ParseColor(string rgb)
        {
            string[] parts = rgb.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 3) return Color.white; // Fallback to white

            // Attempt to parse each component
            if (float.TryParse(parts[0], out float r) &&
                float.TryParse(parts[1], out float g) &&
                float.TryParse(parts[2], out float b))
            {
                return new Color(r / 255, g / 255, b / 255);
            }

            return Color.white; // Fallback to white if parsing fails
        }
    }
}
