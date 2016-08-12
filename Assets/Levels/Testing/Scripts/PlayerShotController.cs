using UnityEngine;
using System.Collections;

public class PlayerShotController : MonoBehaviour 
{
	public float speed = 5f;
	public float damage;

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
