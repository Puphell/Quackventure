using UnityEngine;

public class PointToPointMovement : MonoBehaviour
{
    public Transform point1; // �lk nokta
    public Transform point2; // �kinci nokta
    public float speed = 1f; // Hareket h�z�

    private bool isMovingToPoint1 = true;

    private void Update()
    {
        if (isMovingToPoint1)
        {
            transform.position = Vector3.MoveTowards(transform.position, point1.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, point1.position) < 0.1f)
            {
                isMovingToPoint1 = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, point2.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, point2.position) < 0.1f)
            {
                isMovingToPoint1 = true;
            }
        }
    }
}
