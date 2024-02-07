using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : TowerBase
{
    // Tower Attributes
    [Header("Tower Attributes")]
    public float turnSpeed = 10f;
    public int damage = 1;
    public int health = 10;
    public float range = 5f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    public GameObject bulletPrefab;

    private Transform target;
    private bool canShoot = false;

    void Start()
    {
        cost = 10;
        InvokeRepeating("FindTarget", 0f, 0.5f);
    }

    void Update()
    {
        if (target != null)
        {
            // Update the tower's rotation to face the target
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

            // Check if the tower can shoot
            if (canShoot)
            {
                Shoot();
            }
        }
    }

    private void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            canShoot = true;
        }
        else
        {
            target = null;
            canShoot = false;
        }
    }

    private void Shoot()
    {
        fireCountdown += Time.deltaTime;
        if (fireCountdown >= fireRate)
        {
            fireCountdown = 0f;
            GameObject bulletGO = Instantiate(bulletPrefab, transform.position, transform.rotation);
            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.damage = damage;
                bullet.target = target;
            }
        }
    }
}
