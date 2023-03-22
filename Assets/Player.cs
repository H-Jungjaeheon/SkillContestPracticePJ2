using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BasicUnit
{
    [SerializeField]
    private float maxMoveX;

    [SerializeField]
    private float maxMoveZ;

    [SerializeField]
    private float minMoveZ;

    private StageManager stageManager;
    
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private TrailRenderer tr;

    private float maxShootDelay = 0.3f;

    private int power = 1;

    public int Power
    {
        get
        {
            return power;
        }
        set
        {
            power = value;

            maxShootDelay -= 0.05f;
        }
    }

    void Start()
    {
        stageManager = StageManager.instance;
        StartCoroutine(Move());
        StartCoroutine(Shoot());
    }

    protected override IEnumerator Move()
    {
        Vector3 moveVector = Vector3.zero;

        string horizontal = "Horizontal";
        string vertical = "Vertical";

        while (true)
        {
            if (stageManager.curGameState == GameState.Playing)
            {
                tr.enabled = Input.GetKey(KeyCode.UpArrow);
                
                moveVector.x = (transform.position.x + (Input.GetAxisRaw(horizontal) * speed * Time.deltaTime) < maxMoveX && transform.position.x + (Input.GetAxisRaw(horizontal) * speed * Time.deltaTime) > -maxMoveX)
                    ? Input.GetAxisRaw(horizontal) : 0f;

                moveVector.z = (transform.position.z + (Input.GetAxisRaw(vertical) * speed * Time.deltaTime) < maxMoveZ && transform.position.z + (Input.GetAxisRaw(vertical) * speed * Time.deltaTime) > minMoveZ)
                    ? Input.GetAxisRaw(vertical) : 0f;

                transform.Translate(moveVector * Time.deltaTime * speed);
            }

            yield return null;
        }
    }

    protected override IEnumerator Shoot()
    {
        float curShootDelay = 0;
        Vector3 plusVector = Vector3.zero;

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Power++;
            }

            if (stageManager.curGameState == GameState.Playing)
            {

                if (Input.GetKey(KeyCode.Z) && curShootDelay >= maxShootDelay)
                {
                    switch (power)
                    {
                        case 1:
                            Instantiate(bullet, transform.position, bullet.transform.rotation);
                            break;
                        case 2:

                            plusVector.x = -0.5f;

                            for (int curindex = 0; curindex < 2; curindex++)
                            {
                                Instantiate(bullet, transform.position + plusVector, bullet.transform.rotation);
                                plusVector.x = 1f;
                            }

                            break;
                        case 3:

                            for (int curindex = 0; curindex < 2; curindex++)
                            {
                                Instantiate(bullet, transform.position, Quaternion.Euler(90f, 0f, -20f + curindex * 15f));
                            }

                            for (int curindex = 0; curindex < 2; curindex++)
                            {
                                Instantiate(bullet, transform.position, Quaternion.Euler(90f, 0f, 5f + curindex * 15f));
                            }

                            break;
                    }
                    curShootDelay = 0f;
                }

                curShootDelay += Time.deltaTime;

            }

            yield return null;
        }
    }

    public override IEnumerator Hit(float damage)
    {
        yield return base.Hit(damage);
    }
}
