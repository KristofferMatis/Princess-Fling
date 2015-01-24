using UnityEngine;
using System.Collections;

public class Princess : MonoBehaviour {

	CharacterController m_Controller;
	const float SPEED = 75.0f;

	// Use this for initialization
	void Start ()
	{
		m_Controller = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update ()
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
