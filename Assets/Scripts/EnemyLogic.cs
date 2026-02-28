using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;
    [SerializeField] private int damage = 100;
    void Awake()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
