
using System;
using System.Collections.Generic;


public class StringComparison
{
    public enum CharState
    {
        Positioned,
        Connected,
        Disconnected,
        NotPresent
    }

    public enum SimpleCharState
    {
        Green,
        Yellow,
        Black
    }

    public static Dictionary<int, CharState> FindValidity(String testString, String solutionString)
    {
        if (testString.Length != solutionString.Length)
        {
            Console.WriteLine(String.Format("\nError: {0} and {1} do not have the same size ({2} != {3})", testString, solutionString, testString.Length, solutionString.Length));
            return null;
        }

        Dictionary<int, CharState> validityData = new Dictionary<int, CharState>();

        //This allows us to check if a character has already been used
        Dictionary<char, int> connectionsLeft = new Dictionary<char, int>();

        //* Check for correctly positioned characters first
        for (int position = 0; position < testString.Length; position++)
        {
            if (testString[position] == solutionString[position])
            {
                validityData.Add(position, CharState.Positioned);
            }
            else
            {
                if (connectionsLeft.ContainsKey(solutionString[position]))
                {
                    connectionsLeft[testString[position]] += 1;
                }
                else
                {
                    connectionsLeft.Add(solutionString[position], 1);
                }
            }


        }

        //Next check for all other character states
        for (int position = 0; position < testString.Length; position++)
        {
            //Checks if the character has already been identified 
            if (validityData.ContainsKey(position) == false)
            {
                if (solutionString.Contains(testString.Substring(position, 1)))
                {
                    //Need to check and see if the character we are refrencing has already been connected
                    if (connectionsLeft[testString[position]] > 0)
                    {
                        connectionsLeft[testString[position]] -= 1;
                        validityData.Add(position, CharState.Connected);
                    }
                    else
                    {
                        validityData.Add(position, CharState.Disconnected);
                    }

                }
                else
                {
                    validityData.Add(position, CharState.NotPresent);
                }
            }
        }

        return validityData;
    }

    public static Dictionary<CharState, List<int>> FindValidityByState(Dictionary<int, CharState> baseValidity)
    {
        Dictionary<CharState, List<int>> stateBasedValidity = new Dictionary<CharState, List<int>>();

        foreach (int position in baseValidity.Keys)
        {
            CharState positionalValidity = baseValidity[position];


            if (stateBasedValidity.ContainsKey(positionalValidity) == false)
            {
                stateBasedValidity.Add(positionalValidity, new List<int>());
            }

            stateBasedValidity[positionalValidity].Add(position);
        }


        return stateBasedValidity;
    }

    public static Dictionary<int, CharState> SimpleStateTranslator(String testString, Dictionary<int, SimpleCharState> simpleStateData)
    {
        if (testString.Length != simpleStateData.Count)
        {
            Console.WriteLine(String.Format("Error: The sizes of {0} and the data ({1} , {2) are not equal", testString, testString.Length, simpleStateData.Count));
            return null;
        }


        Dictionary<int, CharState> translatedData = new Dictionary<int, CharState>();

        List<char> previousChars = new List<char>();

        //Has 3 functions
        //Checking for any missing data
        //Checking for positioned characters
        //Checking for connected characters
        for (int position = 0; position < testString.Length; position++)
        {
            if (simpleStateData.ContainsKey(position) == false)
            {
                Console.WriteLine(String.Format("Error: StateData does not contain a value for position {0}", position));
                return null;
            }

            if (simpleStateData[position] == SimpleCharState.Green)
            {
                translatedData.Add(position, CharState.Positioned);
                previousChars.Add(testString[position]);
            }

            if (simpleStateData[position] == SimpleCharState.Yellow)
            {
                translatedData.Add(position, CharState.Connected);
                previousChars.Add(testString[position]);
            }
        }

        //Only for the Black state in the simple CharStates information
        for (int position = 0; position < testString.Length; position++)
        {
            if (translatedData.ContainsKey(position) == false)
            {
                char currentChar = testString[position];

                if (previousChars.Contains(currentChar))
                {
                    translatedData.Add(position, CharState.Disconnected);
                }
                else
                {
                    translatedData.Add(position, CharState.NotPresent);
                }

            }
        }

        return translatedData;
    }

}
