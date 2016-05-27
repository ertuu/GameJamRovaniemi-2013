using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {
	public Texture background;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time > 5)
			Application.LoadLevel("Level1");
	}
	void OnGUI()
	{
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), background);
		
	}
	private void MainMenu()
	{

	}
}
