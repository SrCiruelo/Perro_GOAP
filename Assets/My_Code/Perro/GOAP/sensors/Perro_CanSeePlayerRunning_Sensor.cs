using Invector.vCharacterController;
using ReGoap.Core;
using ReGoap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perro_CanSeePlayerRunning_Sensor : ReGoapSensor<string,object>
{
    [SerializeField]
    private vThirdPersonController player;

    public override void Init(IReGoapMemory<string,object> memory)
    {
        base.Init(memory);
        memory.GetWorldState().Set("CanSeePlayerRunning", false);
    }

    public override void UpdateSensor()
    {
        base.UpdateSensor();
        bool last_state = (bool)memory.GetWorldState().Get("CanSeePlayerRunning");

        if (player.isSprinting)
        {
            if (!last_state)
            {
                memory.GetWorldState().Set("CanSeePlayerRunning", true);
                GetComponent<Perro_Agent>().state_change();
            }
                
        }
        else
        {
            if (last_state)
            {
                memory.GetWorldState().Set("CanSeePlayerRunning", false);
                GetComponent<Perro_Agent>().state_change();
            }
                
        }
    }
}
