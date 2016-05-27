using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour {

	public enum States {Walking, Jumping, Idling};
	public States state;

	public Texture idleTexture;
	public Vector2 iSize;
	public int iMinFrame;
	public int iMaxFrame;
	public float iTimeInterval;
	public bool iRepeat;

	public Texture walkTexture;
	public Vector2 wSize;
	public int wMinFrame;
	public int wMaxFrame;
	public float wTimeInterval;
	public bool wRepeat;

	public Texture jumpTexture;
	public Vector2 jSize;
	public int jMinFrame;
	public int jMaxFrame;
	public float jTimeInterval;
	public bool jRepeat;

	public Vector2 atlasSize;//just a grid, not pixels   1|2|3
	public Vector2 frameCoord;//also, just a grid coord  4|5|6 etc.
	public float timeInterval;
	private float startTime;
	public int minFrame;
	public int maxFrame;
	public bool repeat=true;

	private bool isPlayingAnim=true;

	// Use this for initialization
	void Start () {
		state=States.Idling;
		atlasSize.x=iSize.x;
		atlasSize.y=iSize.y;
		minFrame=iMinFrame;
		maxFrame=iMaxFrame;
		frameCoord=CoordOfFrame(minFrame,atlasSize);
		timeInterval=iTimeInterval;
		repeat=iRepeat;
		renderer.material.SetTexture("_MainTex",idleTexture);
		renderer.material.SetFloat("_HorizontalTotal",atlasSize.x);
		renderer.material.SetFloat("_VerticalTotal",atlasSize.y);
		renderer.material.SetFloat("_HorizontalCoord",frameCoord.x);
		renderer.material.SetFloat("_VerticalCoord",frameCoord.y);
		startTime=Time.time;
		isPlayingAnim=true;

		/*print(renderer.material.GetTexture("_MainTex").width);
		print(renderer.material.GetTexture("_MainTex").height);
		print(renderer.material.GetFloat("_HorizontalTotal").ToString());
		print(renderer.material.GetFloat("_VerticalTotal").ToString());
		print(renderer.material.GetFloat("_HorizontalCoord").ToString());
		print(renderer.material.GetFloat("_VerticalCoord").ToString());*/
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((Time.time-startTime>timeInterval)&&isPlayingAnim)
		{
			startTime=Time.time;
			//print ("Changing "+frameCoord.ToString());
			if (repeat||FrameNumber(frameCoord,atlasSize)<maxFrame)
			{
				frameCoord.x+=1;
				if (frameCoord.x>=atlasSize.x)
				{
					frameCoord.x=0;
					frameCoord.y-=1;
				}
				if (frameCoord.y<0)
					frameCoord.y=atlasSize.y-1;
				if (FrameNumber(frameCoord,atlasSize)>maxFrame)
					frameCoord=CoordOfFrame(minFrame,atlasSize);
				renderer.material.SetFloat("_HorizontalCoord",frameCoord.x);
				renderer.material.SetFloat("_VerticalCoord",frameCoord.y);
			}
		}
	}

	private int FrameNumber(Vector2 coord, Vector2 size)
	{//might be glitching
		return (((int)(size.y)-(int)(coord.y)-1)*(int)(size.x)+(int)(coord.x)+1);
	}

	private Vector2 CoordOfFrame (int fr, Vector2 size)
	{//might be glitching too
		Vector2 result=new Vector2();
		result.y=size.y-(fr/(int)(size.x)+1);
		result.x=(fr-1)%(int)(size.x);
		return result;

	}

	public void Walk(bool inverseTexture)
	{
		//set to walking
		renderer.material.SetFloat("_Inverted",inverseTexture?1:0);
		if (state!=States.Walking)
		{
			state=States.Walking;
			minFrame=wMinFrame;
			maxFrame=wMaxFrame;
			atlasSize=wSize;
			timeInterval=wTimeInterval;
			repeat=wRepeat;
			frameCoord=CoordOfFrame(minFrame,atlasSize);
			renderer.material.SetTexture("_MainTex",walkTexture);
			renderer.material.SetFloat("_HorizontalCoord",frameCoord.x);
			renderer.material.SetFloat("_VerticalCoord",frameCoord.y);
		}
	}

	public void Stop(bool inverseTexture)
	{
		//isWalking=false;
		//set to idle
		//print ("Idling Animation");
		renderer.material.SetFloat("_Inverted",inverseTexture?1:0);
		if (state!=States.Idling)
		{
			state=States.Idling;
			minFrame=iMinFrame;
			maxFrame=iMaxFrame;
			atlasSize=iSize;
			timeInterval=iTimeInterval;
			repeat=iRepeat;
			frameCoord=CoordOfFrame(minFrame,atlasSize);
			renderer.material.SetTexture("_MainTex",idleTexture);
			renderer.material.SetFloat("_HorizontalCoord",frameCoord.x);
			renderer.material.SetFloat("_VerticalCoord",frameCoord.y);
		}
	}

	public void Jump(bool inverseTexture)
	{
		//set to jumping
		//print ("jumping animation");
		//print(state.ToString());
		renderer.material.SetFloat("_Inverted",inverseTexture?1:0);
		if (state!=States.Jumping)
		{
			state=States.Jumping;
			minFrame=jMinFrame;
			maxFrame=jMaxFrame;
			//print (minFrame.ToString()+" "+maxFrame.ToString());
			atlasSize=jSize;
			timeInterval=jTimeInterval;
			repeat=jRepeat;
			frameCoord=CoordOfFrame(minFrame,atlasSize);
			renderer.material.SetTexture("_MainTex",jumpTexture);
			renderer.material.SetFloat("_HorizontalCoord",frameCoord.x);
			renderer.material.SetFloat("_VerticalCoord",frameCoord.y);
		}
	}
	
}
