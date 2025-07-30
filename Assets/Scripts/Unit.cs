using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class Unit : MonoBehaviour
{
    [SerializeField][Range(min: 1, max: 10)] private float _minLifeTime;
    [SerializeField][Range(min: 1, max: 10)] private float _maxLifeTime;
    [SerializeField][Range(min: 1, max: 10)] private float _speed;

    private Coroutine _lifeCycleCoroutine;
    private Coroutine _moveCoroutine;
    private Vector3 _direction;

    public event Action<Unit> Died;

    public void OnEnable()
    {
        if (_lifeCycleCoroutine == null)
        {
            _lifeCycleCoroutine = StartCoroutine(LifeCycle());
        }

        if (_moveCoroutine == null)
        {
            _moveCoroutine = StartCoroutine(Move());
        }
    }

    private void OnDisable()
    {
        if (_lifeCycleCoroutine != null)
        {
            StopCoroutine(_lifeCycleCoroutine);
            _lifeCycleCoroutine = null;
        }

        if (_moveCoroutine != null)
        {
            StopCoroutine(_moveCoroutine);
            _moveCoroutine = null;
        }
    }

    public void Initialize(Vector2 rotation, Vector3 position)
    {
        _direction = new Vector3(rotation.x, 0, rotation.y).normalized;
        transform.position = position;
    }

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime));

        Died?.Invoke(this);
    }

    private IEnumerator Move()
    {
        while(enabled)
        {
            transform.Translate(_direction * _speed*Time.deltaTime);
            yield return null;
        }
    }
}
