using UnityEngine;

/// <summary>
/// Controll a blend tree
/// </summary>
public class SimpleBlendTreeController : MonoBehaviour
{
    // Defines how fast the character will move
    public float MaxMovementSpeed = 9;

    public float LocomotionParameterDamping = 0.1f;

    // Animator playing animations
    private Animator _animator;

    // Hash speed parameter
    private int _speedParameterHash;

    // Hash speed parameter
    private int _isWalkingParameterHash;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _speedParameterHash = Animator.StringToHash("speed");
        _isWalkingParameterHash = Animator.StringToHash("isMoving");
    }

    // Update is called once per frame
    void Update()
    {
        // Stores inputs
        float verticalInput = Input.GetAxis("Vertical");

        // left or right shift-key held?
        bool shouldWalk = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        // Set speed depending on walk or run state
        float speed = shouldWalk ? verticalInput / 2 : verticalInput;

        // Set parameter in animator
        _animator.SetFloat(_speedParameterHash, speed, LocomotionParameterDamping, Time.deltaTime);

        // Move the go along z-axis
        transform.Translate(Vector3.forward * MaxMovementSpeed * verticalInput * Time.deltaTime);
    }
}
