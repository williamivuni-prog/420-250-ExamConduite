using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class BoostEnergyRegleTests
{
    // William Test
    [Test]
    public void Boosting_DecreasesEnergy()
    {
        // Arrange
        BoostEnergyRegle regle = new BoostEnergyRegle();

        // Act
        float resultat = regle.CalculerBoost(
            100f, true, true, 30f, 15f, 100f, 1f
        );

        // Assert
        Assert.AreEqual(70f, resultat);
    }
}
