using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUICanvas : GameHUDWidget
{
    public void Unpause()
    {
        AppEvents.InvokeOnPauseEvent(false);
    }

    public void ReturnToMenu()
    {

    }
    public override void EnableWidget()
    {
        base.EnableWidget();
    }

    public override void DisableWidget()
    {
        base.DisableWidget();
    }
}
