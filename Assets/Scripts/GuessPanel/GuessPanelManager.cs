using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuessPanelManager : MonoBehaviour
{
    //Important Objects
    [Header("Runtime")]
    public Vector2 currentPosition;
    public Dictionary<Vector2, GuessSlotController> guessSlotList = new Dictionary<Vector2, GuessSlotController>();
    public char[] currentString;
    public List<string> fullWordList;
    public List<string> invalidWords;



    [Space]
    [Header("Helpers")]
    public Color greenColor;
    public Color yellowColor;
    public Color blackColor;
    public Color filledColor;

    public Sprite emptyImage;
    public Sprite filledImage;

    //Just for debug Purpouses
    [Space]
    [Header("Debug:")]
    public List<Vector2> SlotList;

    private void Awake()
    {
        
        currentString = new char[WordleRuleset.wordLength];

        for(int pos = 0; pos < WordleRuleset.wordLength; pos++)
        {
            currentString[pos] = '_';
        }

        fullWordList = FileReader.ReadFile("Assets/Data/Wordlist - Full.txt");
    }


    public void KeyPressed(KeyCode key)
    {
        //Handle special cases first
        switch (key)
        {
            case KeyCode.Backspace:
                DeletePressed();
                return;
            case KeyCode.Delete:
                DeletePressed();
                return;
            case KeyCode.Return:
                ReturnPressed();
                return;
            case KeyCode.Space:
                SpacePressed();
                return;
        }


        //Then handle the regular cases
        switch (key)
        {
            case KeyCode.A: SimpleKeyPressed("A"); return;
            case KeyCode.B: SimpleKeyPressed("B"); return;
            case KeyCode.C: SimpleKeyPressed("C"); return;
            case KeyCode.D: SimpleKeyPressed("D"); return;
            case KeyCode.E: SimpleKeyPressed("E"); return;
            case KeyCode.F: SimpleKeyPressed("F"); return;
            case KeyCode.G: SimpleKeyPressed("G"); return;
            case KeyCode.H: SimpleKeyPressed("H"); return;
            case KeyCode.I: SimpleKeyPressed("I"); return;
            case KeyCode.J: SimpleKeyPressed("J"); return;
            case KeyCode.K: SimpleKeyPressed("K"); return;
            case KeyCode.L: SimpleKeyPressed("L"); return;
            case KeyCode.M: SimpleKeyPressed("M"); return;
            case KeyCode.N: SimpleKeyPressed("N"); return;
            case KeyCode.O: SimpleKeyPressed("O"); return;
            case KeyCode.P: SimpleKeyPressed("P"); return;
            case KeyCode.Q: SimpleKeyPressed("Q"); return;
            case KeyCode.R: SimpleKeyPressed("R"); return;
            case KeyCode.S: SimpleKeyPressed("S"); return;
            case KeyCode.T: SimpleKeyPressed("T"); return;
            case KeyCode.U: SimpleKeyPressed("U"); return;
            case KeyCode.V: SimpleKeyPressed("V"); return;
            case KeyCode.W: SimpleKeyPressed("W"); return;
            case KeyCode.X: SimpleKeyPressed("X"); return;
            case KeyCode.Y: SimpleKeyPressed("Y"); return;
            case KeyCode.Z: SimpleKeyPressed("Z"); return;

        }

    }

    //Rules that need to be followed
    // 1. Controller is left on next open position
    // 2. Enter can only be pressed when:
        // There are five characters in the line
        // There are no spaces in the line
        // The word is a valid word -> will need to be set up later
    // 3. Delete can only be pressed
        // When the previous character is in the grid
        // Cannot delete locked characters
    // 4. Space is a very special case that we will continue to work with


    private void SimpleKeyPressed(string key)
    {
        //Checks
        if (SimpleActionChecker() == false) return;
        if (key.Length > 1) return;

        currentString[(int)currentPosition.x] = key.ToCharArray()[0];
        
        guessSlotList[currentPosition].SlotFilled(key);
        guessSlotList[currentPosition].LockSlot();

        currentPosition.x += 1;

    }


    private void DeletePressed()
    {
        //Checks
        if (DeleteActionChecker() == false) return;

        currentPosition.x -= 1;

        currentString[(int)currentPosition.x] = '_';

        guessSlotList[currentPosition].SlotCleared();

    }

    private void ReturnPressed()
    {
        // Check for invalid results
        if (ValidWordChecker() == false)
        {
            for(int pos = 0; pos < WordleRuleset.wordLength; pos++)
            {
                if(guessSlotList[new Vector2(pos, currentPosition.y)].state != GuessSlotController.State.Empty)
                {
                    guessSlotList[new Vector2(pos, currentPosition.y)].SimpleShakeCoroutine();
                }
            }

            Debug.Log("Word is not valid");
            return;
        }

        //Frees all of the positions to be changed
        if(guessSlotList[new Vector2(0, currentPosition.y)].locked == true)
        {
            StartCoroutine(SlotUnlocker());
            return;
        }


        //Checks if all the positions have been scored
        if (ScoredWordChecker() == false)
        {
            for (int pos = 0; pos < WordleRuleset.wordLength; pos++)
            {
                if (guessSlotList[new Vector2(pos, currentPosition.y)].state == GuessSlotController.State.Filled)
                {
                    guessSlotList[new Vector2(pos, currentPosition.y)].SimpleShakeCoroutine();
                }

            }
            return;
        }
        

        //Otherwise we are all good - we lock the data in

        currentPosition.y += 1;
        currentPosition.x = 0;
        
    }



    private void SpacePressed()
    {
        if (SimpleActionChecker() == false) return;

        currentPosition.x += 1;
    }

    private IEnumerator SlotUnlocker()
    {
        for (int pos = 0; pos < WordleRuleset.wordLength; pos++)
        {
            if (guessSlotList[new Vector2(pos, currentPosition.y)].locked == true)
            {
                guessSlotList[new Vector2(pos, currentPosition.y)].locked = false;

                guessSlotList[new Vector2(pos, currentPosition.y)].StylizedUpdateCoroutine(GuessSlotController.State.Filled);

                yield return new WaitForSeconds(.15f);
            }
        }
    }


    private bool SimpleActionChecker()
    {
        if(currentPosition.x > WordleRuleset.wordLength - 1)
        {
            return false;
        }

        if(currentPosition.y > WordleRuleset.numOfRounds - 1)
        {
            return false;
        }

        return true;
    }

    private bool DeleteActionChecker()
    {
        if(currentPosition.x < 1)
        {
            return false;
        }

        return true;
    }

    private bool ValidWordChecker()
    {
        for (int pos = 0; pos < WordleRuleset.wordLength; pos++)
        {
            if(guessSlotList[new Vector2(pos, currentPosition.y)].state == GuessSlotController.State.Empty)
            {
                //Need to set up the visual error message system here
                Debug.Log("Error word is not fully filled in");

                return false;
            }
        }

        if(invalidWords.Contains(new string(currentString)))
        {
            Debug.Log("Error, word does not exist in library");
            return false;
        }

        //Need to add a check to make sure that the word is valid
        if(fullWordList.Contains(new string(currentString)) == false)
        {
            Debug.Log("Error, word does not exist in library");

            invalidWords.Add(new string(currentString));

            return false;
        }



        return true;
    }


    

    private bool ScoredWordChecker()
    {
        for (int pos = 0; pos < WordleRuleset.wordLength; pos++)
        {
            if (guessSlotList[new Vector2(pos, currentPosition.y)].state == GuessSlotController.State.Filled)
            {
                //Need to set up the visual error message system here
                Debug.Log("Pos (" + pos + ") - State:" + guessSlotList[new Vector2(pos, currentPosition.y)].state);


                return false;
            }
        }

        return true;
    }


}
