using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour 
{
	public string Level_For_4;
	public string Level_For_2;

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
   		{
			Application.LoadLevel(Level_For_2);
		}

		if(Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
		{
			Application.LoadLevel(Level_For_4);
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			Application.LoadLevel(Application.loadedLevel);
		}

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
