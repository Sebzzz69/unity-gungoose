using UnityEngine;
using UnityEngine.Assertions.Must;

public class MovementScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    float horizontalInput;
    float verticalInput;

    [SerializeField] KeyCode keyUp;
    [SerializeField] KeyCode keyDown;
    [SerializeField] KeyCode keyLeft;
    [SerializeField] KeyCode keyRight;

    Rigidbody2D rb;

    Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        GetPlayerInput();

        movement = new Vector2(horizontalInput, verticalInput).normalized;

        RotatePlayer();
    }
    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed * 10 * Time.fixedDeltaTime;

        //rb.velocity = Vector2.zero;

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
}
