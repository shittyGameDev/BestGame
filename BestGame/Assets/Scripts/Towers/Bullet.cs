using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float speed = 10f;
    public float range = 5f;
    public Transform target;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (Vector3.Distance(startPosition, transform.position) >= range)
            {
                Destroy(gameObject);
            }
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

    private void HitTarget()
    {
        Debug.Log("Hit Target");
        Destroy(gameObject);
        Destroy(target.gameObject);
        /* Enemy enemy = target.GetComponent<Enemy>();
        enemy.TakeDamage(damage); */
    }
}
