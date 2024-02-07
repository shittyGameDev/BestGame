using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _explosionRadius = 5f;
    //[SerializeField] private float _explosionForce = 700f;
    [SerializeField] private GameObject _explosionEffect;
    [SerializeField] private float _delay = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Explode", _delay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Explode(){
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider nearbyObject in colliders)
        {
            Enemy enemy = nearbyObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                //Destroy(enemy.gameObject);
                enemy.TakeDamage(_damage);
            }
        }
        Instantiate(_explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
