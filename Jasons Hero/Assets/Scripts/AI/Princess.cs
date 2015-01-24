using UnityEngine;
using System.Collections;

public class Princess : Throwable
{
	const float SPEED = 75.0f;

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
}
