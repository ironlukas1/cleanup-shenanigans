using System.Runtime.Serialization;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {
    [SerializeField] private int maxHealth = 100;
    private int health;
    [SerializeField] private int damage = 25;
    private void Awake() {
        health = maxHealth;
    }
    private void Update() {
        
    }
}