using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarBoost : MonoBehaviour
{
    public ParticleSystem boostParticle;
    public AudioClip boostSound;

    [SerializeField] private KeyCode boostKey = KeyCode.LeftShift;
    [SerializeField] private float boostForce = 20f;
    [SerializeField] private float maxBoost = 100f;
    [SerializeField] private float boostUseRate = 30f;
    [SerializeField] private float boostRechargeRate = 15f;

    private Rigidbody carRigidbody;
    private CarController carController;
    private AudioSource boostAudio;
    private float currentBoost;
    private BoostEnergyRegle boostEnergyRegle;
    private bool isBoostPressed;
    private bool isAccelerating;
    private bool isBoosting;
    public static float BoostAmount { get; private set; } = 100f;

    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carController = GetComponent<CarController>();
        boostAudio = gameObject.AddComponent<AudioSource>();
        boostAudio.clip = boostSound;
        boostAudio.loop = true;
        boostAudio.playOnAwake = false;
        currentBoost = maxBoost;
        boostEnergyRegle = new BoostEnergyRegle();

        if (boostParticle != null)
        {
            boostParticle.Stop();
        }
    }

    private void FixedUpdate()
    {
        bool isSelectedVehicle = GameManager.Instance.IsSelectedVehicle(carController.carType);

        if (!isSelectedVehicle)
        {
            isBoosting = false;
            PlayerCamera.SetBoosting(false);
            StopBoostParticle();
            StopBoostSound();
            return;
        }

        isBoosting = isBoosting && currentBoost > 0f &&
            !GameManager.IsGameOver && GameManager.IsGameStarted;

        currentBoost = boostEnergyRegle.CalculerBoost(
            currentBoost,
            isBoosting,
            isBoostPressed,
            boostUseRate,
            boostRechargeRate,
            maxBoost,
            Time.fixedDeltaTime
        );

        BoostAmount = currentBoost;


        PlayerCamera.SetBoosting(isBoosting);

        GererEffetsBoost(isBoosting);

        if (isBoosting)
        {
            AppliquerBoost();
        }
    }


    private void GererEffetsBoost(bool isBoosting)
    {
        if (boostParticle != null)
        {
            if (isBoosting && boostParticle.isStopped)
            {
                boostParticle.Play();
            }
            else if (!isBoosting && boostParticle.isPlaying)
            {
                boostParticle.Stop();
            }
        }

        if (boostSound != null)
        {
            if (isBoosting && !boostAudio.isPlaying)
            {
                boostAudio.Play();
            }
            else if (!isBoosting && boostAudio.isPlaying)
            {
                boostAudio.Stop();
            }
        }
    }

    private void AppliquerBoost()
    {
        carRigidbody.AddForce(
            transform.forward * boostForce,
            ForceMode.Acceleration
        );
    }

    private void StopBoostParticle()
    {
        if (boostParticle != null && boostParticle.isPlaying)
        {
            boostParticle.Stop();
        }
    }

    private void StopBoostSound()
    {
        if (boostAudio.isPlaying)
        {
            boostAudio.Stop();
        }
    }

    private void Update()
    {
        isBoostPressed = Input.GetKey(boostKey);
        isAccelerating = Input.GetAxis("Vertical") > 0f;
        if (isBoostPressed && isAccelerating)
        {
            ActiverBoost();
        }
        else
        {
            isBoosting = false;
        }
    }

    public void ActiverBoost()
    {
        isBoosting = true;
    }
}

