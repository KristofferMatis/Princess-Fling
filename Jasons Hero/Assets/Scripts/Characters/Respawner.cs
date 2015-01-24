using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour
{
	public Transform[] m_RespawnPoints;
	float m_Timer = -1.0f;
	const float RESPAWN_TIME = 2.0f;
	float m_AddToRespawnArea = 0.0f;
	Transform m_PrincessTransform;

	const float MIN_DISTANCE = 5.0f;
	public const float AMOUNT_TO_MOVE_OVER = 5.0f;

	bool m_IsPrincess;

	//Load princess position
	void Start ()
	{
		m_PrincessTransform = GameObject.Find ("Princess").transform;
		if (m_PrincessTransform == transform)
		{
			m_IsPrincess = true;
		}
	}
	
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
	public void Kill(float amountToMoveOver)
	{
		if (m_Timer > -1.0f)
		{
			return;
		}

		m_Timer = RESPAWN_TIME;
		m_AddToRespawnArea = amountToMoveOver;

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
		for (int i = 0; i < m_RespawnPoints.Length; i++)
		{
			float thisDistance = Vector3.Distance(m_PrincessTransform.position + new Vector3 (m_AddToRespawnArea, 0.0f, 0.0f), m_RespawnPoints[i].position);
			if (thisDistance < dist && (m_IsPrincess || thisDistance > MIN_DISTANCE))
			{
				dist = thisDistance;
				chosenPoint = m_RespawnPoints[i].position;
				m_AddToRespawnArea = 0;
			}
		}

		//Respawn
		transform.position = chosenPoint;
		renderer.enabled = true;
	}

	//When the character is off the camera
	void OnBecameInvisible()
	{
		if (!m_IsPrincess)
		{
			if (gameObject.name == "PlayerOne")
			{
				Kill (AMOUNT_TO_MOVE_OVER);
			}
			else
			{
				Kill (-AMOUNT_TO_MOVE_OVER);
			}
		}
	}
}
