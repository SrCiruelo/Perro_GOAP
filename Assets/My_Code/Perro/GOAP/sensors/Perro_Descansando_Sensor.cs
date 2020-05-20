using ReGoap.Core;
using ReGoap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Perro_Descansando_Sensor : ReGoapSensor<string,object>
{
    NavMeshAgent agent;
    [SerializeField]
    private float ismoving_threshhold;
    public override void Init(IReGoapMemory<string, object> memory)
    {
        base.Init(memory);
        agent = GetComponent<NavMeshAgent>();
        memory.GetWorldState().Set("Descansando", false);
    }

    public override void UpdateSensor()
    {
        base.UpdateSensor();
        ReGoapState<string, object> state = memory.GetWorldState();

        if ((bool)state.Get("Descansando"))
        {

            if (agent.velocity.sqrMagnitude > ismoving_threshhold * ismoving_threshhold)
            {
                Debug.Log("Ya no está descansando" + agent.velocity.sqrMagnitude);
                state.Set("Descansando", false);
                GetComponent<Perro_Agent>().state_change();
            }
        }
    }
}
