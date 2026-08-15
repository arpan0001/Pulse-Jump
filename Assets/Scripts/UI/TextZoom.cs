using UnityEngine;
using TMPro;

public class TextZoom : MonoBehaviour
{
    [SerializeField] private float minScale = 1f;
    [SerializeField] private float maxScale = 1.2f;
    [SerializeField] private float speed = 2f;

    private void Update()
    {
        float scale = Mathf.Lerp(
            minScale,
            maxScale,
            (Mathf.Sin(Time.time * speed) + 1f) / 2f
        );

        transform.localScale = Vector3.one * scale;
    }
}