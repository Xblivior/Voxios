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

	//gun references
	public GameObject gunPivot;
	public GameObject aRModel;

	int currentGunNumber;
	GameObject currentGunObj;

	public GameObject bullet;
	public Transform bulletSpawn;

	//speed references
	public float speed;
	public float strafeSpeed;
	public float rotSpeed;

	//heal drone ability veriables
	public GameObject healDrone;
	public Transform droneSpawn;
	float healTimer;

	void Awake () 
	{
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

		//lock the mouse to the middle and make it disappear from view
		Cursor.lockState = CursorLockMode.Locked;

		//note: just for now
		currentGunObj = aRModel;
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

		if (Input.GetMouseButtonDown(0))
		{
			Instantiate (bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation);
		}


	}

	void FixedUpdate () 
	{
		PlayerMovement ();
		PlayerRotation ();
		CameraControl ();
	}

	private void PlayerMovement () 
	{
		rb.MovePosition (transform.position + (transform.forward * Input.GetAxis("Vertical")  * speed * Time.deltaTime) + (transform.right * Input.GetAxis ("Horizontal")) * strafeSpeed * Time.deltaTime);

	}

	private void PlayerRotation () 
	{
		float rotationY = 0f;
		rotationY += Input.GetAxis ("Mouse Y") * rotSpeed * Time.deltaTime;
		rotationY = Mathf.Clamp(rotationY, -12f, 10f);

		transform.Rotate (Vector3.up, Input.GetAxis ("Mouse X") * rotSpeed * Time.deltaTime);

	}

	private void CameraControl ()
	{
		c.transform.LookAt (camAim.transform.position);
		currentGunObj.transform.LookAt (camAim.transform.position);
	}

	void HealDrone()
	{
		//spawn Heal drone
		Instantiate(healDrone, droneSpawn.transform.position, droneSpawn.transform.rotation);

		//start cooldown
		healTimer = 10f;
	}

	void Overshield()
	{
		
	}

	void Overcharge()
	{
		
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
