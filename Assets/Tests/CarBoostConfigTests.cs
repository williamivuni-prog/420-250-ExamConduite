using NUnit.Framework;
using UnityEditor;
using UnityEngine;

public class CarBoostConfigTests
{
    // William Test
    [Test]
    public void TestConvertibleBoostConfig()
    {
        // Première séquence AAA : trouver le prefab
        const string prefabPath = "Assets/Prefabs/Convertible.prefab";

        // Act
        GameObject convertible =
            AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

        // Assert
        Assert.IsNotNull(
            convertible,
            $"Aucun prefab n'a été trouvé à l'emplacement {prefabPath}."
        );

        // Deuxième séquence AAA : vérifier le composant CarBoost
        // Act
        CarBoost carBoost = convertible.GetComponent<CarBoost>();

        // Assert
        Assert.IsNotNull(
            carBoost,
            "CarBoost doit être ajouté au prefab Convertible."
        );
        Assert.IsTrue(
            carBoost.enabled,
            "Le composant CarBoost doit être activé."
        );

        // Troisième séquence AAA : vérifier le composant requis
        // Act
        Rigidbody carRigidbody = convertible.GetComponent<Rigidbody>();

        // Assert
        Assert.IsNotNull(
            carRigidbody,
            "Rigidbody doit être ajouté au prefab Convertible pour appliquer le boost."
        );

        // Quatrième séquence AAA : vérifier les références d'effets
        // Assert
        Assert.IsNotNull(
            carBoost.boostParticle,
            "Le système de particules du boost doit être assigné."
        );
        Assert.IsNotNull(
            carBoost.boostSound,
            "Le son du boost doit être assigné."
        );
    }
}
