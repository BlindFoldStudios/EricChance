using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShooting : MonoBehaviour {

    [Header ("Damage")]
    public float damageAmount = 25;

    private RandomEnemySpawner _randomEnemySpawner;

    public bool enemyHit = false;

    void Start()
    {
        _randomEnemySpawner = FindObjectOfType<RandomEnemySpawner>();

    }

    void Update()
    {


        //TODO: This is old code and will only fire to the right. Change this fucking A.S.A.P
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = new Vector2(transform.position.x + 1, transform.position.y);
            RaycastHit2D ray = Physics2D.Raycast(rayPos, Vector2.right, Mathf.Infinity);

            

            if (ray != null && ray.collider != null && ray.collider.tag == "Enemy")
            {
                //enemyHit = true;
                Debug.Log("Hit Enemy");
            }
            else
            {
                enemyHit = false;
            }
        }


        

    }
    
}
