using UnityEngine;

public abstract class BattleBase : MonoBehaviour
{
    [SerializeField] protected TeamBase leftTeam;
    public TeamBase LeftTeam => leftTeam;
    
    [SerializeField] protected TeamBase rigthTeam;
    public TeamBase RigthTeam => rigthTeam;

    [SerializeField] protected CameraController mainCamera;

    [SerializeField] protected GameObject playButton;

    protected TeamBase winnerTeam;

    protected virtual void Start()
    {
        leftTeam.OnTeamMoveCompleted += SwitchMovingTeam;
        rigthTeam.OnTeamMoveCompleted += SwitchMovingTeam;

        leftTeam.OnTeamDefeated += RigthTeamWins;
        rigthTeam.OnTeamDefeated += LeftTeamWins;
    }

    protected virtual void OnDestroy()
    {
        leftTeam.OnTeamMoveCompleted -= SwitchMovingTeam;
        rigthTeam.OnTeamMoveCompleted -= SwitchMovingTeam;

        leftTeam.OnTeamDefeated -= RigthTeamWins;
        rigthTeam.OnTeamDefeated -= LeftTeamWins;
    }
    protected virtual void SwitchMovingTeam()
    {
        if (winnerTeam == null)
        {
            leftTeam.isTeamMoveNow = !leftTeam.isTeamMoveNow;
            rigthTeam.isTeamMoveNow = !rigthTeam.isTeamMoveNow;
        }
    }

    protected virtual void LeftTeamWins()
    {
        winnerTeam = leftTeam;
        EndBattle();
    }

    protected virtual void RigthTeamWins()
    {
        winnerTeam = rigthTeam;
        EndBattle();
    }

    public abstract void StartBattle();

    public abstract void EndBattle();
}
