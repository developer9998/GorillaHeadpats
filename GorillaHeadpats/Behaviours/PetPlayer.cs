using GorillaHeadpats.Models;
using Photon.Pun;
using UnityEngine;

namespace GorillaHeadpats.Behaviours
{
    [DisallowMultipleComponent, RequireComponent(typeof(VRRig))]
    internal class PetPlayer : MonoBehaviour
    {
        public VRRig Rig => GetComponent<VRRig>();

        public GameObject petObject;

        public void Start()
        {
            petObject = new GameObject("PetObject", typeof(BoxCollider), typeof(PetCollider));
            petObject.SetLayer(UnityLayer.GorillaInteractable);
            petObject.transform.SetParent(Rig.headMesh.transform, false);
            BoxCollider component = petObject.GetComponent<BoxCollider>();
            component.center = new Vector3(0.0f, 0.23f, -0.01f);
            component.size = new Vector3(0.14f, 0.05f, 0.21f);
            component.isTrigger = true;
            petObject.GetComponent<PetCollider>().Player = this;
        }

        public void PlaySound(EPatSound patSound, bool isLeftHand)
        {
            int soundIndex = GetSoundIndex(patSound);
            float tapVolume = Mathf.Clamp(Plugin.PetVolume.Value, 0.05f, 0.5f);

            VRRig rig = GorillaTagger.Instance.offlineVRRig;
            rig.PlayHandTapLocal(soundIndex, isLeftHand, tapVolume);

            if (NetworkSystem.Instance.InRoom && GorillaTagger.Instance.myVRRig is var myVRRig)
                myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.Others, soundIndex, isLeftHand, tapVolume);
        }

        public int GetSoundIndex(EPatSound patSound)
        {
            return patSound switch
            {
                EPatSound.Default => 159,
                EPatSound.CatSqueeze => Random.Range(235, 235),
                EPatSound.CatRelease => Random.Range(236, 236),
                _ => throw new System.ArgumentOutOfRangeException(nameof(patSound))
            };
        }
    }
}
//meow
