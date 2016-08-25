using UnityEngine;
using System.Collections;

public class EnemyShot : PlayerShotController
{

	void Awake()
	{
		damage = 5f;
	}

	public void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Player")
		{

			//send damage message
			other.gameObject.SendMessage ("TakeDamage", damage);

			//destroy itself
			Destroy (gameObject);
		}
	}
}
