using GorillaHeadpats;
using GorillaHeadpats.Behaviours;
using GorillaLibrary;
using MelonLoader;
using MelonLoader.Preferences;
using static MelonLoader.MelonLogger;

[assembly: MelonInfo(typeof(Mod), "GorillaHeadpats", "1.0.1", "dev9998")]
[assembly: MelonGame("Another Axiom", "Gorilla Tag")]
[assembly: MelonAdditionalDependencies("GorillaLibrary")]

namespace GorillaHeadpats;

public class Mod : GorillaMod
{
    public static MelonPreferences_Entry<float> PetVolume;
    public static MelonPreferences_Entry<float> HapticAmplitude;
    public static MelonPreferences_Entry<bool> UseRacoonSounds;

    public override void OnInitializeMelon()
    {
        MelonPreferences_Category category = CreateCategory("GorillaHeadpats");

        PetVolume = category.CreateEntry("Pet Volume", 0.28f, "Pet Volume", "The volume of the pet sound", false, false, new ValueRange<float>(0.05f, 0.5f));
        HapticAmplitude = category.CreateEntry("Haptic Amplitude", 0.25f, "Haptic Amplitude", "The strength of the haptic made when petting", false, false, new ValueRange<float>(0f, 1f));
        UseRacoonSounds = category.CreateEntry("Use Raccoon Sounds", false, "Use Raccoon Sounds", "Whether raccoon toy squeek sounds should replace the regular pettng", false, false, null);

        Events.Rig.OnRigAdded.Subscribe((rig, player) =>
        {
            if (rig.GetComponent<PetPlayer>() is null) rig.gameObject.AddComponent<PetPlayer>();
        });
    }
}
