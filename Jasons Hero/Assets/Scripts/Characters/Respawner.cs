using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour
{
	public Transform[] m_RespawnPoints;
	float m_Timer = -1.0f;
	const float RESPAWN_TIME = 2.0f;
	float m_AddToRespawnArea = 0.0f;
	Transform m_PrincessTransform;

	const float MIN_DISTANCE = 4.0f;
	public const float AMOUNT_TO_MOVE_OVER = 20.0f;
	public const float AMOUNT_TO_MOVE_UP = 5.0f;

	Thrower m_Thrower;

	bool m_IsPrincess;

    public Transform i_DeadPlace;

	AudioSource m_Audio;
	public AudioClip[] m_DeathClips;

	//Load princess position
	void Start ()
	{
		m_PrincessTransform = GameObject.Find ("Princess").transform;
		if (m_PrincessTransform == transform)
		{
			m_IsPrincess = true;
		}

		m_Thrower = GetComponent<Thrower>();
		m_Audio = GetComponent<AudioSource>();
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
		if (m_Thrower != null)
		{
			m_Thrower.drop();
		}

        transform.position = i_DeadPlace.position;

		//Play sound depending on player or princess
		m_Audio.PlayOneShot (m_DeathClips[UnityEngine.Random.Range(0, m_DeathClips.Length)]);

		if (gameObject.name == "Princess")
		{
			Camera.main.GetComponent<CameraController>().m_FollowedTransform = null;
		}
	}

	//Respawn this character
	public void Respawn ()
	{
		if (gameObject.name == "Princess")
		{
			Camera.main.GetComponent<CameraController>().m_FollowedTransform = transform;
		}

		m_Timer = -1.0f;

		//Find nearest
		float dist = float.MaxValue;
		Vector3 chosenPoint = Vector3.zero;
		for (int i = 0; i < m_RespawnPoints.Length; i++)
		{
			float thisDistance = Vector3.Distance(m_PrincessTransform.position + new Vector3 (m_AddToRespawnArea, AMOUNT_TO_MOVE_UP, 0.0f), m_RespawnPoints[i].position);
			if (thisDistance < dist && (m_IsPrincess || thisDistance > MIN_DISTANCE))
			{
				dist = thisDistance;
				chosenPoint = m_RespawnPoints[i].position;
				m_AddToRespawnArea = 0;
			}
		}

		//Respawn
		transform.position = new Vector3(chosenPoint.x, chosenPoint.y, 0.0f);

		Throwable temp = gameObject.GetComponent<Throwable> ();
		if(temp != null)
		{
			temp.Velocity = Vector2.zero;
		}
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

	public bool IsAlive()
	{
		if (m_Timer == -1.0f)
		{
			return true;
		}
		return false;
	}
}
