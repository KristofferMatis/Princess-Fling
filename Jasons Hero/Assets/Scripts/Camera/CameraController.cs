using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//Followed enemy
	public Transform m_FollowedTransform;
	
	//Update
	void Update ()
	{
		updatePosition ();
	}

	//Lerps the camera to the followed enemy
	void updatePosition ()
	{
		Vector3 pos = Vector3.Lerp (transform.position, m_FollowedTransform.position, 0.1f);
		setPosition (pos);
	}

	//Sets the cameras position
	public void setPosition(Vector3 pos)
	{
		transform.position = new Vector3 (pos.x, pos.y, -10.0f);
	}

	//Set a new transform to follow
	public void setFollowedTransform (Transform followed)
	{
		m_FollowedTransform = followed;
		setPosition(followed.position);
	}
}
