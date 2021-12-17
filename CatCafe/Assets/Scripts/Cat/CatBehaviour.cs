using UnityEngine;
using PathCreation;
using Photon.Pun;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(ActionBasedController))]
public class CatBehaviour : MonoBehaviourPunCallbacks, IPunObservable
{
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    private bool synced = false;
    private float patFinishTime = 0f;
    private float patLength;
    public Animator animator;
    private float distanceTravelled;

    void Start()
    {
        animator = GetComponent<Animator>();
        foreach (var clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == "rig|Play")
            {
                patLength = clip.length;
            }
        }
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
            if (patFinishTime <= Time.fixedTime)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
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
            distanceTravelled = (float)stream.ReceiveNext();
            synced = true;
        }
    }

    [PunRPC]
    private void PlayPatAnimation(Vector3 whereTolookAt)
    {
        transform.LookAt(whereTolookAt);
        patFinishTime = Time.fixedTime + patLength;
        animator.SetTrigger("Pat");
    }

    public void Pat(ActivateEventArgs args)
    {
        if (patFinishTime <= Time.fixedTime)
        {
            photonView.RPC("PlayPatAnimation", RpcTarget.All, args.interactor.transform.position);
        }
    }
}