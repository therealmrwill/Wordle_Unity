using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KeyController : MonoBehaviour
{
    public KeyCode key;
    public char character;
    public KeyData data;

    private GameObject _GameManager;
    private KeyBoardManager _KeyBoardManager;

    private Image _image;


    private void Awake()
    {
        _GameManager = GameObject.FindGameObjectWithTag("Game Manager");
        _KeyBoardManager = _GameManager.GetComponent<GameManager>()._KeyBoardController.GetComponent<KeyBoardManager>();
        _image = GetComponent<Image>();

        data = new KeyData(character);

        _KeyBoardManager.characterSet.Add(key, gameObject);
        _KeyBoardManager.baseKeySet.Add(gameObject); 

    }

    public void positionUpdate(int newPosition)
    {
        switch (data.positionalStatus[newPosition])
        {
            case KeyData.KeyState.Positioned: _image.color = _KeyBoardManager.greenColor; break;
            case KeyData.KeyState.Testable:
                if (data.numConnected > 0)
                {
                    _image.color = _KeyBoardManager.yellowColor;
                }
                else
                {
                    _image.color = _KeyBoardManager.greyColor;
                }
                break;
            case KeyData.KeyState.Invalid: _image.color = _KeyBoardManager.blackColor; break;
        }
    }

    public void KeyPressed()
    {
        _KeyBoardManager.KeyPressed(key);
    }


    //Really just for debugging
    public void ChangeGreen()
    {
        _image.color = _KeyBoardManager.greenColor;
    }

    




}
