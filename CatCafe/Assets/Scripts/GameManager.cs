using Photon.Pun;
using Photon.Voice.PUN;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public GameObject playerPrefab;
    [HideInInspector]
    public GameObject playerObject;
    public GameObject xrRig;
    
    private Seat[] seats;

    // The rig is higher and in front of the player object
    private readonly Vector3 rigOffset = new Vector3(0f, 7.25f, 1.5f);

    private byte Group
    {
        set
        {
            PhotonVoiceNetwork.Instance.Client.GlobalInterestGroup = value;
            PhotonVoiceNetwork.Instance.PrimaryRecorder.InterestGroup = value;
        }
    }

    void Start()
    {
        instance = this;
        seats = GameObject.FindObjectsOfType<Seat>();
    }

    public override void OnJoinedRoom()
    {
        var seat = seats[PhotonNetwork.PlayerList.Length - 1];
        Group = seat.group;

        playerObject = PhotonNetwork.Instantiate(playerPrefab.name, seat.transform.position, seat.transform.rotation, 0);

        var position = seat.transform.position + rigOffset;
        xrRig.transform.position = seat.transform.position;
        xrRig.transform.Translate(rigOffset, seat.transform);
        xrRig.transform.rotation = seat.transform.rotation;

    }
}
