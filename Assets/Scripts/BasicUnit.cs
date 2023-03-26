using System.Collections;
using UnityEngine;

public enum State
{
    Basic,
    Dead
}

public enum MaterialKind
{
    Basic,
    Hit
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
    protected MeshRenderer[] mrs;

    [SerializeField]
    protected Material[] materials;

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
}
