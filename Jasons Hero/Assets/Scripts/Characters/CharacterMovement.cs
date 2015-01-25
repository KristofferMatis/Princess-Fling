using UnityEngine;
using System.Collections;

public class CharacterMovement : Throwable 
{
	const float WALKING_SPEED = 22.0f;
    const float JUMPING_SPEED = 0.45f;
    const float AIRBORNE_CONTROL = 9.0f;
    const float GRAVITY = 1.1f;

    const float FLOAT_POWER = 1.1f;
    const float FLOAT_POWER_LOSS = 1.7f;
    float m_CurrentFloatPower = FLOAT_POWER;

	public Players m_Player;

    bool m_IsHoldingA = false;

    const float STUN_TIME = 1.0f;
    float m_Timer = 0.0f;

	int m_RaycastMask = -1;

    public bool IsCarryingOBJ = false;
    const float PENALTY = 1.5f;

	protected override void Start ()
	{
		base.Start ();
		m_RaycastMask = ~LayerMask.GetMask ("Throwable");
	}

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
        m_Timer = STUN_TIME;
    }

    protected override void onAirborn()
    {
        base.onAirborn();
        m_Timer = 0.0f;
    }

    public void stun()
    {
        m_Timer = STUN_TIME;
    }

    protected override void nope()
    {
        //if(m_Timer > 0.0f)
        //{
        //    m_Timer -= Time.deltaTime;
        //    return;
        //}

        if (m_Controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, 1.0f, m_RaycastMask))
        {
            //Hit jump
            if (InputManager.getJumpDown(m_Player))
            {
                if (!IsCarryingOBJ)
                {
                    m_Velocity.y = JUMPING_SPEED;
                }
                else
                {
                    m_Velocity.y = JUMPING_SPEED / PENALTY;
                }
                m_IsHoldingA = true;
                return;
            }

            //Walk
            if (!IsCarryingOBJ)
            {
                m_Velocity.x = InputManager.getLeftStick(m_Player).x * Time.deltaTime * WALKING_SPEED;
            }
            else
            {
                m_Velocity.x = InputManager.getLeftStick(m_Player).x * Time.deltaTime * WALKING_SPEED / (PENALTY*2.0f);
            }
        }
        //Airborne
        else
        {
            if (m_IsHoldingA)
            {
                if (InputManager.getJumpUp(m_Player) || m_Velocity.y < 0.0f)
                {
                    m_IsHoldingA = false;
                    m_CurrentFloatPower = FLOAT_POWER;

                }
                else
                {
                    if (!IsCarryingOBJ)
                    {
                        m_Velocity.y += m_CurrentFloatPower * Time.deltaTime;
                    }
                    else
                    {
                        m_Velocity.y += m_CurrentFloatPower / PENALTY * Time.deltaTime;
                    }
                    m_CurrentFloatPower -= FLOAT_POWER_LOSS * Time.deltaTime;
                }
            }

            m_Velocity.y -= GRAVITY * Time.deltaTime;
            if (!IsCarryingOBJ)
            {
                m_Velocity.x = InputManager.getLeftStick(m_Player).x * Time.deltaTime * AIRBORNE_CONTROL;
            }
            else
            {
                m_Velocity.x = InputManager.getLeftStick(m_Player).x * Time.deltaTime * AIRBORNE_CONTROL/ PENALTY;
            }
        }

		m_Controller.Move (m_Velocity);
	}
}
