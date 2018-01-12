using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikerAI : MonoBehaviour
{

	//TODO: - Allow the enemy to patrol.
	//TODO: - Check if the enemy can see the player.
	//TODO: - If they can do something (appropiate to the type of enemy).

	private Player playerController; 
	private Animator anim;
	private SpriteRenderer spriteRenderer;

	[Header("Patrolling")]
	public float walkSpeed = 1.0f;      // Walkspeed
	public float wallLeft = 0.0f;       // Define wallLeft
	public float wallRight = 5.0f;      // Define wallRight
	float walkingDirection = 1.0f;
	Vector2 walkAmount;
	float originalX; // Original float value


	public bool punchHitPlayer;

	[Header("Raycast")]
	private RaycastHit2D rayHit;
	private GameObject lineOfSight;


	void Start()
	{
		playerController = FindObjectOfType<Player>();
		anim = GetComponent<Animator>();

		this.originalX = this.transform.position.x;

		//!TODO: Make it so it's not the index, that will almost definitely fuck things up in the future
		//lineOfSight = transform.SearchForChild("Line Of Sight").gameObject;
	}


	void Update()
	{
		#region //Patrolling
		walkAmount.x = walkingDirection * walkSpeed * Time.deltaTime;


		if (walkingDirection > 0.0f && transform.position.x >= wallRight)
		{
			//rayHit = Physics2D.Raycast(lineOfSight.transform.position, Vector2.left);

			walkingDirection = -1.0f;
			anim.SetBool("isWalking", true);


			transform.localScale = new Vector3(1, 1, 1);
		}
		else if (walkingDirection < 0.0f && transform.position.x <= wallLeft)
		{
			//rayHit = Physics2D.Raycast(lineOfSight.transform.position, Vector2.right);
			walkingDirection = 1.0f;
			anim.SetBool("isWalking", true);


			transform.localScale = new Vector3(-1, 1, 1);

		}
		transform.Translate(walkAmount);
		#endregion

		if (punchHitPlayer)
		{
			Debug.Log("Punched Player");
		}

		#region //Line Of Sight
		//Has the way seens something?
		if (rayHit.collider != null)
		{
			//If it has, is it the player
			if (rayHit.collider.tag == "Player")
			{
				//INvoke PlayerSeen function
				BikerPlayerSeen();
			}
		}

		#endregion


		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			BikerPlayerSeen();

		}

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


	public void BikerPlayerSeen()
	{

		Debug.Log("Player Seen");
		//Fire raycast, if the player is inside said raycast, then invoke 'PlayerSeen' State.

		//If Player is still in sight
		//TODO: - Run to the player / running animation
		//TODO: - Attacking Player / animation

		//Moves towards the player
		Vector2.MoveTowards(transform.position, playerController.gameObject.transform.position, 100f);

		//Makes the biker start punching animation
		anim.SetBool("isPunching", true);

		//Stops the biker from moving
		walkSpeed = 0;
	}
}
