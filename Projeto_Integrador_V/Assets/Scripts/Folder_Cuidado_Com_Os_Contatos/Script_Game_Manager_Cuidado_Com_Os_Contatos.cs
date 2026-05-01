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
        Score,
        Inconsistences;

    float Sending_Ratio, 
        Max_Timer, 
        Current_Timer;

    List<string> Possible_Names = new List<string> { "Ana", "André", "Amanda", "Arthur", "Alice", "Augusto", "Aline", "Adriano", "Alessandra", "Antônio",
        "Bruno", "Bianca", "Beatriz", "Bernardo", "Bárbara", "Breno", "Bruna", "Benício", "Bento", "Beto",
        "Carlos", "Camila", "Caio", "Carolina", "César", "Clara", "Cristiano", "Cíntia", "Cauã", "Cláudio",
        "Daniel", "Daniela", "Diego", "Débora", "Davi", "Diana", "Douglas", "Denise", "Dalton", "Darlan",
        "Eduardo", "Erika", "Elias", "Elaine", "Enzo", "Ester", "Everton", "Eliane", "Emanoel", "Ellen",
        "Felipe", "Fernanda", "Fábio", "Flávia", "Francisco", "Fabiana", "Fernando", "Fátima", "Frederico", "Filipe",
        "Gabriel", "Gabriela", "Gustavo", "Giovana", "Guilherme", "Gisele", "Geraldo", "Glória", "Geovane", "Gilberto",
        "Henrique", "Helena", "Hugo", "Heloísa", "Heitor", "Hadassa", "Higor", "Hilda", "Herbert", "Hélio",
        "Igor", "Isabela", "Ivan", "Ingrid", "Ícaro", "Iara", "Isaque", "Ivone", "Israel", "Irineu",
        "João", "Juliana", "José", "Júlia", "Jefferson", "Jéssica", "Jonas", "Janaína", "Joaquim", "Júnior",
        "Kaio", "Karina", "Kelvin", "Kelly", "Kauã", "Kátia", "Kleber", "Kiara", "Kawan", "Karen",
        "Lucas", "Larissa", "Leonardo", "Luana", "Luiz", "Letícia", "Leandro", "Lívia", "Lorenzo", "Lúcio",
        "Marcos", "Maria", "Mateus", "Mariana", "Miguel", "Márcia", "Murilo", "Mirela", "Marcelo", "Milena",
        "Nicolas", "Natália", "Nelson", "Nicole", "Nathan", "Nayara", "Nataniel", "Neide", "Nivaldo", "Noemi",
        "Otávio", "Olivia", "Osvaldo", "Olívia", "Orlando", "Odete", "Othon", "Olga", "Omar", "Ofélia",
        "Paulo", "Patricia", "Pedro", "Priscila", "Pablo", "Pamela", "Pietro", "Paloma", "Patrick", "Penélope",
        "Quirino", "Queila", "Quésia", "Quirina", "Quincas", "Quelen", "Quésia", "Quirineu", "Quitéria", "Quiana",
        "Rafael", "Renata", "Rodrigo", "Raquel", "Ricardo", "Rita", "Ramon", "Roberta", "Ruan", "Rosana",
        "Samuel", "Sabrina", "Sérgio", "Simone", "Sandro", "Sara", "Silas", "Sofia", "Saulo", "Sheila",
        "Thiago", "Tatiane", "Tiago", "Tainá", "Tomás", "Teresa", "Túlio", "Talita", "Theo", "Tereza",
        "Ulisses", "Ubirajara", "Ueliton", "Uilson", "Ueslei", "Uanda", "Urias", "Uelma", "Ugo", "Ualace",
        "Victor", "Vanessa", "Vinícius", "Vitória", "Valter", "Vânia", "Vitor", "Verônica", "Vicente", "Viviane",
        "William", "Wagner", "Wesley", "Wanessa", "Willian", "Wendel", "Walace", "Wilma", "Wallyson", "Washington",
        "Xavier", "Ximena", "Xande", "Xênia", "Xisto", "Xuxa", "Xadrez", "Xarleen", "Xaviera", "Ximene",
        "Yuri", "Yasmin", "Yago", "Yara", "Yan", "Yohana", "Ygor", "Yvone", "Yago", "Yandra",
        "Zé", "Zilda", "Zacarias", "Zuleica", "Zeno", "Zara", "Zaqueu", "Zoraide", "Zélia", "Zoran"},

        Possible_Progressions = new List<string> { "Quantity", "Time", "Ratio", "Inconsistences" };

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

    public GameObject Player,
        Hook;

    Vector3 Hook_Starting_Position, Hook_Ending_Position;
    void Start()
    {
        Hook_Starting_Position = new Vector3(Hook.transform.position.x, Hook.transform.position.y, Hook.transform.position.z);
        Hook_Ending_Position = new Vector3(Player.transform.position.x, Player.transform.position.y + 1f, Player.transform.position.z);

        Max_Timer = 30f;
        Score = 0;
        Inconsistences = 5;
        Max_Messages = 3;
        Messages = 0;
        Sending_Ratio = 3f;
        Is_On_Round = false;

        StartRound();
        Making_Inconsistences();
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
            Hook.transform.position = Hook_Starting_Position;
        }
        
    }

    public void StartRound()
    {
        Hook.transform.position = Hook_Starting_Position;
        Profile_Name.text = (Possible_Names[Random.Range(0, Possible_Names.Count)]).ToString();
        Score_Display.text = Score.ToString();
        Current_Timer = Max_Timer;

        Messages = 0;
        First_Profile = true;

        Is_On_Round = true;
    }

    void Time_Ticking()
    {
        if (Is_On_Round)
        {
            Current_Timer -= Time.deltaTime;

            float t = 1f - (Current_Timer / Max_Timer);

            t = Mathf.Clamp01(t);

            t = Mathf.SmoothStep(0f, 1f, t);

            Hook.transform.position = Vector3.Lerp(Hook_Starting_Position, Hook_Ending_Position, t);

            if (Current_Timer <= 0f)
            {
                print("Perdeu");
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

    void Lower_Inconsistences()
    {
        Inconsistences--;

        if(Inconsistences == 1)
        {
            Possible_Progressions.Remove("Inconsistences");
        }
    }

    void Increase_Quantity()
    {
        Max_Timer++;

        if (Max_Timer == 20)
        {
            Possible_Progressions.Remove("Quantity");
        }
    }

    void Decrease_Time()
    {
        Max_Timer -= 2f;

        if(Max_Timer == 4f)
        {
            Possible_Progressions.Remove("Time");
        }
    }

    void Increase_Ratio()
    {
        switch (Sending_Ratio)
        {
            case 3f: Sending_Ratio = 2f; break;
            case 2f: Sending_Ratio = 1f; break;
            case 1f: Sending_Ratio = 0.8f; break;
            case 0.8f: Sending_Ratio = 0.6f; break;
            case 0.6f: Sending_Ratio = 0.4f; break;
            case 0.4f: Sending_Ratio = 0.3f; Possible_Progressions.Remove("Ratio"); break;
        }
    }

    void Making_Inconsistences()
    {
        for(int i = 0; i < Inconsistences; i++)
        {
            print(i);
        }
    }

    void Right_Ansher()
    {
        Score++;
        Score_Display.text = Score.ToString();

        if(Possible_Progressions != null)
        {
            string Becoming_Harder = (Possible_Progressions[Random.Range(0, Possible_Progressions.Count)]);

            print($"Aumentar dificuldade: {Becoming_Harder}");

            switch (Becoming_Harder)
            {
                case "Quantity": Increase_Quantity(); break;
                case "Time": Decrease_Time(); break;
                case "Ratio": Increase_Ratio(); break;
                case "Inconsistences": Lower_Inconsistences(); break;
            }
        }        
    }
}
