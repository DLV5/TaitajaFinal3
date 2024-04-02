using System;
using UnityEngine;

public class AttackColisionDetection : MonoBehaviour
{
    public static event Action<IDamagable> OnHittedEnemy;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            if(damagable != null )
            {
                OnHittedEnemy?.Invoke(damagable);
            }
        }
    }
}
