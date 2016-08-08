using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour 
{
	public float speed = 5f;
	//self destruct timer
	float lifeTime = 1f;

	// Use this for initialization
	void Start () 
	{	
		//destroy bullet after 2 secs
		Destroy (gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}
}
