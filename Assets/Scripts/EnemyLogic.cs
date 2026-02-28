using UnityEngine;
using System.Collections.Generic;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    private HashSet<int> processedShotIds = new HashSet<int>();

    void Awake()
    {
        health = maxHealth;
    }
    void Update()
    {
        
    }

    public void TakeDamage(int damage, int shotId)
    {
        if (processedShotIds.Contains(shotId)) return;
        processedShotIds.Add(shotId);

        health -= damage;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.RegisterKill();
        Destroy(gameObject);
    }
}
