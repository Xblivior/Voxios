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
	public float maxOvershield = 100f;
	public float maxHeat = 100f;
	public float heat;

	public float currentHP;
	public float currentShield;
	public float currentHeat;
	public float currentOvershield;

	float overheatCooldown = 5f;
	bool isOverheated = false;
	bool isOvershielded = false;

	public ParticleSystem blood;

	//gun references
	public enum gunLocker {assaultRifle, submachineGun, magnum};
	public gunLocker equipedGun;

	public GameObject gunPivot;
	public GameObject aRModel;

	int currentGunNumber;
	GameObject currentGunObj;

	public GameObject[] weapons;
	public int currentWeapon = 0;
	int numWeapons; 

	public GameObject bullet, bulletClone;
	public Transform bulletSpawn;

	bool canShoot = true;

	//speed references
	public float speed;
	public float strafeSpeed;
	public float rotSpeed;

	//ability veriables
	public GameObject healDrone;
	//public Transform droneSpawn;
	float healCooldown;
	float overshieldCooldown;
	float overshieldTimer = 8;


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

		numWeapons = weapons.Length; 
		SelectWeapon (currentWeapon);

	}

	// Update is called once per frame
	void Update () 
	{

		//ability Cooldown timer
		healCooldown -= Time.deltaTime;
		overshieldCooldown -= Time.deltaTime;

		//if heat is >= 0
		if (currentHeat >= 0)
		{
			//have it cooldown at 2 heat/sec
			currentHeat -= 2 * Time.deltaTime;
		}

		//if timer gets below 0
		if (healCooldown <= 0f)
		{
			//make it 0
			healCooldown = 0f;
		}
			
		if (overshieldCooldown <= 0f)
		{
			//make it 0
			overshieldCooldown = 0f;
		}

		//if player has overshield
		if (isOvershielded == true)
		{
			//start overshield timer
			overshieldTimer -= Time.deltaTime;

			//if overshieldtimer hits 0
			if (overshieldTimer <= 0f)
			{
				//over shield is 0 and isOvershielded is false
				currentOvershield = 0;
				isOvershielded = false;
			}
		}

		//if currentheat is < 100
		if (currentHeat < 100)
		{
			//overheat is false
			isOverheated = false;
		} 

		if (canShoot == false)
		{
			overheatCooldown -= Time.deltaTime;

			if (overheatCooldown <= 0f)
			{
				canShoot = true;
				overheatCooldown = 5f;
			}
		}

		//shooting
		if (Input.GetMouseButtonDown(0) && canShoot == true)
		{
//			//NOTE: move to gun swap function
//			if (equipedGun == gunLocker.assaultRifle) 
//			{
//				heat = 1f;
//				HeatGain (heat);
//			}
//
//			//instantiate the bullet as a clone so i can access its variables
			bulletClone = Instantiate (bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation) as GameObject;

			HeatGain(weapons [currentWeapon].GetComponent<WeaponController> ().Shoot ());

		}

		//if the player presses 1
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			//and the timer is 0
			if (healCooldown == 0f)
			{
				//use heal drone ability
				HealDrone();
				healCooldown = 20f;
			}
		}

		//if the player presses 2
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			//and the timer is 0
			if (overshieldCooldown == 0f)
			{
				//use overshield ability
				Overshield();
			}
		}

		//check for weaponswap
		SwapWeapon ();
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

		//Credit: Peter Carey
	}

	private void PlayerRotation () 
	{
		transform.Rotate (Vector3.up, Input.GetAxis ("Mouse X") * rotSpeed * Time.deltaTime);

		//Credit: Peter Carey
	}

	private void CameraControl ()
	{
		c.transform.LookAt (camAim.transform.position);
		gunPivot.transform.LookAt (camAim.transform.position);

		//Credit: Peter Carey
	}

	void HealDrone()
	{
		//spawn Heal drone
		//Instantiate(healDrone, droneSpawn.transform.position, droneSpawn.transform.rotation);
		healDrone.SetActive (true);

		//start cooldown
		healCooldown = 10f;
	}

	void Overshield()
	{
		//add the overshield
		currentOvershield = 100f;

		//overshield is true
		isOvershielded = true;

		//start cooldown
		overshieldCooldown = 20f;
	}

	void Overcharge()
	{
		
	}

	public void SwapWeapon()
	{
		if (Input.GetAxis("Mouse ScrollWheel") < 0) 
		{

			if (currentWeapon + 1 < numWeapons)
			{
				//change this so it goes 1 over
				currentWeapon++;
			} 

			else 
			{
				currentWeapon = 0;
			}

			SelectWeapon(currentWeapon);
		} 

		else if (Input.GetAxis("Mouse ScrollWheel") > 0) 
		{
			if (currentWeapon - 1 >= 0)
			{
				// and one under
				currentWeapon--;
			} 

			else 
			{
				currentWeapon = numWeapons - 1;
			}

			SelectWeapon(currentWeapon);
		}

		if(currentWeapon == numWeapons + 1) 
		{
			currentWeapon = 0;
		}

		if(currentWeapon == -1) 
		{
			currentWeapon = numWeapons;
		}

		//NOTE: Script for Weapon Swap using scrollwheel was made by moinchdog on
		// http://answers.unity3d.com/questions/64076/scroll-wheel-get-weapon.html
		// edited by Liam Hunt and Xblivior 

	}

	public void SelectWeapon(int index)
	{
		for (int i=0; i < numWeapons; i++)    
		{
			if (i == index) 
			{
				weapons[i].SetActive(true);

			} 

			else 
			{ 
				weapons[i].SetActive(false);
			}

			//credit: nastasache, http://answers.unity3d.com/questions/589666/how-to-switch-weaponsc.html
		}
	}

	public void TakeDamage(float damage)
	{
		blood.Play();

		//if player has overshield
		if (currentOvershield > 0f)
		{
			//take overshield off
			currentOvershield -= damage;
		}

		//else if there is no overshield
		else if (currentOvershield <= 0f)
		{
			//take off shield
			currentShield -= damage;
		}

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

		//stop current HP go above maxHP
		if (currentHP >= maxHP)
		{
			currentHP = maxHP;
		}

		//if health is 0
		if (currentHP <= 0f)
		{
			//instantiate explosions and destroy 
			Destroy(gameObject);

			//go to game over
		}
	}

	public void HealthChanges(float hPChange)
	{
		//change HP
		currentHP += hPChange;

		//stop current HP go above maxHP
		if (currentHP >= maxHP)
		{
			currentHP = maxHP;
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
		//add heat
		currentHeat += heat;

		//if currentHeat gets to maxHeat
		if (currentHeat >= maxHeat)
		{
			//player is overheated
			isOverheated = true;
			canShoot = false;
		}
	}
}


//Xblivior
