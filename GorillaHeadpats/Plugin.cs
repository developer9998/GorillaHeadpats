using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace GorillaHeadpats
{
    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static ConfigEntry<float> PetVolume;
        public static ConfigEntry<float> HapticAmplitude;

        public static ConfigEntry<bool> UseCatSounds;

        public void Awake()
        {
            PetVolume = Config.Bind(Constants.Name, "Pet Volume", 0.28f, new ConfigDescription("The volume of the pet sound", new AcceptableValueRange<float>(0.05f, 0.5f)));
            HapticAmplitude = Config.Bind(Constants.Name, "Haptic Amplitude", 0.25f, new ConfigDescription("The strength of the haptic made when petting", new AcceptableValueRange<float>(0f, 1f)));
            
            UseCatSounds = Config.Bind(Constants.Name, "Use Cat Toy Sounds", false, "Whether cat toy squeek sounds should replace the regular petting");
            
            Harmony.CreateAndPatchAll(typeof(Plugin).Assembly, Constants.GUID);
        }
    }
}
//meow
