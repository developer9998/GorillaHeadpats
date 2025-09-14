using GorillaHeadpats.Models;
using System.Threading.Tasks;
using UnityEngine;

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
                await Pet(component.isLeftHand);
                activated = false;
            }
        }

        public async Task Pet(bool isLeftHand)
        {
            float amplitude = Mathf.Clamp(Plugin.HapticAmplitude.Value, 0f, 1f);
            if (!Mathf.Approximately(amplitude, 0f))
                GorillaTagger.Instance.StartVibration(isLeftHand, amplitude, GorillaTagger.Instance.tapHapticDuration);

            bool isRacoon = Plugin.UseRacoonSounds.Value;
            Player.PlaySound(isRacoon ? EPatSound.RacoonSqueeze : EPatSound.Default, isLeftHand);

            await Task.Delay(125);

            if (isRacoon)
                Player.PlaySound(EPatSound.RacoonRelease, isLeftHand);
        }
    }
}
