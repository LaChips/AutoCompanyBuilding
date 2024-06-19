using HarmonyLib;

namespace AutoCompanyBuilding.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        private static GameNetworkManager ___gameNetworkManager = GameNetworkManager.Instance;

        [HarmonyPatch("EndOfGame")]
        [HarmonyPostfix]
        static void ResetHasRerouted()
        {
            if (!___gameNetworkManager.isHostingGame)
            {
                return;
            }

            AutoCompanyBuildingBase.hasRerouted = false;
        }

        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void AutoShipToCompanyBuilding(StartOfRound __instance)
        {

            if (!___gameNetworkManager.isHostingGame)
            {
                return;
            }
            if (__instance.currentLevelID == 3)
            {
                AutoCompanyBuildingBase.hasRerouted = true;
            }
            else if (__instance.CanChangeLevels() && TimeOfDay.Instance.daysUntilDeadline == 0 && AutoCompanyBuildingBase.hasRerouted == false && TimeOfDay.Instance.quotaFulfilled < TimeOfDay.Instance.profitQuota)
            {
                __instance.ChangeLevelServerRpc(3, AutoCompanyBuildingBase.groupCredits);
                __instance.ChangeLevel(3); // 3 -> company building
                AutoCompanyBuildingBase.hasRerouted = true;
            }
        }
    }
}
