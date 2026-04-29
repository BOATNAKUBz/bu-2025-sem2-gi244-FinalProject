using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public PlayerShooting playerShooting;

    void Update()
    {
        ammoText.text =
        playerShooting.currentAmmo + " / " +
        playerShooting.maxAmmo;
    }
}