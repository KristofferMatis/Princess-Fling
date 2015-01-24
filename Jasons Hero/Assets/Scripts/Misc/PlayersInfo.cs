using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayersInfo : MonoBehaviour 
{
	public struct Player
	{
        public Players player;
        public GamepadInput.GamePad.Index gamepad;
	};

    static List<Player> m_Players = new List<Player>();

	//
    public static GamepadInput.GamePad.Index getGamepad(Players player)
	{
        for(int i = 0; i < m_Players.Count; i++)
        {
            if(m_Players[i].player == player)
            {
                return m_Players[i].gamepad;
            }
        }

        return GamepadInput.GamePad.Index.Any;
	}
}
