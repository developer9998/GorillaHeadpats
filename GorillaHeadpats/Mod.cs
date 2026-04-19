using GorillaHeadpats;
using GorillaHeadpats.Behaviours;
using GorillaHeadpats.Models;
using GorillaLibrary;
using MelonLoader;
using MelonLoader.Preferences;

[assembly: MelonInfo(typeof(Mod), "GorillaHeadpats", "1.0.1", "dev9998")]
[assembly: MelonGame("Another Axiom", "Gorilla Tag")]
[assembly: MelonAdditionalDependencies("GorillaLibrary")]

namespace GorillaHeadpats;

internal class Mod : GorillaMod
{
    public static MelonPreferences_Entry<SoundCategory> Category;

    public static MelonPreferences_Entry<float> Volume;

    public static MelonPreferences_Entry<float> Amplitude;

    public override void OnInitializeMelon()
    {
        MelonPreferences_Category category = CreateCategory("GorillaHeadpats");

        Category = category.CreateEntry("Category", SoundCategory.Default, "Category", "The category of sounds used for petting", false, false, null);
        Volume = category.CreateEntry("Pet Volume", 0.28f, "Pet Volume", "The volume of the pet sound", false, false, new ValueRange<float>(0.05f, 0.5f));
        Amplitude = category.CreateEntry("Haptic Amplitude", 0.25f, "Haptic Amplitude", "The amplitude of the haptic made when petting", false, false, new ValueRange<float>(0f, 1f));

        Events.Rig.OnRigAdded.Subscribe((rig, player) =>
        {
            if (rig.GetComponent<PetPlayer>() is null) rig.gameObject.AddComponent<PetPlayer>();
        });
    }
}
