using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour {

    public GameObject detectorBody;
    int layerMask;
    GameObject Player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        layerMask = 1 << 9;
        layerMask = ~layerMask;
	}

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("calling method ontrigger enter");
        if (collision.tag == "Player")
        {
            //Debug.Log("here we are");
            if (!(Physics.Linecast(collision.transform.position, detectorBody.transform.position, layerMask)))
            {
                Debug.Log("I can see your ass");
                collision.gameObject.GetComponent<Character>().paradox();

            }

        }
    }
}
