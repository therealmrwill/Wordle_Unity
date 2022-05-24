using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuessSlotController : MonoBehaviour
{
    public Vector2 position;

    public State state;

    public bool locked = false;

    private GuessPanelManager _manager;
    private Image _image;
    private Text _text;
    private Transform _transform;

    public enum State
    {
        Empty,
        Filled,
        Black,
        Yellow,
        Green
    }


    private void Awake()
    {
        _manager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>()._GuessPanelController.GetComponent<GuessPanelManager>();
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<Text>();
        _transform = GetComponent<Transform>();

        _text.resizeTextMaxSize = 55;
        _text.color = Color.white;

        _manager.guessSlotList.Add(position, this);
        UpdateState(State.Empty);

        //Just for debugging
        _manager.SlotList.Add(position);

    }



    // Callable Methods
    public void SlotFilled(string newText)
    {
        StartCoroutine(SimpleExpansion());

        _text.text = newText;

        UpdateState(State.Filled);

    }

    public void SlotClicked()
    {
        if (locked) return;
        if (Input.GetKeyDown(KeyCode.Return)) return;

        switch (state)
        {
            case State.Empty:
                return;
            case State.Filled:
                StylizedUpdateCoroutine(State.Black);
                break;
            case State.Black:
                StylizedUpdateCoroutine(State.Yellow);
                break;
            case State.Yellow:
                StylizedUpdateCoroutine(State.Green);
                break;
            case State.Green:
                StylizedUpdateCoroutine(State.Black);
                break;
        }

    }

    public void SlotCleared()
    {
        UpdateState(State.Empty);
    }

    public void UpdateState(State newState)
    {
        

        state = newState;

        switch (newState)
        {
            case State.Empty:
                _text.text = "";
                _image.color = _manager.blackColor;
                _image.sprite = _manager.emptyImage;
                break;
            case State.Filled:
                _image.color = _manager.filledColor;
                break;
            case State.Black:
                _image.color = _manager.blackColor;
                _image.sprite = _manager.filledImage;
                break;
            case State.Yellow:
                _image.color = _manager.yellowColor;
                _image.sprite = _manager.filledImage;
                break;
            case State.Green:
                _image.color = _manager.greenColor;
                _image.sprite = _manager.filledImage;
                break; 
        }
    }


    public void LockSlot()
    {
        locked = true;
    }

    public StringComparison.SimpleCharState GetInfo()
    {
        switch (state)
        {
            case State.Black:
                return StringComparison.SimpleCharState.Black;
            case State.Yellow:
                return StringComparison.SimpleCharState.Yellow;
            case State.Green:
                return StringComparison.SimpleCharState.Green;
            default:
                Debug.Log("Error - cannot read info from this character");
                return StringComparison.SimpleCharState.Black;
        }
    }




    //Visual Changes

    private IEnumerator SimpleExpansion()
    {
        float scaleChange = .1f;

        _transform.localScale = new Vector3(_transform.localScale.x + scaleChange, _transform.localScale.y + scaleChange, _transform.localScale.z);

        yield return new WaitForSeconds(.025f);

        _transform.localScale = new Vector3(_transform.localScale.x - scaleChange / 2, _transform.localScale.y - scaleChange / 2, _transform.localScale.z);

        yield return new WaitForSeconds(.025f);

        _transform.localScale = new Vector3(_transform.localScale.x - scaleChange / 2, _transform.localScale.y - scaleChange / 2, _transform.localScale.z);
        
    }

    private IEnumerator StylizedUpdate(State newState)
    {
        float timeToFlip = .06f;
        float numOfUpdates = 10;

        for(int i = 0; i < numOfUpdates; i++)
        {
            _transform.localScale = new Vector3(_transform.localScale.x, _transform.localScale.y - ( 1 / numOfUpdates), _transform.localScale.z);

            yield return new WaitForSeconds(timeToFlip / (numOfUpdates / 2));

        }

        UpdateState(newState);
        yield return new WaitForSeconds(.04f);


        for (int i = 0; i < numOfUpdates; i++)
        {
            _transform.localScale = new Vector3(_transform.localScale.x, _transform.localScale.y + (1 / numOfUpdates), _transform.localScale.z);

            yield return new WaitForSeconds(timeToFlip / (numOfUpdates / 2));

        }

    }

    private IEnumerator SimpleShake()
    {
        float moveSpeed = 5.5f;
        float shakeTime = .12f;
        int numOfShakes = 2;

        for(int i = 0; i < numOfShakes; i++)
        {
            _transform.position = new Vector2(_transform.position.x - moveSpeed, _transform.position.y);

            yield return new WaitForSeconds(shakeTime / 4);

            _transform.position = new Vector2(_transform.position.x + moveSpeed, _transform.position.y);

            yield return new WaitForSeconds(shakeTime / 4);

            _transform.position = new Vector2(_transform.position.x + moveSpeed, _transform.position.y);

            yield return new WaitForSeconds(shakeTime / 4);

            _transform.position = new Vector2(_transform.position.x - moveSpeed, _transform.position.y);

            yield return new WaitForSeconds(shakeTime / 4);

        }

        _transform.position = new Vector2(_transform.position.x - moveSpeed, _transform.position.y);

        yield return new WaitForSeconds(shakeTime / 4);

        _transform.position = new Vector2(_transform.position.x + moveSpeed, _transform.position.y);

        yield return new WaitForSeconds(shakeTime / 4);

    }


    //Coroutines

    public void StylizedUpdateCoroutine(State newState)
    {
        StartCoroutine(StylizedUpdate(newState));
    }

    public void SimpleExpansionCoroutine()
    {
        StartCoroutine(SimpleExpansion());
    }

    public void SimpleShakeCoroutine()
    {
        StartCoroutine(SimpleShake());
    }
}
