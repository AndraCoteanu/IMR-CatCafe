using Photon.Pun;
using Photon.Voice.PUN;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public CharacterOffset[] characters;
    [HideInInspector]
    public GameObject currentCharacter;
    public GameObject xrRig;

    private SeatBehaviour[] seats;
    private SeatBehaviour currentSeat = null;

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
        if (currentSeat != null)
        {
            currentSeat.taken = false;
        }
        currentSeat = seat;
        Group = seat.group;

        var playerPosition = seat.transform.position;
        var playerRotation = seat.transform.rotation;
        if (currentCharacter == null)
        {
            var player = characters[PlayerPrefs.GetInt("CharacterSelected")];
            currentCharacter = PhotonNetwork.Instantiate(
                player.name,
                playerPosition + new Vector3(0f, player.modelOffset, 0f),
                playerRotation,
                0
            );
        }
        else
        {
            var offset = currentCharacter.GetComponent<CharacterOffset>();
            currentCharacter.transform.position = playerPosition + new Vector3(0f, offset.modelOffset, 0f);
            currentCharacter.transform.rotation = playerRotation;
        }

        xrRig.transform.position = currentCharacter.transform.position
            + new Vector3(0f, currentCharacter.GetComponent<CharacterOffset>().rigOffset, 0f);
        xrRig.transform.rotation = currentCharacter.transform.rotation;
    }
}
