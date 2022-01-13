using PathCreation;
using UnityEngine;

public class WhiteCatBehaviour : MonoBehaviour
{
    public PathCreator walkPath;
    public PathCreator walkBackPath;
    public float speed = 4;
    public Animator animator;
    private float distanceTravelled;
    private float timeSlept;
    public float timeToSleep = 5f;
    private enum State
    {
        WALK,
        SLEEP,
        WALK_BACK,
    }
    private State state = State.WALK;

    void Update()
    {
        if (state == State.WALK || state == State.WALK_BACK)
        {
            distanceTravelled += speed * Time.deltaTime;
        }
        if (state == State.WALK)
        {
            transform.position = walkPath.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = walkPath.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            if (distanceTravelled >= walkPath.path.length)
            {
                state = State.SLEEP;
                animator.SetTrigger("Sleep");
                distanceTravelled = 0;
            }
        }
        else if (state == State.WALK_BACK)
        {
            transform.position = walkBackPath.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            transform.rotation = walkBackPath.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
            if (distanceTravelled >= walkBackPath.path.length)
            {
                state = State.WALK;
                distanceTravelled = 0;
            }
        }
        else if (state == State.SLEEP)
        {
            timeSlept += Time.deltaTime;
            if (timeSlept >= timeToSleep)
            {
                state = State.WALK_BACK;
                animator.SetTrigger("Walk");
                timeSlept = 0;
            }
        }
    }
}
