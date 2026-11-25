using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class BoostCameraPlayModeTests
{
    // William Test
    [UnityTest]
    public IEnumerator BoostCameraEntersBoostState()
    {
        yield return SceneManager.LoadSceneAsync("Game Scene", LoadSceneMode.Single);

        GameObject playerCamera = GameObject.Find("Player Cam");
        Assert.IsNotNull(playerCamera, "La caméra du joueur doit être présente dans la scène.");

        Animator animator = playerCamera.GetComponent<Animator>();
        Assert.IsNotNull(animator, "La caméra du joueur doit avoir un Animator.");

        CarBoost[] carBoosts = Object.FindObjectsOfType<CarBoost>();
        foreach (CarBoost carBoost in carBoosts)
        {
            carBoost.enabled = false;
        }

        PlayerCamera.SetBoosting(true);
        yield return new WaitForSeconds(0.3f);

        Assert.IsTrue(
            animator.GetCurrentAnimatorStateInfo(0).IsName("Boost"),
            "L'animation de boost doit être active après l'activation du boost."
        );

        PlayerCamera.SetBoosting(false);
    }
}
