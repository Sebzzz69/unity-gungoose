using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed;
    [SerializeField] int bloomRange;

    [SerializeField] int ammunitionAmount;
    [SerializeField] int reloadTimeInSeconds;
    int bulletCount;
    


    private void Start()
    {
        bulletCount = ammunitionAmount;
    }

    private void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            if (bulletCount <= 0) return;

            Shoot();
            bulletCount--;
        }

        if(Input.GetKeyDown(KeyCode.R)) 
        { 
            ReloadTimer();
            Reload();
        }

    }



    IEnumerable ReloadTimer()
    {
        yield return new WaitForSeconds(reloadTimeInSeconds);
    }

    void Reload()
    {
        bulletCount = ammunitionAmount;
    }


    void Shoot()
    {

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.transform.Rotate(Vector3.forward, BloomControl(bloomRange));

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 bulletDirection = bullet.transform.up.normalized;
        rb.velocity = bulletDirection * bulletSpeed;

        Destroy(bullet, 4f);
    }

    int BloomControl(int bloomRange)
    {
        return Random.Range(bloomRange, -bloomRange);
    }
}



