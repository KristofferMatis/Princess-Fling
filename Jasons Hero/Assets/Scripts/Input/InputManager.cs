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

    public static bool getSwitchRight(Players player)
    {
        return GamepadInput.GamePad.GetButton(GamepadInput.GamePad.Button.RightShoulder, PlayersInfo.getGamepad(player));
    }

    public static bool getSwitchRightDown(Players player)
    {
        return GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.RightShoulder, PlayersInfo.getGamepad(player));
    }

    public static bool getSwitchRightUp(Players player)
    {
        return GamepadInput.GamePad.GetButtonUp(GamepadInput.GamePad.Button.RightShoulder, PlayersInfo.getGamepad(player));
    }

    //==============================================================================================

    public static bool getAbility(Players player)
    {
        return GamepadInput.GamePad.GetButton(GamepadInput.GamePad.Button.A, PlayersInfo.getGamepad(player));
    }

    public static bool getAbilityDown(Players player)
    {
        return GamepadInput.GamePad.GetButtonDown(GamepadInput.GamePad.Button.A, PlayersInfo.getGamepad(player));
    }

    public static bool getAbilityUp(Players player)
    {
        return GamepadInput.GamePad.GetButtonUp(GamepadInput.GamePad.Button.A, PlayersInfo.getGamepad(player));
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

    public static Vector2 getSwitchLeftStick(Players player)
    {
        return GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, PlayersInfo.getGamepad(player));
    }

    //==============================================================================================
}
