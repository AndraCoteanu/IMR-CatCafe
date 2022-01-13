using UnityEngine;
using PathCreation;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class OrangeCatBehaviour : MonoBehaviourPunCallbacks, IPunObservable
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    private bool synced = false;
    public Animator animator;
    private float distanceTravelled;
    private Vector3 whereToLookAt;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        if (pathCreator != null)
        {
            if (isWalking())
            {
                if (synced)
                {
                    distanceTravelled += speed * Time.deltaTime;
                }
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
            else
            {
                transform.LookAt(whereToLookAt);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity != Vector3.zero)
        {
            animator.SetTrigger("die");
        }
    }

    private bool isWalking()
    {
        return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == "rig|Walk";
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
                stream.SendNext(isWalking());
                stream.SendNext(distanceTravelled);
            }
        }
        else
        {
            var isWalking = (bool)stream.ReceiveNext();
            distanceTravelled = (float)stream.ReceiveNext();
            if (isWalking)
            {
                synced = true;
            }
        }
    }

    [PunRPC]
    private void PlayPatAnimation(Vector3 whereToLookAt, int animation)
    {
        this.whereToLookAt = whereToLookAt;
        animator.SetTrigger("pat" + animation);
    }

    public void Pat(ActivateEventArgs args)
    {
        if (isWalking())
        {
            int animation = Random.Range(1, 7);
            photonView.RPC("PlayPatAnimation", RpcTarget.All, args.interactor.transform.position, animation);
        }
    }
}