using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.AI;

public static class NavMeshFunctions 
{
    private static int max_number_of_random_try = 15;
    private static int max_angle_iteration = 50;//Degrees
    private static int min_angle_iteration = 30;//Degrees

    public static bool Find_RandomPoint_InRadius(Vector3 from_point,float radius, out Vector3 return_point)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(from_point, out hit, 10f, NavMesh.AllAreas))
        {
            int random_angle = UnityEngine.Random.Range(0, 360);
            Vector3 vector_fromAngle = vector_from_angle(random_angle) * radius;
            Vector3 navmesh_point = hit.position;
            
            int i = 0;
            for (; i < max_number_of_random_try &&
                !NavMesh.SamplePosition(navmesh_point + vector_fromAngle, out hit, 10f, NavMesh.AllAreas) ; ++i)
            {
                random_angle += UnityEngine.Random.Range(min_angle_iteration, max_angle_iteration);
                vector_fromAngle = vector_from_angle(random_angle) * radius;
            }

            if (i < max_number_of_random_try)
            {
                return_point = hit.position;
                return true;
            }
            else
            {
                return_point = Vector3.zero;
                return false;
            }
        }
        else
        {
            return_point = Vector3.zero;
            return false;
        }
    }

    public static bool get_navmesh_point(Vector3 point, out Vector3 return_point)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(point, out hit, 10f, NavMesh.AllAreas))
        {
            return_point = hit.position;
            return true;
        }
        else
        {
            return_point = Vector3.zero;
            return false;
        }
    }

    public static bool get_navmesh_point(Vector3 point)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(point, out hit, 10f, NavMesh.AllAreas))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private static Vector3 vector_from_angle(int angle)
    {
        return new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));
    }
}
