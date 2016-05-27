using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {

	public int s;
	public int h = 0;
	GameObject pl;
	Health h1; 

	// Use this for initialization
	void Start () 
	{
	 	pl = GameObject.FindGameObjectWithTag("Player");
		h1 = pl.gameObject.GetComponent<Health>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		Score score = (Score)gameObject.GetComponent("Score");

		h = h1.health;
		s = score.score;
	}
	void OnGUI() 
	{
		GUI.color = Color.white;
		GUI.Label (new Rect (0.8f * Screen.height , 0.2f * Screen.width, 0.1f * Screen.height , 0.1f * Screen.height ),"Health " +  h.ToString());
		GUI.Label (new Rect (0.9f * Screen.height , 0.2f * Screen.width, 0.1f * Screen.height , 0.1f * Screen.height ),"Score " +  s.ToString());
	}
}
