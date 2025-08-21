using UnityEngine;

[RequireComponent(typeof(UnitPool))] 
public class Spawner : MonoBehaviour
{
    [SerializeField] private Target _target;

    private UnitPool _pool;

    private void Awake()
    {
        _pool = GetComponent<UnitPool>();
    }

    public void SpawnUnit()
    {
        Unit unit = _pool.GetUnit();
        unit.Initialize(Random.insideUnitCircle, transform.position);
        unit.Died += DespawnUnit;
    }
    public void SpawnUnitWithTarget()
    {
        Unit unit = _pool.GetUnit();
        unit.Initialize(_target, transform.position);
        unit.Died += DespawnUnit;
    }

    private void DespawnUnit(Unit unit)
    {
        unit.Died -= DespawnUnit;

        _pool.ReturnUnitToPool(unit);
    }
}
