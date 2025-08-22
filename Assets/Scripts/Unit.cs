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

    public event Action<Unit> Died;

    private void OnEnable()
    {
        if (_lifeCycleCoroutine == null)
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

    public void Initialize(Target target, Vector3 position)
    {
        transform.position = position;

        if (_moveCoroutine == null)
        {
            _moveCoroutine = StartCoroutine(Move(target));
        }
    }

    private IEnumerator LifeCycle()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(_minLifeTime, _maxLifeTime));

        Died?.Invoke(this);
    }

    private IEnumerator Move(Target target)
    {
        while (enabled)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.Position, _speed * Time.deltaTime);
            yield return null;
        }
    }
}
