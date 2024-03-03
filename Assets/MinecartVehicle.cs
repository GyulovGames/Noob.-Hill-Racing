using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class MinecartVehicle : MonoBehaviour
{
    [SerializeField] private float fuelDrainSpeed;
    [SerializeField] private float gasFuelDrainSpeed;
    [SerializeField] private float curentFuelAmount;
    [SerializeField] private float vehicleRotationSpeed;

    [Space(15)]
    [SerializeField] private Animator noobAnimator;
    [SerializeField] private Rigidbody2D vehicleRigidBody;
    [SerializeField] private Rigidbody2D frontWheelRigidBody;
    [SerializeField] private Rigidbody2D rearWheelRigidBody;
    [SerializeField] private WheelJoint2D frontWheel;
    [SerializeField] private WheelJoint2D rearWheel;
    [SerializeField] private CircleCollider2D frontCircleCollider;
    [SerializeField] private CircleCollider2D rearCircleCollider;
 
    private bool isPause = false;

    private float enginePower;
    private float frontDrive;
    private float suspensionStab;
    private float maxFuelAmount;
    private float wheelsGrip;



    private void Start()
    {
        LoadSpecifications();
        SetCurrentAmount();
    }

    private void LoadSpecifications()
    {
        enginePower = YandexGame.savesData.minecartEnginePower;
        frontDrive = YandexGame.savesData.minecartFrontDirve;
        suspensionStab = YandexGame.savesData.minecartSuspensionStab;
        maxFuelAmount = YandexGame.savesData.minecartMaxFuelAmount;
        wheelsGrip = YandexGame.savesData.minecartWheelsGrip;

        JointSuspension2D ration = frontWheel.suspension; //!!!!!!!!!
        ration.dampingRatio = YandexGame.savesData.minecartSuspensionStab;
        frontWheel.suspension = ration;
        rearWheel.suspension = ration;

        frontCircleCollider.sharedMaterial.friction = YandexGame.savesData.minecartWheelsGrip;
        rearCircleCollider.sharedMaterial.friction = YandexGame.savesData.minecartWheelsGrip;
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

            rearWheelRigidBody.AddTorque(-horizontal * enginePower * Time.fixedDeltaTime);
            frontWheelRigidBody.AddTorque(-horizontal * frontDrive * Time.fixedDeltaTime);
            vehicleRigidBody.AddTorque(horizontal * vehicleRotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void UpdateUIFuelBar(float value)
    {
        if(value > 0 || value < 0)
        {
            curentFuelAmount -= Time.deltaTime * gasFuelDrainSpeed;
        }
        else
        {
            curentFuelAmount -= Time.deltaTime * fuelDrainSpeed;
        }

        if(curentFuelAmount <= 0)
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
        if(collision.gameObject.tag == "Coin")
        {
            GameCanvas.InstanceGC.CoinsCollect();
        }
    }
}