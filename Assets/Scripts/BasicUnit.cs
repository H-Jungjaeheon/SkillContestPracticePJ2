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
    protected float damage;

    [SerializeField]
    protected float speed;

    protected State curState;

    protected abstract IEnumerator Move();

    protected abstract IEnumerator Attack();

    protected abstract IEnumerator Dead();

    protected abstract IEnumerator Hit();
}
