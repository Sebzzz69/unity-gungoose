using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    public PlayerHealth healthSystem;
    [SerializeField] int detectionRange;
    [SerializeField] int moveSpeed;
    [SerializeField] int damageAmount;
    [SerializeField] int damageCooldownSeconds;

    RaycastHit2D targetRaycast;
    Rigidbody2D rb;


    Vector3 raycastOffset;
    Vector2 targetVector;

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
        raycastOffset = this.transform.localPosition + this.transform.right * (transform.localScale.y / 2f + 0.07f);

        //Creating raycast
        //targetRaycast = Physics2D.Raycast(raycastOffset, this.transform.right, detectionRange);

        // Creating raycasts
        for (int i = 0; i < 5; i++)
        {
            RaycastHit2D hitRight = Physics2D.Raycast(raycastOffset, new Vector2(this.transform.localPosition.x, i * 0.5f), detectionRange);
            RaycastHit2D hitLeft = Physics2D.Raycast(raycastOffset, new Vector2(this.transform.localPosition.x, -i * 0.5f), detectionRange);

            CheckRaycastHit(hitRight);
            CheckRaycastHit(hitLeft);

            //Debug.DrawRay(raycastOffset, new Vector2(this.transform.position.x, this.transform.position.y * (i * 0.5f)), Color.green);
            //Debug.DrawRay(raycastOffset, new Vector2(this.transform.position.x, this.transform.position.y * (-i * 0.5f)), Color.green);

            Debug.DrawRay(raycastOffset, transform.TransformDirection(new Vector3 (1, (i * 0.5f), 0), Color.green));
            Debug.DrawRay(raycastOffset, transform.TransformDirection(new Vector3(1, (-i * 0.5f), 0), Color.green));
        }

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


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                healthSystem.TakeDamage(damageAmount);
            }
        }
    }

