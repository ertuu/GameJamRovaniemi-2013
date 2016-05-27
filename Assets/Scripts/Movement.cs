//also add rigidbody2d
//and set its gravity to zero

using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public AudioClip playerWalk;
	public AnimationManager animator;
	
	public float maxForwardSpeed = 20.0f;
	public float mAcceleration = 20.0f;
	public float mJumpForce = 3.0f;
	public float mDragForce = 10.0f;
	public float mGravitation = 10.0f;
	
	private float dJumpDuration;
	private float dJumpStartTime;
	private bool dJump = false;
	private float dSpeedDuration;
	private float dSpeedStartTime;
	private bool dSpeed = false;
	
	public bool isJumping = false;
	public bool isGrounded = false;
	public bool isLookingUp = false;
	public bool isLookingDown=false;
	public bool isFacingRight=true;
	
	private uint touchingFlags=0x0000;//0001-top,0010-bottom,0100-right,1000-left
	public bool touchingTop
	{
		get {return (touchingFlags&0x0001)!=0;}
	}
	public bool touchingBottom
	{
		get {return (touchingFlags&0x0010)!=0;}
	}
	public bool touchingRight
	{
		get {return (touchingFlags&0x0100)!=0;}
	}
	public bool touchingLeft
	{
		get {return (touchingFlags&0x1000)!=0;}
	}
	public Vector2 mSpeedRightNow = new Vector2(0.0f,0.0f);
	public Transform transformOfThis;
	private Shooting shooter;
	
	
	// Use this for initialization
	void Start () 
	{
		transformOfThis=this.transform;
		shooter=gameObject.GetComponent<Shooting>();
	}
	
	void FixedUpdate()//priority is first fixed update, then oncollisions, then update
	{
		//print (touchingBottom.ToString()+touchingTop.ToString()+touchingLeft.ToString()+touchingRight.ToString());
		if (touchingTop&&mSpeedRightNow.y>0)
			mSpeedRightNow.y=0;
		if (touchingBottom&&mSpeedRightNow.y<0)
			mSpeedRightNow.y=0;
		if (touchingRight&&mSpeedRightNow.x>0)
			mSpeedRightNow.x=0;
		if (touchingLeft&&mSpeedRightNow.x<0)
			mSpeedRightNow.x=0;
		if (touchingBottom)
		{
			//TODO just one more animator use i dont want to lsoe
			if (!isGrounded)
				animator.Stop(!isFacingRight);
			isJumping=false;
			isGrounded=true;
		}
		else
		{
			animator.Jump(!isFacingRight);
		}
		touchingFlags=0x0000;
	}
	
	// Update is called once per frame
	void Update () 
	{
		RenewBonuses();
		ProcessInputs();
		mSpeedRightNow.y-=mGravitation*Time.deltaTime;
		TryToMove();
		/*

		Vector3 initialPosition =transformOfThis.position;




		if (isGrounded)
			mSpeedRightNow.y = 0.0f;

		initialPosition.x+=mSpeedRightNow.x*Time.deltaTime;
		initialPosition.y+=mSpeedRightNow.y*Time.deltaTime;
		transformOfThis.position=initialPosition;
*/
	}
	
	private void TryToMove()
	{
		Vector3 initialPosition =transformOfThis.position;
		if ((mSpeedRightNow.x<0&&!touchingLeft)||
		    (mSpeedRightNow.x>0&&!touchingRight))
		{
			initialPosition.x+=mSpeedRightNow.x*Time.deltaTime;
			if (touchingBottom)
			{
				animator.Walk(!isFacingRight);
				//AudioSource.PlayClipAtPoint(playerWalk, transform.position, 0.35f); 
				if (!audio.isPlaying)
					audio.Play();
			}
		}
		else
		{
			if (touchingBottom)
				animator.Stop(!isFacingRight);
		}
		if ((mSpeedRightNow.y>0&&!touchingTop)||
		    (mSpeedRightNow.y<0&&!touchingBottom))
		{
			initialPosition.y+=mSpeedRightNow.y*Time.deltaTime;
		}
		transformOfThis.position=initialPosition;
		
	}
	
	private void RenewBonuses()
	{
		if (dJump)
		{
			if (Time.time-dJumpStartTime>dJumpDuration)
				dJump=false;
		}
		if (dSpeed)
		{
			if (Time.time-dSpeedStartTime>dSpeedDuration)
				dSpeed=false;
		}
	}
	
	private void ProcessInputs()
	{
		if(Input.GetKey("left"))
		{
			if (dSpeed)
			{
				mSpeedRightNow.x-=2*mAcceleration*Time.deltaTime;
			mSpeedRightNow.x=mSpeedRightNow.x<2*maxForwardSpeed*-1?2*maxForwardSpeed*-1:mSpeedRightNow.x;
			}
			else
			{
				mSpeedRightNow.x-=mAcceleration*Time.deltaTime;
			mSpeedRightNow.x=mSpeedRightNow.x<maxForwardSpeed*-1?maxForwardSpeed*-1:mSpeedRightNow.x;
			}
			isFacingRight=false;
		}
		else if(Input.GetKey("right"))
		{
			if (dSpeed)
			{
				mSpeedRightNow.x+=2*mAcceleration*Time.deltaTime;
			mSpeedRightNow.x=mSpeedRightNow.x>2*maxForwardSpeed?2*maxForwardSpeed:mSpeedRightNow.x;
			}
			else
			{
				mSpeedRightNow.x+=mAcceleration*Time.deltaTime;
			mSpeedRightNow.x=mSpeedRightNow.x>maxForwardSpeed?maxForwardSpeed:mSpeedRightNow.x;
			}
			isFacingRight=true;
		}
		else 
		{
			if (mSpeedRightNow.x>=0)
			{
				mSpeedRightNow.x-=mDragForce*Time.deltaTime;
			mSpeedRightNow.x=mSpeedRightNow.x<0.0f?0.0f:mSpeedRightNow.x;
			}
			else
			{
				mSpeedRightNow.x+=mDragForce*Time.deltaTime;
			mSpeedRightNow.x=mSpeedRightNow.x>0.0f?0.0f:mSpeedRightNow.x;
			}
			
		}
		
		if(Input.GetButton("Jump"))
		{
			if(!isJumping&&isGrounded)
			{
				//TODO :not actually "to do", just animator here
				animator.Jump(!isFacingRight);
				if (dJump)
					mSpeedRightNow.y=2*mJumpForce;
				else
					mSpeedRightNow.y=mJumpForce;
				isJumping = true;
				isGrounded=false;
			}
		}

		isLookingUp=Input.GetKey("up");
		if (Input.GetButton ("Fire1"))
		{
			shooter.Shoot(isFacingRight, isLookingUp);
			/*if (isLookingUp)//it was supposed to make shooting up anim, but it's not working
			{
				//changing texture manually
				Texture newTex;
				if (isJumping)
					newTex=Resources.Load<Texture>("Textures/shooting_up_jump.png");
				else
					newTex=Resources.Load<Texture>("Textures/shooting_up_still.png");
				renderer.material.SetTexture("_MainTex",newTex);
				renderer.material.SetFloat("_HorizontalTotal",2);
				renderer.material.SetFloat("_VerticalTotal",2);
				renderer.material.SetFloat("_HorizontalCoord",1);
				renderer.material.SetFloat("_VerticalCoord",1);
				renderer.material.SetFloat("_Inverted",isFacingRight?0:1);
				animator.state=AnimationManager.States.Idling;

			}*/
		}
		/*else if((Input.GetButton ("Fire1")) && Input.GetButton("Up"))
		{
			shooter.Shoot(isFacingRight, isLookingUp);
		}*/
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{
		Vector2 normal=collision.contacts[0].normal;
		if (Mathf.Abs(normal.x)>=Mathf.Abs(normal.y))
		{
			if (normal.x>=0)
			{
				touchingFlags|=0x1000;
			}
			else
			{
				touchingFlags|=0x0100;
			}
		}
		else
		{
			if (normal.y>=0)
			{
				touchingFlags|=0x0010;
			}
			else
			{
				touchingFlags|=0x0001;
			}
		}
		/*Collider2D collider=collision.collider;
		if (collider.transform.position.y<transform.position.y 
		    && collision.gameObject.layer!=10
		    && collision.gameObject.layer!=12)
		{
			isGrounded=true;
			isJumping=false;
		}*/
		//Debug.Log("HAAAAAAI");
	}
	
	void OnCollisionStay2D(Collision2D collision)
	{
		Vector2 normal=collision.contacts[0].normal;
		if (Mathf.Abs(normal.x)>=Mathf.Abs(normal.y))
		{
			if (normal.x>=0)
			{
				touchingFlags|=0x1000;
			}
			else
			{
				touchingFlags|=0x0100;
			}
		}
		else
		{
			if (normal.y>=0)
			{
				touchingFlags|=0x0010;
			}
			else
			{
				touchingFlags|=0x0001;
			}
		}
	}
	
	void OnCollisionExit2D (Collision2D collision)
	{
		if (collision.collider.transform.position.y<transform.position.y)
		{
			isGrounded=false;
			isJumping=false;
		}
		//Debug.Log("Bye!");
	}
	
	public void SetDoubleJump(float duration)
	{
		print ("Double Jump");
		dJumpDuration=duration;
		dJumpStartTime=Time.time;
		dJump=true;
	}
	
	public void SetDoubleSpeed(float duration)
	{
		print ("Double Speed");
		dSpeedDuration=duration;
		dSpeedStartTime=Time.time;
		dSpeed=true;
	}
}