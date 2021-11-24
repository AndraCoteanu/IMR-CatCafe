using Photon.Pun;
using UnityEngine;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public GameObject playerPrefab;
    [HideInInspector]
    public GameObject playerObject;
    public GameObject xrRig;

    // The rig is higher and in front of the player object
    private readonly Vector3 rigOffset = new Vector3(0f, 7.25f, 1.5f);

    void Start()
    {
        instance = this;
    }

    public override void OnJoinedRoom()
    {
        var position = new Vector3(0f, 0f, 5f * PhotonNetwork.CountOfPlayers);
        var rotation = Quaternion.identity;
        playerObject = PhotonNetwork.Instantiate(playerPrefab.name, position, rotation, 0);

        position += rigOffset;
        xrRig.transform.position = position;
        xrRig.transform.rotation = rotation;
    }
}
