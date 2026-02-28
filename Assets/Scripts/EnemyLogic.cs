using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    void Awake()
    {
        health = maxHealth;
    }
    void Update()
    {
        
    }
}
