using UnityEngine;
using System.Collections;

public class Area : MonoBehaviour
{
	public CameraController m_Camera;
	public Transform m_PlayerOneRespawn;
	public Transform m_PlayerTwoRespawn;

	//When this area is exited
	void OnTriggerExit (Collider collider)
	{
		if (collider.gameObject.name == "Princess")
		{
			m_Camera.setPosition (transform.position);
		}
		else if (collider.gameObject.name == "PlayerOne")
		{
			collider.transform.position = m_PlayerOneRespawn.position;
		}
		else if (collider.gameObject.name == "PlayerTwo")
		{
			collider.transform.position = m_PlayerTwoRespawn.position;
		}
	}
}
