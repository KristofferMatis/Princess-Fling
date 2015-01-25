using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Transform m_Goto;

	void OnTriggerEnter (Collider other)
	{
		if (other.name == "Princess")
		{
			other.transform.position = m_Goto.position;

			//Door sound
		}
	}
}
