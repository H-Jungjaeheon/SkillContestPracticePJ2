using System.Collections;
using UnityEngine;

public enum State
{
    Basic,
    Dead
}

public abstract class BasicUnit : MonoBehaviour
{
    [SerializeField]
    protected float maxHp;

    [SerializeField]
    protected float hp;

    [SerializeField]
    protected float speed;

    protected Vector3 moveVector = Vector3.zero;

    public State curState;

    [SerializeField]
    protected SpriteRenderer sr;

    protected WaitForSeconds hitEffectDelay = new WaitForSeconds(0.05f);

    protected abstract IEnumerator Move();

    protected abstract IEnumerator Attack();

    protected abstract IEnumerator Dead();

    public virtual IEnumerator Hit(int damage)
    {
        if (curState == State.Basic)
        {
            hp -= damage;

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
}
