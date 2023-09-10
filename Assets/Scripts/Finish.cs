using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    // TODO singleton

    [SerializeField] GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("In a finish");
            gameManager.SetCurrentState(GameManager.State.SPECTATING);
        }
    }
}
