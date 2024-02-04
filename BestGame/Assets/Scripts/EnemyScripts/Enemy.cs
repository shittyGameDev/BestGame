using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyDeath(Enemy enemy);

    public event EnemyDeath OnDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Die(){
        OnDeath?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnMouseDown()
    {
        Die();
    }

}
