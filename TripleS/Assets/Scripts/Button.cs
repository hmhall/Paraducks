
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public GameObject Player;
    public GameObject PastPlayer;
    private bool isPressed = false;
    // Use this for initialization
	void Start () {
        //this.GetComponent<Rigidbody>().Sleep();
	}
    
    private void OnCollisionStay(Collision col)
    {
        isPressed = true;
        Vector3 scale = this.transform.localScale;
        if (col.gameObject.name == "Player" || col.gameObject.name == "PastPlayer(Clone)")
        {
            scale.y = 0.2f;
        }
        this.transform.localScale = scale;
    }
    

    private void OnCollisionExit(Collision col)
    {
        isPressed = false;
        Vector3 scale = this.transform.localScale;
        if (col.gameObject.name == "Player" || col.gameObject.name == "PastPlayer(Clone)")
        {
            scale.y = 0.3f;
        }
        this.transform.localScale = scale;
    }

    public bool pressCheck()
    {
        return isPressed;
    }
}
