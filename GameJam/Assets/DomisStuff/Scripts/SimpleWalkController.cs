using UnityEngine;

/// <summary>
/// Simple implementaion of a characer controller
/// </summary>
public class SimpleWalkController : MonoBehaviour
{
    // Defines how fast the GO can move
    public float movementSpeed = 3;

    // Defines how fast the GO can turn
    public float turnSpeed = 90;

    public float runningSpeed = 20;

    // Define the Animator Component
    private Animator _animator;

    // Define the variable names for Animator Hashes
    private int isWalkingParameterHash;
    private int directionParameterHash;
    private int isRunningParameterHash;

    // Definde if LeftShift is pressed or not
    private bool isRunning;

    private void Start()
    {
        // Define the Animator Component and its variables to hashes
        _animator = GetComponent<Animator>();
        isWalkingParameterHash = Animator.StringToHash("isWalking");
        directionParameterHash = Animator.StringToHash("direction");
        isRunningParameterHash = Animator.StringToHash("isRunning");
    }
    // Update is called once per frame
    void Update()
    {
        // Stores inputs
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float verticalInputRaw = Input.GetAxisRaw("Vertical");

        // Are we running
        /// Yes
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movementSpeed = runningSpeed;
        }
        /// No
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movementSpeed = 3;
        }

        // Translate along z-axis
        transform.Translate(Vector3.forward * verticalInput * movementSpeed * Time.deltaTime);

        // Are we moving
        if(verticalInput != 0) 
        {
            // calculate the angle around y-axis
            float turnAngle = turnSpeed * horizontalInput * Input.GetAxisRaw("Vertical") * Time.deltaTime;
            transform.Rotate(Vector3.up, turnAngle);
        }


        _animator.SetBool(isWalkingParameterHash, verticalInputRaw != 0);
        _animator.SetFloat(directionParameterHash, verticalInputRaw);
        _animator.SetBool(isRunningParameterHash, Input.GetKey(KeyCode.LeftShift));
    }

}
