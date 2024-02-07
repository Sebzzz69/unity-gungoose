using UnityEngine;

public class Bullet : MonoBehaviour
{
    int damageAmount;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy");
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(damageAmount);
        }
    }

    public void SetBulletDamage(int damageAmount)
    {
        this.damageAmount = damageAmount;
    }

}
