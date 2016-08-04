using UnityEngine;
using System.Collections;

public class PlayerBrain : MonoBehaviour 
{

	//stats
	public float maxHP = 100f;
	public float maxShield = 100f;
	public float maxHeat = 100f;

	public float currentHP;
	public float currentShield;
	float currentHeat;

	//heal drone ability veriables
	public GameObject healDrone;
	public Transform droneSpawn;
	float healTimer;


	// Use this for initialization
	void Start () 
	{
		currentHP = maxHP;
		currentShield = maxShield;
		currentHeat = 0f;
	}

	// Update is called once per frame
	void Update () 
	{
		//heal drone Cooldown timer
		healTimer -= Time.deltaTime;

		//if timer gets below 0
		if (healTimer <= 0f)
		{
			//make it 0
			healTimer = 0f;
		}

		//if the player presses 1
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			//and the timer is 0
			if (healTimer == 0f)
			{
				//use heal drone ability
				HealdDrone();
			}
		}
	}

	void HealdDrone()
	{
		//spawn Heal drone
		Instantiate(healDrone, droneSpawn.transform.position, droneSpawn.transform.rotation);

		//start cooldown
		healTimer = 10f;
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

			//go to game over
		}
	}

	public void HeatGain(float heat)
	{
		currentHeat += heat;

	}
}
