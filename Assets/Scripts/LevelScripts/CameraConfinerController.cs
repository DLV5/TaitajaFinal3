using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfinerController : MonoBehaviour
{
    public static event Action OnPlayerReachedNewBattle;

    [SerializeField] private List<Collider> _colliders;
    [SerializeField] private Collider _generalCollider;

    [SerializeField] private CinemachineConfiner _confiner;

    [SerializeField] private GameObject _player;

    private bool _shouldCheckForNewPoint = false;
    private int index = 0;

    private void OnEnable()
    {
        LevelManager.Instance.MoveToTheNextBattle += OnBattleChange;
        OnPlayerReachedNewBattle += OnReachedNewBattlePoint;
    }

    private void Update()
    {
        if (!_shouldCheckForNewPoint) return;

        if (IsPlayerReachedNewPoint())
        {
            OnPlayerReachedNewBattle?.Invoke();
            _shouldCheckForNewPoint = false;
        }
    }

    private void OnDisable()
    {
        LevelManager.Instance.MoveToTheNextBattle -= OnBattleChange;
        OnPlayerReachedNewBattle -= OnReachedNewBattlePoint;
    }

    public void OnReachedNewBattlePoint()
    {
        if (index > _colliders.Count) return;

        _confiner.m_BoundingVolume = _colliders[index];

        index++;
    }

    private void OnBattleChange()
    {
        _confiner.m_BoundingVolume = _generalCollider;
        _shouldCheckForNewPoint = true;
    }

    private bool IsPlayerReachedNewPoint()
    {
        if (index >= _colliders.Count) return false;

        return (_colliders[index].transform.position - _player.transform.position).magnitude < 11.5f;
    }
}
