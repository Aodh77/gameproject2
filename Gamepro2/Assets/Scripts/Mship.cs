using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mship : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 force = Vector3.zero;

    public float mass = 1.0f;

    public float maxSpeed = 5;
    public float maxForce = 10;

    public float speed = 0;


    public bool seekEnabled = false;
    public bool arriveEnabled = false;

    private Transform target;
    private Vector3 targetPos;
    private int pointIndex = 0;

    public float banking = 0.1f;
    public float damping = 0.1f;
    public float slowingDistance = 10;

    void Start()
    {
        target = Path.points[0];
        targetPos = target.position;
        Debug.Log(targetPos);
    }

    // Update is called once per frame
    void Update()
    {
        force = CalculateForce();
        acceleration = force / mass;
        velocity += acceleration * Time.deltaTime;

        transform.position += velocity * Time.deltaTime;
        speed = velocity.magnitude;
        if (speed > 0)
        {
            Vector3 tempUp = Vector3.Lerp(transform.up, Vector3.up + (acceleration * banking), Time.deltaTime * 3.0f);
            transform.LookAt(transform.position + velocity, tempUp);
            //transform.forward = velocity;
            velocity -= (damping * velocity * Time.deltaTime);
        }

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextPoint();
        }
 
    }

    void GetNextPoint()
    {
        pointIndex++;
        target = Path.points[pointIndex];
        targetPos = target.position;

    }

    


    public Vector3 CalculateForce()
    {
        Vector3 force = Vector3.zero;
        if (seekEnabled)
        {
            force += Seek(targetPos);
        }
        if (arriveEnabled)
        {
            force += Arrive(targetPos);
        }
        return force;
    }

    Vector3 Seek(Vector3 targetPos)
    {
        Vector3 toTarget = targetPos - transform.position;
        Vector3 desired = toTarget.normalized * maxSpeed;

        return desired - velocity;
    }

    Vector3 Arrive(Vector3 targetPos)
    {
        Vector3 toTarget = targetPos - transform.position;
        float dist = toTarget.magnitude;

        float ramped = (dist / slowingDistance) * maxSpeed;
        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = (toTarget / dist) * clamped;

        return desired - velocity;
    }

 
}
