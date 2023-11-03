using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaypointCircuit;

public class RaceFollowPath : MonoBehaviour
{
    private WaypointCircuit circuit;

    public float progressDistance;

    public float speed;

    public WaypointCircuit.RoutePoint progressPoint { get; private set; }

    public bool inCommission = true;

    public GameObject[] Weels;

    public GameObject EndVec;

    public int Thisone;
    void Awake()
    {
        circuit = GameObject.FindObjectOfType(typeof(WaypointCircuit)) as WaypointCircuit;
        Thisone = 2;
    }

    void Update()
    {
        if (!inCommission)
        {
            return;
        }

        if (Vector3.Distance(transform.position, EndVec.transform.position) < 0.5f)
        {
            return;
        }

        RoutePoint nextPoint = circuit.GetRoutePoint(progressDistance + Time.deltaTime * speed);
        transform.position = nextPoint.position;
        transform.rotation = Quaternion.LookRotation(nextPoint.direction);
       
       
        // if ()
        
        progressDistance += Time.deltaTime * speed;

         Vector3 vector31 = circuit.Waypoints[Thisone].position;
        var cross = Vector3.Cross(transform.forward, vector31-this.transform.position);
        if(Vector3.Distance(transform.position, vector31)<0.5f)
        {
            Thisone += 1;
        }


        if (cross.y < 0)
        {
           
        }
        else
        {
           
        }

        //Debug.Log(vector31);
        
        Weels[0].transform.localEulerAngles = new Vector3(Weels[0].transform.localEulerAngles.x, cross.y*10, Weels[0].transform.localEulerAngles.z);
        // Weels[1].transform.localEulerAngles = new Vector3(Weels[1].transform.localEulerAngles.x, cross.y * 10, 0);

        for (int i = 2; i <6; i++)
        {
            Weels[i].transform.Rotate(new Vector3(speed/10, 0, 0));
        }

       

       

    }
   


}
