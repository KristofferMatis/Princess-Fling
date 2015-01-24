using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour {

	CharacterController m_Controller;

	Vector3 m_Velocity = new Vector3 (0.0f, -0.1f, 0.0f);

	const float WALKING_SPEED = 100.0f;
	const float JUMPING_SPEED = 100.0f;
	const float AIRBORNE_CONTROL = 100.0f;
	const float GRAVITY = 20.0f;

	public Players m_Player;


	// Use this for initialization
	void Start ()
	{
		m_Controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_Controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, 1.0f))
		{
			//Hit jump
			if (InputManager.getAbilityDown(m_Player))
			{
				m_Velocity.y = JUMPING_SPEED;
				return;
			}

			//Walk
			m_Velocity.x = InputManager.getSwitchLeftStick(m_Player).x * Time.deltaTime * WALKING_SPEED;
		}
		//Airborne
		else
		{
			m_Velocity.y -= GRAVITY * Time.deltaTime;
			m_Velocity.x += InputManager.getSwitchLeftStick(m_Player).x * Time.deltaTime * AIRBORNE_CONTROL;
		}

		m_Controller.Move (m_Velocity);
	}
}
