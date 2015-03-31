using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//Followed enemy
	public Transform m_FollowedTransform;
	const float LERP_SPEED_PRE_DELTA = 1.0f;
	const float ZOOM_LERP_SPEED_PRE_DELTA = 4.0f;
	const float MIN_DISTANCE_TO_ZOOM_OUT = 15.0f;
	const float SHAKEAMOUNT = 0.4f;
	const float SHAKETIMER = 0.2f;
	float m_ShakeTimer = 0.0f;
	Vector3 m_ShakeDirection;
	Vector3 m_Velocity;

	Respawner[] m_Players;


	void Start ()
	{
		setPosition(m_FollowedTransform.position);

		CharacterMovement[] players = (CharacterMovement[])GameObject.FindObjectsOfType<CharacterMovement> ();
		m_Players = new Respawner[players.Length];
		for (int i = 0; i < players.Length; i++)
		{
			m_Players[i] = players[i].GetComponent<Respawner>();
		}
	}

	//Update
	void Update ()
	{
		if (m_FollowedTransform != null)
		{
			updatePosition ();
			updateZoom ();
		}
	}

	//Lerps the camera to the followed enemy
	void updatePosition ()
	{
		Vector3 realPos = transform.position - m_ShakeDirection;
		Vector3 pos = Vector3.Lerp (realPos, m_FollowedTransform.position, Mathf.Min(LERP_SPEED_PRE_DELTA * Time.deltaTime, 1.0f));

		if (m_ShakeTimer > 0.0f)
		{
			m_ShakeTimer -= Time.deltaTime;
			float shakeAmount = SHAKEAMOUNT * m_ShakeTimer / SHAKETIMER;
			m_ShakeDirection = m_Velocity * shakeAmount;
			m_ShakeDirection += new Vector3(UnityEngine.Random.Range(0.0f,shakeAmount), UnityEngine.Random.Range(0.0f,shakeAmount), 0.0f) * m_Velocity.magnitude;
			pos += m_ShakeDirection;
		}
		setPosition (pos);
	}

	void updateZoom ()
	{
		//Find distance to zoom out to
		float distance = Vector3.Distance (transform.position, m_FollowedTransform.position);
		for (int i = 0; i < m_Players.Length; i++)
		{
			if (m_Players[i].IsAlive())
			{
				float playerDist = Vector3.Distance (transform.position, m_Players[i].transform.position) * 0.65f;
				
				if (distance < playerDist)
				{
					distance = playerDist;
				}
			}
		}

		//Check if we should zoom at all
		if (distance < MIN_DISTANCE_TO_ZOOM_OUT)
		{
			distance = MIN_DISTANCE_TO_ZOOM_OUT;
		}
		setZoom (Mathf.Lerp(camera.orthographicSize, distance, Mathf.Min(ZOOM_LERP_SPEED_PRE_DELTA * Time.deltaTime, 1.0f)));
	}

	//Sets the cameras position
	public void setPosition(Vector3 pos)
	{
		transform.position = new Vector3 (pos.x, pos.y, -10.0f);
	}

	//Sets camera zoom
	public void setZoom (float zoom)
	{
		camera.orthographicSize = zoom;
	}

	public void CameraShake (Vector2 force)
	{
		if (force.magnitude < 1.0f)
		{
			return;
		}

		if (m_ShakeTimer <= 0.0f)
		{
			m_ShakeTimer = SHAKETIMER * force.magnitude;
		}
		m_Velocity = new Vector3 (force.x, force.y, 0.0f);
	}


}
