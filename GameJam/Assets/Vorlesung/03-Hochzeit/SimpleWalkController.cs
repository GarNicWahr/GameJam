using UnityEngine;

/// <summary>
/// Simple implementaion of a characer controller
/// </summary>
public class SimpleWalkController : MonoBehaviour
{
    // Defines how fast the GO can move
    public float movementSpeed = 10;

    // Defines how fast the GO can turn
    public float turnSpeed = 90;

    //Define the Animator Component
    private Animator _animator;

    private int isWalkingParameterHash;
    private int directionParameterHash;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        isWalkingParameterHash = Animator.StringToHash("isWalking");
        directionParameterHash = Animator.StringToHash("direction");
    }
    // Update is called once per frame
    void Update()
    {
        // Stores inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float verticalInputRaw = Input.GetAxisRaw("Vertical");

        // Translate along z-axis
        transform.Translate(Vector3.forward * verticalInput * movementSpeed * Time.deltaTime);

        // Are we moving
        if(verticalInput != 0) 
        {
            // calculate the angle around y-axis
            float turnAngle = turnSpeed * horizontalInput * Input.GetAxisRaw("Vertical") * Time.deltaTime;
            transform.Rotate(Vector3.up, turnAngle);
            _animator.SetBool("isWalking", true);
        }

        _animator.SetBool(isWalkingParameterHash, verticalInputRaw != 0);
        _animator.SetFloat(directionParameterHash, verticalInputRaw);
        
    }
}
