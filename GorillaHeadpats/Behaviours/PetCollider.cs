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

                switch (Plugin.SelectedPetType.Value)
                {
                    case PetType.Raccoon:
                        await Pet(isLeft, EPatSound.RaccoonSqueeze, EPatSound.RaccoonRelease);
                        break;
                    case PetType.Cat:
                        await Pet(isLeft, EPatSound.CatSqueeze, EPatSound.CatRelease);
                        break;
                    case PetType.Sponge:
                        await Pet(isLeft, EPatSound.SpongeSqueeze, EPatSound.SpongeRelease);
                        break;
                    default:
                        await Pet(isLeft, EPatSound.Default);
                        break;
                }

                activated = false;
            }
        }

        private async Task Pet(bool isLeftHand, EPatSound contactSound, EPatSound? releaseSound = null)
        {
            float amplitude = Mathf.Clamp(Plugin.HapticAmplitude.Value, 0f, 1f);
            if (!Mathf.Approximately(amplitude, 0f))
                GorillaTagger.Instance.StartVibration(isLeftHand, amplitude, GorillaTagger.Instance.tapHapticDuration);

            Player.PlayPetSound(contactSound, isLeftHand);

            if (releaseSound.HasValue)
            {
                await Task.Delay(125);
                Player.PlayPetSound(releaseSound.Value, isLeftHand);
            }
        }
    }
}
