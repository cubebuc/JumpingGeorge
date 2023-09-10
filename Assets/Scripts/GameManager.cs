using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Keybinds")]
    [SerializeField] KeyCode placingKeyCode = KeyCode.G;
    [SerializeField] KeyCode playingKeyCode = KeyCode.O;


    [Header("Objects")]
    [SerializeField] GameObject player;

    public enum State
    {
        WAITING,
        SELECTION,
        PLACING,
        PLAYING,
        SPECTATING
    }
    
    public static State currentState;

    public void SetCurrentState(State newState)
    {

        switch (newState)
        {
            case State.WAITING:
                Debug.Log("Waiting");
                break;
            case State.SELECTION:
                Debug.Log("Selecting");
                break;
            case State.PLACING:
                Debug.Log("Placing");
                player.GetComponent<PlayerController>().SetGhostMode(true);
                break;
            case State.PLAYING:
                Debug.Log("Playing");
                player.GetComponent<PlayerController>().SetGhostMode(false);
                break;
            case State.SPECTATING:
                Debug.Log("Spectating");
                player.GetComponent<PlayerController>().SetGhostMode(true);
                break;
            default:
                break;
        }
        currentState = newState;
    }


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(placingKeyCode))
            SetCurrentState(State.PLACING);
        else if (Input.GetKeyDown(playingKeyCode))
            SetCurrentState(State.PLAYING);

    }

    //
    // SELECTION PHASE
    //


    //
    // PLACE PHASE
    //


    //
    // RUN PHASE
    //

}
