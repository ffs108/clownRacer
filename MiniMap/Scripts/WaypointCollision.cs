using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCollision : MonoBehaviour
{
    public GameObject waypointObject;

    private Waypoints waypointScript;
    // Start is called before the first frame update
    void Start()
    {
        waypointScript = waypointObject.GetComponent<Waypoints>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player Car")
            Debug.Log("hi");
        {
            if (waypointScript.GetCurWaypoint().Equals(gameObject))
            {
                waypointScript.ReachedCheckPoint();
                Destroy(GetComponent<BoxCollider>());
            }
        }
    }
}
