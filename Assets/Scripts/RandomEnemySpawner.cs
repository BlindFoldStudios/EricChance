using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomEnemySpawner : MonoBehaviour {


    //TODO: Only spawn enemies when the dialogue box is not open!
    [Header("Random Spawn Point")]
    private float spawnPointYRandom;

    [Header("GameObject References")]
    public GameObject enemyPrefab;
    [HideInInspector] //This will hide the variable in the inspector! obviousl....
    public GameObject enemyGameObject;

    [Header("Script References")]
    public TextBoxManager textBoxManager;
    


    public Transform player;

    public bool gameIsStarted = true; //TODO: Please for the love of god take this out sometime, it's like entirely useless.


	


	

	void Start()
	{        
            StartCoroutine(SpawnTimer());        
    }
	

    IEnumerator SpawnTimer()
    {
            while (gameIsStarted) //Not a real boolean, be careful! he bites..
            {
               // if(characterControl.levelIsOver == true)
              //  {
             //       break;
             //   }

                yield return new WaitForSeconds(Random.Range(2, 3));
                SpawnEnemyPrefab();
            }
    }
                   
	void SpawnEnemyPrefab() 
	{
		spawnPointYRandom = Random.Range (-2.26f, -4.32f); //This will randomly generate a number to be used as the y;
		Vector3 pos = new Vector3(player.position.x + 40, spawnPointYRandom,-1);  
        enemyGameObject = (GameObject)Instantiate(enemyPrefab, pos, transform.rotation); //This will instantiate the object

	} 
}
