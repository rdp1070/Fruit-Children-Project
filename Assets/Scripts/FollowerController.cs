using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour
{

    [Tooltip("Whether or not the follower is currently following.")]
    public bool isFollowing;

    [Tooltip("What the follower is currently following.")]
    public GameObject followTarget;

    [Tooltip("How fast the follower is currently following.")]
    public float speed;
    [Tooltip("How fast they go when the follow target exceeds a certain distance.")]
    public float sprint_speed;

    [Tooltip("How much the current follower cares about following.")]
    public float strength;

    [Tooltip("How close the follower gets before they stop following.")]
    public float satisfied_distance;

    [Tooltip("How close the follower gets before they start panic-following.")]
    public float panic_distance;

    [Tooltip("How fast the follower turns")]
    public float rotationSpeed = 3f; //speed of turning


    private Transform myTransform;
    private Renderer renderer;

    void Awake()
    {
        renderer = GetComponent<Renderer>();
        myTransform = transform; //cache transform data for easy access/preformance
        followTarget = GameObject.FindWithTag("Player");
    }

    // Use this for initialization
    void Start()
    {
        speed = 2f;
        sprint_speed = speed * 2;
        satisfied_distance = speed;
        panic_distance = 30f;
        strength = 1;
        isFollowing = true;
    }

    void FixedUpdate()
    {

        // get the location of the FollowTarget
        var target_loc = followTarget.transform.position;
        var distance = Vector3.Distance(target_loc, transform.position);

        if (isFollowing) // if it is currently following
        {
            // if they are close enough, just skip this part.
            if (distance > satisfied_distance)
            {
                //rotate to look at the player
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
                Quaternion.LookRotation(target_loc - myTransform.position), rotationSpeed * Time.deltaTime);

                if (distance > panic_distance)
                {
                    //move towards the player in a hurry
                    renderer.material.color = Color.red;
                    myTransform.position += myTransform.forward * sprint_speed * Time.deltaTime;
                }
                else
                {
                    //move towards the player
                    renderer.material.color = Color.gray;
                    myTransform.position += myTransform.forward * speed * Time.deltaTime;
                }
            }
            else
            {
                renderer.material.color = Color.green;
                isFollowing = false;
            }
        }
        else {
            if (distance >= satisfied_distance * 2) {
                isFollowing = true;
            }
        }

        
    }
}
