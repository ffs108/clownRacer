using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public GameObject[] waypoints;

    public AudioSource audioSource;
    public AudioClip checkPointAudio;
    public AudioClip finishAudio;

    private GameObject curWaypoint;
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        curWaypoint = waypoints[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReachedCheckPoint()
    {
        index++;
        if (index == waypoints.Length) // finish line checkpoint
        {
            Debug.Log("Congratualations you finished");
            audioSource.clip = finishAudio;
            audioSource.Play();
        } else
        {
            Debug.Log("Checkpoint " + index + " reached");
            curWaypoint = waypoints[index];
            audioSource.clip = checkPointAudio;
            audioSource.Play();
        }
    }

    public GameObject GetCurWaypoint()
    {
        return curWaypoint;
    }


}
