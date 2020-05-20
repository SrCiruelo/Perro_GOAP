using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGoap.Unity;

public class Perro_PlayWithBall_Goal : ReGoapGoal<string,object>
{
    protected override void Awake()
    {
        base.Awake();
        goal.Set("HasBall", false);
        goal.Set("IsNearPlayer", true);
        goal.Set("IsNearBall", true);
    }
}
