using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] int moveSpeed;
    [SerializeField] int detectionRange;

    Rigidbody2D rb;

    Vector3 raycastOffset;
    Vector2 movement;

    bool isFollowingPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Create raycast offest
        // Start at the end of the object
        raycastOffset = this.transform.position + this.transform.up * (transform.localScale.y / 2f + 0.07f);
        RaycastHit2D hit = Physics2D.Raycast(raycastOffset, transform.up, detectionRange);

        CheckRaycastHit(hit);

        if (isFollowingPlayer)
        {
            MoveTowards(hit.collider.gameObject);
        }
        else if(!isFollowingPlayer)
        {
            StopMoving();
        }
        
        Debug.DrawRay(raycastOffset, transform.up * detectionRange, Color.green);
        Debug.Log(movement);
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed * 10 * Time.deltaTime;
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

    void MoveTowards(GameObject target)
    {
        // Calculate the movement
        movement = (target.transform.position - this.transform.position).normalized;

        float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        // Create a Quaternion rotation based on the calculated angle
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);
    }
    void StopMoving()
    {
        movement = Vector2.zero;
    }

}



