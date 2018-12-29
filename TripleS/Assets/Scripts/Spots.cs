using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spots : MonoBehaviour {
    public static Vector3 startLocator(string level, int iter)
    {
        //if(level == "testLevel")
        //{
        //    GameObject player = GameObject.FindGameObjectWithTag("player");
        //    position = new Vector3();
        //    position = player.GetComponent<player>().transform.position;
        //    position.z -= iter * 10;
            
        //}
        if(level == "Level1")
        {
            return new Vector3(0, 2, iter * -10);
        }

        if (level == "Level2")
        {
            if (iter == 0)
                return new Vector3(0, 2, 0);
            else if (iter == 1)
                return new Vector3(-7.5f, 2, 27);
            else
                return new Vector3(0, -10, 0);
        }

        return new Vector3(0, 2, 0);
    }
}
