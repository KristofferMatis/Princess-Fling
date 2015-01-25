using UnityEngine;
using System.Collections;

public class Princess : Throwable
{
	Thrower LastThrower;

	const float SPEED = 500.0f;

	void Awake ()
	{
		m_Weight = 1.4f;
	}


	public override Thrower BeingCarriedBy
	{
		get { return BeingCarriedBy; }
		set { m_BeingCarriedBy = value;
			if (m_BeingCarriedBy != null)
			{
				LastThrower = m_BeingCarriedBy;
			}
		}
	}

    protected override void nope()
    {
		if (m_Controller.isGrounded || Physics.Raycast(transform.position, Vector3.down, 1.0f))
		{
			//Walk

			if (transform.position.x > 0.5f)
			{
				m_Controller.SimpleMove(Vector3.left * SPEED * Time.deltaTime);
				return;
			}
			else if (transform.position.x < -0.5f)
			{
				m_Controller.SimpleMove(Vector3.right * SPEED * Time.deltaTime);
				return;
			}
		}
		//Airborne movement

        m_Controller.SimpleMove(Vector3.zero);
    }

	public Thrower getLastThrower ()
	{
		return  LastThrower;
	}
}
