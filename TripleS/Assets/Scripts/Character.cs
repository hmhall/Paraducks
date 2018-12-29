using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Class for player control of current iteration aswell as recording commands for past iterations,
 * instantiating and placing current and past iterations, and keeping track of all said iterations.
 */
public class Character : MonoBehaviour {

    public string Level; //Level string specified in editor

    //Set speed of player
    public static float verSpeed = 15.0f;
    public static float horSpeed = 12.0f;

    //Set mouse speed of player
    public static float sensitivity = 5.0f;
    public static float smoothing = 2.0f;

    //Declare variables
    private Vector2 mouseLook; //value of mouse's x and y rotation, must be reset to reset rotation 
    private Vector2 smoothV; //velocity of mouse
    private int iterNum = 0; //current iteration the player is playing as
    public static Vector2 yConstraints = new Vector2(-90, 45);

    //Declare data structures
    public static Iterations allRec; // Every iteration fully recorded so far. SHOULD BE USEFUL FOR JUMPING BACK ITERATIONS IN CASE OF PARADOX.
    public static Iteration currRec; // The iteration we are currently recording for

    //timer variable used for text
    private int timer = 0;
    private bool showText = false;

    GameObject character;
    public GameObject PastSelf; //Past character prefab passed to this script in editor
    Camera view;

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.Locked; // Hide mouse

        allRec = new Iterations(); //Instantiate data structures
        currRec = new Iteration();

        character = this.gameObject; //Player Object is object this script is attached to
        view = character.GetComponentInChildren<Camera>(); //Player view, child of player object
    }

    // Update is called once per frame
    void FixedUpdate () {
        //Keyboard movement
        float translation = Input.GetAxis("Vertical") * verSpeed; //y input
        float straffe = Input.GetAxis("Horizontal") * horSpeed; //x input
        Vector2 mvMent = new Vector2(straffe, translation); //Save inputs for recording
        translation *= Time.deltaTime; //calculate distance
        straffe *= Time.deltaTime;
        translation = Mathf.Clamp(translation, translation / 2, translation);
        character.transform.Translate(straffe, 0, translation);

        //Mouse Movement
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")); //input
        Vector2 mseMvment = md; //Save input for recording
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * 2/3 * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;
        mouseLook.y = Mathf.Clamp(mouseLook.y, yConstraints.x, yConstraints.y); //limit how far up and down the player can look

        //Rotate view
        view.transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

        //record full command
        currRec.Add(next: new Command(mvMent, mseMvment));

        //checks to see if characters are within line of sight of each other
        //int layerMask = 1 << 9;
        //layerMask = ~layerMask;

        //for (int i = 0; i < allRec.Count(); i++)
        //{
        //    Debug.Log("allRec.Count = " + allRec.Count());
        //    Iteration itor = allRec[i];
        //    if (Physics.Linecast(transform.position, itor.avatar.transform.position, layerMask))
        //    {
        //        Debug.Log("No LOS");
        //    }
        //    else
        //    {
        //        Debug.Log("LOS");
        //    }
        //}

            //If next iteration is begun
        if (Input.GetKeyDown("space"))
        {
            //instantiate PastSelf for recorded iteration and save iteration to allRec
            Iteration temp = currRec; //create duplicate
            temp.avatar = Instantiate(PastSelf, Spots.startLocator(Level, iterNum), new Quaternion(0,0,0,1)); //instantiate
            temp.avatar.GetComponentInChildren<Camera>().enabled = false; //disable camera to avoid conflicts
            temp.avatar.GetComponent<PastCharacter>().iterNum = iterNum++; //keep track of and iterate iteration number; MAY BE USEFUL FOR JUMPING BACK ITERATIONS IN CASE OF PARADOX
            temp.avatar.GetComponent<PastCharacter>().inputList = temp; //apply recorded commands to PastSelf
            allRec.Add(temp); //Add iteration to list of iterations

            //Reset recording iteration
            currRec = new Iteration();

            //place player for next iteration
            character.transform.position = Spots.startLocator(Level, iterNum);
            character.transform.localRotation = Quaternion.Euler(0, 0, 0);
            mouseLook.y = mouseLook.x = 0;
        }

        //Control of mouse visibility
        if (Input.GetKeyDown("escape")) //free mouse if esc is pressed
            Cursor.lockState = CursorLockMode.None;
        if (Input.GetMouseButtonDown(0)) //hide mouse if game is clicked
            Cursor.lockState = CursorLockMode.Locked;

        //increment timing function
        timer++;
        //after around 3 seconds hide the text
        if (timer >= 180)
            showText = false;
    }
    public void paradox()
    {
        currRec = new Iteration();
        character.transform.position = Spots.startLocator(Level, iterNum);
        character.transform.localRotation = Quaternion.Euler(0, 0, 0);
        mouseLook.y = mouseLook.x = 0;
        showText = true;
        timer = 0;
        foreach (Iteration itor in allRec.iterations)
        {
            itor.avatar.GetComponent<PastCharacter>().paradox();
        }

    }
    void OnGUI()
    {
        if (showText == true)
        {
            var style = new GUIStyle { fontSize = 48, fontStyle = FontStyle.Bold };
            GUI.Label(new Rect(100, 100, 300, 100), "Paradox! You have been seen by your past self!");
        }
    }
}
