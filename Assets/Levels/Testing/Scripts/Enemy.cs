using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{

	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			other.gameObject.SendMessage("TakeDamage", 50f);
		}
		print("hit");
	}
}
