using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rturret : MonoBehaviour
{
    public Transform target;
    public float range = 20f;

    public string enemyTag = "Renemy";

    public Transform Rotate;
    public float turnSpeed = 10f;

    public float fireRate = 1f;
    private float fireCountdown = 0f;

    public GameObject bulletO;
    public Transform firePoint;
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

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(Rotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        Rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletO, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet != null)
            bullet.Btarget(target);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
