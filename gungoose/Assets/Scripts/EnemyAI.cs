using Unity.Burst.CompilerServices;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameManager gameManager;

    [Header("Movement Settings")]
    [SerializeField] int moveSpeed;
    [SerializeField] int damageAmount;
    [SerializeField] int damageCooldownSeconds;



    [Header("Raycast settings for player detection")]
    [SerializeField] Transform raycastOrigin;
    [SerializeField] int detectionRange;
    [SerializeField] int detectionSpreadAngle;
    [SerializeField] int detectionDensity;

    Rigidbody2D rb;
    RaycastHit2D targetRaycast;
    Vector2 movement;
    bool isFollowingPlayer;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        isFollowingPlayer = false;
    }

    private void Update()
    {
        HandleRaycast();

        if (targetRaycast.collider != null && Vector2.Distance(this.transform.position, targetRaycast.collider.transform.position) >= detectionRange + 2)
        {
            isFollowingPlayer = false;
        }

        if (isFollowingPlayer)
        {
            MoveTowards(targetRaycast.collider.gameObject);
            LookAt2D(this.transform, targetRaycast.collider.transform.position);
        }
        else if (!isFollowingPlayer)
        {
            StopMoving();
        }

    }

    private void FixedUpdate()
    {
        Move();
    }

    void HandleRaycast()
    {

        float angleStep = detectionSpreadAngle / (detectionDensity - 1); // Calculate the angle between each raycast
        float startAngle = -detectionSpreadAngle / 2;

        // Creating raycasts
        if (!isFollowingPlayer)
        {
            for (int i = 0; i < detectionDensity; i++)
            {
                float angle = startAngle + angleStep * i;
                Vector2 direction = Quaternion.Euler(0, 0, angle) * transform.right;

                RaycastHit2D hit = Physics2D.Raycast(raycastOrigin.position, direction, detectionRange);

                if (hit.collider != null)
                {
                    CheckRaycastHit(hit);
                }

                Debug.DrawRay(raycastOrigin.position, direction * detectionRange, Color.red);
            }
        }


    }
    void CheckRaycastHit(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            isFollowingPlayer = hit.collider.CompareTag("Player");

            if (isFollowingPlayer)
            {
                targetRaycast = hit;
            }
        }

        if (hit.collider == null)
        {
            isFollowingPlayer = false;
        }
    }

    public void LookAt2D(Transform transform, Vector2 target)
    {
        // IDK HTF this works
        // Too lazy to figure out

        Vector2 direction = target - (Vector2)transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Move()
    {
        rb.velocity = movement * moveSpeed * 10 * Time.fixedDeltaTime;
    }

    void MoveTowards(GameObject target)
    {
        // Calculate the movement direction
        movement = (target.transform.position - this.transform.position).normalized;
    }

    void StopMoving()
    {
        movement = Vector2.zero;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
        }
    }
}
