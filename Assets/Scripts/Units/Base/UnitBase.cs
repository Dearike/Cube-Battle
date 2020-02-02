using UnityEngine;
using System;

[RequireComponent(typeof(Characteristics))]
public abstract class UnitBase : MonoBehaviour
{
    [SerializeField] protected GameObject unitPrefab;
    public GameObject UnitPrefab => unitPrefab;

    [SerializeField] protected Cell currentCell;
    public Cell CurrentCell
    {
        get { return currentCell; }
        set { currentCell = value; }
    }

    public event Action OnMoveCompleted = () => { };
    public event Action OnDeath = () => { };

    public bool IsLeftTeam { get; set; }

    [Space]

    [SerializeField] protected Characteristics characteristics;
    public Characteristics Characteristics => characteristics;

    protected int health;
    protected int currentHealth;
    protected int attackPower;

    protected virtual void Awake()
    {
        this.GetReferences();
    }

    protected virtual void GetReferences()
    {
        characteristics = GetComponent<Characteristics>();
    }

    public virtual void GoToCell(Cell cell)
    {
        currentCell = cell;
        transform.position = cell.transform.position;
    }

    protected void OnMoveCompletedCall()
    {
        OnMoveCompleted?.Invoke();
    }

    protected void OnDeathCall()
    {
        OnDeath?.Invoke();
    }

    public abstract void MakeMove();
    protected abstract void DoMove(Vector2 move);
    public abstract void GetDamage(int damage);


}
