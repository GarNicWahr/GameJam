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

    public float runningSpeed = 9;

    // Define the Animator Component
    private Animator _animator;

    // Define the variable names for Animator Hashes
    private int _isWalkingParameterHash;
    private int _directionParameterHash;
    private int _isRunningParameterHash;


    private void Start()
    {
        // Define the Animator Component and its variables to hashes
        _animator = GetComponent<Animator>();
        _isWalkingParameterHash = Animator.StringToHash("isWalking");
        _directionParameterHash = Animator.StringToHash("direction");
        _isRunningParameterHash = Animator.StringToHash("isRunning");
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


        _animator.SetBool(_isWalkingParameterHash, verticalInputRaw != 0);
        _animator.SetFloat(_directionParameterHash, verticalInputRaw);
        _animator.SetBool(_isRunningParameterHash, Input.GetKey(KeyCode.LeftShift));
    }

}
