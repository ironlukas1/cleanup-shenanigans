/*using UnityEngine;

public class PlayerLogic : MonoBehaviour {
    private bool isAlive;
    [SerializeField] private int damage = 25;
    [SerializeField] private string enemyTag = "Enemy";

    private void Awake() {
        isAlive = true;

        if (string.IsNullOrEmpty(enemyTag)) {
            enemyTag = "Enemy";
        }

        Debug.Log($"{name}: PlayerLogic Awake. Enemy tag = {enemyTag}");
    }
    private void Update() {
        if (!isAlive) {
            Debug.Log("Player is dead.");
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log($"{name}: OnCollisionEnter with {collision.gameObject.name}");
        TryDieFromTag(collision.gameObject.tag);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"{name}: OnTriggerEnter with {other.gameObject.name}");
        TryDieFromTag(other.gameObject.tag);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log($"{name}: OnCollisionEnter2D with {collision.gameObject.name}");
        TryDieFromTag(collision.gameObject.tag);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log($"{name}: OnTriggerEnter2D with {other.gameObject.name}");
        TryDieFromTag(other.gameObject.tag);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        Debug.Log($"{name}: OnControllerColliderHit with {hit.gameObject.name}");
        TryDieFromTag(hit.gameObject.tag);
    }

    private void TryDieFromTag(string tag) {
        if (!isAlive) {
            return;
        }

        if (tag == enemyTag) {
            isAlive = false;
            Debug.Log("Player has died.");
        }
    }
}*/