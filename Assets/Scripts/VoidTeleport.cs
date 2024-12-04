using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidTeleport : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerRG;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerRG.SetActive(false);
            player.position = destination.position; 
            playerRG.SetActive(true);
        }
    }
}
