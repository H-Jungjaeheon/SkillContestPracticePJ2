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
        string horizontal = "Horizontal";
        string vertical = "Vertical";

        while (true)
        {
            if (sm.curState == GameState.Play && curState == State.Basic)
            {
                moveVector.x = (transform.position.x + Input.GetAxisRaw(horizontal) * Time.deltaTime * speed > maxMoveX || transform.position.x + Input.GetAxisRaw(horizontal) * Time.deltaTime * speed < -maxMoveX)
                   ? 0f : Input.GetAxisRaw(horizontal);

                moveVector.z = (transform.position.z + Input.GetAxisRaw(vertical) * Time.deltaTime * speed > maxMoveZ || transform.position.z + Input.GetAxisRaw(vertical) * Time.deltaTime * speed < minMoveZ)
                    ? 0f : Input.GetAxisRaw(vertical);

                transform.Translate(moveVector * Time.deltaTime * speed);
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
                            Instantiate(bullet, transform.position, Quaternion.Euler(90f, 0f, 0f));
                            break;
                        case 1:
                            Instantiate(bullet, transform.position, Quaternion.Euler(90f, 0f, 0f));
                            break;
                        case 2:
                            Instantiate(bullet, transform.position, Quaternion.Euler(90f, 0f, 0f));
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
                for (int i = 0; i < mrs.Length; i++)
                {
                    mrs[i].material = materials[(int)MaterialKind.Hit];
                }

                yield return hitEffectDelay;

                for (int i = 0; i < mrs.Length; i++)
                {
                    mrs[i].material = materials[(int)MaterialKind.Basic];
                }
            }

        }
    }

    private IEnumerator HitEffect()
    {
        color = Color.red;

        while (color.a > 0f)
        {
            color.a -= Time.deltaTime * 6f;
            hitEffectImage.color = color;
            yield return null;
        }
    }
}
