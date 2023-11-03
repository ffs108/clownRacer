// ISTA 425 / INFO 525 Algorithms for Games
//
// Sample code file

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(-1.73f, 5.48f, -14.86f);
    //public Vector3 offset = new Vector3(-14.86f, 5.48f, -1.73f);
    //public GameObject curCam;
    public float hDist;
    public float vDist;
    public float startDist;
    public float springCon;
    private float dampCon;
    Vector3 velocity; 
    Vector3 curPos;

    private void Start()
    {
        transform.rotation = player.transform.rotation;
        transform.position = player.transform.position + offset;
        //hDist = player.transform.position.x - this.transform.position.x;
        //vDist = this.transform.position.y - player.transform.position.y;
        //velocity = player.GetComponent<Rigidbody>().velocity;
        //curCam = GameObject.FindGameObjectWithTag("MainCamera");
        dampCon = 2f * Mathf.Sqrt(springCon);
        velocity = new Vector3(0f, 0f, 0f);
        curPos = player.transform.position - (player.transform.forward * hDist) + (player.transform.up * vDist);
        startDist = hDist;
    }
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            hDist -= 1f;//0.1f
            Debug.Log("plus was pressed");
        }
        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            hDist += 1f;
        }
        if (Input.GetKey(KeyCode.Backspace))
        {
            hDist = startDist;
        }
        this.transform.forward = player.transform.forward;
        Vector3 idealPos = player.transform.position - player.transform.forward * hDist + player.transform.up * vDist;
        //calc vec from ideal to cur
        Vector3 displace = curPos - idealPos;
        //com the acc of spring and integrat
        Vector3 springAcc = (-springCon * displace) - (dampCon * velocity);
        velocity += springAcc * Time.deltaTime;
        curPos += velocity * Time.deltaTime;
        this.transform.position = curPos;
        //Camera.main.transform.LookAt(velocity * Time.deltaTime);
    }
}
