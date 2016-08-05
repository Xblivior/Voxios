using UnityEngine;
using System.Collections;

public class PlayerBrain : MonoBehaviour 
{
	//Stat Ref

	public static GameObject playerBrain;
	public GameObject camAim;
	private Rigidbody rb;
	private GameObject c;

	//stats
	public float maxHP = 100f;
	public float maxShield = 100f;
	public float maxHeat = 100f;

	public float currentHP;
	public float currentShield;
	float currentHeat;

	public float speed;
	public float strafeSpeed;
	public float rotSpeed;

	//heal drone ability veriables
	public GameObject healDrone;
	public Transform droneSpawn;
	float healTimer;

	void Awake () {
		playerBrain = this.gameObject;
		rb = GetComponent<Rigidbody> ();
		c = Camera.main.gameObject;
	}

	// Use this for initialization
	void Start () 
	{
		currentHP = maxHP;
		currentShield = maxShield;
		currentHeat = 0f;


		Cursor.lockState = CursorLockMode.Locked;
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
				HealDrone();
			}
		}
	}

	void FixedUpdate () {
		PlayerMovement ();
		PlayerRotation ();
		CameraControl ();
	}

	private void PlayerMovement () 
	{
		rb.MovePosition (transform.position + (transform.forward * Input.GetAxis("Vertical")  * speed * Time.deltaTime) + (transform.right * Input.GetAxis ("Horizontal")) * strafeSpeed * Time.deltaTime);

	}

	private void PlayerRotation () {
		transform.Rotate (Vector3.up, Input.GetAxis ("Mouse X") * rotSpeed * Time.deltaTime);
	}

	private void CameraControl ()
	{
		c.transform.LookAt (camAim.transform.position);
	}

	void HealDrone()
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
