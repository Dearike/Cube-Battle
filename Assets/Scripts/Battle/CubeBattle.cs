using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CubeBattle : BattleBase
{
    [SerializeField] protected GameObject winnerPannel;

    protected void Awake()
    {
        winnerPannel.SetActive(false);
    }

    public override void StartBattle()
    {
        leftTeam.SpawnTeam(true);
        rigthTeam.SpawnTeam(false);

        playButton.SetActive(false);

        StartCoroutine(mainCamera.RotateCamera(45f));
    }

    public override void EndBattle()
    {
        leftTeam.isTeamMoveNow = false;
        rigthTeam.isTeamMoveNow = false;

        StartCoroutine(SlowEndBattle());
    }

    private IEnumerator SlowEndBattle()
    {
        ShowWinnerPanel();

        yield return new WaitForSeconds(2f);

        winnerPannel.SetActive(false);
        winnerTeam = null;

        StartCoroutine(mainCamera.RotateCamera(-45f));

        playButton.SetActive(true);

        leftTeam.DestroyTeam();
        rigthTeam.DestroyTeam();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndBattle();
        }
    }

    private void ShowWinnerPanel()
    {
        winnerPannel.SetActive(true);
        var text = winnerPannel.transform.GetChild(0).GetComponent<Text>();
        text.text = winnerTeam.TeamName + " wins!";
        text.color = winnerTeam.Color;
    }
}
