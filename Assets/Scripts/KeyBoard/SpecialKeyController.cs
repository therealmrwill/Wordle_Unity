using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialKeyController : MonoBehaviour
{
    public KeyCode key;

    private KeyBoardManager _manager;
    private Image _image;

    private void Awake()
    {
        _manager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>()._KeyBoardController.GetComponent<KeyBoardManager>();
        _manager.specialKeySet.Add(gameObject);
        _manager.specialSet.Add(key, gameObject);
        _image = GetComponent<Image>();
    }

    public void KeyPressed()
    {
        _manager.KeyPressed(key);
    }

    //Purely for debug purpouses
    public void ChangeGreen()
    {
        _image.color = _manager.greenColor;
    }

}
