using UnityEngine;

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

    private void TryDieFromTag(string tag) {
        if (!isAlive) {
            return;
        }

        if (tag == enemyTag) {
            isAlive = false;
            Debug.Log("Player has died.");
        }
    }
}