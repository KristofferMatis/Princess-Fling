using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour
{
	void OnTriggerEnter (Collider other)
	{
		if (other.name == "Princess")
		{
			Debug.Log("Win for X Player");
		}
	}
}
