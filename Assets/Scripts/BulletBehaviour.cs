using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(AudioSource))]

public class BulletBehaviour : MonoBehaviour {

	public float speed;
	private float spawnTime;
	private float lifetime;
	public int hitDamage;
	private Vector3 direction;
	
	//public AudioClip[] BulletHit;
	public BulletBehaviour()
	{
		direction=new Vector3(1,0,0);
	}

	public BulletBehaviour (Vector3 dir)
	{
		direction=dir;
	}

	
	// Use this for initialization
	void Start () {
	 	//speed=25;
		lifetime=10.0f;
		spawnTime=Time.time;
		//hitDamage = 3;
		direction.Normalize();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time-spawnTime>lifetime)
		{
			Destroy(gameObject);
		}
		//-print(transform.position.ToString());
		transform.Translate(direction*speed*Time.deltaTime);
	}

	public void SetDirection (Vector3 newDir)
	{
		direction=newDir;
		direction.Normalize();
	}

	public void SetDamage (int newDamage)
	{
		if (newDamage>0) 
			hitDamage=newDamage;
	}
	
	
	void OnCollisionEnter2D (Collision2D collision)
	{
			//print ("Collided");
			
			//AudioSource.PlayClipAtPoint(BulletHit[Random.Range(0, 3)], transform.position, Random.Range(0.65f, 0.85f));
			
			Health H;
			H=collision.collider.GetComponent<Health>();
			//print (collision.gameObject.name.ToString());
			//print(H==null);
		      if (H!=null)
				H.ApplyDamage(hitDamage);
			Destroy(gameObject);
	}
}
