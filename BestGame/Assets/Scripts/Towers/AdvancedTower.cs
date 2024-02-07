using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedTower : TowerBase
{
    public float turnSpeed = 5f;
    public int damage = 3;
    public int health = 20;
    public float range = 10f;
    public float fireRate = 0.5f;
    public float fireCountdown = 0f;
    public GameObject bulletPrefab;

    private Transform target;
    private bool canShoot = false;
    // Start is called before the first frame update
    void Start()
    {
        cost = 25;
        InvokeRepeating("FindTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
            Shoot();
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
