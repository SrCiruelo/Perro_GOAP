using ReGoap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perro_Descansar_Goal : ReGoapGoal<string,object>
{
    protected override void Awake()
    {
        base.Awake();
        goal.Set("Descansando", true);
    }
}
