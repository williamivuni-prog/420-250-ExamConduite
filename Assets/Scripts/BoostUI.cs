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
        boostBar.fillAmount = CarBoost.BoostAmount / 100f;
    }
}
