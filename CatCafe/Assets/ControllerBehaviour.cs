using UnityEngine;

public class ControllerBehaviour : MonoBehaviour
{
    public GameObject forearm;
    public GameObject hand;
    public bool left;

    void Update()
    {
        forearm.transform.position = transform.position;

        var angles = transform.localRotation.eulerAngles;
        var quaternion = new Quaternion();
        // The x and z axis are reversed on the hand and the right hand has the z axis flipped
        quaternion.eulerAngles = new Vector3(angles.z, angles.y, angles.x * (left ? 1f : -1f));
        hand.transform.localRotation = quaternion;
    }
}
