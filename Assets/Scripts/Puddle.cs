using UnityEngine;

public enum PuddleType { Water, Dirt, Dust }

public class Puddle : MonoBehaviour
{
    public PuddleType type;

    private void Start()
    {
        // Flatten into a disc shape
        transform.localScale = new Vector3(0.5f, 0.15f, 0.5f);

        // Color by type
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            Color color;
            switch (type)
            {
                case PuddleType.Dirt:  color = new Color(0.45f, 0.25f, 0.1f); break;
                case PuddleType.Dust:  color = new Color(0.7f, 0.7f, 0.65f); break;
                default:               color = new Color(0.2f, 0.5f, 0.9f, 0.7f); break; // Water
            }
            rend.material.color = color;
        }
    }
}
