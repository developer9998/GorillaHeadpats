using GorillaHeadpats.Behaviours;
using HarmonyLib;

namespace GorillaHeadpats.Patches
{
    [HarmonyPatch(typeof(RigContainer), "set_Creator")]
    internal class RigCreatorPatch
    {
        public static void Postfix(RigContainer __instance)
        {
            if (__instance.Rig is VRRig rig && rig.GetComponent<PetPlayer>() is null)
                rig.gameObject.AddComponent<PetPlayer>();
        }
    }
}
