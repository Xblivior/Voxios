using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour 
{
	
	public PlayerBrain player;

	public float damage;
	public float heat;
	public float fireRate;

	// Use this for initialization
	void Start () 
	{
		

	}
	
	// Update is called once per frame
	void Update () 
	{
		

	}
		
	public void Shoot()
	{
		print (damage);
		print (heat);
		print (fireRate);
	}
		
}


