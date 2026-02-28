using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifetime = 5f;
    public int shotId;
    private float ignoreCollisionTime = 0.1f; // Ignore collisions for first 0.1 seconds
    private float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
        // Destroy the bullet after 5 seconds if it hasn't collided with anything
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Ignore collisions for a brief moment after spawning (prevents hitting the player/gun)
        if (Time.time - spawnTime < ignoreCollisionTime)
        {
            return;
        }

        EnemyLogic enemy = other.GetComponent<EnemyLogic>();
        if (enemy != null)
            enemy.TakeDamage(50, shotId);

        // Destroy the bullet when it collides with anything
        Destroy(gameObject);
    }
}
