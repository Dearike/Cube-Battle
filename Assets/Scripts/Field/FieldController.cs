using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class FieldController : MonoBehaviour
{
    public static FieldController Instance { get; private set; }

    [SerializeField] private List<Cell> cells = new List<Cell>();
    public List<Cell> Cells => cells;
    public FieldGenerator.FieldSize FieldSize { get; set; }
    public Vector3 FieldCenter;

    public event Action OnCellsInitialiazed = () => {};

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        this.FindCells();
    }

    private void FindCells()
    {
        var cellsInSceneList = FindObjectsOfType<Cell>();
        cells = cellsInSceneList.OrderBy(c => c.Coordinate.x).ToList();

        OnCellsInitialiazed();
    }

    public void AddCell(Cell cell)
    {
        cells.Add(cell);
    }

    public void UnselectAll()
    {
        cells.ForEach(c => c.SetState(CellStateTypes.DEFAULT));
    }

    public Cell GetCellByCoordinates(Vector2 cellCoordinate)
    {
        return cells.First(c =>
            c.Coordinate.x == cellCoordinate.x &&
            c.Coordinate.y == cellCoordinate.y);
    }

    public bool CheckEnemyAtRow(Vector2 cellCoordinate) //переделать
    {
        return cells.Any(c =>
            c.UnitOnCell != null &&
            c.Coordinate.x == cellCoordinate.x && 
            c.Coordinate.y != cellCoordinate.y);
    }

    public Cell GetEnemyPosition(Cell cell)
    {
        return cells.First(c => c != cell && c.UnitOnCell != null);
    }

}
