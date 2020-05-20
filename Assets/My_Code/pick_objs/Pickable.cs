using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Pickable : MonoBehaviour
{
    public bool getting_picked
    {
        private set;
        get;
    }

    private Rigidbody rb;
    private Collider collider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    public void pick_item(Transform parent,Vector3 new_pos)
    {
        if (getting_picked)
        {
            Debug.LogError("Trying to picked an already picked item");
            return;
        }

        getting_picked = true;
        collider.isTrigger = true;
        transform.position = new_pos;
        transform.SetParent(parent);
        rb.isKinematic = true;
    }

    public void throw_object(Vector3 force)
    {
        if (!getting_picked)
        {
            Debug.LogError("Triying to throw a non picked item");
            return;
        }

        getting_picked = false;
        collider.isTrigger = false;
        rb.isKinematic = false;
        transform.SetParent(null);
        rb.AddForce(force, ForceMode.Impulse);
    }
}
