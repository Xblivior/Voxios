using UnityEngine;
using System.Collections;

public class RangedTrigger : MonoBehaviour 
{
	public RangedAi rangedAi;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			rangedAi.inRange = true;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			rangedAi.inRange = false;
		}
	}
}
