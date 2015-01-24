using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour
{
	public Transform[] m_RespawnPoints;
	float m_Timer = -1.0f;
	const float RESPAWN_TIME = 2.0f;
	
	// Update is called once per frame
	void Update ()
	{
		if (m_Timer > -1.0f)
		{
			m_Timer -= Time.deltaTime;
			if (m_Timer < 0.0f)
			{
				Respawn ();
			}
		}
	}

	//Kill this character
	public void Kill()
	{
		if (m_Timer > -1.0f)
		{
			return;
		}

		m_Timer = RESPAWN_TIME;

		//REMOVE CHARACTER
		renderer.enabled = false;
	}

	//Respawn this character
	public void Respawn ()
	{
		m_Timer = -1.0f;

		//Find nearest
		float dist = float.MaxValue;
		Vector3 chosenPoint = Vector3.zero;
		foreach (Transform point in m_RespawnPoints)
		{
			float thisDistance = Vector3.Distance(transform.position, point.position);
			if (thisDistance < dist)
			{
				dist = thisDistance;
				chosenPoint = point.position;
			}
		}

		//Respawn
		transform.position = chosenPoint;
		renderer.enabled = true;
	}

	//When the character is off the camera
	void OnBecameInvisible()
	{
		Kill ();
	}
}
