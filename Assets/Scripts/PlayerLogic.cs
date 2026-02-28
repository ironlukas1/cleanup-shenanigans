using UnityEngine;

public class PlayerLogic : MonoBehaviour {
    public bool IsAlive { get; private set; }
    [SerializeField] private string enemyTag = "Enemy";

    private void Awake() {
        IsAlive = true;

        if (string.IsNullOrEmpty(enemyTag)) {
            enemyTag = "Enemy";
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(enemyTag))
            Die();
    }

    private void Die() {
        if (!IsAlive) return;
        IsAlive = false;

        // Disable player controls
        var movement = GetComponent<PlayerMovement>();
        if (movement != null) movement.enabled = false;

        var cam = GetComponentInChildren<CameraMovement>();
        if (cam != null) cam.enabled = false;

        var weapon = GetComponent<PlayerWeaponManager>();
        if (weapon != null) weapon.enabled = false;

        var gun = GetComponentInChildren<Gun>();
        if (gun != null) gun.enabled = false;

        if (GameManager.Instance != null)
            GameManager.Instance.PlayerDied();
    }
}