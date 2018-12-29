using UnityEngine;

public class Level2EventListener : MonoBehaviour
{
    public GameObject Wall01;
    public GameObject Wall02;

    public GameObject DiscoBall;
    private void OnDisable()
    {
        EventManager.StopListening("Level2Button1", this.OpenDoor1);
        EventManager.StopListening("Level2Button1", this.OpenDoor2);
        EventManager.StartListening("NotLevel2Button1", this.CloseDoor1);
        EventManager.StartListening("NotLevel2Button2", this.CloseDoor2);

    }

    private void OnEnable()
    {
        EventManager.StartListening("Level2Button1", this.OpenDoor1);
        EventManager.StartListening("Level2Button2", this.OpenDoor2);
        EventManager.StartListening("NotLevel2Button1", this.CloseDoor1);
        EventManager.StartListening("NotLevel2Button2", this.CloseDoor2);
    }

    private void OpenDoor1()
    {
        if (Wall01.GetComponent<Transform>().position.y < 15) 
            Wall01.transform.Translate(0, 0.1f, 0);
    }

    private void CloseDoor1()
    {
        if (Wall01.GetComponent<Transform>().position.y > 5)
            Wall01.transform.Translate(0, -0.4f, 0);
    }

    private void OpenDoor2()
    {
        if (Wall02.GetComponent<Transform>().position.y < 15)
            Wall02.transform.Translate(0, 0.1f, 0);
    }

    private void CloseDoor2()
    {
        if (Wall02.GetComponent<Transform>().position.y > 5)
            Wall02.transform.Translate(0, -0.4f, 0);
    }
}