using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using GorillaHeadpats.Models;

namespace GorillaHeadpats.Behaviours
{
    internal class PetCollider : MonoBehaviour
    {
        public PetPlayer Player;

        private bool activated;

        public async void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out GorillaTriggerColliderHandIndicator component) && !activated)
            {
                activated = true;
                bool isLeft = component.isLeftHand;

                if (Plugin.UseRaccoonSounds.Value)
                {
                    await PetRaccoon(isLeft);
                }
                else if (Plugin.UseCatSounds.Value)
                {
                    await PetCat(isLeft);
                }
                else if (Plugin.UseSpongeSounds.Value)
                {
                    await PetSponge(isLeft);
                }
                else
                {
                    Player.PlaySoundCat(EPatSound.Default, isLeft);
                }
                activated = false;
            }
        }

        public async Task PetCat(bool isLeftHand)
        {
            float amplitude = Mathf.Clamp(Plugin.HapticAmplitude.Value, 0f, 1f);
            if (!Mathf.Approximately(amplitude, 0f))
                GorillaTagger.Instance.StartVibration(isLeftHand, amplitude, GorillaTagger.Instance.tapHapticDuration);

            bool isCat = Plugin.UseCatSounds.Value;
            Player.PlaySoundCat(isCat ? EPatSound.CatSqueeze : EPatSound.Default, isLeftHand);

            await Task.Delay(125);

            if (isCat)
                Player.PlaySoundCat(EPatSound.CatRelease, isLeftHand);
        }

        public async Task PetRaccoon(bool isLeftHand)
        {
            float amplitude = Mathf.Clamp(Plugin.HapticAmplitude.Value, 0f, 1f);
            if (!Mathf.Approximately(amplitude, 0f))
                GorillaTagger.Instance.StartVibration(isLeftHand, amplitude, GorillaTagger.Instance.tapHapticDuration);

            bool isRacoon = Plugin.UseRaccoonSounds.Value;
            Player.PlaySoundRaccoon(isRacoon ? EPatSound.RaccoonSqueeze : EPatSound.Default, isLeftHand);

            await Task.Delay(125);

            if (isRacoon)
                Player.PlaySoundRaccoon(EPatSound.RaccoonRelease, isLeftHand);
        }

        public async Task PetSponge(bool isLeftHand)
        {
            float amplitude = Mathf.Clamp(Plugin.HapticAmplitude.Value, 0f, 1f);
            if (!Mathf.Approximately(amplitude, 0f))
                GorillaTagger.Instance.StartVibration(isLeftHand, amplitude, GorillaTagger.Instance.tapHapticDuration);

            bool isSponge = Plugin.UseSpongeSounds.Value;
            Player.PlaySoundSponge(isSponge ? EPatSound.SpongeSqueeze : EPatSound.Default, isLeftHand);

            await Task.Delay(125);

            if (isSponge)
                Player.PlaySoundSponge(EPatSound.SpongeRelease, isLeftHand);
        }
    }
}

