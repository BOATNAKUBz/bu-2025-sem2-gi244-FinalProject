using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    public Image fillBar;
    public PlayerHealth playerHealth;

    void Update()
    {
        fillBar.fillAmount =
            playerHealth.currentHealth /
            playerHealth.maxHealth;
    }
}