using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eship : MonoBehaviour
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

    public GameObject target;
    //public GameObject newTarget;
    public Vector3 targetPos;

    public float banking = 0.1f;
    public float damping = 0.1f;
    public float slowingDistance = 10;

    void Start()
    {
        targetPos = target.transform.position;
    }


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

    Vector3 Arrive(Vector3 targetPos)
    {
        Vector3 toTarget = targetPos - transform.position;
        float dist = toTarget.magnitude;

        float ramped = (dist / slowingDistance) * maxSpeed;
        float clamped = Mathf.Min(ramped, maxSpeed);
        Vector3 desired = (toTarget / dist) * clamped;

        return desired - velocity;
    }

    Vector3 Seek(Vector3 targetPos)
    {
        Vector3 toTarget = targetPos - transform.position;
        Vector3 desired = toTarget.normalized * maxSpeed;

        return desired - velocity;
    }
}
