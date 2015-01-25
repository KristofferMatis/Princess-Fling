using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	//Followed enemy
	public Transform m_FollowedTransform;
	const float LERP_SPEED_PRE_DELTA = 1.0f;
	const float MIN_DISTANCE_TO_ZOOM_OUT = 12.0f;

	void Start ()
	{
		setPosition(m_FollowedTransform.position);
	}

	//Update
	void Update ()
	{
		updatePosition ();
		updateZoom ();
	}

	//Lerps the camera to the followed enemy
	void updatePosition ()
	{
		Vector3 pos = Vector3.Lerp (transform.position, m_FollowedTransform.position, Mathf.Min(LERP_SPEED_PRE_DELTA * Time.deltaTime, 1.0f));
		setPosition (pos);
	}

	void updateZoom ()
	{
		float distance = Vector3.Distance (transform.position, m_FollowedTransform.position);
		if (distance > MIN_DISTANCE_TO_ZOOM_OUT)
		{
			setZoom (Mathf.Lerp(camera.orthographicSize, distance, Mathf.Min(LERP_SPEED_PRE_DELTA * Time.deltaTime, 1.0f)));
		}
	}

	//Sets the cameras position
	public void setPosition(Vector3 pos)
	{
		transform.position = new Vector3 (pos.x, pos.y, -10.0f);
	}

	//Sets camera zoom
	public void setZoom (float zoom)
	{
		camera.orthographicSize = zoom;
	}
}
