using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BasicUnit
{
    [SerializeField]
    protected int score;

    [SerializeField]
    private GameObject bullet;

    private int randIndex;

    WaitForSeconds delay = new WaitForSeconds(0.8f);

    private void Start()
    {
        moveVector.z = -1f;

        StartCoroutine(Move());
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

        int plusZ;
        
        for (int j = 0; j < 4; j++)
        {
            plusZ = -30;

            targetZ = Mathf.Atan2((player.transform.position - transform.position).x, (player.transform.position - transform.position).z) * Mathf.Rad2Deg;

            for (int i = 0; i < 5; i++)
            {
                curBullet = Instantiate(bullet, transform.position, Quaternion.Euler(90f, targetZ + 180 + plusZ, 0f));
                curBullet.GetComponent<BossBullet>().SetSpeed(curSpeed);

                plusZ += 15;
            }

            curSpeed += 2f;
            
            yield return shootDelay;
        }

        StartCoroutine(Attack());
    }

    private IEnumerator SecondPatton()
    {
        WaitForSeconds shootDelay = new WaitForSeconds(0.25f);

        float plusIndex = 0f;

        for (int j = 0; j < 7; j ++)
        {
            yield return shootDelay;

            for (int i = 0; i < 360; i += 30)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(90f, i + plusIndex, 0f));
            }

            plusIndex += 7.5f;
        }

        StartCoroutine(Attack());
    }

    private IEnumerator ThirdPatton()
    {
        WaitForSeconds shootDelay = new WaitForSeconds(0.1f);

        float curAngle;

        for (int j = 0; j < 2; j++)
        {
            curAngle = -30f;
            for (int i = 0; i < 7; i++)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(90f, curAngle, 0f));

                curAngle += 10;

                yield return shootDelay;
            }

            for (int i = 0; i < 4; i++)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(90f, curAngle, 0f));

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
                StartCoroutine(Attack());
                break;
            }

            transform.Translate(moveVector * Time.deltaTime * speed);

            yield return null;
        }
    }
}
