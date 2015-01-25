using UnityEngine;
using System.Collections;

public class SetUp : MonoBehaviour
{
	const string THROWABLE = "Throwable";
	const string THROWER = "Thrower";

	void Awake()
	{
        Physics.IgnoreLayerCollision(LayerMask.NameToLayer(THROWABLE), LayerMask.NameToLayer(THROWABLE), true);
	
        PlayersInfo.Player one;
        one.gamepad = GamepadInput.GamePad.Index.One;
        one.player = Players.PlayerOne;

        PlayersInfo.addPlayer(one);

        PlayersInfo.Player two;
        two.gamepad = GamepadInput.GamePad.Index.Two;
        two.player = Players.PlayerTwo;

        PlayersInfo.addPlayer(two);


		PlayersInfo.Player three;
		three.gamepad = GamepadInput.GamePad.Index.Three;
		three.player = Players.PlayerThree;
		
		PlayersInfo.addPlayer(three);
		
		PlayersInfo.Player four;
		four.gamepad = GamepadInput.GamePad.Index.Four;
		four.player = Players.PlayerFour;
		
		PlayersInfo.addPlayer(four);
    }

	// Use this for initialization
	void Start () 
	{
	    
	}
}
