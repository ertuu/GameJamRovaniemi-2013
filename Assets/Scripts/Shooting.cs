using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour {

	public AudioClip weaponSound;
	public BulletBehaviour prefab;
	public float fireRate;//time in seconds between shots
	private float prevShot;
	public int bulletDamage;

	private bool dDamage = false;
	private float dDamageDuration;
	private float dDamageStartTime;

	// Use this for initialization
	void Start () 
	{
	
		prevShot=0.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (dDamage)
		{
			if (Time.time-dDamageStartTime>dDamageDuration)
				dDamage=false;
		}
	}

	public void Shoot(bool toTheRight, bool toTheUp)
	{
		if (Time.time-prevShot>fireRate)
		{
			AudioSource.PlayClipAtPoint(weaponSound, transform.position, 0.35f); 
			Transform objTransform=transform;
			Vector3 pos=objTransform.position;
			if (toTheRight)
				pos.x+=1;
			else
				pos.x-=1;
			if (toTheUp)
				pos.y+=1;
			BulletBehaviour behav=(BulletBehaviour)Instantiate(prefab,pos,Quaternion.identity);
			if (toTheUp)
			{
				behav.SetDirection(new Vector3(0,1,0));
			}
			else
			{
				behav.SetDirection(toTheRight?new Vector3(1,0,0):new Vector3(-1,0,0));
			}
			if (dDamage)
				behav.SetDamage(2*bulletDamage);
			else
				behav.SetDamage(bulletDamage);
			//TODO:add diagonal/vertical directions
			prevShot=Time.time;
		}
	}

	public void SetDoubleDamage(float duration)
	{
		print ("Double Damage");
		dDamageDuration=duration;
		dDamageStartTime=Time.time;
		dDamage=true;
	}
}
