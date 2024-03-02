using UnityEngine;
using HarmonyLib;
using GameNetcodeStuff;

namespace TreysHealthText
{
    [HarmonyPatch(typeof(HUDManager), "Awake")]
    public static class HUDManager_Awake_Patch
    {
        static void Postfix(HUDManager __instance)
        {
            Plugin.PluginLogger.LogInfo("HUDManager Patch Ran");
            // Access the PlayerControllerB instance
            PlayerControllerB playerController = GameObject.FindObjectOfType<PlayerControllerB>();
            if (playerController != null)
            {
                // Update the health UI with the player's current health
                int playerHealth = playerController.health;
                HUDManager.Instance.UpdateHealthUI(playerHealth, Plugin.HideSystemsOnline.Value); //Player Intro Skip was caused by second parameter being true
                Plugin.PluginLogger.LogInfo("UpdatedHealth");
            }
            else
            {
                Plugin.PluginLogger.LogError("PlayerControllerB not found.");
            }
        }
    }
/*  Old Code I thought might help with the SystemsOnlineAnimation skip but I think I didn't need it
    [HarmonyPatch(typeof(PlayerControllerB), "SpawnPlayerAnimation")]
    public static class HudManager_Awake_Patch
    {
        static void Postfix(PlayerControllerB __instance)
        {
            Plugin.PluginLogger.LogInfo("SpawnPlayerAnimation Patch Ran");
            // Access the PlayerControllerB instance
            PlayerControllerB playerController = GameObject.FindObjectOfType<PlayerControllerB>();
            if (playerController != null)
            {
                // Update the health UI with the player's current health
            int playerHealth = __instance.health;
            HUDManager.Instance.UpdateHealthUI(playerHealth, false);
            //HUDManager_UpdateHealthUI_Patch.
            Plugin.PluginLogger.LogInfo("UpdatedHealth");
            //}
            else
            {
                Plugin.PluginLogger.LogError("PlayerControllerB not found.");
            }
        }
    }*/
}
