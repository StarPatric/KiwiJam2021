using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gaurd : MonoBehaviour
{
    [Header("Pathing")]
    [SerializeField] Transform[] pathRoute; //This is simple to get the prototype working
    [SerializeField] float speed = 0.15f;

    int pathingPoint = 0; //Point I am going to 

    enum AIStates
    { 
        wandering,
        stunned,
        spendingTimewithFamily,
        helpingTheCommunity,
        volunteeringAtCharities,
    }

    AIStates currentState = AIStates.wandering;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case AIStates.wandering:
                {
                    pathing();
                    break;
                }

            default:break;
        }
    }

    void pathing()
    {
        transform.position = Vector3.Lerp(transform.position, pathRoute[pathingPoint].position, speed * Time.deltaTime);
        //Arrived
        if (Vector3.Distance(transform.position, pathRoute[pathingPoint].position) < 0.5f)
        {
            pathingPoint = pathingPoint == pathRoute.Length - 1 ? 0 : pathingPoint + 1;
        }
    }


    public void getStunned()
    {
        Debug.Log("OW!");
        currentState = AIStates.stunned;
    }

    void OnDrawGizmosSelected()
    {
        if (pathRoute.Length > 0)
        {
            Gizmos.color = Color.yellow;
            for (int x = 0; x < pathRoute.Length;x++)
            {
                if (pathRoute[x])
                {
                    int endPos = x + 1 == pathRoute.Length ? 0 : x + 1;

                    Vector3 lineStart = pathRoute[x].position;
                    Vector3 lineEnd = pathRoute[endPos].position;


                    Gizmos.DrawLine(lineStart, lineEnd);
                }
            }
        }
    }

}
