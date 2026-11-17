using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarBoost : MonoBehaviour
{
    [SerializeField] private KeyCode boostKey = KeyCode.LeftShift;
    [SerializeField] private float boostForce = 20f;
    [SerializeField] private float maxBoost = 100f;
    [SerializeField] private float boostUseRate = 30f;
    [SerializeField] private float boostRechargeRate = 15f;

    private Rigidbody carRigidbody;
    private CarController carController;
    private float currentBoost;

    public static float BoostAmount { get; private set; } = 100f;

    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carController = GetComponent<CarController>();
        currentBoost = maxBoost;
    }

    private void FixedUpdate()
    {
        bool isSelectedVehicle = GameManager.Instance.IsSelectedVehicle(carController.carType);

        if (!isSelectedVehicle)
        {
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

        if (isBoosting)
        {
            carRigidbody.AddForce(
                transform.forward * boostForce,
                ForceMode.Acceleration
            );
        }
    }
}
