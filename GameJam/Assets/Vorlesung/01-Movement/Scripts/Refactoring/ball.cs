using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ball : MonoBehaviour {
    
    // Bewegungs-Geschwindigkeit
    public float speed = 0;

    // Sprung Stärke
    public float jmpStr = 50;

    // Rigidbody of the player.
    private Rigidbody rb;

    // Bewegung entlang der X- und Y-Achse.
    private float movX;
    private float movY;
    

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        movX = Input.GetAxis("Horizontal");
        movY = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Space) && GroundCheck()) {
            Jumping();
        }
    }

    void FixedUpdate() {
        Movement();
    }

    private void Movement() {
        Vector3 Movement = new Vector3(movX, 0.0f, movY);
        rb.AddForce(Movement * speed);
    }

    private void Jumping() {
        rb.AddForce(Vector3.up * jmpStr, ForceMode.Impulse);
    }

    private bool GroundCheck() {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, -Vector3.up, out hit, 0.6f)) {
            print(hit.collider.name);
            return true;
        }
        else {
            return false;
        }
    }
}
