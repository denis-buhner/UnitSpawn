using System.Collections.Generic;
using UnityEngine;

public class UnitPool : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;
    [SerializeField] private Transform _container;

    private Queue<Unit> _pool = new Queue<Unit>();

    public Unit GetUnit()
    {
        if (_pool.Count == 0)
        {
            Unit unit = Instantiate(_unitPrefab, _container);
            return unit;
        }

        Unit uniToReturn = _pool.Dequeue();
        uniToReturn.gameObject.SetActive(true);

        return uniToReturn;
    }

    public void ReturnUnitToPool(Unit unit)
    {
        unit.gameObject.SetActive(false);
        _pool.Enqueue(unit);
    }
}
