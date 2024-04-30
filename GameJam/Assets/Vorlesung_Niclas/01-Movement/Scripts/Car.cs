using UnityEngine;

public class Car : MonoBehaviour
{
    // Bewegungsgeschwindigkeit
    public float MovementSpeed = 8.0f;

    // Lenkgeschwindigkeit
    public float TurnSpeed = 20.0f;

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float verticalInputRaw = Input.GetAxisRaw("Vertical");
        float horizonatlInput = Input.GetAxis("Horizontal");

        // Fahren
        transform.Translate(Vector3.forward * verticalInput * MovementSpeed * Time.deltaTime);

        // Lenken
        transform.Rotate(Vector3.up, TurnSpeed * horizonatlInput * verticalInputRaw * TurnSpeed * Time.deltaTime);
    }
}
