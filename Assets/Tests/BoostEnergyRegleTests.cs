using NUnit.Framework;

public class BoostEnergyRegleTests
{
    // William Test
    [Test]
    public void TestBoostEnergyRules()
    {
        // Arrange
        BoostEnergyRegle regle = new BoostEnergyRegle();

        // Act
        float energieApresBoost = regle.CalculerBoost(
            100f, true, true, 30f, 15f, 100f, 1f
        );

        // Assert
        Assert.AreEqual(70f, energieApresBoost, 0.001f);

        // Act
        float energieApresRecharge = regle.CalculerBoost(
            50f, false, false, 30f, 15f, 100f, 2f
        );

        // Assert
        Assert.AreEqual(80f, energieApresRecharge, 0.001f);

        // Act
        float energieAvecToucheMaintenue = regle.CalculerBoost(
            50f, false, true, 30f, 15f, 100f, 2f
        );

        // Assert
        Assert.AreEqual(50f, energieAvecToucheMaintenue, 0.001f);

        // Act
        float energieSousZero = regle.CalculerBoost(
            10f, true, true, 30f, 15f, 100f, 1f
        );

        // Assert
        Assert.AreEqual(0f, energieSousZero, 0.001f);

        // Act
        float energieAuDessusMaximum = regle.CalculerBoost(
            95f, false, false, 30f, 15f, 100f, 1f
        );

        // Assert
        Assert.AreEqual(100f, energieAuDessusMaximum, 0.001f);
    }
}
