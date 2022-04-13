using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : HealthComponent
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        PlayerEvents.InvokeOnHealthInitialized(this);
    }

    public override void Destroy()
    {
        Debug.Log("Player dead");
        //Application.Quit();

        // Load a new Game Over Scene when at 0 
    }

}
