using UnityEngine;

public enum WeaponMode { Mop, Shotgun }

public class PlayerWeaponManager : MonoBehaviour
{
    [Header("Ammo")]
    [SerializeField] private int startingAmmo = 8;
    public int CurrentAmmo { get; private set; }

    [Header("Mop Settings")]
    [SerializeField] private float mopRange = 3f;
    [SerializeField] private LayerMask puddleLayer;

    [Header("References")]
    [SerializeField] private Gun gun;

    public WeaponMode CurrentMode { get; private set; } = WeaponMode.Mop;
    public int CurrentShotId { get; private set; }

    private void Awake()
    {
        CurrentAmmo = startingAmmo;

        if (gun == null)
            gun = GetComponentInChildren<Gun>();
    }

    private void Start()
    {
        SetMode(WeaponMode.Mop);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ToggleMode();

        if (CurrentMode == WeaponMode.Mop && Input.GetKeyDown(KeyCode.Mouse0))
            TryCleanPuddle();
    }

    private void ToggleMode()
    {
        SetMode(CurrentMode == WeaponMode.Mop ? WeaponMode.Shotgun : WeaponMode.Mop);
    }

    private void SetMode(WeaponMode mode)
    {
        CurrentMode = mode;
        if (gun != null)
            gun.enabled = (mode == WeaponMode.Shotgun);
    }

    private void TryCleanPuddle()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, mopRange, puddleLayer);
        if (hits.Length == 0) return;

        // Find nearest puddle
        Collider nearest = null;
        float nearestDist = float.MaxValue;
        foreach (var col in hits)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position);
            if (dist < nearestDist)
            {
                nearestDist = dist;
                nearest = col;
            }
        }

        if (nearest == null) return;

        Puddle puddle = nearest.GetComponent<Puddle>();
        if (puddle != null)
        {
            if (puddle.type == PuddleType.Dirt)
                AddAmmo(Random.Range(5, 10));

            Destroy(nearest.gameObject);
        }
    }

    public bool ConsumeAmmo()
    {
        if (CurrentAmmo <= 0) return false;
        CurrentAmmo--;
        CurrentShotId++;
        return true;
    }

    public void AddAmmo(int amount)
    {
        CurrentAmmo += amount;
    }
}
