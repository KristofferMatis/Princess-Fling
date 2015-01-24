using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	//Sets the cameras position
	public void setPosition(Vector3 pos)
	{
		transform.position = new Vector3 (pos.x, pos.y, -10.0f);
	}
}
