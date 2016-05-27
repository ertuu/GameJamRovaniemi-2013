using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	public int score;
	public int score2;

	GameObject g;
	Health h;
	// Use this for initialization

	void Start () 
	{
		score = 0;
		g = GameObject.FindGameObjectWithTag("Player");
		h = g.GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () 
	{
				
		if(h.playerDeath == true)
		{
			finalScore(score);
		}
	}
	public int plusScore(int mScore)
	{
		if(mScore > 0)
		{
			score+=mScore;
		}
		return score;
	}

	public int finalScore(int score)
	{
		{
		 score2 = score;
		 return score2;
		 //print("hey," + score);
		}
	}
}
