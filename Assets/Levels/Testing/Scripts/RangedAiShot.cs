using UnityEngine;
using System.Collections;

public class RangedAiShot : MonoBehaviour 
{
	//mobility
	public float speed = 5;
	public GameObject target;

	//damage
	public float damage = 5f;
	// Use this for initialization
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag ("BarrageWP");
	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.Translate (Vector3.up * speed * Time.deltaTime);
		transform.position = Vector3.Lerp (transform.position, target.transform.position, 1 * Time.deltaTime);
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "BarrageWP")
		{
			target = GameObject.FindGameObjectWithTag ("Player");
			Rocket ();
		}

		if (other.tag == "Player")
		{

			//send damage message
			other.gameObject.SendMessage ("TakeDamage", damage);

			//destroy itself
			Destroy (gameObject);
		}
	}

	public void Rocket()
	{
		transform.position = Vector3.Lerp (transform.position, target.transform.position, 1 * Time.deltaTime);
	}
}
