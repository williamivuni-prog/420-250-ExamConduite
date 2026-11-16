using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarBoost : MonoBehaviour
{
    [SerializeField] private KeyCode boostKey = KeyCode.LeftShift;
    [SerializeField] private float boostForce = 20f;

    private Rigidbody carRigidbody;
    private CarController carController;

    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
        carController = GetComponent<CarController>();
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
            !GameManager.IsGameOver && GameManager.IsGameStarted;

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
