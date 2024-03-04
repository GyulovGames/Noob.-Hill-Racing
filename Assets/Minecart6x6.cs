using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Minecart6x6 : MonoBehaviour
{
    [SerializeField] private float fuelDrainSpeed;
    [SerializeField] private float gasFuelDrainSpeed;
    [SerializeField] private float curentFuelAmount;
    [SerializeField] private float vehicleRotationSpeed;

    [Space(15)]
    [SerializeField] private Animator noobAnimator;
    [SerializeField] private Rigidbody2D vehicleRigidBody;
    [SerializeField] private Rigidbody2D frontWheel_RigidBody;
    [SerializeField] private Rigidbody2D rearWheel_RigidBody;
    [SerializeField] private Rigidbody2D rearWheel_RigidBody2;
    [SerializeField] private WheelJoint2D frontWheel_Joint;
    [SerializeField] private WheelJoint2D rearWheel_Joint;
    [SerializeField] private WheelJoint2D rearWheel_Joint2d;

    [SerializeField] private CircleCollider2D frontWheel_CircleCollider;
    [SerializeField] private CircleCollider2D rearWheel_CircleCollider;
    [SerializeField] private CircleCollider2D rearWheel_CircleCollider2;


    private bool isPause = false;

    public float enginePower;
    public float frontDrive;
    public float suspensionStab;
    public float maxFuelAmount;
    public float wheelsGrip;



    private void Start()
    {
        LoadSpecifications();
        SetCurrentAmount();
    }

    private void LoadSpecifications()
    {
        enginePower = YandexGame.savesData.minecart4x4Part[0];
        frontDrive = YandexGame.savesData.minecart4x4Part[2];
        maxFuelAmount = YandexGame.savesData.minecart4x4Part[4];

        JointSuspension2D jointSuspension = frontWheel_Joint.suspension;
        jointSuspension.dampingRatio = YandexGame.savesData.minecart4x4Part[1];
        frontWheel_Joint.suspension = jointSuspension;
        rearWheel_Joint.suspension = jointSuspension;

        frontWheel_CircleCollider.sharedMaterial.friction = YandexGame.savesData.minecart4x4Part[3];
        rearWheel_CircleCollider.sharedMaterial.friction = YandexGame.savesData.minecart4x4Part[3];
        rearWheel_CircleCollider2.sharedMaterial.friction = YandexGame.savesData.minecart4x4Part[3];

    }

    private void SetCurrentAmount()
    {
        curentFuelAmount = maxFuelAmount;
        GameCanvas.InstanceGC.UpdateFuelBarOnStart(maxFuelAmount);
    }

    private void FixedUpdate()
    {
        if (!isPause)
        {
            float horizontal = GameCanvas.InstanceGC.horizontalInput;
            NoobAnimations(horizontal);
            UpdateUIFuelBar(horizontal);

            rearWheel_RigidBody.AddTorque(-horizontal * enginePower * Time.fixedDeltaTime);
            rearWheel_RigidBody2.AddTorque(-horizontal * enginePower * Time.fixedDeltaTime);

            frontWheel_RigidBody.AddTorque(-horizontal * frontDrive * Time.fixedDeltaTime);


            vehicleRigidBody.AddTorque(horizontal * vehicleRotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void UpdateUIFuelBar(float value)
    {
        if (value > 0 || value < 0)
        {
            curentFuelAmount -= Time.deltaTime * gasFuelDrainSpeed;
        }
        else
        {
            curentFuelAmount -= Time.deltaTime * fuelDrainSpeed;
        }

        if (curentFuelAmount <= 0)
        {
            isPause = true;
            StartCoroutine(GameCanvas.InstanceGC.OpenResultWindow("Fuel"));
        }

        GameCanvas.InstanceGC.UpdateFuelBar(curentFuelAmount);

    }

    private void NoobAnimations(float value)
    {
        switch (value)
        {
            case 1:
                noobAnimator.SetBool("DrivingForward", true);
                break;
            case -1:
                noobAnimator.SetBool("DrivingBackward", true);
                break;
            default:
                noobAnimator.SetBool("DrivingForward", false);
                noobAnimator.SetBool("DrivingBackward", false);
                break;
        }
    }

    public void HitNoobHead()
    {
        if (!isPause)
        {
            isPause = true;
            noobAnimator.SetBool("Dead", true);
            StartCoroutine(GameCanvas.InstanceGC.OpenResultWindow("Crash"));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Canister")
        {
            print("Full!");
            curentFuelAmount = maxFuelAmount;
            GameCanvas.InstanceGC.UpdateFuelBar(curentFuelAmount);

        }
        if (collision.gameObject.tag == "Coin")
        {
            GameCanvas.InstanceGC.CoinsCollect();
        }
    }
}
