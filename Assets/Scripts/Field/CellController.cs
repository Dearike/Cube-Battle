using UnityEngine;

[RequireComponent(typeof(Cell))]
public class CellController : MonoBehaviour
{
    private Cell cell;

    private void Awake()
    {
        cell = GetComponent<Cell>();
    }

    private void OnMouseEnter()
    {
        if(cell.CurrentState == CellStateTypes.SELECTED)
        {
            return;
        }

        cell.SetState(CellStateTypes.HOVER);
    }

    private void OnMouseExit()
    {
        if (cell.CurrentState == CellStateTypes.SELECTED)
        {
            return;
        }

        cell.SetState(CellStateTypes.DEFAULT);
    }

    private void OnMouseDown()
    {
        if (cell.CurrentState == CellStateTypes.SELECTED)
        {
            return;
        }

        FieldController.Instance.UnselectAll();

        cell.SetState(CellStateTypes.SELECTED);
    }
}
