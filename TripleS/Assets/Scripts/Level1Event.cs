using UnityEngine;

public class Level1Event : MonoBehaviour
{
    public GameObject Button0;
    public GameObject Button1;
    public GameObject Button2;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            EventManager.TriggerEvent("Quack");
        }

        if (Button0.GetComponent<Button>().pressCheck() == true && Button1.GetComponent<Button>().pressCheck() == true && Button2.GetComponent<Button>().pressCheck() == true)
        {
            Debug.Log("Got eem");
            EventManager.TriggerEvent("Level1Buttons");
        }
    }

}