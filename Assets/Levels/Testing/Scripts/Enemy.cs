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
	public Transform shotSpawn;
	float shotTimer;

	// Use this for initialization
	void Start () 
	{
		currentHP = maxHP;
		currentShield = maxShield;
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

	public void TakeDamage(float damage)
	{
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
