using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class Seat : MonoBehaviour, IPunObservable
{
    [Min(1)]
    public byte group = 1;
    public TeleportationAnchor anchor;
    public bool synced = false;
    public bool taken = false;

    void Start()
    {
        if (anchor != null)
        {
            anchor.enabled = false;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(synced);
            stream.SendNext(taken);
        }
        else
        {
            synced = (bool)stream.ReceiveNext();
            taken = (bool)stream.ReceiveNext();
        }
    }
}
