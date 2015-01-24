using UnityEngine;
using System.Collections;

public class CharacterMovement : Throwable 
{
	const float WALKING_SPEED = 10.0f;
    const float JUMPING_SPEED = 0.2f;
    const float AIRBORNE_CONTROL = 7.0f;
    const float GRAVITY = 0.8f;

    const float FLOAT_POWER = 0.9f;
    const float FLOAT_POWER_LOSS = 0.9f;
    float m_CurrentFloatPower = FLOAT_POWER;

	public Players m_Player;

    bool m_IsHoldingA = false;

    const float CARRY_TIME = 1.0f;
    float m_Timer = 0.0f;

    protected override void carry()
    {
        if(m_Timer > 0.0f)
        {
            m_Timer -= Time.deltaTime;
            return;
        }

        m_BeingCarriedBy.drop();
    }

    protected override void onCarry()
    {
        base.onCarry();
        m_Timer = CARRY_TIME;
    }

    protected override void onAirborn()
    {
        base.onAirborn();
    }

    protected override void nope()
    {
		if (m_Controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, 1.0f))
		{
			//Hit jump
			if (InputManager.getJumpDown(m_Player))
			{
				m_Velocity.y = JUMPING_SPEED;
                m_IsHoldingA = true;
				return;
			}

			//Walk
			m_Velocity.x = InputManager.getLeftStick(m_Player).x * Time.deltaTime * WALKING_SPEED;
		}
		//Airborne
		else
		{
            if(m_IsHoldingA)
            {
                if(InputManager.getJumpUp(m_Player) || m_Velocity.y < 0.0f)
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
			m_Velocity.x = InputManager.getLeftStick(m_Player).x * Time.deltaTime * AIRBORNE_CONTROL;
		}

		m_Controller.Move (m_Velocity);
	}
}
