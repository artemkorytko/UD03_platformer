using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private Transform firstPoint;
    [SerializeField] private Transform secondPoint;

    private Transform _target;
    private EnemyAnimationController _animationController;

    private void Awake()
    {
        _animationController = GetComponent<EnemyAnimationController>();
    }

    private void Start()
    {
        _target = Random.Range(0, 2) == 0 ? firstPoint : secondPoint;
    }

    private void Update()
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        float moveDistance = speed * Time.deltaTime;
        float distanceToTarget = Vector3.Distance(_target.position, transform.position);

        if (moveDistance > distanceToTarget)
        {
            moveDistance = distanceToTarget;
            if (_target == firstPoint)
            {
                _target = secondPoint;
            }
            else
            {
                _target = firstPoint;
            }
        }
        
        transform.Translate(direction * moveDistance);
        _animationController.SetSpeed((int)Mathf.Sign(direction.x));
        UpdateSide((int)Mathf.Sign(direction.x));
    }

    private void UpdateSide(int side)
    {
        Vector2 localScale = transform.localScale;
        if (Mathf.Sign(localScale.x) != side)
        {
            localScale.x *= -1;
        }

        transform.localScale = localScale;
    }
}