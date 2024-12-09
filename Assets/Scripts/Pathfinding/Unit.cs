using System;
using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    const float minPathUpdateTime = 0.4f;
    
    public bool isStopped = false;
    public Transform target;
    public float speed = 10;
    private float turnDst = 0;
    public float turnSpeed = 3;
    public float stoppingDst = 1f;
    public bool isDecelerating = true;
    
    Path path;

    [HideInInspector] public Rigidbody rb;

    private Vector3 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(UpdatePath());
    }

    private void FixedUpdate()
    {
        // if (isStopped) return;
        // if (isDecelerating && isStopped) Decelerate();
        if (!isDecelerating) return;
        
        Vector3 speedDif = Vector3.Lerp(rb.velocity, speed * moveDirection, Time.deltaTime * speed);
        
        Vector3 movement = speedDif - rb.velocity;
        movement.y = 0f;
        
        rb.AddForce(movement, ForceMode.VelocityChange);
    }

    private void Decelerate()
    {
        if (rb.velocity.magnitude > 0.1f)
        {
            rb.AddForce(-rb.velocity * 0.5f, ForceMode.Acceleration);
        }
    }

    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
        if (pathSuccessful) {
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);
            
            StopCoroutine(nameof(FollowPath));
            StartCoroutine(nameof(FollowPath));
        }
    }

    IEnumerator UpdatePath()
    {
        // Give a small delay on the start cuz yes
        if (Time.timeSinceLevelLoad < 0.3f)
        {
            yield return new WaitForSeconds(0.3f);
        }
        
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound, this));

        while (true) {
            yield return new WaitForSeconds(minPathUpdateTime);
            PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound, this));
        }
    }

    IEnumerator FollowPath() {
        bool followingPath = true;
        int pathIndex = 0;
        
        float speedPercent;
        
        while (followingPath) {
            if (isStopped)
            {
                moveDirection = Vector3.zero;
                yield break;
            }
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
            while (path.turnBounderies[pathIndex].HasCrossedLine(pos2D))
            {
                if (pathIndex == path.finishLineIndex)
                {
                    followingPath = false;
                    break;
                }
                pathIndex++;
            }

            if (followingPath)
            {
                if (pathIndex >= path.slowDownIndex && stoppingDst > 0f) {
                    speedPercent = Mathf.Clamp01(path.turnBounderies[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst);
                    if (speedPercent < 0.01f)
                    {
                        followingPath = false;
                    }
                }
                
                Vector3 targetDirection = (path.lookPoints[pathIndex] - transform.position).normalized;
                moveDirection = Vector3.Lerp(moveDirection, targetDirection, Time.deltaTime * turnSpeed).normalized;
                moveDirection.y = 0f;

                // transform.Translate(moveDirection * speed * Time.deltaTime);
            }
            
            yield return null;
        }
        moveDirection = Vector3.zero;
    }

    public void OnDrawGizmos() {
        path?.DrawWithGizmos();
    }
}