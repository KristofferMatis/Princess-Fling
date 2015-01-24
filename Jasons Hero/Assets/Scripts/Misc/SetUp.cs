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
    }

	// Use this for initialization
	void Start () 
	{
	    
	}
}
