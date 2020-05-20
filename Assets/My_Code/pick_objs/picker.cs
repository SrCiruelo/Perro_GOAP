using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider))]
public class picker : MonoBehaviour
{

    [SerializeField]
    private Transform pick_socket;

    public bool can_pick
    {
        private set; get;
    }

    public Pickable possible_pick_item
    {
        private set; get;
    }

    public bool ISpicking_obj
    {
        private set;
        get;
    }

    public Pickable picked_object
    {
        private set; get;
    }

    private void OnTriggerEnter(Collider other)
    {
        can_pick = true;
        possible_pick_item = other.GetComponent<Pickable>();
    }

    
    private void OnTriggerExit(Collider other)
    {
        can_pick = false;
        possible_pick_item = null;
    }

    public void pick_obj()
    {
        
        if (can_pick)
        {
            if (!possible_pick_item.getting_picked)
            {
                picked_object = possible_pick_item;
                ISpicking_obj = true;
                possible_pick_item.pick_item(pick_socket, pick_socket.position);
            }

        }
    }

    public void throw_obj(Vector3 force)
    {
        if (ISpicking_obj)
        {
            picked_object.throw_object(force);
            picked_object = null;
            ISpicking_obj = false;
        }
    }
}
