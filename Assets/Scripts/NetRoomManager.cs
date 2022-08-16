using Mirror;
using UnityEngine;

public class NetRoomManager : NetworkRoomManager
{
    bool showStartButton;

    public override void OnRoomServerPlayersReady()
    {
        showStartButton = true;
    }

    public override void OnGUI()
    {
        base.OnGUI();

        if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
        {
            showStartButton = false;

            ServerChangeScene(GameplayScene);
        }
    }
}
