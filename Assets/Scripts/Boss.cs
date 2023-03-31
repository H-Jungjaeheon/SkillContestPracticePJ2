using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BasicUnit
{
    [SerializeField]
    protected int score;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject bossDeadParitcleObj;

    [SerializeField]
    private SpriteRenderer shoutEffect;

    private StageManager sm; 

    private int randIndex;

    private bool isStartMoving = true;

    WaitForSeconds delay = new WaitForSeconds(0.8f);

    private IEnumerator attackCoroutine;

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

                StopCoroutine(attackCoroutine);

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
                attackCoroutine = FirstPatton();
                break;
            case 1:
                attackCoroutine = SecondPatton();
                break;
            case 2:
                attackCoroutine = ThirdPatton();
                break;
        }

        StartCoroutine(attackCoroutine);
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

        attackCoroutine = Attack();
        StartCoroutine(attackCoroutine);
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

        attackCoroutine = Attack();
        StartCoroutine(attackCoroutine);
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

        attackCoroutine = Attack();
        StartCoroutine(attackCoroutine);
    }

    protected override IEnumerator Dead()
    {
        curState = State.Dead;

        StageManager.instance.BossUIOff();

        GameManager.instance.plusScore(score);

        bossDeadParitcleObj.SetActive(true);

        yield return new WaitForSeconds(5f);

        Destroy(gameObject);
    }

    protected override IEnumerator Move()
    {
        while (curState == State.Basic)
        {
            if (transform.position.z <= 14f)
            {
                StageManager.instance.StartCoroutine(StageManager.instance.BossStartUIAnim());
                StartCoroutine(Shout());
                break;
            }

            transform.Translate(moveVector * Time.deltaTime * speed);

            yield return null;
        }
    }

    protected IEnumerator Shout()
    {
        Color color;

        Vector3 curScale;

        CamShake.instance.StartShake(90, 1f);

        for (int i = 0; i < 5; i++)
        {
            color = Color.white;
            curScale = Vector3.zero;

            while (true)
            {
                shoutEffect.color = color;
                shoutEffect.transform.localScale = curScale;

                color.a -= Time.deltaTime * 2f;

                curScale.x += Time.deltaTime * 60f;
                curScale.y += Time.deltaTime * 60f;

                if (color.a <= 0f)
                {
                    break;
                }

                yield return null;
            }
        }

        isStartMoving = false;
        sm.curState = GameState.Play;

        attackCoroutine = Attack();
        StartCoroutine(attackCoroutine);
    }
}
