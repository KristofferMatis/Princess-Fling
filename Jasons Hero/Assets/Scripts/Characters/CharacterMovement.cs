using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour 
{

	CharacterController m_Controller;

	Vector3 m_Velocity = new Vector3 (0.0f, -0.1f, 0.0f);

	const float WALKING_SPEED = 10.0f;
    const float JUMPING_SPEED = 0.2f;
    const float AIRBORNE_CONTROL = 7.0f;
    const float GRAVITY = 0.8f;

    const float FLOAT_POWER = 0.9f;
    const float FLOAT_POWER_LOSS = 0.9f;
    float m_CurrentFloatPower = FLOAT_POWER;


	public Players m_Player;

    bool m_IsHoldingA = false;


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
                m_IsHoldingA = true;
				return;
			}

			//Walk
			m_Velocity.x = InputManager.getSwitchLeftStick(m_Player).x * Time.deltaTime * WALKING_SPEED;
		}
		//Airborne
		else
		{
            if(m_IsHoldingA)
            {
                if(InputManager.getAbilityUp(m_Player) || m_Velocity.y < 0.0f)
                {
                    m_IsHoldingA = false;
                    m_CurrentFloatPower = FLOAT_POWER;
                }
                else
                {
                    m_Velocity.y += m_CurrentFloatPower * Time.deltaTime;
                    m_CurrentFloatPower -= FLOAT_POWER_LOSS * Time.deltaTime;
                }
            }

			m_Velocity.y -= GRAVITY * Time.deltaTime;
			m_Velocity.x = InputManager.getSwitchLeftStick(m_Player).x * Time.deltaTime * AIRBORNE_CONTROL;
		}

		m_Controller.Move (m_Velocity);
	}
}
