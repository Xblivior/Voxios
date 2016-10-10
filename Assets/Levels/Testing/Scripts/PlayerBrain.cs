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

	float fireRate;

	bool canShoot = true;

	//speed references
	public float speed;
	public float strafeSpeed;
	public float rotSpeed;
	public float boost;

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

		//fire rate
		if (fireRate > 0)
		{
			fireRate -= Time.deltaTime;
		}

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
		if (Input.GetMouseButton(0) && canShoot == true)
		{

			if (fireRate <= 0)
			{
				//instantiate the bullet as a clone so i can access its variables
				bulletClone = Instantiate (bullet, bulletSpawn.transform.position, bulletSpawn.transform.rotation) as GameObject;

				HeatGain(weapons [currentWeapon].GetComponent<WeaponController> ().heat);
				fireRate = weapons [currentWeapon].GetComponent<WeaponController> ().fireRate;
			}

		}

		//if player presses shift and they have less than 80 heat
		if (Input.GetKeyDown(KeyCode.LeftShift) && currentHeat < 80f)
		{
			//Use boost
			Boost();
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


	void Boost()
	{
		if (Input.GetAxis("Vertical") != 0f)
		{
			//teleport in forward or backwards
			transform.Translate((Vector3.forward * Input.GetAxis("Vertical")  * boost * Time.deltaTime));
		}

		else if (Input.GetAxis("Horizontal")!= 0f)
		{
			//teleport left or right
			transform.Translate((Vector3.right * Input.GetAxis("Horizontal")  * boost * Time.deltaTime));
		}
		//add heat 
		currentHeat += 20f;
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

		//total of overshield+shield+hp - the damage
		float total = currentHP + currentShield + currentOvershield - damage;

		//if player has overshield
		if (currentOvershield > 0f)
		{
			//if overshield becomes less than 0
			if (currentOvershield - (currentHP + currentShield + currentOvershield - total) < 0) 
			{
				//take off that amount
				currentOvershield -= currentHP + currentShield + currentOvershield - total;

				//and take the remainder of the damage done to overshield off of shield
				currentShield -= Mathf.Abs (currentOvershield);

				//set overshield to 0
				currentOvershield = 0;
			} 

			else 
			{
				//take overshield off
				currentOvershield -= currentHP + currentShield + currentOvershield - total;
			}

		}

		//if player has shield
		if (currentShield > 0f)
		{
			//if shield becomes less than 0
			if (currentShield - (currentHP + currentShield - total) < 0) 
			{
				//take off that amount
				currentShield -= currentHP + currentShield - total;

				//and take the remainder of the damage done to shield off of HP
				currentHP -= Mathf.Abs (currentShield);

				//set shield to 0
				currentShield = 0;
			} 

			else 
			{
				//take shield off
				currentShield -= currentHP + currentShield - total;
			}

		}

		//else if there is no shield
		else if (currentShield <= 0f)
		{
			//take off health
			currentHP -= currentHP - total;
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

		//CREDIT: Jacob Kreck and Xblivior
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
			Overheated ();
		}
	}

	void Overheated()
	{
		
	}
}


//Xblivior
