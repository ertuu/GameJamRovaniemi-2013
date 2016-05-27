using UnityEngine;
using System.Collections;

public class ShaderTest : MonoBehaviour {

	public float repeats;

	// Use this for initialization
	void Start () {
		repeats=transform.localScale.x/transform.localScale.y;
		renderer.material.SetFloat("_Repeations",repeats);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
