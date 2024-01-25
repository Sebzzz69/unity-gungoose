using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float bulletSpeed;

    [SerializeField] int bloomRange;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
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



