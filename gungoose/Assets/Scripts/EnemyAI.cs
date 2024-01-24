using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] int moveSpeed;
    [SerializeField] int detectionRange;

    Rigidbody2D rb;

    [SerializeField] LayerMask player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, transform.forward, detectionRange);

        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Debug.Log("Player detected!");
        }

        Debug.DrawRay(this.transform.position, transform.forward * detectionRange, Color.green);
    }

}
