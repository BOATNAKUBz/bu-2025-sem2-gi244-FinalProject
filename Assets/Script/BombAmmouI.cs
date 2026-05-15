using UnityEngine;
using TMPro;

public class BombAmmoUI : MonoBehaviour
{
    public PlayerShooting playerShooting;

    public TMP_Text bombAmmoText;

    void Update()
    {
        bombAmmoText.text =
            playerShooting.currentBombAmmo.ToString();
    }
}