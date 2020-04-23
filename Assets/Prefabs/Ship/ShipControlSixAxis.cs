using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipControlSixAxis : MonoBehaviour
{
    public float speed = 1;
    public float maxSpeed = 1;
    
    public float rotationSpeed = 1;
    public Rigidbody rigB;

    Vector3 InputVelocity;
    Quaternion InputRotation;

    bool isRoll = false;

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        rigB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("ShipX");
        float y = Input.GetAxis("ShipY");
        float z = Input.GetAxis("ShipZ");

        InputVelocity = (transform.forward * z + transform.up * y + transform.right * x) * speed;

        if (Input.GetKeyDown(KeyCode.R))
        {
            isRoll = true;
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            isRoll = false;
        }

        if (!isRoll)
        {
            float pitch = -Input.GetAxis("Mouse Y");
            float yaw = Input.GetAxis("Mouse X");

            InputRotation = Quaternion.Euler(new Vector3(pitch, yaw, 0) * rotationSpeed);
        } 
        else
        {
            float unPitch = Input.GetAxis("Mouse Y");
            float roll = -Input.GetAxis("Mouse X");

            InputRotation = Quaternion.Euler(new Vector3(unPitch, 0, roll) * rotationSpeed);
        }
    }

    private void FixedUpdate()
    { 
        Vector3 ShipDirection = rigB.velocity + InputVelocity;
        float ShipSpeed = Mathf.Clamp(ShipDirection.magnitude, 0, maxSpeed);

        rigB.velocity = ShipSpeed * ShipDirection.normalized;
        //print(ShipSpeed);
        //print(InputRotation);
        if (InputRotation.w != 0)
        {
            transform.rotation *= InputRotation;
        }
    }
}
