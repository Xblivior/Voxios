using UnityEngine;
using System.Collections;

public class RangedAi : MonoBehaviour 
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

	//shooting references
	public GameObject shot;
	public GameObject shotClone;
	public Transform[] shotSpawn;
	public bool inRange = false;

	GameObject target;
	float shotTimer;
	public float shotWait;
	int shotCount;
	int shotSpawnNum;
	public bool canFire = true;

	// Use this for initialization
	void Start () 
	{
		//set current shield and HP
		currentHP = maxHP;
		currentShield = maxShield;

		//nav mesh
		navMesh = GetComponent<NavMeshAgent> ();
		speed = navMesh.speed;

		//set player as target
		target = GameObject.FindGameObjectWithTag ("Player");

		canFire = true;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
//		if (shotTimer <= 0f)
//		{
//			shotTimer = 0f;
//		}
//
//		if (inRange == true)
//		{
//			shotTimer -= Time.deltaTime;
//			Barrage ();
//		}

		if (shotWait > 0)
		{
			shotWait -= Time.deltaTime;
		}
	}

	void FixedUpdate()
	{
		//raycast is called hit
		RaycastHit hit;

		//if SphereCast (AIposition, 2radius size, going forward, output as hit, 10 units long)
		if (Physics.SphereCast(transform.position, 2, transform.forward, out hit, 10))
		{
			//if (hit tag is PLayer)
			if (hit.transform.tag == "Player")
			{
				print ("Player"); 

			}
		}

	}

	public IEnumerator Barrage()
	{
//		gameObject.transform.LookAt(target.transform.position);
//
//		if (shotTimer <= 0f)
//		{
//			shotCount = 0;
//			shotSpawnNum = 0;
//
//			while (shotCount < 4 && shotWait <= 0)
//			{
//				//instantiate the bullet as a clone so i can access its variables
//				shotClone = Instantiate (shot, shotSpawn[shotSpawnNum].transform.position, shotSpawn[shotSpawnNum].transform.rotation) as GameObject;
//
//				shotCount++;
//				shotSpawnNum++;
//				shotWait = 0.2f;
//			}
//		}
//
//		//reset shot timer
//		shotTimer = 2f;
		shotWait = 0.5f;
		shotTimer = 2;
		canFire = false;
		transform.LookAt (new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z));

		GameObject shota = Instantiate (shot, shotSpawn [0].transform.position, shotSpawn[0].transform.rotation) as GameObject;

		yield return new WaitForSeconds (shotWait);

		GameObject shota2 = Instantiate (shot, shotSpawn [1].transform.position, shotSpawn[1].transform.rotation) as GameObject;

		yield return new WaitForSeconds (shotWait);

		GameObject shota3 = Instantiate (shot, shotSpawn [2].transform.position, shotSpawn[2].transform.rotation) as GameObject;

		yield return new WaitForSeconds (shotWait);

		GameObject shota4 = Instantiate (shot, shotSpawn [3].transform.position, shotSpawn[3].transform.rotation) as GameObject;

		yield return new WaitForSeconds (shotWait);

		canFire = true;

		if (inRange) {
			yield return new WaitForSeconds (shotTimer);
			StartCoroutine (Barrage ());

		}
		yield break;

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
