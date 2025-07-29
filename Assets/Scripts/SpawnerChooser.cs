using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerChooser : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;
    [SerializeField][Range(min: 1, max: 10)] private float _chooseSpawnerDelay;

    private Coroutine _chooseSpawnerCoroutine;

    private void Update()
    {
        if (_chooseSpawnerCoroutine == null)
        {
            _chooseSpawnerCoroutine = StartCoroutine(ChooseSpawner());
        }
    }

    private IEnumerator ChooseSpawner()
    {
        yield return new WaitForSeconds(_chooseSpawnerDelay);

        if(_spawners.Count > 0 )
        {
            _spawners[Random.Range(0, _spawners.Count)].SpawnUnit();
        }

        _chooseSpawnerCoroutine = null;
    }
}
