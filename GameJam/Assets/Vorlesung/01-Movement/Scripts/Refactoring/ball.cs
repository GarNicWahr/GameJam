using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{

    // Bewegungs-Geschwindigkeit
    public float MovementSpeed = 0;

    // Sprung Stärke
    public float JumpStrength = 50;

    // Rigidbody of the player.
    private Rigidbody _rigidBody;

    // Bewegung entlang der X- und Y-Achse.
    private float _movementX;
    private float _movementY;


    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _movementX = Input.GetAxis("Horizontal");
        _movementY = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(_movementX, 0.0f, _movementY);
        _rigidBody.AddForce(movement * MovementSpeed);
    }

    private void Jump()
    {
        _rigidBody.AddForce(Vector3.up * JumpStrength, ForceMode.Impulse);
    }

    private bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 0.6f))
        {
            print(hit.collider.name);
            return true;
        }
        else
        {
            return false;
        }
    }
}
