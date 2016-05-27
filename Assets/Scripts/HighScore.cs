using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Text;

public class HighScore : MonoBehaviour {

	string[] readText = new string[10000];
	string[] temp;
	private bool setScoreFromGame = false;
	public string[] highscores;
	public string [] names;
	public int [] scores;
	public int scoreFromGame = 0;
	public string [] parts;
	public string path=@"..\Assets\highscore.txt";

	// Use this for initialization
	void Start () 
	{
		getFile();

		parts = new string[readText.Length];
		highscores = new string[readText.Length];
		names = new string[readText.Length + 1];
		scores=new int[readText.Length + 1] ;

		for(int i = 0; i < readText.Length; i++)
		{

			parts = readText[i].Split(new char[]{' '});
			names[i] = parts[0];
			scores[i] = int.Parse(parts[1]);
			//scores[i] = int.Parse(highscores[i]);
		}
		// To own function
		bubbelsort();

	}
	
	// Update is called once per frame
	void Update () 
	{
		Score score = (Score)gameObject.GetComponent("Score");
		scoreFromGame = score.score2;

		if(scoreFromGame > 0)
		{
			if(setScoreFromGame == false)
			{
				//print ("heyheyheybheyhdjnglksn");
				scores[scores.Length - 1] = scoreFromGame;
				names[names.Length - 1] = "hey";
				bubbelsort();
				saveHighScore();
			}
			setScoreFromGame = true;
		}
	}

	public void bubbelsort()
	{
	
		for(int f = 0; f < scores.Length; f++)
		{
			for(int i = 0; i < scores.Length; i++)
			{
				//Debug.Log("hei");
				if(i < scores.Length - 1)
				{
					int a = scores[i];
					int b = scores[i + 1];
					string c = names[i];
					string d = names [i + 1];
					
					if(a < b)
					{
						scores[i] = b;
						scores[i + 1] = a;
						names[i] = d;
						names[i + 1] = c;
					}
				}
			}
		}
		for(int i = 0; i < names.Length; i++)
		{
			//Debug.Log("Name"+ names[i]);
			//Debug.Log ("Score"+ scores[i]);
		}
	}

	public void getFile()
	{
		readText = File.ReadAllLines(path);
	}

	public void saveHighScore()
	{
		parts = new string[parts.Length + 1];
		ArrayList temp = new ArrayList();
		//print ("hey");
		for(int i = 0; i < parts.Length; i++)
		{
			temp.Add(names[i]+" "+scores[i].ToString()); 
			print (temp[i]);
			//print (parts[i]);
		}

		//parts = new string[temp];

		for(int i = 0; i < parts.Length; i++)
		{
			temp.CopyTo(parts);
			//print(parts[i]);
		}
		//File.WriteAllLines(path, parts);

		File.WriteAllLines(path, parts);
		//File.WriteAllText(path,parts[1]);
		//print (parts.Length);
	}
}
