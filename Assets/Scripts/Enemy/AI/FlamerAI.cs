using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamerAI : MonoBehaviour {

	//TODO: - Allow the enemy to patrol.
	//TODO: - Check if the enemy can see the player.
	//TODO: - If they can do something (appropiate to the type of enemy).

	private Player playerController;
	private Animator anim;
	private SpriteRenderer spriteRenderer;


	[Header("AI")]
	public bool playerHasBeenSpotted;





	[Header("Patrolling")]
	public float walkSpeed = 1.0f;      // Walkspeed
	public float wallLeft = 0.0f;       // Define wallLeft
	public float wallRight = 5.0f;      // Define wallRight
	float walkingDirection = 1.0f;
	Vector2 walkAmount;
	float originalX; // Original float value


	

	[Header("Raycast")]
	private RaycastHit2D rightRay;
	private Vector2 rightRayPos;

	private RaycastHit2D leftRay;
	private Vector2 leftRayPos;


	void Start()
	{
		playerController = FindObjectOfType<Player>();
		anim = GetComponent<Animator>();

		this.originalX = this.transform.position.x;



		rightRayPos = new Vector2(transform.position.x + 0.5f, playerController.gameObject.transform.position.y);
		leftRayPos = new Vector2(transform.position.x - 0.5f, playerController.gameObject.transform.position.y);


		rightRay = Physics2D.Raycast(rightRayPos, Vector2.right);
		leftRay = Physics2D.Raycast(leftRayPos, Vector2.right);
	}


	void Update()
	{
		
		#region //Patrolling
		walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;


		if (walkingDirection > 0.0f && transform.position.x >= wallRight)
		{


			walkingDirection = -1.0f;
			anim.SetBool("isWalking", true);


			transform.localScale = new Vector3(1, 1, 1);
		}
		else if (walkingDirection < 0.0f && transform.position.x <= wallLeft)
		{
			//
			walkingDirection = 1.0f;
			anim.SetBool("isWalking", true);


			transform.localScale = new Vector3(-1, 1, 1);

		}
		transform.Translate(walkAmount);
		#endregion


		#region //Line Of Sight
		//Has the way seens something?
		if (rightRay.collider != null || leftRay.collider != null)
		{
			//If it has, is it the player
			if (rightRay.collider.tag == "Player" || leftRay.collider.tag == "Player")
			{
				//Player has been spotted
				PlayerHasBeenSpotted();


			}
		}

		#endregion




		//!TODO: Testing for enemy is seen
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//Checks if the gameobject is the flamer

			//Increases walk speed
			walkSpeed = 5;
			//Sets the running animation
			anim.SetBool("isRunning", true);
		}


	}





	/// <summary>
	/// Executes animation changes & Enemy State change
	/// </summary>
	public void PlayerHasBeenSpotted()
	{
		//Setting playerSpotted bool to be true
		playerHasBeenSpotted = true;

		Debug.Log("Player Has Been spotted");




		if (Input.GetKeyDown(KeyCode.Q))
		{
			//Starts the animations
			anim.SetBool("isShooting", true);
		}


		if (Input.GetKeyDown(KeyCode.Space))
		{
			//goes into ending state and back into idle
			anim.SetBool("isNotShooting", true);
		}

	}
}
