using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class TeamBase : MonoBehaviour
{
    [SerializeField] protected string teamName;
    public string TeamName
    {
        get { return teamName; }
        set { teamName = value; }
    }

    [SerializeField] protected Color color;
    public Color Color => color;

    protected List<UnitBase> units = new List<UnitBase>();
    public List<UnitBase> Units
    {
        get { return units; }
        set { units = value; }
    }
    public int UnitsCount => units.Count;
    public bool isTeamMoveNow { get; set; } = false;

    public event Action OnTeamMoveCompleted = () => { };
    public event Action OnTeamDefeated = () => { };

    protected void OnTeamMoveCompletedCall()
    {
        OnTeamMoveCompleted?.Invoke();
    }
    protected void OnTeamDefeatedCall()
    {
        OnTeamDefeated?.Invoke();
    }

    public abstract void SpawnTeam(bool isLeftTeam);
    public abstract void DestroyTeam();
}
