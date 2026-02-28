using UnityEngine;

public class Gun : MonoBehaviour
{
    public float bulletScale = 0.2f;
    //bullet force
    public float shootForce;

    //gun stats (spread is in degrees)
    public float timeBetweenShooting, spread, timeBetweenShots;
    public int bulletsPerTap;
    public bool allowButtonHold;
    public int bulletsShot;

    //reference
    public Camera fpsCam;
    public Transform attackPoint;
    private PlayerWeaponManager weaponManager;

    //other
    bool shooting, readyToShoot;
    public bool allowInvoke = true;

    private void Awake()
    {
       readyToShoot = true;
       weaponManager = GetComponentInParent<PlayerWeaponManager>();
    }

    private void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        if (weaponManager != null && (weaponManager.CurrentMode != WeaponMode.Shotgun || weaponManager.CurrentAmmo <= 0))
            return;

        if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting)
        {
            bulletsShot = 0;
            if (weaponManager != null && !weaponManager.ConsumeAmmo())
                return;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        RaycastHit hit;
        Vector3 targetPoint;
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100f);
        }
        Debug.Log($"Target point: {targetPoint}");
        
        Vector3 directionWithoutSpread = (targetPoint - attackPoint.position).normalized;

        float spreadX = Random.Range(-spread, spread);
        float spreadY = Random.Range(-spread, spread);

        Quaternion spreadRotation = Quaternion.Euler(spreadY, spreadX, 0f);
        Vector3 directionWithSpread = spreadRotation * directionWithoutSpread;
        Debug.Log($"Direction with spread: {directionWithSpread}");

        // Create bullet programmatically
        GameObject currentBullet = CreateBullet(attackPoint.position, directionWithSpread.normalized);
        
        Debug.Log($"Bullet direction: {currentBullet.transform.forward}");
        Debug.Log($"With spread normalized: {directionWithSpread.normalized}");

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);

        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerTap)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private GameObject CreateBullet(Vector3 position, Vector3 direction)
    {
        // Create bullet GameObject
        GameObject bulletObj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bulletObj.name = "Bullet";
        bulletObj.transform.position = position;
        bulletObj.transform.forward = direction;
        bulletObj.transform.localScale = Vector3.one * bulletScale;

        Collider bulletCollider = bulletObj.GetComponent<Collider>();
        if (bulletCollider != null)
        {
            bulletCollider.isTrigger = true;
        }

        // Add Rigidbody
        Rigidbody rb = bulletObj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = bulletObj.AddComponent<Rigidbody>();
        }
        rb.mass = 1f;
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

        // Add Bullet script for collision and lifetime management
        Bullet bullet = bulletObj.AddComponent<Bullet>();
        if (weaponManager != null)
            bullet.shotId = weaponManager.CurrentShotId;

        return bulletObj;
    }

}