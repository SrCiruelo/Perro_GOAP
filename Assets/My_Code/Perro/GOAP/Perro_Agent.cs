using ReGoap.Core;
using ReGoap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perro_Agent : ReGoapAgent<string,object>
{

    protected override void Awake()
    {
        base.Awake();
        ReGoapState<string,object> state =  memory.GetWorldState();


        
    }
    protected override void OnDonePlanning(IReGoapGoal<string, object> newGoal)
    {
        base.OnDonePlanning(newGoal);
        Debug.Log("new plan " + currentGoal.GetName());
    }
    public void state_change()
    {
        if (currentActionState != null)
        {

            Debug.Log("Plan already going on: " + currentActionState.Action.ToString());
            return;
        }

        if (CalculateNewGoal())
        {
            /*Debug.Log("CanSeePlayer: " + ((bool)memory.GetWorldState().Get("CanSeePlayer")));
            Debug.Log("CanSeePlayerRunning" + ((bool)memory.GetWorldState().Get("CanSeePlayer")));
            Debug.Log("CanSeeBall: " + ((bool)memory.GetWorldState().Get("CanSeeBall")));
            Debug.Log("IsNearPlayer: " + ((bool)memory.GetWorldState().Get("IsNearPlayer")));
            Debug.Log("IsNearBall: " + ((bool)memory.GetWorldState().Get("IsNearBall")));
            Debug.Log("HasBall: " + ((bool)memory.GetWorldState().Get("HasBall")));
            Debug.Log("Descansando: " + ((bool)memory.GetWorldState().Get("Descansando")));*/
        }


        
        
        //if plan not finished return 
        /*Debug.Log("State change");
        if (currentActionState != null)
        {
            
            Debug.Log("Plan already going on: " + currentActionState.ToString());
            return;
        }*/
        //Debug.Log("Near Player: " + memory.GetWorldState().Get("IsNearPlayer") + " CanSeePlayerRunning: " + memory.GetWorldState().Get("CanSeePlayerRunning"));
        //Debug.Log("Descansando: " + memory.GetWorldState().Get("Descansando"));

        
        
    }

    private void Update()
    {
        
    }
}
