//using UnityEngine;
//using System.Collections;
//
//public class PlayerRaycastShoot : MonoBehaviour 
//{
//
//	public LayerMask whatIsEverything;
//
//	// Use this for initialization
//	void Start () 
//	{
//	
//	}
//	
//	// Update is called once per frame
//	void FixedUpdate () 
//	{
//		GunDirection();
//	}
//
//	void GunDirection()
//	{
//		Vector3 gunDirection = transform.TransformDirection(Vector3.forward);
//
//		if (Physics.Raycast(transform.position, gunDirection, out RaycastHit, 100, whatIsEverything)) //100 units forward
//			print("ObjectHit");
//
//	}
//}
