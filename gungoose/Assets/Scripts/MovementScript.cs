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

        movement = acceleration;


        //Debug.Log(rb.velocity);
        
    }
    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed * 10 * Time.deltaTime;
    }

    void GetPlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
    void RotatePlayer()
    {

        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, -90);
        }

        if(Input.GetAxisRaw("Vertical") < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            this.transform.eulerAngles = Vector3.zero;
        }
    }

    void ApplyAcceleration()
    {
        Vector2 targetVelocity = new Vector2(horizontalInput, verticalInput).normalized * moveSpeed;

        acceleration = Vector2.Lerp(acceleration, targetVelocity, accelerationSpeed * Time.deltaTime);
    }

    void ApplyDrag()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            acceleration = Vector2.Lerp(acceleration, Vector2.zero, drag * decayFactor * Time.deltaTime);

            movement = acceleration;
        }
    }
}
