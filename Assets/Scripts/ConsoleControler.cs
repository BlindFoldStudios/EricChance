using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleControler : MonoBehaviour {

    public LayerMask enemyLayerMask;

    public bool enemyIsInRadius;
    private bool playerIsInRadius;
    private bool moveButtonHasBeenPressed;

    private bool consoleUiIsActive = false;

    [Header("References")]
    public GameObject keyPressHint;
    public GameObject consoleUI;
    public GameObject noEnemyDetectedText;

    public Player player;

    [Header("Enemy List")]
    public List<GameObject> enemiesInsideRadius = new List<GameObject>(); //List of all enemies inside the radius of the console collider.
    private GameObject closestEnemy; //This is used to calculate which enemy is closest to the console pos


    void Start()
    {
		//Get's a reference to the player (maybe change this to something more efficent later on)
        player = FindObjectOfType<Player>();
    }

    void Update ()
    {

		//!TODO: Used for testing
        if (Input.GetKeyDown(KeyCode.Q))
        {
            CalculateCloestEnemy();
            Debug.Log(closestEnemy.transform.position);
        }



		//If tha player is in the radius then give the key press hint.
        if (playerIsInRadius)
        {
			//!TODO: This is getting in the way
			//keyPressHint.SetActive(true);  

			if (Input.GetKeyDown(KeyCode.E))
            {
                if (consoleUiIsActive) //If e is pressed and the console is already pressed then de-activate the console.
                {
                    consoleUI.SetActive(false);
                    consoleUiIsActive = false;

                    player.isTakenControlOf = false; //Makes sure the the player isn't controllable when you are accesing the console.
                    return;
                }


                player.isTakenControlOf = true; //Allows 
                consoleUI.SetActive(true);
                consoleUiIsActive = true;
            }
        }
        else
        {
            keyPressHint.SetActive(false);
        }


    }

    public void CalculateCloestEnemy() //This is used to calculate which enemy is closest to the console pos
    {
        closestEnemy = null;
        float distance = 0f;
        float maxDistance = Mathf.Infinity;

        if (enemiesInsideRadius.Count == 0)
        {
            noEnemyDetectedText.SetActive(true);
            Debug.Log("No Enemy In Radius");
            return;

        }

        foreach (GameObject enemy in enemiesInsideRadius)
        {
            

            distance = Vector2.Distance(transform.position, enemy.transform.position); //Gets the distance between every enemy in the list

            if (distance < maxDistance) //Returns the closest enemy to the console.
            {
                maxDistance = distance;
                closestEnemy = enemy;
            }
        }
    }


    public void KillClosestEnemy()  
    {
        CalculateCloestEnemy();

        Destroy(closestEnemy);
    }


    public void TakeControlOfEnemy()
    {
        CalculateCloestEnemy();

        //!TODO: The ability to take control of enemies using bool in scripts!!!
    }
    

    public void MoveEnemyToConsole()
    {       
         CalculateCloestEnemy();

         closestEnemy.transform.position = transform.position;
        
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            enemyIsInRadius = true;
            enemiesInsideRadius.Add(col.gameObject);

        }

        if (col.gameObject.tag == "Player")
        {
            playerIsInRadius = true;
        }
    }



    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            enemyIsInRadius = false;
            enemiesInsideRadius.Remove(col.gameObject);
        }

        if (col.gameObject.tag == "Player")
        {
            playerIsInRadius = false;
        }
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            playerIsInRadius = true;
        }
    }
}
