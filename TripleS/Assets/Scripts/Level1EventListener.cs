using UnityEngine;

public class Level1EventListener : MonoBehaviour
{
    public GameObject DiscoBall;
    private void OnDisable()
    {
        EventManager.StopListening("Quack", this.SomeFunction);
        EventManager.StopListening("Level1Buttons", this.SomeFunction);

    }

    private void OnEnable()
    {
        EventManager.StartListening("Quack", this.SomeFunction);
        EventManager.StartListening("Level1Buttons", this.SomeFunction);
    }

    private void SomeFunction()
    {
        Debug.Log("GOT EEM");
        Instantiate(DiscoBall, new Vector3(0, 5, 70), new Quaternion(0, 0, 0, 1));
        Destroy(this.gameObject);
    }

    private void Update()
    {
        var currEvent = Event.current;
        if (currEvent != null)
        {
            if (currEvent.ToString() == "Level1Buttons")
            {
            }
        }
    }
}