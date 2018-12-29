using UnityEngine;

public class Level2Event : MonoBehaviour
{
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            EventManager.TriggerEvent("Quack");
        }

        if (Button1.GetComponent<Button>().pressCheck() == true)
        {
            EventManager.TriggerEvent("Level2Button1");
        }
        else
        {
            EventManager.TriggerEvent("NotLevel2Button1");

        }

        if (Button2.GetComponent<Button>().pressCheck() == true)
        {
            EventManager.TriggerEvent("Level2Button2");
        } else
        {
            EventManager.TriggerEvent("NotLevel2Button2");
        }
    }

}