using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour {

    public GameObject Button0;
    public GameObject Button1;
    public GameObject Button2;
    private int spawn = 0;
	// Update is called once per frame
	void Update () {
        
        if (Button0.GetComponent<Button>().pressCheck() == true && Button1.GetComponent<Button>().pressCheck() == true && Button2.GetComponent<Button>().pressCheck() == true && spawn == 0)
        {
            spawn = 1;
            EventManager.TriggerEvent("Level1Buttons");
        }
    }
}
