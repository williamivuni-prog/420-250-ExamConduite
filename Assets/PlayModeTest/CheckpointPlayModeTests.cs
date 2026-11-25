using System;
using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class CheckpointPlayModeTests
{
    private Scene raceScene;
    private GameObject gameManager;
    private GameObject testObject;
    private Component lapsManager;
    private FieldInfo lastCheckpoint;
    private GameObject firstRing;
    private GameObject secondRing;

    [UnitySetUp]
    public IEnumerator LoadRace()
    {
        const string scenePath = "Assets/Scenes/Game Scene.unity";

        yield return SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
        raceScene = SceneManager.GetSceneByPath(scenePath);
        Assert.IsTrue(raceScene.isLoaded, "La scène n'est pas chargée.");

        // Donner aux scripts de la scène le temps d'exécuter Start().
        yield return null;

        gameManager = GameObject.Find("Game Manager");
        GameObject managerObject = GameObject.Find("LapsManager");
        Assert.IsNotNull(managerObject, "Le gestionnaire de tours doit exister.");

        Transform firstLap = managerObject.transform.Find("1st lap");
        Assert.IsNotNull(firstLap, "Le premier tour doit exister.");
        Assert.IsTrue(firstLap.childCount >= 2,
            "Le premier tour doit contenir au moins deux anneaux.");

        firstRing = firstLap.GetChild(0).gameObject;
        secondRing = firstLap.GetChild(1).gameObject;

        Assert.IsTrue(firstRing.activeInHierarchy,
            "Le premier anneau doit être actif au départ.");
        Assert.IsFalse(secondRing.activeInHierarchy,
            "Le deuxième anneau doit être inactif au départ.");

        Type managerType = Type.GetType("LapsManager, Assembly-CSharp", true);
        lapsManager = managerObject.GetComponent(managerType);
        lastCheckpoint = managerType.GetField("LastCheckpoint");
    }

    [UnityTest]
    public IEnumerator UnexpectedRingDoesNotReplaceLastCheckpoint()
    {
        // Arrange
        GameObject previousCheckpoint = (GameObject)lastCheckpoint.GetValue(null);
        Assert.IsNotNull(previousCheckpoint,
            "Un point de réapparition doit déjà être mémorisé.");
        Assert.IsFalse(previousCheckpoint == firstRing,
            "Le point mémorisé et l'anneau attendu doivent être différents.");

        // Act : envoyer le deuxième anneau alors que le premier est attendu.
        lapsManager.GetType().GetMethod("RemoveLastCheckpoint")
            .Invoke(lapsManager, new object[] { secondRing });

        // Assert
        GameObject actualCheckpoint = (GameObject)lastCheckpoint.GetValue(null);
        Assert.AreEqual(previousCheckpoint, actualCheckpoint,
            "Un anneau inattendu ne doit pas changer le point de réapparition.");
        Assert.IsTrue(firstRing.activeInHierarchy,
            "L'anneau attendu doit rester actif.");
        Assert.IsFalse(secondRing.activeInHierarchy,
            "Le deuxième anneau doit rester inactif.");

        yield return null;
    }

    [UnityTest]
    public IEnumerator ExitingFirstRingActivatesSecondRing()
    {
        // Arrange
        Collider trigger = firstRing.GetComponent<Collider>();
        Assert.IsNotNull(trigger, "Le premier anneau doit avoir un Collider.");
        Assert.IsTrue(trigger.isTrigger,
            "Le Collider du premier anneau doit être configuré en Is Trigger.");

        Bounds bounds = trigger.bounds;
        testObject = new GameObject("Checkpoint test vehicle");
        testObject.transform.position = bounds.center;

        SphereCollider vehicleCollider = testObject.AddComponent<SphereCollider>();
        vehicleCollider.radius = 0.1f;

        Rigidbody body = testObject.AddComponent<Rigidbody>();
        body.isKinematic = true;

        // Act : laisser Unity détecter la sphère dans la zone
        yield return new WaitForFixedUpdate();

        Vector3 outsidePosition = new Vector3(
            bounds.max.x + 2f, bounds.center.y, bounds.center.z);

        body.MovePosition(outsidePosition);

        // Laisser Unity traiter la sortie de la zone
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        yield return null;

        // Assert
        Assert.IsFalse(firstRing.activeInHierarchy,
            "Le premier anneau doit disparaître après le passage.");
        Assert.IsTrue(secondRing.activeInHierarchy,
            "Le deuxième anneau doit apparaître après le passage.");
    }


    /*  Après chaque test, retire les objets créés et décharge la scène
     pour que le test suivant reparte du même état.*/
    [UnityTearDown]
    public IEnumerator CleanUp()
    {
        if (testObject != null)
            Object.Destroy(testObject);

        if (gameManager != null)
            Object.Destroy(gameManager);

        if (lastCheckpoint != null)
            lastCheckpoint.SetValue(null, null);

        if (raceScene.IsValid() && raceScene.isLoaded)
            yield return SceneManager.UnloadSceneAsync(raceScene);

        yield return null;
    }
}