using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Game_Manager_Evite_A_Isca : MonoBehaviour
{
    string Right_Word, 
        Right_Domain;

    List<string> Possible_Names = new List<string> { "Google", "OLX", "YouTube", "mais um" }, 
        Possible_Domains = new List<string>{ ".com", ".org", ".cu" }, 
        Possible_Downgrades = new List<string> { "Doors", "Time", "Difficult" };
    //Não sei se Downgrades é um nome condizente pra essa lista. São aspectos do jogo que vão mudando para ficar mais dificeis.

    float Max_Timer, 
        Current_Timer;

    public int Player_Score, 
        Player_Highscore;

    [SerializeField] public TextMeshProUGUI Right_Word_Text, 
        Door_Text1, 
        DoorText2, 
        Door_Text3, 
        DoorText4, 
        Door_Text5, 
        Timer_Display, 
        Score_Display;

    bool Is_On_Round;

    int Difficult_Level, 
        Door_Amount;

    void Start()
    {
        Player_Score = 0; 
        Door_Amount = 2; 
        Difficult_Level = 1; 
        Max_Timer = 30f;

        Start_New_Round();
    }

    void Update()
    {

        Time_Ticking();
        Touching_Screen();
    }

    void Generating_Correct_Word()
    {
        Right_Word = (Possible_Names[Random.Range(0, Possible_Names.Count)]);
        Right_Domain = (Possible_Domains[Random.Range(0, Possible_Domains.Count)]);

        print($"Palavra correta: {Right_Word}{Right_Domain}");

        Generating_Wrong_Words();
    }

    void Generating_Wrong_Words()
    {
        string Wrong_Word = Right_Word;
        string Wrong_Domain = Right_Domain;

        /*1º mudar o dominio: FEITO
         * 2º Umas letras faltando: INCOMPLETO
         3º Letras diferentes: INCOMPLETO*/


        //Separar está parte em outro lugar
        while (Wrong_Domain == Right_Domain)
        {
            Wrong_Domain = (Possible_Domains[Random.Range(0, Possible_Domains.Count)]);
        }

        print($"Palavra incorreta: {Wrong_Word}{Wrong_Domain}");
    }

    void Start_New_Round()
    {
        Current_Timer = Max_Timer;
        Is_On_Round = true;

        Generating_Correct_Word();
    }

    void Time_Ticking()
    {
        if (Is_On_Round)
        {
            Current_Timer -= Time.deltaTime;

            Timer_Display.text = ((int)Current_Timer).ToString();


            if (Current_Timer <= 0f)
            {
                Defeat();
            }
        }
    }

    void Touching_Screen()
    {
        if (Is_On_Round)
        {
            if(Input.touchCount > 0)
            {
                Touch Getting_Touch = Input.GetTouch(0);

                if(Getting_Touch.phase == TouchPhase.Began)
                {

                    //Eu não pensei em nomes condinzentes para por nessas variaveis...
                    Ray r = Camera.main.ScreenPointToRay(Getting_Touch.position);
                    RaycastHit hit;

                    if(Physics.Raycast(r, out hit))
                    {
                        if (hit.transform.CompareTag("Correct_Tag_Placeholder"))
                        {
                            Right_Ansher();
                        }
                        else if (hit.transform.CompareTag("Wrong_Tag_Placeholder"))
                        {
                            Defeat();
                        }
                    }
                }
            }
        }
    }

    void Defeat()
    {

        print("Você Perdeu!");
        Is_On_Round = false;
    }

    void Right_Ansher()
    {

        Player_Score++;
        print($"Você Acertou!, pontuação: {Player_Score}");

        if (Player_Score % 5 == 0 && Possible_Downgrades.Count != 0)
        {
            print("Dificultando");
            Difficulting();
        }
        else if(Possible_Downgrades.Count == 0)
        {
            print("Não dá pra dificultar mais");
        }

        Start_New_Round();
    }

    void Difficulting()
    {

        //Nome extremamente goofy pra uma variavel, eu sei. Ainda vou achar um nome melhor.
        string Decide_Whats_Bcome_Harder = (Possible_Downgrades[Random.Range(0, Possible_Downgrades.Count)]);

        print($"Aumentar dificuldade: {Decide_Whats_Bcome_Harder}");

        switch (Decide_Whats_Bcome_Harder)
        {
            case "Doors": Add_Doors(); break;
            case "Time":    Reduce_Max_Timer(); break;
            case "Difficult":   Deixando_Mais_Dificil();    break;


        }
    }

    void Add_Doors()
    {
        Door_Amount++;

        print($"Quantidade de portas: {Door_Amount}");

        if(Door_Amount == 5)
        {

            Possible_Downgrades.Remove("Doors");
            print("Nível máximo alcançado em PORTAS");
        }
    }

    void Reduce_Max_Timer()
    {
        Max_Timer -= 5f;

        print($"Tempo máximo: {Max_Timer}");

        if (Max_Timer == 5f)
        {
            Possible_Downgrades.Remove("Time");
            print("Nível máximo alcançado em TEMPO");
        }
    }

    //Ainda vou trocar o nome desse método. Eu também não gosto de como tem 2 coisas falando sobre "dificultar". Queria um nome diferente
    void Deixando_Mais_Dificil()
    {
        Difficult_Level++;

        print($"Dificuldade nível: {Difficult_Level}");

        if (Difficult_Level == 3)
        {
            Possible_Downgrades.Remove("Difficult");
            print("Nível máximo alcançado em DIFICULDADE");
        }
    }
}
