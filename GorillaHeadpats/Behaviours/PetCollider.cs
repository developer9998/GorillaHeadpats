using GorillaHeadpats.Models;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace GorillaHeadpats.Behaviours;

internal class PetCollider : MonoBehaviour
{
    public PetPlayer Player;

    private static readonly Dictionary<SoundCategory, Tuple<SoundType, SoundType>> _sounds = new()
    {
        { SoundCategory.Default, Tuple.Create(SoundType.Standard, SoundType.None) },
        { SoundCategory.Raccoon, Tuple.Create(SoundType.RacoonSqueeze, SoundType.RacoonRelease) },
        { SoundCategory.Sponge, Tuple.Create(SoundType.SpongeSqueeze, SoundType.SpongeRelease) },
        { SoundCategory.Kitty, Tuple.Create(SoundType.KittySqueeze, SoundType.KittyRelease) }
    };

    private bool activated;

    public async void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out GorillaTriggerColliderHandIndicator component) && !activated)
        {
            activated = true;
            await Pet(component.isLeftHand);
            activated = false;
        }
    }

    public async Task Pet(bool isLeftHand)
    {
        float amplitude = Mathf.Clamp(Mod.Amplitude.Value, 0f, 1f);
        if (!Mathf.Approximately(amplitude, 0f)) GorillaTagger.Instance.StartVibration(isLeftHand, amplitude, GorillaTagger.Instance.tapHapticDuration);

        try
        {
            Tuple<SoundType, SoundType> sounds = _sounds[Mod.Category.Value];
            Player.PlaySound(sounds.Item1, isLeftHand);
            await Task.Delay(125);
            Player.PlaySound(sounds.Item2, isLeftHand);
        }
        catch(Exception ex)
        {
            Melon<Mod>.Logger.Error($"Pet sounds could not be played: {ex}");
        }
    }
}
