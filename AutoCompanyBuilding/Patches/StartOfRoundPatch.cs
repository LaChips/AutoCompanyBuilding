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
        static void AutoShipToCompanyBuilding()
        {
            StartOfRound ___startofround = StartOfRound.Instance;

            if (!___gameNetworkManager.isHostingGame)
            {
                return;
            }

            if (___startofround.currentLevelID == 3)
            {
                AutoCompanyBuildingBase.hasRerouted = true;
            }
            else if (___startofround.CanChangeLevels() && TimeOfDay.Instance.daysUntilDeadline == 0 && ___startofround.currentLevelID != 3 && AutoCompanyBuildingBase.hasRerouted == false)
            {
                ___startofround.ChangeLevelServerRpc(3, AutoCompanyBuildingBase.groupCredits);
                ___startofround.ChangeLevel(3); // 3 -> company building
                AutoCompanyBuildingBase.hasRerouted = true;
            }
        }
    }
}
