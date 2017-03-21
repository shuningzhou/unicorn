using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour {

	public enum ChallengeState
	{
		standby, 
		inGame
	};

	public GameObject challengeObject;
	public ChallengeState challengeState;
	float startGameDistance;

	// Use this for initialization
	void Start () 
	{
		enterStandby ();
	}

	void enterStandby()
	{
		challengeObject.transform.position = new Vector3 (0, 0, -5);
		challengeObject.SetActive (false);
		challengeState = ChallengeState.standby;
	}

	public void enterGame()
	{
		challengeObject.SetActive (true);
		startGameDistance = ChallengeManager.sharedManager.challengeStartPosition + ChallengeManager.sharedManager.distance;
		challengeObject.transform.position = new Vector3 (0, 0, startGameDistance);
		challengeState = ChallengeState.inGame;
	}

	// Update is called once per frame
	void Update () 
	{
		switch (challengeState)
		{
			case ChallengeState.inGame:
				{
					challengeObject.transform.position = new Vector3 (challengeObject.transform.position.x, challengeObject.transform.position.y, (startGameDistance - ChallengeManager.sharedManager.distance));
					checkFinished ();
				}
				break;
			default:
				{
					
				}
				break;
		}
	}
				
	void checkFinished()
	{
		if (challengeObject.transform.position.z < -5) 
		{
			enterStandby ();
		}
	}
}
