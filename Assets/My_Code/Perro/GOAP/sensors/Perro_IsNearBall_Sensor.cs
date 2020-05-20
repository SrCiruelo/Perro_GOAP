using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGoap.Unity;
using ReGoap.Core;

public class Perro_IsNearBall_Sensor : ReGoapSensor<string,object>
{
    [SerializeField]
    private float max_distance_to_ball = 0.5f;
    Perro_Agent perro_agent;

    public override void Init(IReGoapMemory<string, object> memory)
    {
        base.Init(memory);
        memory.GetWorldState().Set("IsNearBall", false);
        perro_agent = GetComponent<Perro_Agent>();
    }

    public override void UpdateSensor()
    {
        base.UpdateSensor();

        bool can_see_ball = (bool)memory.GetWorldState().Get("CanSeeBall");
        bool is_near_ball = (bool)memory.GetWorldState().Get("IsNearBall");

        if (can_see_ball)
        {
            
            if (is_near_ball)
            {
                Vector3 ball_position = ((GameObject)memory.GetWorldState().Get("BallObject")).transform.position;
                if((ball_position - transform.position).sqrMagnitude > max_distance_to_ball)
                {
                    Debug.Log("Not Near ball anymore: " + (ball_position - transform.position).magnitude);
                    memory.GetWorldState().Set("IsNearBall", false);
                    perro_agent.state_change();
                }
            }
        }
        else
        {
            if (is_near_ball)
            {
                memory.GetWorldState().Set("IsNearBall", false);
                perro_agent.state_change();
            }
        }
    }
}
