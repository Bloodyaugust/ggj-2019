using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed;
    public float RotationSpeed;

    Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update() {

        rigidBody.AddForce(transform.up * -1 * Input.GetAxis("Vertical") * MovementSpeed);
        rigidBody.AddTorque(Input.GetAxis("Horizontal") * RotationSpeed * -1);
    }
}
