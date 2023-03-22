using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    StageReady,
    Playing,
    GameEnd
}

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    public GameState curGameState;

    private Color color;

    [HideInInspector]
    public int curStageIndex = 0;

    [Header("���� ������ ��ҵ� ����")]

    [SerializeField]
    private GameObject meteorObj;

    [SerializeField]
    private GameObject earthObj;

    [SerializeField]
    private GameObject earthBoomEffectObj;

    [SerializeField]
    private GameObject destroyEarthObj;

    [SerializeField]
    private GameObject stageParticleObj;

    [SerializeField]
    private EnemySpawner es;

    [Header("�������� ������ ��ҵ� ����")]

    [SerializeField]
    private GameObject playerObj;

    [TextArea]
    [SerializeField]
    private string[] stageIntroduce;

    [SerializeField]
    private Text stageText;

    [SerializeField]
    private Image fadeImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        StartCoroutine(StageStart());
    }

    /// <summary>
    /// �������� ���� �Լ�(ġƮ �� �������� �ѱ� �� �� �ڷ�ƾ ȣ��)
    /// </summary>
    /// <returns></returns>
    IEnumerator StageStart()
    {
        curGameState = GameState.StageReady;

        curStageIndex = 1;

        color = Color.black;

        fadeImage.color = color;

        stageText.text = stageIntroduce[curStageIndex - 1];
        
        fadeImage.enabled = true;

        stageText.enabled = true;

        //�ʿ� �����ϴ� ��, �Ѿ� �����۾�

        if (curStageIndex == 1)
        {
            playerObj.transform.position = new Vector3(0f, 0f, -10f);
            meteorObj.transform.position = new Vector3(0f, -100f, 360f);
            destroyEarthObj.transform.position = new Vector3(0f, -40f, -20f);
            earthBoomEffectObj.transform.localScale = Vector3.zero;

            stageParticleObj.SetActive(false);
            earthBoomEffectObj.SetActive(true);
            earthObj.SetActive(true);
            meteorObj.SetActive(true);
        }
        else
        {
            playerObj.transform.position = Vector3.zero;
            es.EnemySpawnStart(curStageIndex);
        }

        yield return new WaitForSeconds(3f);

        stageText.enabled = false;

        while (color.a > 0f)
        {
            fadeImage.color = color;

            color.a -= Time.deltaTime;

            yield return null;
        }

        fadeImage.enabled = false;

        if (curStageIndex == 1)
        {
            StartCoroutine(BattleOpning());
        }
        else
        {
            curGameState = GameState.Playing;
        }
    }

    IEnumerator BattleOpning()
    {
        Vector3 vector = Vector3.zero;

        vector.z = -50f;

        while (meteorObj.transform.position.z > 0f)
        {
            meteorObj.transform.Translate(vector * Time.deltaTime);
            
            yield return null;
        }

        meteorObj.SetActive(false);

        //ȭ�� ���� ȿ��

        fadeImage.enabled = true;

        color = Color.white;
        color.a = 0f;

        vector.x = 10f;
        vector.z = 10f;

        while (true)
        {
            earthBoomEffectObj.transform.localScale += vector * Time.deltaTime;

            if (earthBoomEffectObj.transform.localScale.x >= 15f)
            {
                fadeImage.color = color;
                color.a += Time.deltaTime;
                
                if (color.a >= 1)
                {
                    break;
                }
            }

            yield return null;
        }

        earthBoomEffectObj.SetActive(false);
        earthObj.SetActive(false);
        destroyEarthObj.SetActive(true);
        stageParticleObj.SetActive(true);

        yield return new WaitForSeconds(3f);

        while (color.a > 0f)
        {
            fadeImage.color = color;

            color.a -= Time.deltaTime;

            yield return null;
        }

        fadeImage.enabled = false;

        StartCoroutine(EarthMoveEvent());

        vector = Vector3.zero;

        vector.z = 10f;

        while (playerObj.transform.position.z < 0f)
        {
            playerObj.transform.Translate(vector * Time.deltaTime);

            yield return null;
        }

        playerObj.transform.position = Vector3.zero;

        curGameState = GameState.Playing;

        es.EnemySpawnStart(curStageIndex);
    }

    IEnumerator EarthMoveEvent()
    {
        Vector3 moveSpeed = Vector3.zero;
        
        moveSpeed.z = -5f;
        
        while (destroyEarthObj.transform.position.z > -150f)
        {
            destroyEarthObj.transform.Translate(moveSpeed * Time.deltaTime);

            yield return null;
        }

        destroyEarthObj.SetActive(false);
    }
}
