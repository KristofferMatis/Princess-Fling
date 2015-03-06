using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class InputManager
{
    public static bool getSwitchLeft(Players player)
    {
        return GamepadInput.GamePad.GetButton(GamepadInput.GamePad.Button.LeftShoulder, PlayersInfo.getGamepad(player));
    }

    public static bool getSwitchLeftDown(Players player)
    {
        return GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.LeftShoulder, PlayersInfo.getGamepad(player));
    }

    public static bool getSwitchLeftUp(Players player)
    {
        return GamepadInput.GamePad.GetButtonUp(GamepadInput.GamePad.Button.LeftShoulder, PlayersInfo.getGamepad(player));
    }

    //==============================================================================================

    public static bool getDash(Players player)
    {
        return GamepadInput.GamePad.GetButton(GamepadInput.GamePad.Button.RightShoulder, PlayersInfo.getGamepad(player));
    }

	public static bool getDashDown(Players player)
    {
        return GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.RightShoulder, PlayersInfo.getGamepad(player));
    }

	public static bool getDashUp(Players player)
    {
        return GamepadInput.GamePad.GetButtonUp(GamepadInput.GamePad.Button.RightShoulder, PlayersInfo.getGamepad(player));
    }

    //==============================================================================================

    public static bool getJump(Players player)
    {
        return GamepadInput.GamePad.GetButton(GamepadInput.GamePad.Button.A, PlayersInfo.getGamepad(player));
    }

    public static bool getJumpDown(Players player)
    {
        return GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.A, PlayersInfo.getGamepad(player));
    }

    public static bool getJumpUp(Players player)
    {
        return GamepadInput.GamePad.GetButtonUp(GamepadInput.GamePad.Button.A, PlayersInfo.getGamepad(player));
    }

    //==============================================================================================

    public static bool getThrow(Players player)
    {
        return GamepadInput.GamePad.GetButton(GamepadInput.GamePad.Button.X, PlayersInfo.getGamepad(player));
    }

    public static bool getThrowDown(Players player)
    {
        return GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.X, PlayersInfo.getGamepad(player));
    }

    public static bool getThrowUp(Players player)
    {
        return GamepadInput.GamePad.GetButtonUp(GamepadInput.GamePad.Button.X, PlayersInfo.getGamepad(player));
    }

    //==============================================================================================

    public static bool getStart(Players player)
    {
        return GamepadInput.GamePad.GetButton(GamepadInput.GamePad.Button.Start, PlayersInfo.getGamepad(player));
    }

    public static bool getStartDown(Players player)
    {
        return GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start, PlayersInfo.getGamepad(player));
    }

    public static bool getStartUp(Players player)
    {
        return GamepadInput.GamePad.GetButtonUp(GamepadInput.GamePad.Button.Start, PlayersInfo.getGamepad(player));
    }

    //::::::
    static List<Players> playersWhoUsedInput = new List<Players>();
    public static Players[] getStart()
    {
        playersWhoUsedInput.Clear();

        for (int i = 0; i < (int)Players.count; i++ )
        {
            if(GamepadInput.GamePad.GetButton(GamepadInput.GamePad.Button.Start, PlayersInfo.getGamepad((Players)i)))
            {
                playersWhoUsedInput.Add((Players)i);
            }
        }

        return playersWhoUsedInput.ToArray();
    }

    public static Players[] getStartDown()
    {
        playersWhoUsedInput.Clear();

        for (int i = 0; i < (int)Players.count; i++)
        {
            if (GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.Start, PlayersInfo.getGamepad((Players)i)))
            {
                playersWhoUsedInput.Add((Players)i);
            }
        }

        return playersWhoUsedInput.ToArray();
    }

    public static Players[] getStartUp()
    {
        playersWhoUsedInput.Clear();

        for (int i = 0; i < (int)Players.count; i++)
        {
            if (GamepadInput.GamePad.GetButtonUp(GamepadInput.GamePad.Button.Start, PlayersInfo.getGamepad((Players)i)))
            {
                playersWhoUsedInput.Add((Players)i);
            }
        }

        return playersWhoUsedInput.ToArray();
    }

    //==============================================================================================

    public static Vector2 getLeftStick(Players player)
    {
        return GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, PlayersInfo.getGamepad(player));
    }

    //==============================================================================================
}
