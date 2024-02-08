using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCompanyBuilding.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPatch("Update")]
        [HarmonyPostfix]
        static void AutoShipToCompanyBuilding()
        {
            StartOfRound ___startofround = StartOfRound.Instance;
            if (___startofround.CanChangeLevels() && TimeOfDay.Instance.daysUntilDeadline == 0 && ___startofround.currentLevelID != 3) {
                Terminal ___terminal = new Terminal();
                ___startofround.ChangeLevelServerRpc(3, ___terminal.groupCredits);
                ___startofround.ChangeLevel(3); // 3 -> company building
                Terminal.Destroy(___terminal);
            }
        }
    }
}
