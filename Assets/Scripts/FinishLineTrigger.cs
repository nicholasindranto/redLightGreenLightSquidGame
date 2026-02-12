using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    public GameObject youWinText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // menang si playernya
        {
            other.GetComponent<PlayerController>().hasBeenWon = true;
            youWinText.SetActive(true);
        }
    }
}
