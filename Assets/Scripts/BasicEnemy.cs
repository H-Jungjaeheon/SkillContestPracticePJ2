using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyKind
{
    BasicEnemy,
    ShotGunEnemy,
    PowerUpBasicEnemy,
    PowerUpShotGunEnemy,
    SuperPowerUpBasicEnemy,
    SuperPowerUpShotGunEnemy
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
    private GameObject deadParticleObj;

    [SerializeField]
    private Image hpBarImg;

    private void Start()
    {
        moveVector.z = -1f;

        StartCoroutine(Move());
        
        if (enemyKind == EnemyKind.BasicEnemy || enemyKind == EnemyKind.PowerUpBasicEnemy || enemyKind == EnemyKind.SuperPowerUpBasicEnemy)
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
        if (enemyKind == EnemyKind.BasicEnemy)
        {
            while (true)
            {
                yield return new WaitForSeconds(2f);

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

            while (true)
            {
                yield return new WaitForSeconds(2f);

                plusZ = -15;

                targetZ = Mathf.Atan2((player.transform.position - transform.position).x, (player.transform.position - transform.position).z) * Mathf.Rad2Deg;

                for (int i = 0; i < 3; i++)
                {
                    Instantiate(bullet, transform.position, Quaternion.Euler(0f, targetZ + 180 + plusZ, 0f));

                    plusZ += 15;
                }
            }
        }
        else if (enemyKind == EnemyKind.PowerUpBasicEnemy)
        {
            GameObject curBullet;

            while (true)
            {
                yield return new WaitForSeconds(2.5f);

                for (int i = 0; i <= 360; i += 30)
                {
                    Instantiate(bullet, transform.position, Quaternion.Euler(0f, i, 0f));
                }

                yield return new WaitForSeconds(0.5f);

                for (int i = 0; i <= 180; i += 15)
                {
                    curBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, i, 0f));

                    curBullet.GetComponent<Bullet>().speedSetting(-7.5f);
                }

                yield return new WaitForSeconds(2.5f);

                for (int i = 0; i <= 360; i += 30)
                {
                    Instantiate(bullet, transform.position, Quaternion.Euler(0f, i, 0f));
                }

                yield return new WaitForSeconds(0.5f);

                for (int i = 0; i >= -180; i -= 15)
                {
                    curBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, i, 0f));

                    curBullet.GetComponent<Bullet>().speedSetting(-7.5f);
                }
            }
        }
        else if (enemyKind == EnemyKind.PowerUpShotGunEnemy)
        {
            Player player = StageManager.instance.player;

            GameObject curBullet;

            float targetZ;
            int plusZ;

            while (true)
            {
                yield return new WaitForSeconds(2f);

                plusZ = -30;

                targetZ = Mathf.Atan2((player.transform.position - transform.position).x, (player.transform.position - transform.position).z) * Mathf.Rad2Deg;

                for (int i = 0; i < 5; i++)
                {
                    curBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, targetZ + 180 + plusZ, 0f));

                    curBullet.GetComponent<Bullet>().speedSetting(-13f);

                    plusZ += 15;
                }

                yield return new WaitForSeconds(0.5f);

                for (int i = 0; i < 360; i += 90)
                {
                    Instantiate(bullet, transform.position, Quaternion.Euler(0f, i, 0f));
                }
            }
        }
        else if (enemyKind == EnemyKind.SuperPowerUpBasicEnemy)
        {
            GameObject curBullet;

            WaitForSeconds delay = new WaitForSeconds(0.1f);

            while (true)
            {
                yield return new WaitForSeconds(2f);

                for (int i = 0; i <= 360; i += 30)
                {
                    Instantiate(bullet, transform.position, Quaternion.Euler(0f, i, 0f));
                }

                yield return new WaitForSeconds(0.5f);

                for (int i = 0; i <= 360; i += 15)
                {
                    curBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, i, 0f));

                    curBullet.GetComponent<Bullet>().speedSetting(-7.5f);
                }

                yield return new WaitForSeconds(1f);

                for (int i = 80; i >= 0; i -= 10)
                {
                    Instantiate(bullet, transform.position, Quaternion.Euler(0f, i, 0f));
                    Instantiate(bullet, transform.position, Quaternion.Euler(0f, -i, 0f));

                    yield return delay;
                }
            }
        }
        else if (enemyKind == EnemyKind.SuperPowerUpShotGunEnemy)
        {
            Player player = StageManager.instance.player;

            GameObject curBullet;

            float targetZ;
            int plusZ;

            while (true)
            {
                yield return new WaitForSeconds(2f);

                plusZ = -39;

                targetZ = Mathf.Atan2((player.transform.position - transform.position).x, (player.transform.position - transform.position).z) * Mathf.Rad2Deg;

                for (int i = 0; i < 7; i++)
                {
                    curBullet = Instantiate(bullet, transform.position, Quaternion.Euler(0f, targetZ + 180 + plusZ, 0f));

                    curBullet.GetComponent<Bullet>().speedSetting(-13f);

                    plusZ += 13;
                }

                yield return new WaitForSeconds(0.5f);

                for (int i = 0; i < 360; i += 45)
                {
                    Instantiate(bullet, transform.position, Quaternion.Euler(0f, i, 0f));
                }
            }
        }
    }

    protected override IEnumerator Dead()
    {
        curState = State.Dead;

        EnemySpawner.instance.EnemyDeadCount++;

        GameManager.instance.plusScore(score);

        Instantiate(deadParticleObj, transform.position, deadParticleObj.transform.rotation);

        CamShake.instance.StartShake(11, 1f);

        Destroy(gameObject);

        yield return null;
    }

    protected override IEnumerator Move()
    {
        while (curState == State.Basic)
        {
            if (transform.position.z <= minZ)
            {
                if (enemyKind == EnemyKind.ShotGunEnemy || enemyKind == EnemyKind.PowerUpShotGunEnemy || enemyKind == EnemyKind.SuperPowerUpShotGunEnemy)
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
