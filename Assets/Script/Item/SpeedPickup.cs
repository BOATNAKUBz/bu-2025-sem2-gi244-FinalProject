using UnityEngine;
using System.Collections;

public class SpeedPickup : MonoBehaviour
{
    public float speedBoostAmount = 3f;
    public float duration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerController player =
            other.GetComponent<PlayerController>();

        if (player != null)
        {
            StartCoroutine(
                SpeedBoost(player)
            );
        }

        // ซ่อนก่อน
        GetComponent<Collider>().enabled = false;

        MeshRenderer mr =
            GetComponent<MeshRenderer>();

        if (mr != null)
        {
            mr.enabled = false;
        }
    }

    IEnumerator SpeedBoost(PlayerController player)
    {
        Debug.Log("⚡ Speed Boost");

        player.moveSpeed += speedBoostAmount;

        yield return new WaitForSeconds(duration);

        player.moveSpeed -= speedBoostAmount;

        Destroy(gameObject);
    }
}