using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;

public class SeatBehaviour : XRBaseInteractable, IPunObservable
{
    [Min(1)]
    public byte group = 1;
    private TeleportationAnchor anchor;
    [HideInInspector]
    public bool synced = false;
    public bool taken;

    void Start()
    {
        anchor = GetComponent<TeleportationAnchor>();
        selectExited.AddListener(OnSelected);
    }

    private void OnSelected(SelectExitEventArgs _)
    {
        if (!taken)
        {
            GameManager.instance.TakeSeat(this);
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
