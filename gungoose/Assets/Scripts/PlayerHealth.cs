using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;
    public int Health { get { return health; } }


    private void Start()
    {

    }

    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
    }





}
