using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//Followed enemy
	public Transform m_FollowedTransform;
	const float LERP_SPEED_PRE_DELTA = 1.0f;

	void Start ()
	{
		setPosition(m_FollowedTransform.position);
	}

	//Update
	void Update ()
	{
		updatePosition ();
	}

	//Lerps the camera to the followed enemy
	void updatePosition ()
	{
		Vector3 pos = Vector3.Lerp (transform.position, m_FollowedTransform.position, Mathf.Min(LERP_SPEED_PRE_DELTA * Time.deltaTime, 1.0f));
		setPosition (pos);
	}

	//Sets the cameras position
	public void setPosition(Vector3 pos)
	{
		transform.position = new Vector3 (pos.x, pos.y, -10.0f);
	}
}
