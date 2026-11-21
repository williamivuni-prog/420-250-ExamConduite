
using UnityEngine;

public class BoostEnergyRegle
{
    public float CalculerBoost(
        float currentBoost,
        bool isBoosting,
        bool isBoostPressed,
        float boostUseRate,
        float boostRechargeRate,
        float maxBoost,
        float deltaTime)
    {
        if (isBoosting)
        {
            currentBoost -= boostUseRate * deltaTime;
        }
        else if (!isBoostPressed)
        {
            currentBoost += boostRechargeRate * deltaTime;
        }

        return Mathf.Clamp(currentBoost, 0f, maxBoost);
    }
}
