using UnityEngine;
using System.Collections;

public class RangedTrigger : MonoBehaviour 
{
	public RangedAi rangedAi;

	void OnTriggerEnter(Collider other)
	{
		print ("ICU");
		if (other.tag == "Player")
		{
			rangedAi.inRange = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		print("Run away");
		if (other.tag == "Player")
		{
			rangedAi.inRange = false;
		}
	}
}
