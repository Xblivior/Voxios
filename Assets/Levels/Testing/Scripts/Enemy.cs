using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	//HP and Shield references
	public float maxHP = 50f;
	public float maxShield = 25f;
	public float currentHP;
	public float currentShield;

	//shooting references
	public GameObject shot;
	public GameObject shotClone;
	public Transform shotSpawn;

	GameObject target;
	float shotTimer;

	public ParticleSystem blood;

	// Use this for initialization
	void Start () 
	{
		currentHP = maxHP;
		currentShield = maxShield;

		target = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () 
	{
		shotTimer -= Time.deltaTime;
		if (shotTimer <= 0f)
		{
			shotTimer = 0f;
		}
	}

	public void OnTriggerStay (Collider other)
	{
		if (other.tag == "Player")
		{
			gameObject.transform.LookAt(target.transform.position);

			if (shotTimer <= 0f)
			{
				//instantiate the bullet as a clone so i can access its variables
				shotClone = Instantiate (shot, shotSpawn.transform.position, shotSpawn.transform.rotation) as GameObject;
				shotTimer = 2f;
			}

			//other.gameObject.SendMessage("TakeDamage", 50f);
		}
		print("hit");
	}

	public void TakeDamage(float damage)
	{
		blood.Play();

		//if player has shield
		if (currentShield > 0f)
		{
			//take shield off
			currentShield -= damage;
		}

		//else if there is no shield
		else if (currentShield <= 0f)
		{
			//take off health
			currentHP -= damage;
		}

		//if health is 0
		if (currentHP <= 0f)
		{
			//instantiate explosions and destroy 
			Destroy(gameObject);
		
		}
	}
}
