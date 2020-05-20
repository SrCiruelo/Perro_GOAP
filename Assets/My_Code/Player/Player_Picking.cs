using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(picker))]
public class Player_Picking : MonoBehaviour
{
    // Start is called before the first frame update
    picker picker_actor;
    [SerializeField]
    private float throw_force;

    void Start()
    {
        picker_actor = GetComponent<picker>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (picker_actor.ISpicking_obj)
            {
                picker_actor.throw_obj(transform.forward * throw_force);
            }
            else
            {
                picker_actor.pick_obj();
            }
        }
    }
}
