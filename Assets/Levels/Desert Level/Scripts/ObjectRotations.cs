using UnityEngine;
using System.Collections;

public class ObjectRotations : MonoBehaviour 
{
	public GameObject rockObject;
	public float rightSpeed;
	public float upSpeed;



	void Update () 
	{
		rockObject.transform.Rotate (Vector3.right * Time.deltaTime * rightSpeed);
		rockObject.transform.Rotate (Vector3.up * Time.deltaTime * upSpeed);

//		rockTwo.transform.Rotate (Vector3.right * Time.deltaTime * 12);
//		rockTwo.transform.Rotate (Vector3.up * Time.deltaTime * 22);
//
//		rockThree.transform.Rotate (Vector3.right * Time.deltaTime * 14);
//		rockThree.transform.Rotate (Vector3.up * Time.deltaTime * 19);
//
//		rockFour.transform.Rotate (Vector3.right * Time.deltaTime * 21);
//		rockFour.transform.Rotate (Vector3.up * Time.deltaTime * 26);
//
//		rockFive.transform.Rotate (Vector3.right * Time.deltaTime * 30);
//		rockFive.transform.Rotate (Vector3.up * Time.deltaTime * 12);
	}
}
