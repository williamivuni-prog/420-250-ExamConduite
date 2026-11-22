using UnityEngine;
using UnityEngine.UI;

public class BoostUI : MonoBehaviour
{
    public Image boostBar;

    private void Start()
    {
        boostBar.fillAmount = 1f;
    }

    private void Update()
    {
        MettreAJourBarre(CarBoost.BoostAmount / 100f);
    }

    public void MettreAJourBarre(float valeurNormalisee)
    {
        boostBar.fillAmount = valeurNormalisee;
    }
}
