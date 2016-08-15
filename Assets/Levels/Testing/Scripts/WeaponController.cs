using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour 
{
	public GameObject[] weapons;
	public int currentWeapon = 0;
	int numWeapons; 
	public PlayerBrain player;
	public GameObject activeWeapon;
	public float damage;
	public float heat;
	public float fireRate;

	// Use this for initialization
	void Start () 
	{
		numWeapons = weapons.Length; 

	}
	
	// Update is called once per frame
	void Update () 
	{
		print (activeWeapon);



	}
		
	public void Shoot(float damage, float heat, float fireRate)
	{
		print (damage);
		print (heat);
		print (fireRate);
	}
		
}


