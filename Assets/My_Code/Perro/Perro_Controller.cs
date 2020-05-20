using ReGoap.Unity;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public delegate void getDestination_callback();
public delegate bool procedural_condition();

[RequireComponent(typeof(NavMeshAgent))]
public class Perro_Controller : MonoBehaviour
{
    NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }
    
    public void moveTo(Vector3 move_to_pos)
    {
        NavMeshHit hit;
        
        if (NavMesh.SamplePosition(move_to_pos, out hit, 100.0f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
        if (agent.destination != move_to_pos)
        {
            Debug.Log("Not a valid Move To pos");
        }
    }

    [SerializeField]
    private float path_update_time;
    [SerializeField]
    private float max_distance_to_obj;
    public IEnumerator move_towards_object_relative_pos(GameObject follow_to_obj,Vector3 relativePos,
                                                        getDestination_callback succes_callback,
                                                        getDestination_callback failure_callback)
    {
        
        Vector3 destination;

        bool valid_destination = NavMeshFunctions.get_navmesh_point(follow_to_obj.transform.position + relativePos, out destination);

        agent.SetDestination(destination);
        
        Vector3 my_pos;
        NavMeshFunctions.get_navmesh_point(transform.position, out my_pos);
        float remaining_distance = (destination - my_pos).sqrMagnitude;


        //Es probable que se esten juntando rutinas cuando se cancela un plan y se empieza otro
        while (valid_destination && remaining_distance > max_distance_to_obj * max_distance_to_obj && agent.destination == destination)
        {

            valid_destination = NavMeshFunctions.get_navmesh_point(follow_to_obj.transform.position + relativePos, out destination);

            NavMeshFunctions.get_navmesh_point(transform.position, out my_pos);
            remaining_distance = (destination - my_pos).sqrMagnitude;


            agent.SetDestination(destination);

            yield return new WaitForSeconds(path_update_time);
        }
        if (remaining_distance > max_distance_to_obj || !valid_destination)
        {
            Debug.Log("Fail Path: " + agent.destination + " dest: " + destination);

            failure_callback();
        }
        else
        {
            Debug.Log("Succesful Path: " + (destination - my_pos).magnitude);
            succes_callback();
        }

        
    }

    public IEnumerator move_towards_object_relative_pos(GameObject follow_to_obj, Vector3 relativePos,
                                                        getDestination_callback succes_callback,
                                                        getDestination_callback failure_callback,procedural_condition new_condition)
    {
        Vector3 destination;

        bool valid_destination = NavMeshFunctions.get_navmesh_point(follow_to_obj.transform.position + relativePos, out destination);

        agent.SetDestination(destination);

        Vector3 my_pos;
        NavMeshFunctions.get_navmesh_point(transform.position, out my_pos);
        float remaining_distance = (destination - my_pos).sqrMagnitude;


        //Es probable que se esten juntando rutinas cuando se cancela un plan y se empieza otro
        while (valid_destination && remaining_distance > max_distance_to_obj * max_distance_to_obj && agent.destination == destination && !new_condition())
        {

            valid_destination = NavMeshFunctions.get_navmesh_point(follow_to_obj.transform.position + relativePos, out destination);

            NavMeshFunctions.get_navmesh_point(transform.position, out my_pos);
            remaining_distance = (destination - my_pos).sqrMagnitude;

            agent.SetDestination(destination);
            yield return new WaitForSeconds(path_update_time);
        }
        if (new_condition())
        {
            Debug.Log("Out of loop due to procedural condition");
            succes_callback();
        }
        else if (remaining_distance > max_distance_to_obj || !valid_destination)
        {
            failure_callback();
        }
        else
        {
            
            succes_callback();
        }
    }
    public void stay_in_pos()
    {
        agent.SetDestination(transform.position);
    }

    [SerializeField]
    private GameObject debug_move_to_obj;

    [ContextMenu("MoveTo")]
    public void move_to_obj_debug()
    {

        moveTo(debug_move_to_obj.transform.position);
    }


    public void Update()
    {
        
        
    }


}
