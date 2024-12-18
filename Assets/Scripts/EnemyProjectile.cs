using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    //Gun stats
    public int dmg;
    public float timeBetweenShooting, spread, range, reloadTime;
    public GameObject muzzleFlash;
    public int magazineSize;
    int bulletsLeft;

    //some bools
    bool shooting, readyToShoot, reloading;

    public Transform enemy;
    public Transform attackPos;
    public RaycastHit rayHit;
    public LayerMask whatIsPlayer;

    private void Start()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        shooting = enemy.GetComponent<Enemy>().playerInAttackRange;
        if (shooting && readyToShoot && bulletsLeft > 0 && !reloading) Shoot();
    }

    public void Shoot()
    {
        readyToShoot = false;

        //RayCast
        if (Physics.Raycast(transform.position, enemy.forward, out rayHit, range, whatIsPlayer))
        {
            if (rayHit.collider.gameObject.GetComponent<Character>())
                //rayHit.collider.GetComponent<Character>().TakeDmg(dmg);

                Debug.Log(rayHit.collider.gameObject.name);
        }

        bulletsLeft--;

        Instantiate(muzzleFlash, attackPos.position, Quaternion.identity);

        Invoke("ShotReset", timeBetweenShooting);
    }
    private void ShotReset()
    {
        readyToShoot = true;
    }
    private void Reload()
    {
        reloading = true;

        Invoke("ReloadingFinished", reloadTime);
    }
    private void ReloadingFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
