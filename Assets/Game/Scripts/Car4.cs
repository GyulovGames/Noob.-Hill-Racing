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
    [Header("Default Characteristics")]
    [SerializeField] private float engineForce;
    [SerializeField] private float carDampinRatio;
    [SerializeField] private float frontWheelsForce;
    [SerializeField] private float wheelsGrip;
    [SerializeField] private float fuelAmount;
    [Space(5)]
    [SerializeField] private Rigidbody2D carRigidBody;
    [Space(5)]
    [SerializeField] private Rigidbody2D wheel_1Rigid;
    [SerializeField] private Rigidbody2D wheel_2Rigid;
    [Space(5)]
    [SerializeField] private WheelJoint2D wheelJoint1;
    [SerializeField] private WheelJoint2D wheelJoint2;
    [Space(5)]
    [SerializeField] private CircleCollider2D circleCollider1;
    [SerializeField] private CircleCollider2D circleCollider2;
    [Space(5)]
    [SerializeField] private WheelGrounded wheel1_Grounded;
    [SerializeField] private WheelGrounded wheel2_Grounded;
    [Space(5)]
    [SerializeField] private ParticleSystem wheel1_Particles;
    [SerializeField] private ParticleSystem wheel2_Particles;
    [Space(5)]
    [SerializeField] private PhysicsMaterial2D physicsMaterial;
    [SerializeField] private AudioSource engineSoundPlayer;
    [SerializeField] private Animator noobAnimator;

    private float originalFuelAmount;
    private float horizontal;
    private bool stopGame;


    private void Awake()
    {
        DownloadUpgrades();

        GameCanvas.PauseEvent.AddListener(PausePlay);
    }

    private void Start()
    {
        UpdateFuelBarOnStart();
    }

    private void PausePlay()
    {
        if (stopGame)
        {
            stopGame = false;
            engineSoundPlayer.Play();
            carRigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            stopGame = true;
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
        if (!stopGame)
        {
            stopGame = true;
            noobAnimator.SetBool("Dead", true);
            StartCoroutine(GameCanvas.Instance.OpenResultWindow("Crash"));
        }
    }
    private void CarMovement()
    {
        wheel_1Rigid.AddTorque(-horizontal * frontWheelsForce * Time.fixedDeltaTime);

        wheel_2Rigid.AddTorque(-horizontal * engineForce * Time.fixedDeltaTime);
    }
    private void UpdateFuelBar()
    {
        if (horizontal == 0)
        {
            fuelAmount -= Time.deltaTime * fuelConsuptionRate;
        }
        else
        {
            fuelAmount -= Time.deltaTime * gasFuelConsuptionRate;
        }

        GameCanvas.Instance.UpdateFuelbar(fuelAmount);

        if (fuelAmount <= 0)
        {
            stopGame = true;
            StartCoroutine(GameCanvas.Instance.OpenResultWindow("FuelOut"));
            engineSoundPlayer.Stop(); ;
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
        if (!wheel1_Grounded.wheelGrounded && !wheel2_Grounded.wheelGrounded)
        {
            carRigidBody.AddTorque(horizontal * vehicleRotationSpeed * Time.fixedDeltaTime);
        }
    }
    private void DownloadUpgrades()
    {
        if (YandexGame.savesData.Car4_Upgrades[0] > 1)
        {
            engineForce += 10 * YandexGame.savesData.Car4_Upgrades[0];
        }

        frontWheelsForce *= YandexGame.savesData.Car4_Upgrades[2];


        JointSuspension2D suspension = wheelJoint1.suspension;
        suspension.dampingRatio = carDampinRatio *= YandexGame.savesData.Car4_Upgrades[1];
        wheelJoint1.suspension = suspension;
        wheelJoint2.suspension = suspension;

        physicsMaterial.friction = wheelsGrip *= YandexGame.savesData.Car4_Upgrades[3];
        circleCollider1.sharedMaterial = physicsMaterial;
        circleCollider2.sharedMaterial = physicsMaterial;

        if (YandexGame.savesData.Car4_Upgrades[4] > 1)
        {
            fuelAmount += 5 * YandexGame.savesData.Car4_Upgrades[4];
        }

    }
    private void EngineSoundControl()
    {
        float carCurrentPitch = horizontal + carRigidBody.velocity.x * 10f * Time.deltaTime;

        if (carCurrentPitch > 1)
        {
            engineSoundPlayer.pitch = carCurrentPitch;
        }
        else if (carCurrentPitch < -1)
        {
            float newPich = Mathf.Abs(carCurrentPitch);
            engineSoundPlayer.pitch = newPich;
        }
        else
        {
            engineSoundPlayer.pitch = 1f;
        }
    }
    private void UpdateFuelBarOnStart()
    {
        originalFuelAmount = fuelAmount;
        GameCanvas.Instance.UpdateFuelBarOnStart(fuelAmount);
    }


    private void Update()
    {
        if (!stopGame)
        {
            if (horizontal != 0 && wheel1_Grounded.wheelGrounded)
            {
                if (!wheel1_Particles.isPlaying)
                {
                    wheel1_Particles.Play();
                }
            }
            else
            {
                if (wheel1_Particles.isPlaying)
                {
                    wheel1_Particles.Stop();
                }
            }

            if (horizontal != 0 && wheel2_Grounded.wheelGrounded)
            {
                if (!wheel2_Particles.isPlaying)
                {
                    wheel2_Particles.Play();
                }
            }
            else
            {
                if (wheel2_Particles.isPlaying)
                {
                    wheel2_Particles.Stop();
                }
            }
        }
        else
        {
            wheel1_Particles.Stop();
            wheel2_Particles.Stop();
        }
    }
    private void FixedUpdate()
    {
        if (!stopGame)
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
            fuelAmount = originalFuelAmount;
            GameCanvas.Instance.UpdateFuelBarOnStart(fuelAmount);
        }
    }
}