using Photon.Pun;
using UnityEngine;

public class OrderBehaviour : MonoBehaviour
{
    public Vector3 position;
    public string prefabName;

    public void InstantiateOrder()
    {
         PhotonNetwork.InstantiateRoomObject(prefabName, position, Quaternion.identity);
    }
}
