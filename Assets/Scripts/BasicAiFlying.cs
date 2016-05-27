using UnityEngine;
using System.Collections;

public class BasicAiFlying : MonoBehaviour {
	public GameObject target;
	public AnimationManager animator;
	public bool isFacingRight=true;
	public float speed;
	public bool isMoving=true;
	
	private Shooting shooter;
	
	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player");
		shooter=this.gameObject.GetComponent<Shooting>();
	}
	
	// Update is called once per frame
	void Update () {
		TryMoving();
		CheckShooting();

	}
	
	private void TryMoving()
	{
		Vector2 origin=new Vector2 (this.transform.position.x+(isFacingRight?.3f:-.3f),
		                            this.transform.position.y);
		RaycastHit2D hit=Physics2D.Raycast(origin, new Vector2(isFacingRight?1:-1,0),Mathf.Infinity,~(1<<9));
		print(hit.collider.gameObject.ToString());
		if (hit!=null)//horizontal raycast
		{
			Vector3 point=new Vector3(hit.point.x,hit.point.y,0);
			
			//Debug.Log((point-transform.position).sqrMagnitude.ToString());
			
			if ((point-transform.position).sqrMagnitude<1)
				isFacingRight=!isFacingRight;
		}
		/*hit=Physics2D.Raycast(origin,new Vector2(0,-1));
		if (hit!=null)//vertical raycast
		{
			Vector3 point=new Vector3(hit.point.x,hit.point.y,0);
			
			//Debug.Log((point-transform.position).sqrMagnitude.ToString());
			
			if ((point-transform.position).sqrMagnitude>9)
				isFacingRight=!isFacingRight;
		}*/
		else
		{
			isFacingRight=!isFacingRight;
		}
		if (isMoving)
		{
			animator.Walk(isFacingRight);
			if (isFacingRight)
			{
				Vector3 newPos=transform.position;
				newPos.x+=speed*Time.deltaTime;
				//Debug.Log(newPos.ToString());
				transform.position=newPos;
			}
			else
			{
				Vector3 newPos=transform.position;
				newPos.x-=speed*Time.deltaTime;
				//Debug.Log(newPos.ToString());
				transform.position=newPos;
			}
		}
	}
	
	private void CheckShooting()
	{
		isMoving=true;
		Vector3 targetPosition=target.transform.position;
		Vector3 position=this.gameObject.transform.position;
		//sqrmagnitude is faster
		if ((targetPosition-position).sqrMagnitude<625.0)//25*25
		{
			//check direction and height now
			if (((targetPosition.x<position.x)&&(!isFacingRight))||
			    ((targetPosition.x>position.x)&&(isFacingRight)))
			{
				if (Mathf.Abs(targetPosition.x-position.x)<3.0f)
					isMoving=false;
				
				if (Mathf.Abs(targetPosition.y-position.y)<5.0f)
				{
					shooter.Shoot(isFacingRight, false);
				}
			}
		}
	}
}