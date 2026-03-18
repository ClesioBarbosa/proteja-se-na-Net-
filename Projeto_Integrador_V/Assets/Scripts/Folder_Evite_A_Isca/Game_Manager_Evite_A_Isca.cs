using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Game_Manager_Evite_A_Isca : MonoBehaviour
{
    string Right_Word;

    string[] Possible_Names = {"Google", "OLX", "YouTube", "mais um"}, Possible_Domains = {".com", ".org", ".cu"};

    int Evite_A_Isca_Timer = 30;

    [SerializeField] public TextMeshProUGUI Right_Word_Text, Door_Text1, DoorText2, Door_Text3, DoorText4, Door_Text5;

    void Start()
    {
        Randomizing_Words();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Randomizing_Words()
    {
        int Random_Word = Random.Range(0, Possible_Names.Length);
        int Random_Domain = Random.Range(0, Possible_Domains.Length);

        Right_Word = (Possible_Names[Random_Word] + Possible_Domains[Random_Domain]);
        Confusing_Word();
    }

    void Confusing_Word()
    {
        string Wrong_Word = Right_Word;

        

        print(Right_Word);

    }

    void Defeat()
    {

    }

    void Right_Ansher()
    {

    }
}
