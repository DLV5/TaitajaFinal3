using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> OnDied;
    private float _attackDistance;
    private bool _isAttacking;
    private bool _isInitialized = false;
    private Transform _playerTransform;
    private float _speed;

    // TODO enemy logic
    public void Initialize(Transform playerTransform, float speed, float attackDistance)
    {
        _playerTransform = playerTransform;
        _speed = speed;
        _attackDistance = attackDistance;
        _isInitialized = true;
    }

    private void Update()
    {
        if (!_isInitialized)
            return;
        if (_isAttacking)
            return;

        transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, _speed/10 * Time.deltaTime);
        if(Vector3.Distance(transform.position, _playerTransform.position) < _attackDistance)
        {
            StartCoroutine(Attacking());
        }
    }

    private void OnTriggerEnter(Collider other) // Imitation of death
    {
        if (other.gameObject.CompareTag("aaa"))
        {
            OnDied?.Invoke(this);
            Debug.Log("Invoking");
            _isInitialized = false;
        }
    }

    public void DestroyEnemy()
    {
        // TO DO
        Debug.Log("Destroy " + name);
        Destroy(gameObject);
    }

    IEnumerator Attacking() // To-Do:  attack logic
    {
        _isAttacking = true;
        Debug.Log("Started Enemy Attack");
        yield return new WaitForSeconds(2f);
        Debug.Log("Ended Enemy Attack");
        _isAttacking = false;
    }

}
