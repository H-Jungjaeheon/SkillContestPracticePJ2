using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : BasicUnit
{
    [SerializeField]
    private GameObject deadParticle;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private GameObject[] bodyObjs;

    [SerializeField]
    private float destoryTime;

    [SerializeField]
    private GameObject smokeParticle;

    [SerializeField]
    private Rigidbody rigid;


    private void Start()
    {
        StartCoroutine(BasicAnim());
        StartCoroutine(Move());
        Destroy(gameObject, destoryTime);
    }

    IEnumerator BasicAnim()
    {
        Vector3 openPos = new Vector3(1.25f, 0f, 0f);
        Vector3 closePos = new Vector3(0.75f , 0f, 0f);

        float curAnimTime = 0f;
        float maxAnimTime = 3f;

        bool isClosing = false;

        while (true)
        {
            if (hp <= 0)
            {
                break;
            }

            if (curAnimTime >= maxAnimTime)
            {
                curAnimTime = 0f;

                isClosing = !isClosing;

                StartCoroutine(Shoot());
            }

            bodyObjs[0].transform.position = Vector3.Lerp(bodyObjs[0].transform.position, isClosing ? transform.position + -closePos : transform.position + -openPos, 0.025f);
            bodyObjs[1].transform.position = Vector3.Lerp(bodyObjs[1].transform.position, isClosing ? transform.position + closePos : transform.position + openPos, 0.025f);
            
            curAnimTime += Time.deltaTime;
            
            yield return null;
        }
    }

    protected override IEnumerator Move()
    {
        Vector3 moveVector = Vector3.zero;

        moveVector.z = speed;

        while (true)
        {
            if (hp <= 0)
            {
                break;
            }

            transform.Translate(moveVector * Time.deltaTime);

            yield return null;
        }
    }

    protected override IEnumerator Shoot()
    {
        int randIndex = Random.Range(0, 2);

        if (randIndex == 0)
        {
            for (int i = 0; i < 360; i += 40)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(0f, i, 0f));
            }
        }
        else
        {
            Vector3 distance = StageManager.instance.playerObj.transform.position - transform.position;

            float plusAngle = -20f;

            for (int i = 0; i < 3; i ++)
            {
                Instantiate(bullet, transform.position, Quaternion.Euler(0f, Mathf.Atan2(distance.x, distance.z) * Mathf.Rad2Deg + 180f + plusAngle, 0f));
                plusAngle += 20;
            }
        }

        yield return null;
    }

    protected override IEnumerator Dead()
    {
        float curRotation = 0;

        smokeParticle.SetActive(true);

        rigid.useGravity = true;

        Instantiate(deadParticle, transform.position, Quaternion.identity);

        while (curRotation < 20f)
        {
            curRotation += Time.deltaTime * 10f;
            transform.rotation = Quaternion.Euler(curRotation, 0f, curRotation);

            yield return null;
        }

        Instantiate(deadParticle, transform.position, Quaternion.identity);

        for (int curIndex = 0; curIndex < materials.Length; curIndex++)
        {
            materials[curIndex] = null;
        }

        Destroy(gameObject);
    }
}
