using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    protected virtual void Start()
    {
        FieldController.Instance.OnCellsInitialiazed += ListenToAllCells;
    }

    protected virtual void OnDestroy()
    {
        FieldController.Instance.OnCellsInitialiazed -= ListenToAllCells;
    }

    protected virtual void ListenToAllCells()
    {
        var cells = FieldController.Instance.Cells;

        foreach (var cell in cells)
        {
            cell.OnCellSelected += OnCellSelected;
        }
    }

    protected virtual void UnlistenToAllCells()
    {
        var cells = FieldController.Instance.Cells;

        foreach (var cell in cells)
        {
            cell.OnCellSelected -= OnCellSelected;
        }
    }

    protected virtual void OnCellSelected(Cell cell)
    {
        Debug.Log($"Cell {cell.gameObject.name} selected.");
    }
}
