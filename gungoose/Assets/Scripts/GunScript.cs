using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed;

    [SerializeField] int bloomRange;

    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }



    void Shoot()
    {
        bloomRange = BloomControl();

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, bloomRange));

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        rb.velocity = firePoint.up * bulletSpeed;

        Destroy(bullet, 4f);
    }

    int BloomControl()
    {

        int maxRotation = (int)firePoint.localRotation.z + 5;
        int minRotation = (int)firePoint.localRotation.z - 5;

        return Random.Range(minRotation, maxRotation);
    }
}



