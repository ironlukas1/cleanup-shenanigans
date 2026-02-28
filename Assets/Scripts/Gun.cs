using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    //bullet
    public GameObject bullet;

    //bullet force
    public float shootForce;

    //gun stats
    public float timeBetweenShooting, spread, timeBetweenShots;
    public int bulletsPerTap;
    public bool allowButtonHold;
    public int bulletsShot;

    //reference
    public Camera fpsCam;
    public Transform attackPoint;

    //other
    bool shooting, readyToShoot;
    public bool allowInvoke = true;

    private void Awake()
    {
       readyToShoot = true; 
    }

    private void Update()
    {
        MyInput();
    }
    private void MyInput()
    {
        if(allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting)
        {
            bulletsShot = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }
        
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;

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
}
