using UnityEngine;

public class ControllerBehaviour : MonoBehaviour
{
    private Transform forearm;
    private Transform hand;
    public bool left;

    void Update()
    {
        if (forearm == null || hand == null)
        {
            var playerObject = GameManager.instance.playerObject;
            if (playerObject == null)
            {
                return;
            }
            var prefix = left ? "Left" : "Right";
            forearm = playerObject.transform.Find("Hips/Spine/Spine1/Spine2/" + prefix + "Shoulder/" + prefix + "Arm/" + prefix + "ForeArm");
            hand = forearm.transform.Find(prefix + "Hand");
        }

        forearm.position = transform.position;

        var angles = transform.localRotation.eulerAngles;
        var quaternion = new Quaternion();
        // The x and z axis are reversed on the hand and the right hand has the z axis flipped
        quaternion.eulerAngles = new Vector3(angles.z, angles.y, angles.x * (left ? 1f : -1f));
        hand.localRotation = quaternion;
    }
}
