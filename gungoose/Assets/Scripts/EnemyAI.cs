using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] int moveSpeed;
    [SerializeField] int detectionRange;

    RaycastHit2D hit;

    Rigidbody2D rb;

    Vector3 raycastOffset;
    Vector3 raycastTarget;
    Vector2 movement;

    bool isFollowingPlayer = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Create raycast offest
        // Start at the end of the this object
        raycastOffset = this.transform.position + this.transform.up * (transform.localScale.y / 2f + 0.07f);
        

        CheckRaycastHit(hit);

        if (isFollowingPlayer)
        {
            hit = Physics2D.Raycast(raycastOffset, raycastTarget, detectionRange);

            MoveTowards(hit.collider.gameObject);
            
        }
        else if(!isFollowingPlayer)
        {
            StopMoving();
        }
        
        Debug.DrawRay(raycastOffset, raycastTarget * detectionRange, Color.green);
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

                raycastTarget = hit.collider.gameObject.transform.position;

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

        // Calculate new raycast end to follow 'target'
        //raycastTarget = target.gameObject.transform.position;

        /*float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

        // Create a Quaternion rotation based on the calculated angle
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);*/
    }
    void StopMoving()
    {
        // Raycast target will be away from enemy
        hit = Physics2D.Raycast(raycastOffset, raycastTarget, detectionRange);

        movement = Vector2.zero;

    }

}



