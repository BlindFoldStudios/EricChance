using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TextBoxManager : MonoBehaviour {
    
    public GameObject textBox;
    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public Player _player;

    public bool isActive;  //This is used to active text.

    public bool stopPlayerMovement; //This will stop the player when text box is active.

    private bool isTyping = false;
    private bool cancelTyping = false;

    public float typeSpeed;




    void Start()
    {
        _player = FindObjectOfType<Player>();

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));  //This check if there is actually a text file.          
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }

        if (isActive)
        {
            EnableTextBox(); //This will make sure the text is always there.. to be read.
        }
        else
        {
            DisableTextBox();
        }

    }

 
    
    void Update()
    {

    

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping && textBox.active)
            {
                currentLine += 1;

                if (currentLine > endAtLine)
                {                    
                    DisableTextBox();                   
                }
                else
                {
                    StartCoroutine(TextScroll(textLines[currentLine])); //This will start the text to scroll                
                }

            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;            
            }
        }
            
        

        if(currentLine > endAtLine)
        {
            DisableTextBox();
        }
    }


    private IEnumerator TextScroll (string lineOfText)
    {
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;

        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
     
        }

        theText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;

    }

    public void EnableTextBox()
    {
        textBox.SetActive(true);

        if (stopPlayerMovement)
        {
            //player.canMove = false;
        }

        StartCoroutine(TextScroll(textLines[currentLine]));  //This will start the text to scroll.
    }

    public void DisableTextBox()
    {

        textBox.SetActive(false);        

        //player.canMove = true;
    }

    public void ReloadScript(TextAsset theText)
    {
        if(theText != null)
        {
            textLines = new string[1];
            textLines = (theText.text.Split('\n'));

        }

    }
}
