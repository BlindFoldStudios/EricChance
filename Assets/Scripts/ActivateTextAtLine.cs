using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {

    public TextAsset theText;

    public int startLine;
    public int endLine;

    public TextBoxManager theTextBox;

    public bool requireButtonPress;
    private bool waitForPress;


    public bool destroyWhenActivated;

	void Start ()
    {
        theTextBox = FindObjectOfType<TextBoxManager>();
	}
	

	void Update ()
    {
	    if(waitForPress && Input.GetKeyDown(KeyCode.Space))
        {
            theTextBox.stopPlayerMovement = true;
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();

            if (destroyWhenActivated)
            {
                theTextBox.stopPlayerMovement = false;
                Destroy(gameObject);
            }

        }	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            if (requireButtonPress)  //This will make sure that the text does not load up if the game requires a button press. 
            {
                waitForPress = true;
                return;
            }

            theTextBox.stopPlayerMovement = true;
            theTextBox.ReloadScript(theText);
            theTextBox.currentLine = startLine;
            theTextBox.endAtLine = endLine;
            theTextBox.EnableTextBox();

            if (destroyWhenActivated)
            {
                theTextBox.stopPlayerMovement = false;
                Destroy(gameObject);
            }
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            waitForPress = false; //This will make sure that player can not activate text box when outside zone
        }
    }
    

}
