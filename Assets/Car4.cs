using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Car4 : MonoBehaviour
{
    [SerializeField] private float fuelConsuptionRate;
    [SerializeField] private float gasFuelConsuptionRate;
    [SerializeField] private float vehicleRotationSpeed;
    [Space(5)]

    [Header("Upgrade Multipliers")]
    [SerializeField] private float engineForce;
    [SerializeField] private float carDampinRatio;
    [SerializeField] private float frontWheelsForce;
    [SerializeField] private float wheelsGrip;                    // Множимые не должны использоваться. Переделать.
    [SerializeField] private float fuelAmount;
    [Space(5)]

    [SerializeField] private Rigidbody2D carRigidBody;
    [SerializeField] private Rigidbody2D frontWheel_RigidBody;
    [SerializeField] private Rigidbody2D rearWheel_RigidBody;
    [SerializeField] private WheelJoint2D frontWheel_Joint;
    [SerializeField] private WheelJoint2D rearWheel_Joint;
    [SerializeField] private CircleCollider2D frontWheel_CircleCollider;
    [SerializeField] private CircleCollider2D rearWheel_CircleCollider;
    [SerializeField] private PhysicsMaterial2D wheelsPhysicsMaterial;
    [Space(5)]

    [SerializeField] private WheelGrounded frontWheelGround;
    [SerializeField] private WheelGrounded rearWheelGround;
    [SerializeField] private ParticleSystem frontWheel_Particles;
    [SerializeField] private ParticleSystem rearWheel_Particles;
    [SerializeField] private AudioSource engineSoundPlayer;
    [SerializeField] private Animator noobAnimator;

    public float curentFuelAmount;
    private float horizontal;
    private bool stop = false;


    private void Awake()
    {
        DownloadUpgrades();

        GameCanvas.PauseEvent.AddListener(PausePlay);
    }

    private void Start()
    {
        UpdateFuelBarOnStart();
        EngineSound();
    }

    private void EngineSound()
    {
        bool sounds = YandexGame.savesData.Sounds_sdk;

        if (sounds)
        {
            engineSoundPlayer.Play();
        }
    }
    private void PausePlay()
    {
        if (stop)
        {
            stop = false;
            engineSoundPlayer.Play();
            carRigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            stop = true;
            engineSoundPlayer.Stop();
            carRigidBody.bodyType = RigidbodyType2D.Static;
        }
    }
    private void UserInput()
    {
        horizontal = GameCanvas.Instance.horizontalInput;
    }
    public void HitNoobHead()
    {
        if (!stop)
        {
            stop = true;
            noobAnimator.SetBool("Dead", true);
            StartCoroutine(GameCanvas.Instance.OpenResultWindow("Crash"));
        }
    }
    private void CarMovement()
    {
        rearWheel_RigidBody.AddTorque(-horizontal * engineForce * Time.fixedDeltaTime);
        frontWheel_RigidBody.AddTorque(-horizontal * frontWheelsForce * Time.fixedDeltaTime);
    }
    private void UpdateFuelBar()
    {
        if (horizontal == 0)
        {
            curentFuelAmount -= Time.deltaTime * fuelConsuptionRate;
        }
        else
        {
            curentFuelAmount -= Time.deltaTime * gasFuelConsuptionRate;
        }

        GameCanvas.Instance.UpdateFuelbar(curentFuelAmount);

        if (curentFuelAmount <= 0)
        {
            stop = true;
            StartCoroutine(GameCanvas.Instance.OpenResultWindow("FuelOut"));
            engineSoundPlayer.Stop();

        }
    }
    private void NoobAnimations()
    {
        switch (horizontal)
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
    private void RotationControl()
    {
        if (!frontWheelGround.wheelGrounded && rearWheelGround.wheelGrounded)
        {
            carRigidBody.AddTorque(horizontal * vehicleRotationSpeed * Time.fixedDeltaTime);
        }
    }
    private void DownloadUpgrades()
    {
        if (YandexGame.savesData.Car4_Upgrades[0] == 1)
        {
            engineForce = 160f;
        }
        else
        {
            engineForce = 160 + 11 * YandexGame.savesData.Car4_Upgrades[0];
        }

        if (YandexGame.savesData.Car4_Upgrades[2] == 1)
        {
            frontWheelsForce = 27f;
        }
        else
        {
            frontWheelsForce = 27f * YandexGame.savesData.Car4_Upgrades[2];
        }

        JointSuspension2D suspension = frontWheel_Joint.suspension;
        suspension.dampingRatio = carDampinRatio *= YandexGame.savesData.Car4_Upgrades[1];
        frontWheel_Joint.suspension = suspension;
        rearWheel_Joint.suspension = suspension;

        wheelsPhysicsMaterial.friction = wheelsGrip *= YandexGame.savesData.Car4_Upgrades[3];
        frontWheel_CircleCollider.sharedMaterial = wheelsPhysicsMaterial;
        rearWheel_CircleCollider.sharedMaterial = wheelsPhysicsMaterial;

        if (YandexGame.savesData.Car4_Upgrades[4] == 1)
        {
            curentFuelAmount = fuelAmount;
        }
        else
        {
            curentFuelAmount = fuelAmount + 10 * YandexGame.savesData.Car4_Upgrades[4];
        }
    }
    private void EngineSoundControl()
    {
        float carCurrentPitch = horizontal + rearWheel_RigidBody.velocity.x * 10f * Time.deltaTime;

        if (carCurrentPitch > 1)
        {
            engineSoundPlayer.pitch = carCurrentPitch;
        }
        else if (carCurrentPitch < -1)
        {
            engineSoundPlayer.pitch = carCurrentPitch;
        }
        else
        {
            engineSoundPlayer.pitch = 1f;
        }
    }
    private void UpdateFuelBarOnStart()
    {
        GameCanvas.Instance.UpdateFuelBarOnStart(curentFuelAmount);
    }

    private void Update()
    {
        if (!stop)
        {
            if (horizontal != 0 && frontWheelGround.wheelGrounded)
            {
                if (!frontWheel_Particles.isPlaying)
                {
                    frontWheel_Particles.Play();
                }
            }
            else
            {
                if (frontWheel_Particles.isPlaying)
                {
                    frontWheel_Particles.Stop();
                }
            }

            if (horizontal != 0 && rearWheelGround.wheelGrounded)
            {
                if (!rearWheel_Particles.isPlaying)
                {
                    rearWheel_Particles.Play();
                }
            }
            else
            {
                if (rearWheel_Particles.isPlaying)
                {
                    rearWheel_Particles.Stop();
                }
            }
        }
        else
        {
            frontWheel_Particles.Stop();
            rearWheel_Particles.Stop();
        }
    }
    private void FixedUpdate()
    {
        if (!stop)
        {
            UserInput();
            CarMovement();
            UpdateFuelBar();
            NoobAnimations();
            RotationControl();
            EngineSoundControl();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            GameCanvas.Instance.CoinCounter();
        }
        else if (collision.gameObject.tag == "Canister")
        {
            curentFuelAmount = fuelAmount * YandexGame.savesData.Car4_Upgrades[4];
            GameCanvas.Instance.UpdateFuelBarOnStart(curentFuelAmount);
        }
    }
}
