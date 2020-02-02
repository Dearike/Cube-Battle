using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Cell : MonoBehaviour
{
    [System.Serializable]
    private struct ColorByState
    {
        public CellStateTypes StateType;

        public Color Color;
    }

    [SerializeField] private List<ColorByState> colorByState;

    [SerializeField] private Dictionary<CellStateTypes, ColorByState> colorByStateDict = new Dictionary<CellStateTypes, ColorByState>();

    [SerializeField] private Vector2 coordinate;

    [SerializeField] private UnitBase unitOnCell;
    public UnitBase UnitOnCell
    {
        get { return unitOnCell; }
        set { unitOnCell = value; }
    }
    public bool IsHasUnit => UnitOnCell != null;

    public event Action<Cell> OnCellSelected = (cell) => {};
    public event Action<Cell> OnCellHover = (cell) => {};

    public Vector2 Coordinate
    {
        get { return coordinate; }
        set { coordinate = value; }
    }

    private MeshRenderer meshRenderer;

    [SerializeField] private CellStateTypes currentState;
    public CellStateTypes CurrentState => currentState;

    private void Awake()
    {
        this.InitColorsByStateDict();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void InitColorsByStateDict()
    {
        foreach(var colorByState in colorByState)
        {
            colorByStateDict.Add(colorByState.StateType, colorByState);
        }
    }

    public void SetState(CellStateTypes state)
    {
        this.SetColorByState(state);
        currentState = state;

        switch (state)
        {
            case CellStateTypes.SELECTED:
                OnCellSelected(this);
                break;
            case CellStateTypes.HOVER:
                OnCellHover(this);
                break;
        }
    }

    private void SetColorByState(CellStateTypes state)
    {
        var material = meshRenderer.materials.First();
        material.SetColor("_Color", colorByStateDict[state].Color);
    }

    public bool RegisterUnit(UnitBase unit)
    {
        if (IsHasUnit)
        {
            return false;
        }

        this.unitOnCell = unit;
        return true;
    }
}
