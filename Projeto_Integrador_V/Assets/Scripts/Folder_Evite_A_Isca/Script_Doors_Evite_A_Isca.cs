using System.Collections;
using UnityEngine;

public class Script_Doors_Evite_A_Isca : MonoBehaviour
{
    public GameObject Door_Hinge, Spawned_Object;

    public GameObject Seaweed,
        Bait;

    public bool Correct;

    [SerializeField] public Script_Game_Manager_Evite_A_Isca Main_Script;

    Script_Camera_Logic cam;

    private void Awake()
    {
        cam = FindObjectOfType<Script_Camera_Logic>();

    }
    void Start()
    {


        if(gameObject.tag == "Correct_Tag")
        {
            Correct = true;
        }
        else
        {
            Correct = false;
        }
    }

    public IEnumerator Open_Door()
    {

        yield return new WaitUntil(() => cam.Stopped);

        Spawn_Result();

        float Duration = 2f;
        float Animation_Time = 0f;

        Quaternion Initial_Rotation = Quaternion.Euler(-90, 90, 0);
        Quaternion Final_Rotation = Quaternion.Euler(-90, 0, 0);

        while (Animation_Time < Duration)
        {

            Animation_Time += Time.deltaTime;
            float t = Animation_Time / Duration;

            
            t = Mathf.SmoothStep(0, 1, t);

            Door_Hinge.transform.rotation = Quaternion.Lerp(Initial_Rotation, Final_Rotation, t);

            yield return null;
        }

        
        Door_Hinge.transform.rotation = Final_Rotation;

        yield return new WaitForSecondsRealtime(1f);


        if (Correct)
        {
            Main_Script.Right_Ansher();

            yield return new WaitForSecondsRealtime(1f);

            Main_Script.FadeIn = true;
        }
        else
        {
            Main_Script.Defeat();
        }
    }

    void Spawn_Result()
    {

        if (Correct)
        {

            Spawned_Object = Instantiate(Seaweed, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, (gameObject.transform.position.z + 2f)), Quaternion.identity);
        }
        else
        {

            Spawned_Object = Instantiate(Bait, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, (gameObject.transform.position.z + 2f)), Quaternion.identity);
        }
    }
}
