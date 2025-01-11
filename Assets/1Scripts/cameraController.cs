using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{
    public List<Transform> playerObjects; // Player object'lerin listesi
    public float zoomOutSize = 10f;
    public float normalSize = 5f;
    public float zoomDuration = 2f; // Zoom iþlemi süresi

    private Camera cam;
    private Coroutine zoomCoroutine;
    private bool isZoomedOut = false;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    public void StartZoomOut()
    {
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }
        zoomCoroutine = StartCoroutine(SmoothZoom(cam.orthographicSize, zoomOutSize, zoomDuration));
        isZoomedOut = true;
    }

    public void ResetZoom()
    {
        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }
        zoomCoroutine = StartCoroutine(SmoothZoom(cam.orthographicSize, normalSize, zoomDuration));
        isZoomedOut = false;
    }

    private IEnumerator SmoothZoom(float startSize, float endSize, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            cam.orthographicSize = Mathf.Lerp(startSize, endSize, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cam.orthographicSize = endSize;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isZoomedOut)
        {
            ResetZoom();
        }
    }
}
