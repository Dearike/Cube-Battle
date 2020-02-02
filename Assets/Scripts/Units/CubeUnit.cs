using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CubeUnit : UnitBase, IDamageable
{
    private bool isMove;
    [SerializeField] protected Image healthBar;
    [SerializeField] protected Text heathText;
    [SerializeField] protected ParticleSystem getDamageParticles;
    [SerializeField] protected ParticleSystem deathParticlesPrefab;

    protected override void Awake()
    {
        this.GetReferences();
    }

    protected void Start()
    {
        health = currentHealth = Random.Range(5, 11);
        heathText.text = health.ToString();

        attackPower = characteristics.GetPropertyByType(PropertyType.ATTACK).value;
    }

    public override void MakeMove()
    {
        if (isMove)
        {
            return;
        }

        isMove = true;

        var enemyCell = FieldController.Instance.GetEnemyPosition(currentCell);

        if ((Mathf.Abs(enemyCell.Coordinate.x - currentCell.Coordinate.x) == 1 && 
             enemyCell.Coordinate.y == currentCell.Coordinate.y) ||
            (Mathf.Abs(enemyCell.Coordinate.y - currentCell.Coordinate.y) == 1 &&
             enemyCell.Coordinate.x == currentCell.Coordinate.x))
        {
            StartCoroutine(DoDamage(enemyCell.UnitOnCell));
        }
        else
        {
            Vector2 move;

            if (!CheckEnemyAtMyRow()) //по умолчанию идем прямо
            {
                move = IsLeftTeam ? new Vector2(1, 0) : new Vector2(-1, 0);
            }
            else //если нашли врага в ряду
            {
                move = enemyCell.Coordinate.y > currentCell.Coordinate.y ? new Vector2(0, 1) : new Vector2(0, -1);
            }

            this.DoMove(move);
        }
    }

    public override void GetDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            healthBar.fillAmount = 0f;
            currentHealth = 0;
            heathText.text = "0";
            Death();
        }
        else
        {
            heathText.text = currentHealth.ToString();
            healthBar.fillAmount = (float)currentHealth / health;
        }

        getDamageParticles.Play();
    }

    protected override void DoMove(Vector2 move)
    {
        currentCell.UnitOnCell = null;

        currentCell = FieldController.Instance.GetCellByCoordinates(currentCell.Coordinate + move);
        currentCell.UnitOnCell = this;

        StartCoroutine(DoStep(0.5f, currentCell.transform.position));
    }

    private IEnumerator DoStep(float time, Vector3 targetPosition)
    {        
        Vector3 startPosition = transform.position;
        float startTime = Time.realtimeSinceStartup;
        float fraction = 0f;

        while (fraction < 1f)
        {
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time * 0.8f);
            transform.position = Vector3.Lerp(startPosition, targetPosition, fraction);
            yield return null;
        }

        yield return new WaitForSeconds(time * 0.2f);
        isMove = false;
        OnMoveCompletedCall();
    }

    private IEnumerator DoDamage(UnitBase unit)
    {
        unit.GetDamage(attackPower);
        yield return new WaitForSeconds(1f);
        isMove = false;
        OnMoveCompletedCall();
    }

    private void Death()
    {
        OnDeathCall();
        Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private bool CheckEnemyAtMyRow()
    {
        return FieldController.Instance.CheckEnemyAtRow(currentCell.Coordinate);
    }
}
