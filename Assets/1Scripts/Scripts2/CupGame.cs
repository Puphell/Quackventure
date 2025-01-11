using UnityEngine;

public class CupMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool isMoving = false;
    private int direction;

    void Start()
    {
        SetRandomDirection();
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);
        }
    }

    public void StartMoving()
    {
        isMoving = true;
        SetRandomDirection();
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    void SetRandomDirection()
    {
        direction = Random.Range(0, 2) == 0 ? -1 : 1;
    }
}
