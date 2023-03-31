using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : BasicUnit
{
    public static Player instance;

    private int power;

    [SerializeField]
    private float maxMoveX;

    [SerializeField]
    private float maxMoveZ;

    [SerializeField]
    private float minMoveZ;

    [SerializeField]
    private StageManager sm;

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Image hpImage;

    [SerializeField]
    private Text hpText;

    [SerializeField]
    private Image hitEffectImage;

    [SerializeField]
    private CamShake cs;

    Color color;

    WaitForSeconds shootDelay = new WaitForSeconds(0.15f);

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(Move());
        StartCoroutine(Attack());
    }

    public void Upgrade()
    {
        if (power < 2)
        {
            power++;
        }
    }

    protected override IEnumerator Move()
    {
        Vector3 localPos;

        localPos.y = transform.position.y;

        while (true)
        {
            if (sm.curState == GameState.Play && curState == State.Basic)
            {
                moveVector.x = Input.GetAxisRaw("Horizontal");
                moveVector.z = Input.GetAxisRaw("Vertical");

                transform.Translate(moveVector * Time.deltaTime * speed);

                localPos.x = Mathf.Clamp(transform.position.x, -maxMoveX, maxMoveX);
                localPos.z = Mathf.Clamp(transform.position.z, minMoveZ, maxMoveZ);

                transform.position = localPos;
            }

            yield return null;
        }
    }

    protected override IEnumerator Attack()
    {
        while (true)
        {
            if (sm.curState == GameState.Play && curState == State.Basic)
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    switch (power)
                    {
                        case 0:
                            Instantiate(bullet, transform.position, Quaternion.identity);
                            break;
                        case 1:
                            Instantiate(bullet, transform.position, Quaternion.identity);
                            break;
                        case 2:
                            Instantiate(bullet, transform.position, Quaternion.identity);
                            break;
                    }

                    yield return shootDelay;
                }
            }

            yield return null;
        }
    }

    protected override IEnumerator Dead()
    {
        yield return null;
    }

    public override IEnumerator Hit(int damage)
    {
        if (curState == State.Basic)
        {
            hp -= damage;

            hpImage.fillAmount = hp / maxHp;

            hpText.text = $"{hp}/{maxHp}";

            cs.StartShake(6, 3f);

            StartCoroutine(HitEffect());

            if (hp <= 0f)
            {
                curState = State.Dead;

                hp = 0f;

                hpText.text = $"{hp}/{maxHp}";
                
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

    private IEnumerator HitEffect()
    {
        color = Color.red;

        color.a = 0.5f;

        while (color.a > 0f)
        {
            color.a -= Time.deltaTime * 6f;
            hitEffectImage.color = color;
            yield return null;
        }
    }
}
