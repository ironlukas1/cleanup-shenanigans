using UnityEngine;

public class PlayerLogic : MonoBehaviour {
    //[SerializeField] private int maxHealth = 100;
    //private int health;
    private bool isAlive;
    [SerializeField] private int damage = 25;
    [SerializeField] private LayerMask enemyLayers;

    private void Awake() {
        //health = maxHealth;
        isAlive = true;

        if (enemyLayers.value == 0) {
            enemyLayers = LayerMask.GetMask("Enemy");
        }

        Debug.Log($"{name}: PlayerLogic Awake. Enemy mask value = {enemyLayers.value}");
    }
    private void Update() {
        if (!isAlive) {
            Debug.Log("Player is dead.");
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log($"{name}: OnCollisionEnter with {collision.gameObject.name}");
        TryDieFromLayer(collision.gameObject.layer);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"{name}: OnTriggerEnter with {other.gameObject.name}");
        TryDieFromLayer(other.gameObject.layer);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log($"{name}: OnCollisionEnter2D with {collision.gameObject.name}");
        TryDieFromLayer(collision.gameObject.layer);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"{name}: OnTriggerEnter2D with {other.gameObject.name}");
        TryDieFromLayer(other.gameObject.layer);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        Debug.Log($"{name}: OnControllerColliderHit with {hit.gameObject.name}");
        TryDieFromLayer(hit.gameObject.layer);
    }

    private void TryDieFromLayer(int layer) {
        if (!isAlive) {
            return;
        }

        int layerBit = 1 << layer;
        if ((enemyLayers.value & layerBit) != 0) {
            isAlive = false;
            Debug.Log("Player has died.");
        }
    }
}