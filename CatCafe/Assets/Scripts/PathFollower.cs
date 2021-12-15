using UnityEngine;
using PathCreation;
using Photon.Pun;

public class PathFollower : MonoBehaviourPunCallbacks, IPunObservable
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    private float distanceTravelled;
    private bool synced = false;

    void Start()
    {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    public override void OnJoinedRoom()
    {
        if (PhotonNetwork.PlayerList.Length == 1)
        {
            synced = true;
        }
    }

    void Update()
    {
        if (pathCreator != null && synced)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if (synced)
            {
                stream.SendNext(distanceTravelled);
            }
        }
        else
        {
            if (!synced)
            {
                distanceTravelled = (float)stream.ReceiveNext();
                synced = true;
            }
        }
    }
}