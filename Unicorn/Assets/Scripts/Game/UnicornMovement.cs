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

	float currentSpeed = 0;
	float currentWalkSpeed = 0;
	float currentRunSpeed = 0;
	float currentBonusSpeed = 0;
	float initialPosition = 0;
	float turnRate = 60.0f;
	float maxTurn = 60.0f;
	float currentTurn = 0;
	public float currentTurnRate = 60.0f;

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

		if (ChallengeManager.sharedManager.distance > 100 && currentTurnRate <  300)
		{
			currentTurnRate = turnRate + (ChallengeManager.sharedManager.distance - 100) / 6;
		}

		bool turnLeft = Input.GetKey (KeyCode.LeftArrow);
		bool turnRight = Input.GetKey (KeyCode.RightArrow);

		//mobile
		foreach (Touch touch in Input.touches) {
			if (touch.position.x < Screen.width/2)
			{
				turnLeft = true;
			}
			else if (touch.position.x > Screen.width/2)
			{
				turnRight = true;
			}
		}

		if (turnRight&&!turnLeft) {
			float turn = currentTurnRate * Time.deltaTime;
			doTurn (turn);
		} else if (turnLeft&&!turnRight) {
			float turn = currentTurnRate * Time.deltaTime;
			doTurn (-turn);
		} else if (turnLeft && turnRight) 
		{
			if (!inAir) 
			{
				doJump ();
			}
		} else {
			if (currentTurn < - 3) {
				float turn = currentTurnRate * Time.deltaTime;
				doTurn (turn);
			} else if (currentTurn > 3) {
				float turn = currentTurnRate * Time.deltaTime;
				doTurn (-turn);
			} else {
				currentTurn = 0;
				gameObject.transform.rotation = Quaternion.identity;
			}
		}

		ChallengeManager.sharedManager.currentGameSpeed = ChallengeManager.sharedManager.gameSpeed * Mathf.Cos (currentTurn * Mathf.Deg2Rad);

	}

	void doTurn(float turn)
	{
		float move = Mathf.Sin (currentTurn * Mathf.Deg2Rad) * ChallengeManager.sharedManager.gameSpeed * Time.deltaTime / 2.5f;

		gameObject.transform.position = new Vector3 (gameObject.transform.position.x + move, gameObject.transform.position.y, gameObject.transform.position.z);

		if ((currentTurn < -maxTurn  && turn < 0) || (currentTurn > maxTurn && turn > 0)) 
		{
			return;
		}

		transform.Rotate(0,turn,0);
		currentTurn = currentTurn + turn;
	}

	void doJump()
	{
		inAir = true;
		anim.SetBool("Landed", false);
		rb.velocity = new Vector3 (0, 4.0f, 0 );
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "road")
		{
			anim.SetBool("Landed", true);
			inAir = false;
		}
	}
}
