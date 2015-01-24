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


    public static Vector2 getSwitchLeftStick(Players player)
    {
        return GamepadInput.GamePad.GetAxis(GamepadInput.GamePad.Axis.LeftStick, PlayersInfo.getGamepad(player));
    }

    //==============================================================================================
}
