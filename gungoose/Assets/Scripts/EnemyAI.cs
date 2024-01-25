using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] int detectionRange;
    [SerializeField] int moveSpeed;

    RaycastHit2D targetRaycast;
    Rigidbody2D rb;


    Vector3 raycastOffset;
    Vector2 targetVector;

    Vector2 movement;

    bool isFollowingPlayer = false;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        HandleRaycast();

        CheckRaycastHit(targetRaycast);

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
        // Offset so raycast doesn't detect this.gameObject
        raycastOffset = this.transform.position + this.transform.right * (transform.localScale.y / 2f + 0.07f);

        //Creating raycast
        targetRaycast = Physics2D.Raycast(raycastOffset, this.transform.right, detectionRange);

        //Debugging raycast
        Debug.DrawRay(raycastOffset, this.transform.right * detectionRange, Color.green);
    }
    void CheckRaycastHit(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("Player detected!");
                isFollowingPlayer = true;

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

        Vector2 current = transform.position;
        var direction = target - current;
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

}
