using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> OnDied;

    // TODO enemy logic
    public void DestroyEnemy()
    {
        // TO DO
        Debug.Log("Destroy " + name);
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            OnDied?.Invoke(this);
        }
    }
}
