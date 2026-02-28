using UnityEngine;
using UnityEngine.UI;

public class DangerVignette : MonoBehaviour
{
    [SerializeField] private Image vignetteImage;
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private float maxAlpha = 0.4f;
    [SerializeField] private float fadeSpeed = 3f;

    private float targetAlpha;

    private void Start()
    {
        if (vignetteImage != null)
            SetAlpha(0f);
    }

    private void Update()
    {
        if (vignetteImage == null) return;

        targetAlpha = 0f;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            // Find nearest enemy distance
            Transform player = Camera.main != null ? Camera.main.transform : null;
            if (player != null)
            {
                float nearest = float.MaxValue;
                foreach (var enemy in enemies)
                {
                    float dist = Vector3.Distance(player.position, enemy.transform.position);
                    if (dist < nearest)
                        nearest = dist;
                }

                if (nearest < maxDistance)
                {
                    // Closer = higher alpha (inverse lerp)
                    float t = 1f - Mathf.Clamp01(nearest / maxDistance);
                    targetAlpha = t * maxAlpha;
                }
            }
        }

        float current = vignetteImage.color.a;
        float newAlpha = Mathf.MoveTowards(current, targetAlpha, fadeSpeed * Time.unscaledDeltaTime);
        SetAlpha(newAlpha);
    }

    private void SetAlpha(float a)
    {
        Color c = vignetteImage.color;
        c.a = a;
        vignetteImage.color = c;
    }
}
