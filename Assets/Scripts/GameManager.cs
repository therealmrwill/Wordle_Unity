using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _KeyBoardController;
    public GameObject _GuessPanelController;
    public GameObject _HelperPanelController;

    public int currentPosition;


    public void KeyPressed(KeyCode key)
    {
        _GuessPanelController.GetComponent<GuessPanelManager>().KeyPressed(key);
    }



    






}
