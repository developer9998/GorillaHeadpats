using GorillaHeadpats.Models;
using Photon.Pun;
using UnityEngine;

namespace GorillaHeadpats.Behaviours;

[DisallowMultipleComponent, RequireComponent(typeof(VRRig))]
internal class PetPlayer : MonoBehaviour
{
    public GameObject petObject;

    public void Start()
    {
        petObject = new GameObject("PetObject", typeof(BoxCollider), typeof(PetCollider));
        petObject.SetLayer(UnityLayer.GorillaInteractable);
        petObject.transform.SetParent(GetComponent<VRRig>().headMesh.transform, false);

        BoxCollider component = petObject.GetComponent<BoxCollider>();
        component.center = new Vector3(0.0f, 0.23f, -0.01f);
        component.size = new Vector3(0.14f, 0.05f, 0.21f);
        component.isTrigger = true;

        petObject.GetComponent<PetCollider>().Player = this;
    }

    public void PlaySound(SoundType patSound, bool isLeftHand)
    {
        if (patSound == SoundType.None) return;

        int soundIndex = GetSoundIndex(patSound);
        float tapVolume = Mathf.Clamp(Mod.Volume.Value, 0.05f, 0.5f);

        VRRig localRig = GorillaTagger.Instance.offlineVRRig;
        localRig.PlayHandTapLocal(soundIndex, isLeftHand, tapVolume);

        if (NetworkSystem.Instance.InRoom && GorillaTagger.Instance.myVRRig is var networkView)
            networkView.SendRPC("RPC_PlayHandTap", RpcTarget.Others, soundIndex, isLeftHand, tapVolume);
    }

    public static int GetSoundIndex(SoundType patSound) => patSound switch
    {
        SoundType.RacoonSqueeze => Random.Range(274, 277),
        SoundType.RacoonRelease => Random.Range(277, 283),
        SoundType.SpongeSqueeze => 193,
        SoundType.SpongeRelease => 194,
        SoundType.KittySqueeze => 235,
        SoundType.KittyRelease => 236,
        _ => 159
    };
}
