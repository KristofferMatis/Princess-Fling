using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public Transform m_Goto;

	void OnTriggerEnter (Collider other)
	{
		if (other.name == "Princess")
		{
			other.transform.position =  new Vector3(m_Goto.position.x, m_Goto.position.y, 0.0f);
			//Door sound
		}
	}
}
