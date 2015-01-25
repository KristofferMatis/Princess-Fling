using UnityEngine;
using System.Collections;

public class Princess : Throwable
{
	Thrower LastThrower;

	const float SPEED = 220.0f;
	public override Thrower BeingCarriedBy
	{
		get { return BeingCarriedBy; }
		set { m_BeingCarriedBy = value; LastThrower = value;}
	}

    protected override void nope()
    {
        if (transform.position.x > 1.0f)
        {
            m_Controller.SimpleMove(Vector3.left * SPEED * Time.deltaTime);
        }
        else if (transform.position.x < -1.0f)
        {
            m_Controller.SimpleMove(Vector3.right * SPEED * Time.deltaTime);
        }
        else
        {
            m_Controller.SimpleMove(Vector3.zero);
        }
    }

	public Thrower getLastThrower ()
	{
		return  LastThrower;
	}
}
