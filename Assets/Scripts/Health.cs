using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int points;
	public int maxHealth=5;
	public int armor = 4;

	public bool isInvulnerable=false;
	public bool playerDeath = false;

	private float invulDuration;
	private float invulStartTime;
	public int health;

	// Use this for initialization
	void Start () {
		health=maxHealth;
	
	}
	
	// Update is called once per frame
	void Update () {
		if(health <= 0)
		{
			Die();
			health = 0;
		}
		if (isInvulnerable)
		{
			if (Time.time-invulStartTime>invulDuration)
				isInvulnerable=false;
		}
	}

	public void ApplyDamage(int damage)
	{
		if (!isInvulnerable)
		{
			//print ("getting"+damage.ToString()+"damage");
			int resultingDamage=damage-armor;
			resultingDamage=resultingDamage<1?1:resultingDamage;
			health-=resultingDamage;
		}
		Debug.Log(health.ToString());
	}

	public void ApplyHealing(int healing)
	{
		int resultingHealth=health+healing;
		resultingHealth=resultingHealth>maxHealth?maxHealth:resultingHealth;
		health=resultingHealth;
	}

	public void SetInvulnerable (float invTime)
	{
		print ("Getting invul");
		invulDuration=invTime;
		invulStartTime=Time.time;
		isInvulnerable=true;
	}

	public void Die()
	{
		Debug.Log("death");
		// die animation
		if(this.tag == "Enemy")
		{
			Score s = GameObject.Find("Scoremanager").GetComponent<Score>();
			s.plusScore(points);
			Destroy(this.gameObject);
		}
		else if(this.tag == "Player")
		{
			playerDeath = true;
			Application.LoadLevel("Menu");
			//Score s = GameObject.Find("Scoremanager").GetComponent<Score>();
		}
	}

	public bool isDead()
	{
		return (health<=0);
	}
}


