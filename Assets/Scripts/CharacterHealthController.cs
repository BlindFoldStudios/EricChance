using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterHealthController : MonoBehaviour {
    public float playerHealth = 100f;



    void Update()
    {


        if (playerHealth <= 0)
        {
            //SceneManager.LoadScene("EndScreen"); //TODO: This scene currently looks pretty shit, let's change that soon!
            Debug.Log("Player is Dead.");
        }
	}
}
