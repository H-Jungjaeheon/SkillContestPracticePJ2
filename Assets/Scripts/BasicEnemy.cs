using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyKind
{
    BasicEnemy,
    ShotGunEnemy
}

public class BasicEnemy : BasicUnit
{
    [SerializeField]
    private EnemyKind enemyKind; 

    [SerializeField]
    protected int score;

    [SerializeField]
    private GameObject bullet;

    private void Start()
    {
        moveVector.z = -1f;

        StartCoroutine(Move());
        
        if (enemyKind == EnemyKind.BasicEnemy)
        {
            StartCoroutine(Attack());
        }
    }

    protected override IEnumerator Attack()
    {
        WaitForSeconds shootDelay;

        if (enemyKind == EnemyKind.BasicEnemy)
        {
            shootDelay = new WaitForSeconds(2f);

            while (true)
            {
                yield return shootDelay;

                for (int i = 0; i < 360; i += 30)
                {
                    Instantiate(bullet, transform.position, Quaternion.Euler(90f, i, 0f));
                }
            }
        }
        else if (enemyKind == EnemyKind.ShotGunEnemy)
        {
            Player player = StageManager.instance.player;
            float targetZ;
            int plusZ;

            shootDelay = new WaitForSeconds(2f);

            while (true)
            {
                yield return shootDelay;

                plusZ = -15;
                
                targetZ = Mathf.Atan2((player.transform.position - transform.position).x, (player.transform.position - transform.position).z) * Mathf.Rad2Deg;
                
                for (int i = 0; i < 3; i ++)
                {
                    Instantiate(bullet, transform.position, Quaternion.Euler(90f, targetZ + 180 + plusZ, 0f));

                    plusZ += 15;
                }
            }
        }
    }

    protected override IEnumerator Dead()
    {
        curState = State.Dead;

        StageManager.instance.ScoreUpdate(score);

        Destroy(gameObject);

        yield return null;
    }

    protected override IEnumerator Move()
    {
        while (curState == State.Basic)
        {
            if (enemyKind == EnemyKind.ShotGunEnemy && transform.position.z <= 15f)
            {
                StartCoroutine(Attack());

                break;
            }

            transform.Translate(moveVector * Time.deltaTime * speed);

            yield return null;
        }
    }
}
