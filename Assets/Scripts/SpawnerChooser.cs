using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerChooser : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;
    [SerializeField][Range(min: 1, max: 10)] private float _chooseSpawnerDelay;

    private Coroutine _chooseSpawnerCoroutine;

    private void OnEnable()
    {
        if (_chooseSpawnerCoroutine == null)
        {
            _chooseSpawnerCoroutine = StartCoroutine(ChooseSpawner());
        }
    }

    private void OnDisable()
    {
        if (_chooseSpawnerCoroutine != null)
        {
            StopCoroutine( _chooseSpawnerCoroutine );
            _chooseSpawnerCoroutine = null;
        }
    }

    private IEnumerator ChooseSpawner()
    {
        WaitForSeconds delay = new WaitForSeconds( _chooseSpawnerDelay );

        while(enabled)
        {
            yield return delay;

            if (_spawners.Count > 0)
            {
                _spawners[Random.Range(0, _spawners.Count)].SpawnUnit();
            }
        }
    }
}
