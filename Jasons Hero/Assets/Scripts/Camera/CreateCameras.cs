using UnityEngine;
using System.Collections;

public class CreateCameras : MonoBehaviour
{
	void Start ()
	{
		Transform[] cams = new Transform[4];
		for (int i = 0; i < cams.Length; i++)
		{
			cams[i] = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
		}
		setUpCameras (cams);
	}


	public void setUpCameras (Transform[] initialTransforms)
	{
		//Create cameras
		GameObject obj = (GameObject)Resources.Load ("Prefabs/Main Camera");
		GameObject[] camerasCreated = new GameObject[initialTransforms.Length];
		for (int i = 0; i < initialTransforms.Length; i++)
		{
			//Instantiate the camera object
			camerasCreated[i] = (GameObject)GameObject.Instantiate(obj);

			//Tell the camera what transform to follow
			camerasCreated[i].GetComponent<CameraController>().setFollowedTransform(initialTransforms[i]);
		}

		//Set up camera rectangles
		if (initialTransforms.Length == 1)
		{
			camerasCreated[0].camera.rect = new Rect( 0.0f , 0.0f , 1.0f , 1.0f );
		}
		else if (initialTransforms.Length == 2)
		{
			camerasCreated[0].camera.rect = new Rect( 0.0f , 0.0f , 0.5f , 1.0f );
			camerasCreated[1].camera.rect = new Rect( 0.5f , 0.0f , 1.0f , 1.0f );
		}
		else if (initialTransforms.Length == 3)
		{
			camerasCreated[0].camera.rect = new Rect( 0.0f , 0.5f , 0.5f , 1.0f );
			camerasCreated[1].camera.rect = new Rect( 0.5f , 0.5f , 1.0f , 1.0f );
			camerasCreated[2].camera.rect = new Rect( 0.0f , 0.0f , 0.5f , 0.5f );
		}
		else if (initialTransforms.Length == 4)
		{
			camerasCreated[0].camera.rect = new Rect( 0.0f , 0.5f , 0.5f , 1.0f );
			camerasCreated[1].camera.rect = new Rect( 0.5f , 0.5f , 1.0f , 1.0f );
			camerasCreated[2].camera.rect = new Rect( 0.0f , 0.0f , 0.5f , 0.5f );
			camerasCreated[3].camera.rect = new Rect( 0.5f , 0.0f , 1.0f , 0.5f );
		}
	}
}
