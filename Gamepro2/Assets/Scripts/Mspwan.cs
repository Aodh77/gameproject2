using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mspwan : MonoBehaviour
{
    public Transform target;
    public float range = 20f;

    public string enemyTag = "Rocinate";

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject missileO;
    public Transform missilePoint;
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToE = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToE < shortestDistance)
            {
                shortestDistance = distanceToE;
                nearEnemy = enemy;
            }

        }

        if (nearEnemy != null && shortestDistance <= range)
        {
            target = nearEnemy.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject missileGO = (GameObject)Instantiate(missileO, missilePoint.position, missilePoint.rotation);
        Missile missile = missileGO.GetComponent<Missile>();

        if (missile != null)
            missile.Mtarget(target);
    }
}
