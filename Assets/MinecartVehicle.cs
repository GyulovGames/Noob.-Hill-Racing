using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecartVehicle : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private float vehicleRotationSpeed;
    [SerializeField] private Animator noobAnimator;
    [SerializeField] private Rigidbody2D vehicleRigidBody;
    [SerializeField] private Rigidbody2D frontDrive;
    [SerializeField] protected Rigidbody2D rearDrive;




    private void FixedUpdate()
    {
        float horizontalIput = Input.GetAxisRaw("Horizontal");

        rearDrive.AddTorque(-horizontalIput * movementSpeed * Time.fixedDeltaTime);
       //  frontDrive.AddTorque(-horizontalIput * movementSpeed * Time.fixedDeltaTime);


        vehicleRigidBody.AddTorque(horizontalIput * movementSpeed * Time.fixedDeltaTime);


        switch (horizontalIput)
        {
            case 0:
                noobAnimator.SetBool("DrivingForward", false);
                noobAnimator.SetBool("DrivingBackward", false);
                break;
                case 1:
                noobAnimator.SetBool("DrivingForward", true);
                break;
                case -1:
                noobAnimator.SetBool("DrivingBackward", true);
                break;
        }
    }
}