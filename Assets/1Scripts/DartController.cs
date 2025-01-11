using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartController2 : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bullet;
    public float sightRange = 10f; // Dart'ın menzil uzaklığı
    public float starttimebetween;
    private float timebetween;
    public Transform targetTransform; // Hedef Transform

    private void Start()
    {
        timebetween = starttimebetween;
    }

    private void Update()
    {
        if (CheckTargetInSight() && timebetween <= 0)
        {
            Instantiate(bullet, firepoint.position, firepoint.rotation);
            timebetween = starttimebetween;
        }
        else
        {
            timebetween -= Time.deltaTime;
        }

        if (targetTransform != null)
        {
            // Dart'ın hedefe doğru yönelmesi
            Vector3 direction = targetTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private bool CheckTargetInSight()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, sightRange);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                // Hedef "Player" ise, dart ateşlenebilir.
                return true;
            }
        }

        // Hedef "Player" değilse veya hiç hedef yoksa, dart ateşlenemez.
        return false;
    }
}
