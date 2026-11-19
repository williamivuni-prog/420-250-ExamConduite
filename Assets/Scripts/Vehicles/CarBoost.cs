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
            StopBoostParticle();
            StopBoostSound();
            return;
        }

        bool isBoostPressed = Input.GetKey(boostKey);
        bool isAccelerating = Input.GetAxis("Vertical") > 0f;
        bool isBoosting = isBoostPressed && isAccelerating &&
            currentBoost > 0f && !GameManager.IsGameOver && GameManager.IsGameStarted;

        if (isBoosting)
        {
            currentBoost -= boostUseRate * Time.fixedDeltaTime;
        }
        else if (!isBoostPressed)
        {
            currentBoost += boostRechargeRate * Time.fixedDeltaTime;
        }

        currentBoost = Mathf.Clamp(currentBoost, 0f, maxBoost);
        BoostAmount = currentBoost;

        PlayerCamera.SetBoosting(isBoosting);

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

        if (isBoosting)
        {
            carRigidbody.AddForce(
                transform.forward * boostForce,
                ForceMode.Acceleration
            );
        }
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
}
