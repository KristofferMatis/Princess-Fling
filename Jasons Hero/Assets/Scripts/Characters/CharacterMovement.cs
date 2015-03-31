using UnityEngine;
using System.Collections;

public class CharacterMovement : Throwable
{
	#region old
	const float WALKING_SPEED = 22.0f;
    const float JUMPING_SPEED = 0.45f;
    const float AIRBORNE_CONTROL = 9.0f;
    const float GRAVITY = 1.1f;

    const float FLOAT_POWER = 1.1f;
    const float FLOAT_POWER_LOSS = 1.7f;
    float m_CurrentFloatPower = FLOAT_POWER;

	Thrower m_Throw;

	public Players m_Player;

    bool m_IsHoldingA = false;

    const float STUN_TIME = 1.0f;
    float m_Timer = 0.0f;

	int m_RaycastMask = -1;

    public bool IsCarryingOBJ = false;

	public AudioClip[] m_ThrownClips;
	public AudioClip[] m_JumpClips;
	public AudioClip[] m_LandClips;

	bool m_WasGrounded = false;

	public bool stop = true;
#endregion

	public enum DashTypes
	{
		None,
		InstantStop,
		FullControlInfinity,
        FullControl
	};

	bool canDash = true;
	bool m_IsDashing = false;
	public DashTypes m_DashType = DashTypes.InstantStop;
    Vector2 m_DashDirection = Vector2.zero;
    const float DASH_SPEED = 50.0f;
    const float DASH_DIST = 10.0f;
    float m_Distance = 0.0f;

	float m_ClipTimer = 0.0f;

	protected void dashPlayerStop ()
	{
        m_Controller.Move(m_DashDirection * Time.deltaTime * DASH_SPEED);

        m_Distance += (m_DashDirection * Time.deltaTime * DASH_SPEED).magnitude;

        if(m_Distance >= DASH_DIST)
        {
            exitDash();
        }
	}

	protected void FullControlInfinity ()
	{
		m_DashDirection = InputManager.getLeftStick(m_Player);
		m_DashDirection.Normalize();
		
		m_Controller.Move(m_DashDirection * Time.deltaTime * DASH_SPEED);
		
		//m_Distance += (m_DashDirection * Time.deltaTime * DASH_SPEED).magnitude;
		
		if(m_Distance >= DASH_DIST)
		{
			exitDash();
		}
	}

    protected void dashFullControl()
    {
		m_DashDirection = InputManager.getLeftStick(m_Player);
		m_DashDirection.Normalize();

		m_Controller.Move(m_DashDirection * Time.deltaTime * DASH_SPEED);
		
		m_Distance += (m_DashDirection * Time.deltaTime * DASH_SPEED).magnitude;
		
		if(m_Distance >= DASH_DIST)
		{
			exitDash();
		}
    }

	protected override void Update ()
	{
		base.Update ();

		if (m_ClipTimer > 0.0f)
		{
			m_ClipTimer -= Time.deltaTime;
		}
	}

	protected override void Start ()
	{
		base.Start ();
		m_RaycastMask = ~LayerMask.GetMask ("Throwable");
		m_Throw = GetComponent<Thrower>();
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

		if (m_ClipTimer <= 0.0f)
		{
			m_Audio.PlayOneShot (m_ThrownClips[UnityEngine.Random.Range(0, m_ThrownClips.Length)]);
			m_ClipTimer = 1.0f;
		}

		m_Timer = 0.0f;
    }

    public void stun()
    {
        m_Timer = STUN_TIME;
    }

    protected void exitDash()
    {
        m_IsDashing = false;
        m_Distance = 0.0f;
        m_Velocity.x = m_DashDirection.x * 3.0f;
        m_Velocity.y = m_DashDirection.y / 3.0f;
    }

    protected override void nope()
    {
        if (stop)
			return;

        if (m_IsDashing)
        {
            switch (m_DashType)
            {
                case DashTypes.InstantStop:
				dashPlayerStop();
                    break;
			case DashTypes.FullControlInfinity:
				FullControlInfinity();
                    break;
                case DashTypes.FullControl:
                    dashFullControl();
                    break;
            };

            if (InputManager.getDashUp(m_Player))
            {
                exitDash();
            }

            return;
        }

		if(canDash && m_DashType != DashTypes.None)
		{
	        if (InputManager.getDashDown(m_Player))
	        {
	            m_DashDirection = InputManager.getLeftStick(m_Player);
	            m_DashDirection.Normalize();
	            m_IsDashing = true;
				canDash = false;
	            return;
	        }
		}
        if (m_Controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, 1.0f, m_RaycastMask))
        {//grounded
			canDash = true;
			if (!m_WasGrounded)
			{
				m_Audio.PlayOneShot (m_LandClips[UnityEngine.Random.Range(0, m_LandClips.Length)]);
			}

			m_WasGrounded = true;

            //Hit jump
            if (InputManager.getJumpDown(m_Player))
            {
				m_Audio.PlayOneShot (m_JumpClips[UnityEngine.Random.Range(0, m_JumpClips.Length)]);

                if (!IsCarryingOBJ)
                {
                    m_Velocity.y = JUMPING_SPEED;
                }
                else
                {
					m_Velocity.y = JUMPING_SPEED / m_Throw.m_BeingCarried.m_Weight;
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
				m_Velocity.x = InputManager.getLeftStick(m_Player).x * Time.deltaTime * WALKING_SPEED / (m_Throw.m_BeingCarried.m_Weight * 2.0f);
            }
        }
        //Airborne
        else
        {
			m_WasGrounded = false;
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
						m_Velocity.y += m_CurrentFloatPower / m_Throw.m_BeingCarried.m_Weight * Time.deltaTime;
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
				m_Velocity.x = InputManager.getLeftStick(m_Player).x * Time.deltaTime * AIRBORNE_CONTROL/ m_Throw.m_BeingCarried.m_Weight;
            }
        }

		m_Controller.Move (m_Velocity);
	}

	public override void Reset ()
	{
		m_Throw.drop();
		base.Reset ();
		m_Velocity = Vector3.zero;
	}
}
