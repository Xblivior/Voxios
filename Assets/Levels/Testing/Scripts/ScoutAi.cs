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

	//movement references
	NavMeshAgent navMesh;
	public Transform[] waypoints;
	public int waypointNum = 0;
	float speed;

	//Reinforcement references
	public Transform spawnPoint;
	public Transform reinforcementPoint;
	public GameObject reinforcement;
	int reinforcementCount;

	bool seenPlayer = false;


	// Use this for initialization
	void Start () 
	{
		currentHP = maxHP;
		currentShield = maxShield;

		navMesh = GetComponent<NavMeshAgent> ();
		speed = navMesh.speed;

	}

	// Update is called once per frame
	void Update () 
	{
		if (seenPlayer == true)
		{
			Retreat ();
		}

	}

	void FixedUpdate()
	{
		//raycast is called hit
		RaycastHit hit;

		//if SphereCast (AIposition, 0.5radius size, going forward, output as hit, 10 units long)
		if (Physics.SphereCast(transform.position, 2, transform.forward, out hit, 10))
		{
			//if (hit tag is PLayer)
			if (hit.transform.tag == "Player")
			{
				print ("Player"); 

				//set seen player true;
				seenPlayer = true;
			}
		}

		NextPoint ();
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

	void Retreat()
	{
		//loot at reinforcement point
		transform.LookAt (reinforcementPoint);
		//go to the reinforcement point
		//transform.position = Vector3.Lerp(transform.position, reinforcementPoint.position, 1 * Time.deltaTime);
		navMesh.SetDestination (reinforcementPoint.position);

		//increase speed while retreating
		navMesh.speed = 5;

	}

	public void Reinforcements()
	{
		//have reinforcement count at 0
		reinforcementCount = 0;

		//while there is less than 2 enemies
		while (reinforcementCount < 2)
		{
			//spawn in reinforecment
			Instantiate (reinforcement, reinforcementPoint.transform.position, reinforcementPoint.transform.rotation);

			//increase reinforcement count
			reinforcementCount ++;
		}

		Destroy (gameObject);
	}

	public void NextPoint()
	{
		//look at target
		transform.LookAt (waypoints [waypointNum]);
		
		if (seenPlayer == false)
		{
			//go to the reinforcement point
			//transform.position = Vector3.Lerp(transform.position, waypoints[waypointNum].position, 1 * Time.deltaTime);
			navMesh.SetDestination (waypoints [waypointNum].position);

		}
	}
		
	void OnTriggerEnter(Collider other)
	{
		//if it  hits the Reinforcement point
		if (other.gameObject == reinforcementPoint.gameObject)
		{
			//make the spawnpoint at the trigger
			spawnPoint = other.transform;

			//call reinforcements
			Reinforcements ();
		}

		if (other.tag == "Waypoint")
		{
			if(waypointNum + 1 < waypoints.Length)
			{
				waypointNum++;
			}

			else if (waypointNum + 1 >= waypoints.Length)
			{
				waypointNum = 0;
			}
		}
	}
}
