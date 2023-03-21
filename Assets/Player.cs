using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private float maxMoveX;

    [SerializeField]
    private float maxMoveZ;

    private StageManager stageManager;
    
    [SerializeField]
    private GameObject bullet;

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

            maxShootDelay -= 0.1f;
        }
    }

    void Start()
    {
        stageManager = StageManager.instance;
        StartCoroutine(Move());
        StartCoroutine(Shoot());
    }

    IEnumerator Move()
    {
        Vector3 moveVector = Vector3.zero;

        string horizontal = "Horizontal";
        string vertical = "Vertical";

        while (true)
        {
            if (stageManager.curGameState == GameState.Playing)
            {
                moveVector.x = (transform.position.x + (Input.GetAxisRaw(horizontal) * speed * Time.deltaTime) < maxMoveX && transform.position.x + (Input.GetAxisRaw(horizontal) * speed * Time.deltaTime) > -maxMoveX)
                    ? Input.GetAxisRaw(horizontal) : 0f;

                moveVector.z = (transform.position.z + (Input.GetAxisRaw(vertical) * speed * Time.deltaTime) < maxMoveZ && transform.position.z + (Input.GetAxisRaw(vertical) * speed * Time.deltaTime) > -maxMoveZ)
                    ? Input.GetAxisRaw(vertical) : 0f;

                transform.Translate(moveVector * Time.deltaTime * speed);
            }

            yield return null;
        }
    }

    IEnumerator Shoot()
    {
        float curShootDelay = 0;
        Vector3 plusVector = Vector3.zero;

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Power++;
            }

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
                            plusVector.x = 0.5f;
                        }

                        break;
                    case 3:

                        for (int curindex = 0; curindex < 5; curindex++)
                        {
                            Instantiate(bullet, transform.position, Quaternion.Euler(90f, 0f, -40f + curindex * 20f));
                        }

                        break;
                }
                curShootDelay = 0f;
            }

            curShootDelay += Time.deltaTime;

            yield return null;
        }
    }
}
