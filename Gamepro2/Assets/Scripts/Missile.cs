using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;

    public GameObject hitEffect;
    public void Mtarget(Transform mtarget)
    {
        target = mtarget;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }


    void HitTarget()
    {

        GameObject effect = (GameObject)Instantiate(hitEffect, transform.position, transform.rotation);
        Debug.Log("Missile Hit ");
        Destroy(gameObject);
    }
}
