using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    protected float minZ;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Image hpBarImg;

    private void Start()
    {
        moveVector.z = -1f;

        StartCoroutine(Move());
        
        if (enemyKind == EnemyKind.BasicEnemy)
        {
            StartCoroutine(Attack());
        }
    }

    public override IEnumerator Hit(int damage)
    {
        if (curState == State.Basic)
        {
            hp -= damage;

            hpBarImg.fillAmount = hp / maxHp;

            if (hp <= 0)
            {
                curState = State.Dead;

                StartCoroutine(Dead());
            }
            else
            {
                sr.color = Color.red;

                yield return hitEffectDelay;

                sr.color = Color.white;
            }
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
                    Instantiate(bullet, transform.position, Quaternion.Euler(0f, i, 0f));
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
                    Instantiate(bullet, transform.position, Quaternion.Euler(0f, targetZ + 180 + plusZ, 0f));

                    plusZ += 15;
                }
            }
        }
    }

    protected override IEnumerator Dead()
    {
        curState = State.Dead;

        GameManager.instance.plusScore(score);

        Destroy(gameObject);

        yield return null;
    }

    protected override IEnumerator Move()
    {
        while (curState == State.Basic)
        {
            if (transform.position.z <= minZ)
            {
                if (enemyKind == EnemyKind.ShotGunEnemy)
                {
                    StartCoroutine(Attack());
                }

                break;
            }

            transform.Translate(moveVector * Time.deltaTime * speed);

            yield return null;
        }
    }
}
