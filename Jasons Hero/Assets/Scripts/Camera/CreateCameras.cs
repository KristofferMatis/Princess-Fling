using UnityEngine;
using System.Collections;

public class CreateCameras : MonoBehaviour
{
	public int m_PlayerCount = 4;

	void Start ()
	{
		setUp (m_PlayerCount);
	}

	void Update ()
	{
		//Add players

		if (Input.GetKeyDown(KeyCode.G))
		{
			setUp (m_PlayerCount);
		}
	}

	public void setUp (int playerCount)
	{
		setUpEnemies ();
		setUpCameras ();
	}

	//Spawn enemies
	public void setUpEnemies ()
	{
		//Spawn enemies
		//Enemy controller must set up what enemies are controlled by each player
	}

	//Creates all the cameras
	public void setUpCameras ()
	{
		//Create cameras
		GameObject obj = (GameObject)Resources.Load ("Prefabs/Main Camera");
		GameObject[] camerasCreated = new GameObject[m_PlayerCount];
		for (int i = 0; i < m_PlayerCount; i++)
		{
			//Instantiate the camera object
			camerasCreated[i] = (GameObject)GameObject.Instantiate(obj);

			//Tell the camera what transform to follow
			camerasCreated[i].GetComponent<CameraController>().setFollowedTransform(EnemyController.getEnemyTransformControlledByPlayer((Players)i));

		}

		//Set up camera rectangles
		if (m_PlayerCount == 1)
		{
			camerasCreated[0].camera.rect = new Rect( 0.0f , 0.0f , 1.0f , 1.0f );
		}
		else if (m_PlayerCount == 2)
		{
			camerasCreated[0].camera.rect = new Rect( 0.0f , 0.0f , 0.5f , 1.0f );
			camerasCreated[1].camera.rect = new Rect( 0.5f , 0.0f , 1.0f , 1.0f );
		}
		else if (m_PlayerCount == 3)
		{
			camerasCreated[0].camera.rect = new Rect( 0.0f , 0.5f , 0.5f , 1.0f );
			camerasCreated[1].camera.rect = new Rect( 0.5f , 0.5f , 1.0f , 1.0f );
			camerasCreated[2].camera.rect = new Rect( 0.0f , 0.0f , 0.5f , 0.5f );
		}
		else if (m_PlayerCount == 4)
		{
			camerasCreated[0].camera.rect = new Rect( 0.0f , 0.5f , 0.5f , 1.0f );
			camerasCreated[1].camera.rect = new Rect( 0.5f , 0.5f , 1.0f , 1.0f );
			camerasCreated[2].camera.rect = new Rect( 0.0f , 0.0f , 0.5f , 0.5f );
			camerasCreated[3].camera.rect = new Rect( 0.5f , 0.0f , 1.0f , 0.5f );
		}
	}
}
