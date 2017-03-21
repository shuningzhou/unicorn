using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : MonoBehaviour {
	
	public static ChallengeManager sharedManager = null;
	public GameObject challengeLvl1;
	public float distance = 0;
	public int lastChallengePosition = 5;
	public int challengeStartPosition = 150;
	public float gameSpeed = 0;
	public float currentGameSpeed = 0;
	// Use this for initialization
	List<Challenge> challenges_1 = new List<Challenge>();

	void Awake()
	{
		if (sharedManager == null) {
			sharedManager = this;
		} else if (sharedManager != this) {
			Destroy (gameObject);
			return;
		}
		Debug.Log ("challenge manager awake");
	}

	void Start () 
	{
		createChallenges ();
		currentGameSpeed = gameSpeed;
	}
	
	// Update is called once per frame
	void Update () 
	{
		distance = distance + currentGameSpeed * Time.deltaTime;
		int disInt = (int)distance / 1;

		if (disInt > lastChallengePosition) 
		{
			if (Random.Range (0, 1.0f) > 0.98f) 
			{
				addChallenge ();
			}

			lastChallengePosition = disInt;
			if (lastChallengePosition == 300) {
				gameSpeed = 40;
			}
			else if (lastChallengePosition == 600) {
				gameSpeed = 45;
			}
			else if (lastChallengePosition == 1200) {
				gameSpeed = 50;
			}
			else if (lastChallengePosition == 3000) {
				gameSpeed = 55;
			}
			else if (lastChallengePosition == 6000) {
				gameSpeed = 60;
			}
			else if (lastChallengePosition == 10000) {
				gameSpeed = 65;
			}
			Debug.Log ("lastChallengePosition Int = " + lastChallengePosition.ToString ());
		}
	}

	void createChallenges()
	{
		for (int x = 0; x < 150; x ++)
		{
			GameObject go = Instantiate (challengeLvl1, Vector3.zero, Quaternion.identity);
			Challenge c = go.GetComponent<Challenge> ();
			challenges_1.Add (c);
		}
	}

	void addChallenge()
	{
		Challenge c = randomChallenge (1);
		if (c) {
			c.enterGame ();
		}
	}

	Challenge randomChallenge(float factor)
	{
		return getChallenge1 ();
	}

	Challenge getChallenge1()
	{
		return getStandby (challenges_1);
	}

	Challenge getStandby(List<Challenge> lists)
	{
		foreach (Challenge c in lists) {
			if (c.challengeState == Challenge.ChallengeState.standby) {
				return c;
			}
		}

		return null;
	}
}
