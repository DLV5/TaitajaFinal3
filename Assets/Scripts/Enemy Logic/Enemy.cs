using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> OnDied;

    [SerializeField] private Collider _attackHitBox;
    [SerializeField] private EnemyAnimator _animator;

    private float _attackDistance;
    private bool _isAttacking;
    private bool _isInitialized = false;
    private Transform _playerTransform;
    private float _speed;

    private bool _isFlipped = false;


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

        LookAtPlayer();
        MoveToPlayer();
        if (Vector3.Distance(transform.position, _playerTransform.position) < _attackDistance)
        {
            StartCoroutine(Attacking());
        }
    }

    public void DestroyEnemy()
    {
        // TO DO
        Debug.Log("Destroy " + name);
        Destroy(gameObject);
    }

    private void MoveToPlayer()
    {
        Vector3 direction = (_playerTransform.position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
    }

    IEnumerator Attacking() // To-Do:  attack logic
    {
        _isAttacking = true;
        yield return new WaitForSeconds(1f);
        _animator.Attack();

        if (_attackHitBox.bounds.Contains(_playerTransform.position))
        {
            IDamagable damagable = _playerTransform.GetComponent<IDamagable>();
            if (damagable != null)
            {
                damagable.TakeDamage(1);
            }
        }
        _isAttacking = false;
    }

    private void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > _playerTransform.position.x && _isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = false;
        }
        else if (transform.position.x < _playerTransform.position.x && !_isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            _isFlipped = true;
        }
    }

}
