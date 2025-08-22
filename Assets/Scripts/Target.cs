using System.Collections;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField][Range(min:1,max:10)] private float _movementRadius;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _stoppingDistance;

    private Vector3 _waypoint1;
    private Vector3 _waypoint2;
    private Coroutine _movement;
    public Vector3 Position => transform.position;

    private void OnEnable()
    {
        Vector3 startPos = transform.position;
        float heightOffset = 0f;

        Vector2 randomPoint = Random.insideUnitCircle * _movementRadius;
        _waypoint1 = startPos + new Vector3(randomPoint.x, heightOffset, randomPoint.y);

        randomPoint = Random.insideUnitCircle * _movementRadius;
        _waypoint2 = startPos + new Vector3(randomPoint.x, heightOffset, randomPoint.y);

        _movement = StartCoroutine(Move());
    }

    private void OnDisable()
    {
        if(_movement != null)
        {
            StopCoroutine(Move());
            _movement = null;
        }
    }

    private IEnumerator Move()
    {
        Vector3 currentWayPoint = _waypoint1;

        while (isActiveAndEnabled)
        {
            while (IsCloseEnough(currentWayPoint, transform.position))
            {
                transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, _movementSpeed * Time.deltaTime);

                yield return null;
            }

            if (currentWayPoint == _waypoint1)
            {
                currentWayPoint = _waypoint2;
            }
            else
            {
                currentWayPoint = _waypoint1;
            }
        }
    }

    private bool IsCloseEnough(Vector3 currentWayPoint, Vector3 currentPosition)
    {
        return (currentWayPoint - currentPosition).sqrMagnitude > _stoppingDistance * _stoppingDistance;
    }
}
