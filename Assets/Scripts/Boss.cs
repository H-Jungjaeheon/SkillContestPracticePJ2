using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BasicUnit
{
    [SerializeField]
    protected int score;

    [SerializeField]
    private GameObject bullet;

    private StageManager sm; 

    private int randIndex;

    private bool isStartMoving = true;

    WaitForSeconds delay = new WaitForSeconds(0.8f);

    private void Start()
    {
        sm = StageManager.instance;

        moveVector.z = -1f;

        StartCoroutine(Move());
    }

    public override IEnumerator Hit(int damage)
    {
        if (curState == State.Basic && isStartMoving == false)
        {
            hp -= damage;

            sm.bossHpBar.fillAmount = hp / maxHp;

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
        yield return delay;

        randIndex = Random.Range(0, 3);

        switch (randIndex)
        {
            case 0:
                StartCoroutine(FirstPatton());
                break;
            case 1:
                StartCoroutine(SecondPatton());
                break;
            case 2:
                StartCoroutine(ThirdPatton());
                break;
        }

        yield return null;
    }

    private IEnumerator FirstPatton()
    {
        Player player = StageManager.instance.player;
        GameObject curBullet;
        WaitForSeconds shootDelay = new WaitForSeconds(0.1f);

        float targetZ;
        float curSpeed = -10f;

        int plusY;
        
        for (int j = 0; j < 4; j++)
        {
            plusY = -30;

            targetZ = Mathf.Atan2((player.transform.position - transform.position).x, (player.transform.position - transform.position).z) * Mathf.Rad2Deg;

            for (int i = 0; i < 5; i++)
            {
                curBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, targetZ + 180f + plusY, 0f));
                curBullet.GetComponent<BossBullet>().SetSpeed(curSpeed);

                plusY += 15;
            }

            curSpeed += 2f;
            
            yield return shootDelay;
        }

        StartCoroutine(Attack());
    }

    private IEnumerator SecondPatton()
    {
        WaitForSeconds shootDelay = new WaitForSeconds(0.15f);

        float plusIndex = 0f;

        for (int j = 0; j < 7; j ++)
        {
            yield return shootDelay;

            for (int i = 0; i < 360; i += 30)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(0f, i + plusIndex, 0f));
            }

            plusIndex += 7.5f;
        }

        StartCoroutine(Attack());
    }

    private IEnumerator ThirdPatton()
    {
        WaitForSeconds shootDelay = new WaitForSeconds(0.05f);

        float curAngle;

        for (int j = 0; j < 2; j++)
        {
            curAngle = -30f;
            for (int i = 0; i < 7; i++)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(0f, curAngle, 0f));

                curAngle += 10;

                yield return shootDelay;
            }

            for (int i = 0; i < 4; i++)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(0f, curAngle, 0f));

                curAngle -= 15;

                yield return shootDelay;
            }
        }

        StartCoroutine(Attack());
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
            if (transform.position.z <= 18f)
            {
                isStartMoving = false;
                sm.curState = GameState.Play;
                StartCoroutine(Attack());
                break;
            }

            transform.Translate(moveVector * Time.deltaTime * speed);

            yield return null;
        }
    }
}
