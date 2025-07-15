using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace GorillaHeadpats
{
    public enum PetType
    {
        Default,
        Raccoon,
        Cat,
        Sponge,
    }

    [BepInPlugin(Constants.GUID, Constants.Name, Constants.Version)]
    public class Plugin : BaseUnityPlugin
    {
        public static ConfigEntry<float> PetVolume;
        public static ConfigEntry<float> HapticAmplitude;

        public static ConfigEntry<PetType> SelectedPetType;

        public void Awake()
        {
            PetVolume = Config.Bind(Constants.Name, "Pet Volume", 0.28f,
                new ConfigDescription("The volume of the pet sound", new AcceptableValueRange<float>(0.05f, 0.5f)));

            HapticAmplitude = Config.Bind(Constants.Name, "Haptic Amplitude", 0.25f,
                new ConfigDescription("The strength of the haptic made when petting", new AcceptableValueRange<float>(0f, 1f)));

            SelectedPetType = Config.Bind(Constants.Name, "Pet Sound Type", PetType.Default,
                "The type of pet sound to play");

            Harmony.CreateAndPatchAll(typeof(Plugin).Assembly, Constants.GUID);
        }
    }
}
