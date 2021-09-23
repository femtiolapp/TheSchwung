using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class enemyAI : MonoBehaviour
{
    public Transform Target;
    public float speed = 200f;
    public float nextwayPointDist = 3f;
    public Transform AiGrapichs;
    Path path;
    int currentWaypoint = 0;
    bool reachEndOfPath = false;

    Seeker seeker;
    Rigidbody2D RB;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        RB = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        

    }
    void OnPathComplete (Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
        
    }
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(RB.position, Target.position, OnPathComplete);
    }

    void FixedUpdate()
    {
        if (path == null )
        
            return;
       

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;
            return;
        }
        else
        {
            reachEndOfPath = false;

        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - RB.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        RB.AddForce(force);

        float distance = Vector2.Distance(RB.position, path.vectorPath[currentWaypoint]);
        
        if (distance < nextwayPointDist)
        {
            currentWaypoint++;
        }

        if(RB.velocity.x >= 0.01f)
        {
            transform.localScale =  new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            
        }
        else if (RB.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

    }
}
