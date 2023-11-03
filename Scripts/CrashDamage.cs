using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashDamage : MonoBehaviour
{
    public Vector3 curVelocity;
    private Vector3 lastVelocity;
    private Vector3 lastPosition;
    private float lastCalled = 0f;
    public float minorDamage = 3.0f;
    public float moderateDamage = 6.0f;
    public float severeDamage = 10.0f;

    private AudioSource audio;

    public AudioClip sfxMinor;
    public AudioClip sfxModerate;
    public AudioClip sfxSevere;

    private bool crashOccured = false;
    private GameObject crashVictim;

    public Car PlayerCar;

    float elapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = gameObject.transform.position;
        curVelocity = Vector3.zero;
        PlayerCar = gameObject.GetComponent<PlayerCar>();
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Calculates the velocity by finding the deltaPosition over deltaTime
    // Also stores the last velocity
    void calculateVelocity()
    {
        lastVelocity = curVelocity;
        curVelocity = (gameObject.transform.position - lastPosition) * (1/(Time.time - lastCalled));
        lastPosition = gameObject.transform.position;
        lastCalled = Time.time;
    }

    // Calcluates the accleration
    float calcAcceleration()
    {
        return (curVelocity - lastVelocity).magnitude;
    }

    // Calculates the severity of any crash that happens.
    int calculateSeverity(float acceleration)
    {
        acceleration = Mathf.Abs(acceleration);
        if(acceleration >= severeDamage)
        {
            return 3;
        }
        if(acceleration >= moderateDamage)
        {
            return 2;
        }
        if(acceleration >= minorDamage)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsed += Time.deltaTime;
        if(elapsed >= 0.1f)
        {
            // If a crash occurred, calculates the severity. If the damage was severe enough, 
            // puts the car out of commission. Then, if the crash victim was the other car,
            // decommissions it as well. Also plays a different audio based on the crash.
            calculateVelocity();
            if (crashOccured)
            {
                int severity = calculateSeverity(calcAcceleration());
                switch (severity)
                {
                    case 3:
                        Debug.Log("Severe Damage!!");
                        PlayerCar.crashHappened();
                        if(crashVictim.gameObject.tag == "NPC Car")
                        {
                            GameObject npcCar = GameObject.Find("NPC Car");
                            Car npccar = npcCar.GetComponent<NPCCar>();
                            npccar.crashHappened();
                            RaceFollowPath rfp = npcCar.GetComponent<RaceFollowPath>();
                            rfp.inCommission = false;
                        }
                        if(sfxSevere != null)
                        {
                            audio.clip = sfxSevere;
                        }
                        break;
                    case 2:
                        Debug.Log("Moderate Damage!");
                        if(sfxModerate != null)
                        {
                            audio.clip = sfxModerate;
                        }
                        break;
                    case 1:
                        Debug.Log("Minor Damage");
                        if(sfxMinor != null)
                        {
                            audio.clip = sfxMinor;
                        }
                        break;
                    default:
                        break;
                }
                if(severity > 0)
                {
                    audio.Play();
                }
                crashOccured = false;
            }
            elapsed = elapsed % 0.25f;
        }
    }

    // Checks for collision. If there was one, marks crashOccurred as true
    // and also takes note of the crash victim
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 preHitVelocity = curVelocity;
        //Vector3 accelerationLost = curVelocity - gameObject.GetComponent<Rigidbody>().velocity;
        crashOccured = true;
        crashVictim = collision.gameObject;
    }
}
