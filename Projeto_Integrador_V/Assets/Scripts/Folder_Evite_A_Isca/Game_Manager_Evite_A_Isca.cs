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

    public GameObject Door_Prefab;

    List<string> Possible_Names = new List<string> { "Google", "OLX", "YouTube", "mais um" },
        Possible_Domains = new List<string> { ".com", ".org", ".gov", ".edu", ".info", ".io", ".net", ".online", ".blog", ".app" },
        Possible_Downgrades = new List<string> { "Doors", "Time", "Difficult" }, 

        //Sistema de silabas que vai ser utilizado... Mas eu já vi que não é melhor fazer dessa forma.
        Silabas = new List<string> { "ba", "be", "bi", "bo", "bu", "by", 
            "bae", "bai", "bao", "bau", "bay", 
            "bea", "bei", "beo", "beu", "bey", 
            "bia", "bie", "bio", "biu", "biy", 
            "boa", ""};

    List<string> Generate_All_Words()
    {
        List<string> words = new List<string>();

        for (int i = 0; i < Door_Amount; i++)
        {
            words.Add(Generating_Wrong_Words());
        }

        return words;
    }

    List<GameObject> Doors_List = new List<GameObject>();

    //Não sei se Downgrades é um nome condizente pra essa lista. São aspectos do jogo que vão mudando para ficar mais dificeis.

    float Max_Timer, 
        Current_Timer;

    public int Player_Score, 
        Player_Highscore;

    [SerializeField] public TextMeshProUGUI Right_Word_Text, 
        Timer_Display, 
        Score_Display;

    bool Is_On_Round;

    int Difficult_Level, 
        Door_Amount;

    public float Spacing = 2f;

    

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

        Right_Word_Text.text = (Right_Word + Right_Domain);

        List<string> s = Generate_All_Words();
        Spawn_Doors(s);
    }

    string Generating_Wrong_Words()
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

        return Wrong_Word + Wrong_Domain;
    }

    void Spawn_Doors(List<string> Names)
    {

        int correct_Index = Random.Range(0, Door_Amount);

        float total_Width = (Door_Amount - 1) * Spacing;
        float start_X = -total_Width / 2;

        for(int i = 0; i < Door_Amount; i++)
        {
            Vector3 Door_Position = new Vector3(start_X + i * Spacing,
                Door_Prefab.transform.position.y,
                Door_Prefab.transform.position.z);

            GameObject Door_Instance = Instantiate(Door_Prefab,
                Door_Position,
                Quaternion.identity);

            Doors_List.Add(Door_Instance);

            string textToUse;

            if (i == correct_Index)
            {
                textToUse = Right_Word + Right_Domain;
                Door_Instance.tag = "Correct_Tag_Placeholder";
            }
            else
            {
                textToUse = Names[i];
                Door_Instance.tag = "Wrong_Tag_Placeholder";
            }


            TMPro.TextMeshPro Door_Text = Door_Instance.GetComponentInChildren<TMPro.TextMeshPro>();
            Door_Text.text = textToUse;
        }
    }

    void Start_New_Round()
    {

        foreach (GameObject door in Doors_List)
        {
            Destroy(door);
        }

        Doors_List.Clear();
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
                        if (hit.transform.root.CompareTag("Correct_Tag_Placeholder"))
                        {
                            Right_Ansher();
                        }
                        else if (hit.transform.root.CompareTag("Wrong_Tag_Placeholder"))
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
