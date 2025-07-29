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
    private Vector3 _rotation;

    public event Action<Unit> Died;

    public void OnEnable()
    {
        if(_lifeCycleCoroutine == null)
        {
            _lifeCycleCoroutine = StartCoroutine(LifeCycle());
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
        _rotation = new Vector3(rotation.x, 0, rotation.y);
        transform.position = position;
    }

    private IEnumerator LifeCycle()
    {
        _moveCoroutine = StartCoroutine(Move());

        yield return new WaitForSeconds(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime));

        StopCoroutine(_moveCoroutine);
        _moveCoroutine = null;
        Died?.Invoke(this);
    }

    private IEnumerator Move()
    {
        while(true)
        {
            transform.Translate(_rotation * _speed*Time.deltaTime);
            yield return null;
        }
    }
}
