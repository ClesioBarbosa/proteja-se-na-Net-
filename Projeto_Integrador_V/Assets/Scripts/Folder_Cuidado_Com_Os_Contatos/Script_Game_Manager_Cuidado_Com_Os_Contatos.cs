using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Script_Game_Manager_Cuidado_Com_Os_Contatos : MonoBehaviour
{
    bool Correct,
        First_Profile,
        Is_On_Round;

    int Max_Messages,
        Messages, 
        Score;

    float Sending_Ratio, 
        Max_Timer, 
        Current_Timer;

    List<string> Possible_Names = new List<string> { "Andrey", "Bruno", "Clécio", "João", "Victor", "Hugo", "Caíque", "Diego", "Andréa", "Taís",
        "Josef", "Henrique", "Lucas", "Amanda", "Vinicius", "Davi", "Natercio", "Sócrates", "Bárbara", "Luiz",
        "Fábio", "Eduardo", "Thiago", "France", "Michelline", "Jorge", "Márcio", "Tatiana", "Foda-se", "Caralho",
        "Filho da Puta", "Arrombado", "Desgraça", "Vai tomar no cu", "Peste Bubônica", "Porra", "Merda", "Vai se fuder", "Filha duma cadela", "Xerolaine",
        "Xerocada", "Carimbo", "Xerox", "Harry Potter", "AAAAAAAAAAAA", "Sonegue imposto", "Sem nome", "Tô sem criatividade"},

        Possible_Progressions = new List<string> { "Quantity", "Time", "Ratio" };

    public TextMeshProUGUI Profile_Name,
        Score_Display;

    public Sprite Bubble,
        Circ,
        Oval,
        Tri,
        Ret,
        Trap,
        Los,
        Quad,
        Pent,
        Hex;

    public GameObject Player;

    void Start()
    {
        Max_Messages = 3;
        Messages = 0;
        Sending_Ratio = 3f;
        Is_On_Round = false;

        StartRound();
    }


    void Update()
    {

        if (Is_On_Round)
        {
            Touching_System();
            Time_Ticking();
        }
        else
        {
            Current_Timer = Max_Timer;
        }
        
    }

    public void StartRound()
    {
        Profile_Name.text = (Possible_Names[Random.Range(0, Possible_Names.Count)]).ToString();

        Messages = 0;
        First_Profile = true;

    }

    void Time_Ticking()
    {

        if (Is_On_Round)
        {
            Current_Timer -= Time.deltaTime;




            if (Current_Timer <= 0f)
            {
                //Botar perder
            }
        }
    }

    public void Other_Profile()
    {

    }

    public void Touching_System()
    {

    }

    IEnumerator Sending_Message()
    {

        return new WaitForSecondsRealtime(Sending_Ratio);
    }

    IEnumerator Next_Profile()
    {

        return new WaitForSecondsRealtime(Sending_Ratio);
    }

    void Increase_Quantity()
    {

    }

    void Decrease_Time()
    {
        Max_Timer -= 2f;

        if(Max_Timer == 4f)
        {

        }
    }

    void Increase_Ratio()
    {

    }

    void Right_Ansher()
    {
        Score++;
        string Becoming_Harder = (Possible_Progressions[Random.Range(0, Possible_Progressions.Count)]);


        print($"Aumentar dificuldade: {Becoming_Harder}");

        switch (Becoming_Harder)
        {
            case "Quantity": Increase_Quantity(); break;
            case "Time": Decrease_Time(); break;
            case "Ratio": Increase_Ratio(); break;
        }
    }
}
