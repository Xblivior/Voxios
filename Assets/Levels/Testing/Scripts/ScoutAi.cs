using UnityEngine;
using System.Collections;

public class ScoutAi : MonoBehaviour 
{
	//HP and Shield references
	public float maxHP = 50f;
	public float maxShield = 25f;
	public float currentHP;
	public float currentShield;

	public ParticleSystem blood;

	//Reinforcement references
	public GameObject reinforcement;
	int reinforcementCount;
	public Transform spawnPoint;
	public Transform reinforcementPoint;

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

	void Reinforcements()
	{
		//have reinforcement count at 0
		reinforcementCount = 0;

		//while there is less than 2 enemies
		while (reinforcementCount <= 2)
		{
			//spawn in reinforecment
			Instantiate (reinforcement, spawnPoint.transform.position, spawnPoint.transform.rotation);

			//increase reinforcement count
			reinforcementCount ++;
		}
	}
}
