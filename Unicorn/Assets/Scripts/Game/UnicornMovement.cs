using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornMovement : MonoBehaviour {

	Animator anim;
	Rigidbody rb;
	float runSecounds = 0.0f;
	float unicornAcc = 1.0f;
	float maxBonusSpeedRate = 1.0f;
	float normalTopSpeed = 6.0f;
	float maxBonusSpeed = 0;
	bool inAir = false;
	public GameObject feet;
	float currentSpeed = 0;
	float currentWalkSpeed = 0;
	float currentRunSpeed = 0;
	float currentBonusSpeed = 0;
	float initialPosition = 0;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		maxBonusSpeed = maxBonusSpeedRate * normalTopSpeed;
	}

	void FixedUpdate()
	{
	}
	// Update is called once per frame
	void Update () 
	{
		//Debug.DrawRay (feet.transform.position, Vector3.down, Color.red, 1);
		//desktop
		float currentMove = Input.GetAxis("Vertical");
		if (currentMove > 0) 
		{
			currentMove = 1f;
		}
		 
		bool jump = false;
		bool jump2 = false;
		bool jump3 = false;

		if (Input.inputString == "a") {
			jump = true;
		}
		if (Input.inputString == "s") {
			jump2 = true;
		}
		if (Input.inputString == "d") {
			jump3 = true;
		}

		//mobile
		foreach (Touch touch in Input.touches) 
		{
			//			if (touch.phase != TouchPhase.Ended && touch.phase != TouchPhase.Canceled)
			//				fingerCount++;
			if (touch.position.x < Screen.width / 2) 
			{
				//run
				currentMove = 1;
			} 
			else
			{
				//jump
				if (touch.phase == TouchPhase.Ended) 
				{
					jump3 = true;
				}
			}
		}


		if (currentMove == 1)
		{
			runSecounds = runSecounds + Time.deltaTime;
		}
		else
		{
			runSecounds = 0;
		}

		anim.SetFloat("Speed", currentMove);

		currentBonusSpeed = (runSecounds / unicornAcc);
		currentWalkSpeed = 0.1f * normalTopSpeed;
		currentRunSpeed = 0.9f * normalTopSpeed;

		if (currentBonusSpeed > maxBonusSpeed)
		{
			currentBonusSpeed = maxBonusSpeed;
		}

		currentSpeed = currentWalkSpeed + currentRunSpeed + currentBonusSpeed;

		anim.SetFloat("RunSpeed", 1 + currentBonusSpeed / (normalTopSpeed * maxBonusSpeedRate));

//		string text = " currentSpeed = " + currentSpeed.ToString () + "\n"
//			+ " currentBonusSpeed = " + currentBonusSpeed.ToString () + "\n";
//
//		Debug.Log (text);

		if (!inAir) 
		{
			if (jump)
			{
				anim.SetFloat("JumpSpeed", 1);
				doJump (4.0f, currentSpeed);
			} 
			else if (jump2) 
			{
				anim.SetFloat("JumpSpeed", 0.80f);
				doJump (5.0f, currentSpeed);
			} 
			else if (jump3) 
			{
				anim.SetFloat("JumpSpeed", 0.65f);
				doJump (6.0f, currentSpeed);
			}
			else 
			{
				transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z + currentSpeed * Time.deltaTime);
			}
		}
	}

	void doJump(float jumpSpeedY, float jumpSpeedZ)
	{
		anim.SetBool ("Landed", false);
		Vector3 jumpSpeed = new Vector3 (0, jumpSpeedY, jumpSpeedZ );
		rb.velocity = rb.velocity + jumpSpeed;
		inAir = true;
		initialPosition = transform.position.z;
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "road")
		{
			anim.SetBool("Landed", true);
			inAir = false;
			Debug.Log ("Jump = " + (transform.position.z - initialPosition).ToString ());
			rb.velocity = new Vector3 (0, 0, 0 );
				
		}
	}
}
