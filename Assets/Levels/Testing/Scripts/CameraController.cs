using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public float rotSpeed;
	public float rotationY;
	public GameObject camAim;

	// Use this for initialization
	void Start () 
	{
		rotationY = 0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		rotationY += Input.GetAxis ("Mouse Y") * rotSpeed * Time.deltaTime;
		rotationY = Mathf.Clamp(rotationY, -10f, 30f);

		camAim.transform.position = new Vector3 (camAim.transform.position.x, rotationY, camAim.transform.position.z);
	}
}


//Credit: Karl Pytte