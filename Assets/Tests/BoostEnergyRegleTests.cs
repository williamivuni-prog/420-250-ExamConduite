using NUnit.Framework;

public class BoostEnergyRegleTests
{
    // William Test
    [Test]
    public void BoostingDecreasesEnergy()
    {
        // Arrange
        BoostEnergyRegle regle = new BoostEnergyRegle();

        // Act
        float energieApresBoost = regle.CalculerBoost(
            100f, true, true, 30f, 15f, 100f, 1f
        );

        // Assert
        Assert.AreEqual(70f, energieApresBoost, 0.001f);
    }

    // William Test
    [Test]
    public void EnergyRechargesWhenBoostIsNotPressed()
    {
        // Arrange
        BoostEnergyRegle regle = new BoostEnergyRegle();

        // Act
        float energieApresRecharge = regle.CalculerBoost(
            50f, false, false, 30f, 15f, 100f, 2f
        );

        // Assert
        Assert.AreEqual(80f, energieApresRecharge, 0.001f);
    }

    // William Test
    [Test]
    public void EnergyDoesNotRechargeWhileBoostKeyIsPressed()
    {
        // Arrange
        BoostEnergyRegle regle = new BoostEnergyRegle();

        // Act
        float energieAvecToucheMaintenue = regle.CalculerBoost(
            50f, false, true, 30f, 15f, 100f, 2f
        );

        // Assert
        Assert.AreEqual(50f, energieAvecToucheMaintenue, 0.001f);
    }

    // William Test
    [Test]
    public void EnergyCannotGoBelowZero()
    {
        // Arrange
        BoostEnergyRegle regle = new BoostEnergyRegle();

        // Act
        float energieSousZero = regle.CalculerBoost(
            10f, true, true, 30f, 15f, 100f, 1f
        );

        // Assert
        Assert.AreEqual(0f, energieSousZero, 0.001f);
    }

    // William Test
    [Test]
    public void EnergyCannotExceedMaximum()
    {
        // Arrange
        BoostEnergyRegle regle = new BoostEnergyRegle();

        // Act
        float energieAuDessusMaximum = regle.CalculerBoost(
            55f, false, false, 30f, 15f, 60f, 1f
        );

        // Assert
        Assert.AreEqual(60f, energieAuDessusMaximum, 0.001f);
    }
}
