using System.Collections;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    [SerializeField] WeaponData weaponData;

    GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    int bulletSpeed;
    int bloomRange;
    int damageAmount;

    int ammunitionAmount;
    int reloadTimeInSeconds;
    int bulletCount;

    bool isShotgun;
    


    private void Start()
    {
        bulletPrefab = weaponData.bulletPrefab;
        bulletSpeed = weaponData.bulletSpeed;
        bloomRange = weaponData.bloomRange;
        damageAmount = weaponData.damageAmount;

        ammunitionAmount = weaponData.ammunitionAmount;
        reloadTimeInSeconds = weaponData.reloadTimeInSeconds;

        isShotgun = weaponData.isShotgun;

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
        int numberOfBullets = 5;

        if (isShotgun)
        {


            for (int i = 0; i < numberOfBullets; i++)
            {
                GameObject bullet;

                // Calculate random spread within the spread angle
                Quaternion spreadRotation = Quaternion.Euler(0, 0, firePoint.rotation.eulerAngles.z + BloomControl(bloomRange));

                // Instantiate bullet with spread rotation
                bullet = Instantiate(bulletPrefab, firePoint.position, spreadRotation);

                // Set bullet damage
                bullet.GetComponent<Bullet>().SetBulletDamage(damageAmount);

                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                // Apply bullet velocity
                Vector2 bulletDirection = bullet.transform.up.normalized;
                rb.velocity = bulletDirection * bulletSpeed;

                Destroy(bullet, 2f);

                Debug.DrawRay(firePoint.position, bulletDirection * 5, Color.white, 0.1f);
            }


        }
        else
        {
            GameObject bullet;

            bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.transform.Rotate(Vector3.forward, BloomControl(bloomRange));

            // Set bullet damage
            bullet.GetComponent<Bullet>().SetBulletDamage(damageAmount);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            Vector2 bulletDirection = bullet.transform.up.normalized;
            rb.velocity = bulletDirection * bulletSpeed;

            Destroy(bullet, 4f);

            Debug.DrawRay(firePoint.position, bulletDirection * 5, Color.white, 0.1f);
        }
        
    }

    int BloomControl(int bloomRange)
    {
        if (isShotgun)
        {
            return Random.Range(bloomRange + 40, (bloomRange + 40) * -1);
        }
        else
        {
            return Random.Range(bloomRange, -bloomRange);
        }
        
    }
}



