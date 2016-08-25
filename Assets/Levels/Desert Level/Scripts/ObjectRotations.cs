using UnityEngine;
using System.Collections;

public class ObjectRotations : MonoBehaviour 
{
	public GameObject rockOne, rockTwo, rockThree, rockFour, rockFive;
//	public float rightSpeed;
//	public float upSpeed;



	void Update () 
	{
		rockOne.transform.Rotate (Vector3.right * Time.deltaTime * 25);
		rockOne.transform.Rotate (Vector3.up * Time.deltaTime * 15);

		rockTwo.transform.Rotate (Vector3.right * Time.deltaTime * 12);
		rockTwo.transform.Rotate (Vector3.up * Time.deltaTime * 22);

		rockThree.transform.Rotate (Vector3.right * Time.deltaTime * 14);
		rockThree.transform.Rotate (Vector3.up * Time.deltaTime * 19);

		rockFour.transform.Rotate (Vector3.right * Time.deltaTime * 21);
		rockFour.transform.Rotate (Vector3.up * Time.deltaTime * 26);

		rockFive.transform.Rotate (Vector3.right * Time.deltaTime * 30);
		rockFive.transform.Rotate (Vector3.up * Time.deltaTime * 12);
	}
}
