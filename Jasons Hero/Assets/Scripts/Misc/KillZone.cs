using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

	void OnTriggerEnter (Collider other)
	{
		Respawner respawner = other.GetComponent<Respawner>();
		if (respawner != null)
		{
			respawner.Kill();
		}
	}
}
