using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardManager : MonoBehaviour
{
    public float fontSize;
    public int currentPosition;

    public Color greenColor;
    public Color yellowColor;
    public Color greyColor;
    public Color blackColor;

    public GameObject _gameManager;

    public Dictionary<KeyCode, GameObject> characterSet = new Dictionary<KeyCode, GameObject>();
    public Dictionary<KeyCode, GameObject> specialSet = new Dictionary<KeyCode, GameObject>();


    //This is just for visibilities sake, to make things easier
    [Header("Debug Data")]
    public List<GameObject> baseKeySet = new List<GameObject>();
    public List<GameObject> specialKeySet = new List<GameObject>();

    public List<KeyCode> inputList = new List<KeyCode>();


    private void Awake()
    {
        _gameManager = GameObject.FindGameObjectWithTag("Game Manager");

        currentPosition = _gameManager.GetComponent<GameManager>().currentPosition;

        AddKeyCodes(inputList);


    }


    private void Update()
    {
        foreach (KeyCode key in inputList)
        {
            if (Input.GetKeyUp(key))
            {
                KeyPressed(key);
            }
        }
    }


    private KeyCode GetKeyCode(string input)
    {
        switch (input)
        {
            case "A": return KeyCode.A; 
            case "B": return KeyCode.B;
            case "C": return KeyCode.C;
            case "D": return KeyCode.D;
            case "E": return KeyCode.E;
            case "F": return KeyCode.F;
            case "G": return KeyCode.G;
            case "H": return KeyCode.H;
            case "I": return KeyCode.I;
            case "J": return KeyCode.J;
            case "K": return KeyCode.K;
            case "L": return KeyCode.L;
            case "M": return KeyCode.M;
            case "N": return KeyCode.N;
            case "O": return KeyCode.O;
            case "P": return KeyCode.P;
            case "Q": return KeyCode.Q;
            case "R": return KeyCode.R;
            case "S": return KeyCode.S;
            case "T": return KeyCode.T;
            case "U": return KeyCode.U;
            case "V": return KeyCode.V;
            case "W": return KeyCode.W;
            case "X": return KeyCode.X;
            case "Y": return KeyCode.Y;
            case "Z": return KeyCode.Z;
            case "Space": return KeyCode.Space;
            case "Return": return KeyCode.Return;
            case "Enter": return KeyCode.Return;
            case "Delete": return KeyCode.Delete;
            case "Backspace": return KeyCode.Backspace;
            default: return KeyCode.None;

        }
    }

    private void AddKeyCodes(List<KeyCode> keyList)
    {
        keyList.Add(GetKeyCode("A"));
        keyList.Add(GetKeyCode("B"));
        keyList.Add(GetKeyCode("C"));
        keyList.Add(GetKeyCode("D"));
        keyList.Add(GetKeyCode("E"));
        keyList.Add(GetKeyCode("F"));
        keyList.Add(GetKeyCode("G"));
        keyList.Add(GetKeyCode("H"));
        keyList.Add(GetKeyCode("I"));
        keyList.Add(GetKeyCode("J"));
        keyList.Add(GetKeyCode("K"));
        keyList.Add(GetKeyCode("L"));
        keyList.Add(GetKeyCode("M"));
        keyList.Add(GetKeyCode("N"));
        keyList.Add(GetKeyCode("O"));
        keyList.Add(GetKeyCode("P"));
        keyList.Add(GetKeyCode("Q"));
        keyList.Add(GetKeyCode("R"));
        keyList.Add(GetKeyCode("S"));
        keyList.Add(GetKeyCode("T"));
        keyList.Add(GetKeyCode("U"));
        keyList.Add(GetKeyCode("V"));
        keyList.Add(GetKeyCode("W"));
        keyList.Add(GetKeyCode("X"));
        keyList.Add(GetKeyCode("Y"));
        keyList.Add(GetKeyCode("Z"));

        keyList.Add(GetKeyCode("Space"));
        keyList.Add(GetKeyCode("Enter"));
        keyList.Add(GetKeyCode("Delete"));
        keyList.Add(GetKeyCode("Backspace"));

        
    }



    public void KeyPressed(KeyCode key)
    {
        //Debug.Log("Key Pressed: " + key);

        _gameManager.GetComponent<GameManager>().KeyPressed(key);

    }



    

}
