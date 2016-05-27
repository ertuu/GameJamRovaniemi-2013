using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {
	public enum BonusTypes {Invul,Speed,Jump,Damage};
	public BonusTypes type;

	// Use this for initialization
	void Start () 
	{
		//type = new BonusTypes();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//print (" Bonus update");
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		//print ("Bonus collision");
		switch (type)
		{
			case BonusTypes.Invul:
				collision.gameObject.GetComponent<Health>().SetInvulnerable(5.0f);
				GameObject.Destroy(gameObject);
			break;

			case BonusTypes.Jump:
				collision.gameObject.GetComponent<Movement>().SetDoubleJump(5.0f);
				GameObject.Destroy(gameObject);
			break;

			case BonusTypes.Speed:
				collision.gameObject.GetComponent<Movement>().SetDoubleSpeed(5.0f);
				GameObject.Destroy(gameObject);
			break;

			case BonusTypes.Damage:
			collision.gameObject.GetComponent<Shooting>().SetDoubleDamage(5.0f);
			GameObject.Destroy(gameObject);
			break;
		}
	}
}
