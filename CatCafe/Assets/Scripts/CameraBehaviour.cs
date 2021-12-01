using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Transform neck;
    private Transform head;
    private readonly Vector3 neckOffset = new Vector3(0f, -1f, -1.5f);


    void Update()
    {
        if (neck == null)
        {
            var playerObject = GameManager.instance.playerObject;
            if (playerObject == null)
            {
                return;
            }
            neck = playerObject.transform.Find("Hips/Spine/Spine1/Spine2/Neck");
            head = neck.Find("Head");
        }

        Debug.Log(transform.forward);
        head.rotation = transform.rotation;
        neck.position = transform.position;
        neck.Translate(neckOffset, transform);
    }
}
