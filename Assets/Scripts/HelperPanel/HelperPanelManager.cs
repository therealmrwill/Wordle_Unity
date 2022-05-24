using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperPanelManager : MonoBehaviour
{
    public List<string> fullWordList;

    public string[] orderedList;

    private void Awake()
    {
        //fullWordList = FileReader.ReadFile("Assets/Data/Wordlist - Full.txt");

        orderedList = FileReader.DataReformatter("Assets/Data/Wordlist - Full.txt", 20, " ");


    }




}
