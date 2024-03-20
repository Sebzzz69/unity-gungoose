using UnityEngine;

public class MovementScript : MonoBehaviour
{
    

    [SerializeField] int moveSpeed;
    [SerializeField] float accelerationSpeed;
    [SerializeField] float drag;
    [SerializeField] float decayFactor;

    float horizontalInput;
    float verticalInput;

    [SerializeField] KeyCode keyUp;
    [SerializeField] KeyCode keyDown;
    [SerializeField] KeyCode keyLeft;
    [SerializeField] KeyCode keyRight;

    Rigidbody2D rb;

    Vector2 movement;
    Vector2 acceleration;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        GetPlayerInput();

        ApplyAcceleration();
        ApplyDrag();

        RotatePlayer();


    }
    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed * 10 * Time.deltaTime;
    }

    void GetPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (horizontalInput != 0)
        {
            verticalInput = 0;
        }

        movement = new Vector2(horizontalInput, verticalInput).normalized;
    }
    void RotatePlayer()
    {

        if (movement.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (movement.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (movement.y < 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (movement.y > 0)
        {
            transform.eulerAngles = Vector3.zero;
        }

    }

    void ApplyAcceleration()
    {
        Vector2 targetVelocity = movement * moveSpeed;
        movement = Vector2.Lerp(movement, targetVelocity, accelerationSpeed * Time.deltaTime);
    }

    void ApplyDrag()
    {

        if (movement.magnitude == 0)
        {
            movement = Vector2.Lerp(movement, Vector2.zero, drag * decayFactor * Time.deltaTime);
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("AmmunitionClip"))
        {
            this.gameObject.GetComponent<GunScript>().AddClip(1);
            Destroy(collision.gameObject);
        }
    }

 

}
