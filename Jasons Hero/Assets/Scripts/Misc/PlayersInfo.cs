using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayersInfo : MonoBehaviour 
{
	struct players
	{
        public Players player;
        public GamepadInput.GamePad.Index gamepad;
	};

    static List<players> m_Players = new List<players>();


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
