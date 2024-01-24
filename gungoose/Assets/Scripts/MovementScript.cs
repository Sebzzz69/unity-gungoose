using UnityEngine;
using UnityEngine.Assertions.Must;

public class MovementScript : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    float horizontalInput;
    float verticalInput;

    Rigidbody2D rb;

    Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        RotatePlayer(horizontalInput, verticalInput);
        movement = new Vector2(horizontalInput, verticalInput);
    }
    private void FixedUpdate()
    {
        // Move the Rigidbody
        rb.velocity = movement * moveSpeed;

    }


    void RotatePlayer(float horizontalInput, float verticalInput)
    {

        if(horizontalInput < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if(horizontalInput > 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, -90);
        }

        if(verticalInput < 0)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else if (verticalInput > 0)
        {
            this.transform.eulerAngles = Vector3.zero;
        }
    }
}
