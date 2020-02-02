using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGenerator : MonoBehaviour
{
    [System.Serializable]
    public struct FieldSize
    {
        public int X;
        public int Y;
    }

    [SerializeField] private GameObject cellPrefab;

    [SerializeField] private FieldSize fieldSize;

    [SerializeField] private Vector2 offset;

    [ContextMenu("Generate Field")]
    public void GenerateField()
    {
        var gameField = new GameObject("GameField");

        for(int i = 0; i < fieldSize.X; i++)
        {
            for(int j = 0; j < fieldSize.Y; j++)
            {
                var cellClone = Instantiate(cellPrefab, gameField.transform);

                var mesh = cellClone.GetComponent<MeshRenderer>();

                var meshSize = mesh.bounds.size;
                var position = new Vector3((meshSize.x + offset.x) * i, 0,( meshSize.z + offset.y) * j);

                cellClone.transform.position = position;

                cellClone.name = $"Cell X:{i} Y:{j}";

                var cellComponent = cellClone.GetComponent<Cell>();

                if(cellComponent != null)
                {
                    cellComponent.Coordinate = new Vector2(i, j);
                }
                else
                {
                    Debug.LogError("Cell component not found.");
                }
            }
        }
    }

    private void Awake()
    {
        FieldController.Instance.FieldSize = fieldSize;
    }
}
