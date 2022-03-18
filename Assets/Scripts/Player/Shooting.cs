using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Shooting : MonoBehaviour
{

    [SerializeField] float shootForce, upwardForce;

    [SerializeField] float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    [SerializeField] int magazineSize, bulletsPerTap;
    [SerializeField] bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    bool shooting, readyToShoot = false, reloading;

    [SerializeField] Camera cam;
    [SerializeField] Transform attackPoint;

    [SerializeField] bool allowInvoke = true;

    Animator animator;

    [SerializeField] BulletsPool bulletPool;

    private void Awake()
    {
        bulletsLeft = magazineSize;
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            Reload();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;
        animator.SetBool("isShooting", true);

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit) && !hit.transform.CompareTag("Player") && !hit.transform.CompareTag("Ground"))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(75);
        }

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float xSpread = Random.Range(-spread, spread);
        float ySpread = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(xSpread, ySpread, 0);

        transform.LookAt(new Vector3(targetPoint.x, transform.position.y, targetPoint.z));

        GameObject currentBullet = bulletPool.CreateBullet(attackPoint.position).gameObject;
        currentBullet.transform.forward = directionWithSpread;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(cam.transform.up * upwardForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }
    }

    public void ResetShot()
    {
        readyToShoot = true;
        if (animator.GetBool("isShooting"))
            animator.SetBool("isShooting", false);
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }

    public void StopShooting()
    {
        CancelInvoke("ResetShot");
        allowInvoke = false;
        readyToShoot = false;
    }
}
