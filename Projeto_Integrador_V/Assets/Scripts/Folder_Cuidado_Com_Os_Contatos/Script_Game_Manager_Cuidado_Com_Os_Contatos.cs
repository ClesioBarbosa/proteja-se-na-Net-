using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Script_Game_Manager_Cuidado_Com_Os_Contatos : MonoBehaviour
{
    bool Correct;

    int Messages;

    float Sending_Ratio;

    bool First_Profile;

    List<string> Possible_Names = new List<string> { "Andrey", "Bruno", "Clécio", "João", "Victor", "Hugo", "Caíque", "Diego", "Andréa", "Taís",
        "Josef", "Henrique", "Lucas", "Amanda", "Vinicius", "Davi", "Natercio", "Sócrates", "Bárbara", "Luiz",
        "Fábio", "Eduardo", "Thiago", "France", "Michelline", "Jorge", "Márcio", "Tatiana", "Foda-se", "Caralho",
        "Filho da Puta", "Arrombado", "Desgraça", "Vai tomar no cu", "Peste Bubônica", "Porra", "Merda", "Vai se fuder", "Filha duma cadela", "Xerolaine",
        "Xerocada", "Carimbo", "Xerox", "Harry Potter", "AAAAAAAAAAAA", "Sonegue imposto", "Sem nome", "Tô sem criatividade"};

    public TextMeshPro Profile_Name;
    void Start()
    {
        Messages = 3;
        Sending_Ratio = 0.3f;

        StartRound();
    }


    void Update()
    {
        
    }

    public void StartRound()
    {
        Profile_Name.text = (Possible_Names[Random.Range(0, Possible_Names.Count)]).ToString();

        First_Profile = true;

    }

    IEnumerator Sending_Message()
    {

        return new WaitForSecondsRealtime(Sending_Ratio);
    }
}
