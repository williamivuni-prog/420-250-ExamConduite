using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarBoost : MonoBehaviour
{
    [SerializeField] private KeyCode boostKey = KeyCode.LeftShift;
    [SerializeField] private float boostForce = 20f;

    private Rigidbody carRigidbody;

    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        bool isBoostPressed = Input.GetKey(boostKey);
        bool isAccelerating = Input.GetAxis("Vertical") > 0f;

        if (isBoostPressed && isAccelerating)
        {
            carRigidbody.AddForce(
                transform.forward * boostForce,
                ForceMode.Acceleration
            );
        }
    }
}
