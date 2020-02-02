using System.Collections.Generic;
using UnityEngine;
class CubeTeam : TeamBase
{
    [SerializeField] protected List<CubeUnit> unitPrefabs = new List<CubeUnit>();

    private int movedUnitsNumber;
    private int deadUnitsNumber;

    protected virtual void Update()
    {
        if (isTeamMoveNow)
        {
            try
            {
                units.ForEach(u => u?.MakeMove());
            }
            catch(System.InvalidOperationException e)
            {
                Debug.LogError(e.Message);
            }
        }
    }

    protected virtual void ListenToUnits()
    {
        foreach (var unit in units)
        {
            unit.OnMoveCompleted += IncreaseMovedUnitsNumber;
            unit.OnDeath += IncreaseDeadUnitsNumber;
        }
    }

    protected virtual void UnlistenToAllUnits()
    {
        foreach (var unit in units)
        {
            unit.OnMoveCompleted -= IncreaseMovedUnitsNumber;
            unit.OnDeath -= IncreaseDeadUnitsNumber;
        }
    }

    private void IncreaseMovedUnitsNumber()
    {
        movedUnitsNumber++;

        if(movedUnitsNumber == UnitsCount)
        {
            OnTeamMoveCompletedCall();
            movedUnitsNumber = 0;
        }
    }

    private void IncreaseDeadUnitsNumber()
    {
        deadUnitsNumber++;

        if (deadUnitsNumber == UnitsCount)
        {
            deadUnitsNumber = 0;
            OnTeamDefeatedCall();
        }
    }

    public override void SpawnTeam(bool isLeftTeam)
    {
        foreach (var unit in unitPrefabs)
        {
            var x = Random.Range(0, FieldController.Instance.FieldSize.X / 2);

            if (!isLeftTeam)
            {
                x += (FieldController.Instance.FieldSize.X + 1) / 2;
            }

            var y = Random.Range(0, FieldController.Instance.FieldSize.Y);

            var cell = FieldController.Instance.GetCellByCoordinates(new Vector2(x, y));

            var unitClone = Instantiate(unit.UnitPrefab, cell.transform.position, Quaternion.identity).GetComponent<CubeUnit>();

            if (unitClone != null)
            {
                cell.UnitOnCell = unitClone;
                unitClone.CurrentCell = cell;
                unitClone.IsLeftTeam = isLeftTeam;
                unitClone.transform.parent = transform;
                units.Add(unitClone);
            }
        }

        ListenToUnits();

        isTeamMoveNow = isLeftTeam ? true : false;
    }

    public override void DestroyTeam()
    {
        isTeamMoveNow = false;

        foreach (var unit in units)
        {
            if (unit != null)
            {
                Destroy(unit.gameObject);
            }
        }

        units.Clear();
    }
}
