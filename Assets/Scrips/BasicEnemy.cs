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
        StartCoroutine(IdleAnim());
        StartCoroutine(Move());
        StartCoroutine(Shoot());
        Destroy(gameObject, destoryTime);
    }

    IEnumerator IdleAnim()
    {
        Vector3 openPos = new Vector3(1.25f, 0f, 0f);
        Vector3 closePos = new Vector3(0.75f , 0f, 0f);

        float curAnimTime = 0f;
        float maxAnimTime = 3f;

        bool isClosing = false;

        while (true)
        {
            if (curAnimTime >= maxAnimTime)
            {
                curAnimTime = 0f;
                isClosing = !isClosing;
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
            transform.Translate(moveVector * Time.deltaTime);

            yield return null;
        }
    }

    protected override IEnumerator Shoot()
    {
        float shootDelay = 0f;
        float maxShootDelay = 3f;

        while (true)
        {
            if (shootDelay >= maxShootDelay)
            {
                for (float i = 0; i < 360f; i += 40f)
                {
                    Instantiate(bullet, transform.position, Quaternion.Euler(0f, i, 0f));
                }

                shootDelay = 0;
            }

            shootDelay += Time.deltaTime;

            yield return null;
        }
    }

    protected override IEnumerator Dead()
    {
        float curRotation = 0;

        smokeParticle.SetActive(true);

        rigid.useGravity = true;

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
