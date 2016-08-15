using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour 
{
	public GameObject[] weapons;
	public int currentWeapon;
	int numWeapons; 


	// Use this for initialization
	void Start () 
	{
		numWeapons = weapons.Length; 
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxis("Mouse ScrollWheel") < 0) 
		{

			if (currentWeapon + 1 <= numWeapons)
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
				currentWeapon = numWeapons;
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
	}

	public void SelectWeapon(int index)
	{
		for (int i=0; i < numWeapons; i++)    
		{
			if (i == index) 
			{
				weapons[i].gameObject.SetActive(true);
			} 

			else 
			{ 
				weapons[i].gameObject.SetActive(false);
			}
		}
	}

	void AssaultRifle()
	{
		
	}

	void SMG()
	{
		
	}

	void Magnum()
	{
		
	}

}
