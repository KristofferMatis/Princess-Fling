using UnityEngine;
using System.Collections;

public static class InputManager
{
    public static bool getSwitchLeft(Players player)
    {
        return GamepadInput.GamePad.GetButton(GamepadInput.GamePad.Button.LeftShoulder, PlayersInfo.getGamepad(player));

    }

    public static bool getSwitchLeftDown(Players player)
    {
        return false;
    }

    public static bool getSwitchLeftUp(Players player)
    {
        return false;
    }
}
