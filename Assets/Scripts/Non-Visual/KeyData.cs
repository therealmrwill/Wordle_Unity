using System;
using System.Collections.Generic;

public class KeyData
{
    public char character { get; }

    public int numConnected { get; set; }
    public int numPositioned { get; set; }
    
    public Dictionary<int, KeyState> positionalStatus { get; set; }

    public enum KeyState
    {
        Positioned,
        Testable,
        Invalid
    }

    public KeyData(char newChar)
    {
        this.character = newChar;
        this.numConnected = 0;
        this.numPositioned = 0;

        this.positionalStatus = new Dictionary<int, KeyState>();

        for(int pos = 0; pos < WordleRuleset.wordLength; pos++)
        {
            positionalStatus.Add(pos, KeyState.Testable);
        }
        
        
    }

    public void AddData(Dictionary<int, StringComparison.CharState> dataToAdd)
    {
        int totalNeeded = numConnected + numPositioned;

        foreach(int position in dataToAdd.Keys)
        {
            switch (dataToAdd[position])
            { 
                case StringComparison.CharState.Positioned:
                    AddPositioned(position);
                    totalNeeded--;
                    break;
                case StringComparison.CharState.Disconnected: AddDisconnected(position); break;
                case StringComparison.CharState.NotPresent: AddNotPresent(); break;

            }
        }

        foreach(int position in dataToAdd.Keys)
        {
            if(dataToAdd[position] == StringComparison.CharState.Connected)
            {
                if(totalNeeded > 1)
                {
                    totalNeeded--;
                    AddDisconnected(position);
                    
                }
                else
                {
                    AddConnected(position);
                }
            }
        }

        CheckConnected();
    }

    public void AddPositioned(int position)
    {
        if (positionalStatus[position] != KeyState.Testable) return;

        if (numConnected > 0) numConnected--;

        numPositioned += 1;

        positionalStatus[position] = KeyState.Positioned;

    }

    public void AddConnected(int position)
    {
        if (positionalStatus[position] != KeyState.Testable) return;

        numConnected++;
        positionalStatus[position] = KeyState.Invalid;

    }

    public void AddDisconnected(int position)
    {
        positionalStatus[position] = KeyState.Invalid;
    }

    public void AddNotPresent()
    {
        if (numConnected > 0 || numPositioned > 0) return;

        foreach(int position in positionalStatus.Keys)
        {
            positionalStatus[position] = KeyState.Invalid;
        }

    }


    public void CheckConnected()
    {
        if (numConnected == 0) return;

        List<int> testablePositions = new List<int>();

        foreach(int position in positionalStatus.Keys)
        {
            if (positionalStatus[position] == KeyState.Testable)
            {
                testablePositions.Add(position);
            }
        }

        if(numConnected == testablePositions.Count)
        {
            foreach(int position in testablePositions)
            {
                positionalStatus[position] = KeyState.Positioned;
                numConnected--;
                numPositioned++;
            }
        }



    }

    

}
