using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    public float GroundCheckDistance = 0.1f;
    public float GravityMultiplier = 5f;
    public float JumpForce = 10f;
    public float ForceStrength = 100;

    private float _ySpeed;

    private Animator _animator;
    private CharacterController _characterController;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        /*
        if (IsGrounded())
        {
            _ySpeed = 0;
        }

        else
        {
            _ySpeed = _ySpeed * Physics.gravity.y * GravityMultiplier * Time.deltaTime;
        }
        */

        if (IsGrounded())
        {
            _ySpeed = 0;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _ySpeed = JumpForce;
            }
        }
        else
        {
            _ySpeed += Physics.gravity.y * GravityMultiplier * Time.deltaTime;
        }

        _animator.SetFloat("yDirection", _ySpeed);

        Vector3 velocity = new Vector3(0, _ySpeed, 0);
        _characterController.Move(velocity * Time.deltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody != null)
        {
            Vector3 direction = hit.gameObject.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            rigidbody.AddForceAtPosition(direction * ForceStrength, transform.position, ForceMode.Impulse);
        }
    }

    private void OnAnimatorMove()
    {
        _animator.ApplyBuiltinRootMotion();

        Vector3 velocity = _animator.deltaPosition;
        velocity.y = _ySpeed;

        _characterController.Move(velocity * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position + new Vector3(0, GroundCheckDistance * 0.5f, 0), Vector3.down, GroundCheckDistance);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position + new Vector3(0, GroundCheckDistance * 0.5f, 0), Vector3.down * GroundCheckDistance, IsGrounded() ? Color.green : Color.red);
    }
}
