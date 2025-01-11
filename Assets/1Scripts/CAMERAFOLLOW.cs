using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public float yOffset = 1f;
    public Transform player; // Karakter nesnesi
    public Transform followTarget; // Kameranın takip edeceği nesne (örneğin, bir hedef nokta)

    void Update()
    {
        Vector3 targetPosition;

        if (player != null)
        {
            targetPosition = new Vector3(player.position.x, player.position.y + yOffset, -10f);
        }
        else if (followTarget != null)
        {
            targetPosition = new Vector3(followTarget.position.x, followTarget.position.y + yOffset, -10f);
        }
        else
        {
            // Hem karakter hem de takip edilecek hedef yoksa, kamera konumunu sabit tut
            return;
        }

        // Kamera pozisyonunu yumuşak bir şekilde güncelle
        transform.position = Vector3.Slerp(transform.position, targetPosition, FollowSpeed * Time.deltaTime);
    }
}
