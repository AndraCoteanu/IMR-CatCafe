using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private Transform neck;
    private Transform head;
    // The camera is in front of the player's head, not inside it
    private readonly Vector3 cameraOffset = new Vector3(0f, -1f, -2f);

    void Update()
    {
        if (neck == null)
        {
            var character = GameManager.instance.currentCharacter;
            if (character == null)
            {
                return;
            }
            neck = character.transform.Find("Hips/Spine/Spine1/Spine2/Neck");
            head = neck.Find("Head");
        }

        head.rotation = transform.rotation;
        neck.position = transform.position;
        neck.Translate(cameraOffset, transform);
    }
}
