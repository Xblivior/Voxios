using UnityEngine;
using System.Collections;

public class HealDrone : MonoBehaviour 
{
	public LineRenderer healBeam;
	public float heal = 2f;
	float healTime = 10f;

	public Transform healTarget;

	public Transform target;
	public float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;


	// Use this for initialization
	void Start () 
	{
		//set the line renderer start pos 
		healBeam.SetPosition (0, transform.position);

		//set line renderer end pos
		healBeam.SetPosition (1, new Vector3 (0,0,0));

		//set line renderer width and hight
		healBeam.SetWidth (0.01f, 0.01f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 healTargetT = healTarget.transform.position;

		Vector3 targetPosition = target.transform.position;
		transform.position = Vector3.SmoothDamp (transform.position, targetPosition, ref velocity, smoothTime);

		//start heal timer
		healTime -= Time.deltaTime;

		//if heal time hit 0
		if (healTime <= 0)
		{
			Death ();
		}

		//set the line renderer start pos 
		healBeam.SetPosition (0, transform.position);
		//heal beam to heal target
		healBeam.SetPosition (1, healTargetT);
	}

	public void Death()
	{
		gameObject.SetActive (false);
		healTime = 10f;
	}
}
