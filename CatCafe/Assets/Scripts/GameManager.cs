using Photon.Pun;
using Photon.Voice.PUN;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public GameObject playerPrefab;
    [HideInInspector]
    public GameObject playerObject;
    public GameObject xrRig;

    private SeatBehaviour[] seats;
    private SeatBehaviour currentSeat = null;
    private const float seatHeight = 3.5f;

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
        seats = GameObject.FindObjectsOfType<SeatBehaviour>();
    }

    void Update()
    {
        if (currentSeat != null || PhotonNetwork.CurrentRoom == null)
        {
            return;
        }
        foreach (var seat in seats)
        {
            if (seat.synced && !seat.taken)
            {
                TakeSeat(seat);
            }
        }
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.PlayerList.Length == 1)
        {
            TakeSeat(seats[0]);
            foreach (var seat in seats)
            {
                seat.synced = true;
            }
        }
    }

    public void TakeSeat(SeatBehaviour seat)
    {
        seat.taken = true;
        if (currentSeat != null) {
            currentSeat.taken = false;
        }
        currentSeat = seat;
        Group = seat.group;

        var playerPosition = seat.transform.position + (new Vector3(0, seatHeight));
        var playerRotation = seat.transform.rotation;
        if (playerObject == null)
        {
            playerObject = PhotonNetwork.Instantiate(playerPrefab.name, playerPosition, playerRotation, 0);
        }
        else
        {
            playerObject.transform.position = playerPosition;
            playerObject.transform.rotation = playerRotation;
        }

        xrRig.transform.position = playerObject.transform.position;
        xrRig.transform.rotation = playerObject.transform.rotation;
    }
}
