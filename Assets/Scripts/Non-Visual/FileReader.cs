using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileReader
{

    //Todo: Need to change to a HashSet at some point
    //Should be a pretty simple translation

    public static List<string> ReadFile(string filename)
    {
        List<string> dataOut = new List<string>();

        string[] dataLines = System.IO.File.ReadAllLines(@filename);

        foreach (string currentLine in dataLines)
        {
            string tempWord = "";

            for (int pos = 0; pos < currentLine.Length; pos++)
            {
                if (currentLine[pos] == ' ')
                {
                    dataOut.Add(tempWord);
                    tempWord = "";
                }
                else
                {
                    tempWord += currentLine[pos];
                }
            }

            dataOut.Add(tempWord);
        }

        return dataOut;
    }


    public static string[] DataReformatter(string filename, int itemsOnLine, string seperator)
    {
        List<string> dataToFormat = ReadFile(filename);

        string[] dataOut = new string[dataToFormat.Count + itemsOnLine / itemsOnLine];


        for(int yPos = 0; yPos < dataToFormat.Count / itemsOnLine; yPos++)
        {
            string currentData = "";

            for(int xPos = 0; xPos < itemsOnLine; xPos++)
            {
                currentData += dataToFormat[(yPos * itemsOnLine) + xPos].ToUpper();
                currentData += seperator;
            }


            dataOut[yPos] = currentData;
        }



        System.IO.File.WriteAllLines(filename, dataOut);
        return dataOut;
    }


}



