using UnityEngine;
using System.Collections;

public class CreateCameras : MonoBehaviour
{
	public void setUpCameras (Vector3[] initialPositions)
	{
		//Create cameras
		GameObject obj = (GameObject)Resources.Load ("Prefabs/Main Camera");
		GameObject[] camerasCreated = new GameObject[initialPositions.Length];
		for (int i = 0; i < initialPositions.Length; i++)
		{
			camerasCreated[i] = (GameObject)GameObject.Instantiate(obj, initialPositions[i], Quaternion.identity);
		}

		//Set up camera rectangles
		if (initialPositions.Length == 1)
		{
			camerasCreated[0].camera.rect = new Rect( 0.0f , 0.0f , 1.0f , 1.0f );
		}
		else if (initialPositions.Length == 1)
		{
			camerasCreated[0].camera.rect = new Rect( 0.0f , 0.0f , 0.5f , 1.0f );
			camerasCreated[1].camera.rect = new Rect( 0.5f , 0.0f , 1.0f , 1.0f );
		}
		else if (initialPositions.Length == 1)
		{
			camerasCreated[0].camera.rect = new Rect( 0.0f , 0.0f , 0.5f , 0.5f );
			camerasCreated[1].camera.rect = new Rect( 0.5f , 0.0f , 1.0f , 0.5f );
			camerasCreated[2].camera.rect = new Rect( 0.0f , 0.5f , 0.5f , 1.0f );
		}
		else if (initialPositions.Length == 1)
		{
			camerasCreated[0].camera.rect = new Rect( 0.0f , 0.0f , 0.5f , 0.5f );
			camerasCreated[1].camera.rect = new Rect( 0.5f , 0.0f , 1.0f , 0.5f );
			camerasCreated[2].camera.rect = new Rect( 0.0f , 0.5f , 0.5f , 1.0f );
			camerasCreated[3].camera.rect = new Rect( 0.5f , 0.5f , 1.0f , 1.0f );
		}
	}
}
