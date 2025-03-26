using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public float knifeMoveSpeed = 30;
    public Vector3 knifeMoveDir { get; set; }
    public float knifeMoveDistance { get; set; }

    private float knifeMaxMoveDistance = 10;

    //���� Ÿ��
    public float hitNumber = 0;
    //��ų������
    public long skillDamageRate;

    UIManager uiManager;
    ObjectFullingInTest objectFulling;

    void Awake()
    {
        knifeMoveDistance = 0;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        objectFulling = GameObject.Find("ObjectFulling").GetComponent<ObjectFullingInTest>();
    }
    void Update()
    {
        MoveKnife();
    }

    /// <summary>
    /// �ܰ� ���� �ʱ�ȭ
    /// </summary>
    public void InitKnifeInfo(float maxAttackDist,Vector3 moveDir, int hitNum, long skillDamage)
    {
        transform.LookAt(moveDir);
        transform.eulerAngles += new Vector3(gameObject.transform.rotation.x, 90,90);

        knifeMaxMoveDistance = maxAttackDist;
        knifeMoveDir = moveDir;
        knifeMoveDistance = 0;

        hitNumber = hitNum;
        skillDamageRate = skillDamage;
    }
    /// <summary>
    /// �ܰ� �̵�
    /// </summary>
    void MoveKnife()
    {
        gameObject.transform.position += knifeMoveSpeed * knifeMoveDir * Time.deltaTime;
        knifeMoveDistance += knifeMoveSpeed * Time.deltaTime;

        ChangeKnifeDir();
        DestroyArrow();
    }

    /// <summary>
    /// ���� ü����
    /// </summary>
    void ChangeKnifeDir()
    {
        //�ִ� ��Ÿ�
        if(knifeMoveDistance >= knifeMaxMoveDistance)
        {
            knifeMoveDistance = 0;
            knifeMoveDir *= -1;
            hitNumber += 1;
        }
    }
    /// <summary>
    /// �ܰ� �ı�
    /// </summary>
    void DestroyArrow()
    {
        if (hitNumber==2)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Monster"))
        {
            long maxDamage = (PlayerInfo.maxAttackDamage * skillDamageRate) / 100;
            long minDamage = (maxDamage * PlayerInfo.workmanship) / 100;
            long damage = (long)Random.Range(minDamage, maxDamage);

            int criticalValue = Random.Range(0, 100);
            if (criticalValue < PlayerInfo.criticalRate)//ũ�� ����
            {
                damage *= PlayerInfo.criticalDamageRate;
            }

            uiManager.ShowDamage(damage);
            if (criticalValue < PlayerInfo.criticalRate)//ũ�� ����
            {
                ShowCriticalDamageAsSkin(damage, other.gameObject);
            }
            else
            {
                ShowDamageAsSkin(damage, other.gameObject);
            }
        }
    }
    /// <summary>
    /// �Ϲ� ������ ���̱�
    /// </summary>
    /// <param name="Damage">������</param>
    /// <param name="monsterPos">���� ��ġ</param>
    void ShowDamageAsSkin(long Damage, GameObject monsterPos)
    {
        string damageString = Damage.ToString();
        float damageLength = uiManager.damageImage[0].bounds.size.x * damageString.Length;
        Bounds bounds = monsterPos.GetComponent<MeshRenderer>().bounds;
        Vector3 damageStartPos = bounds.center + Vector3.up * (bounds.size.y * 0.5f + 1) + damageLength * Vector3.left * 0.25f;
        damageStartPos += Vector3.up * hitNumber * uiManager.damageImage[0].bounds.size.y * 0.55f;

        for (int i = 0; i < damageString.Length; i++)
        {
            GameObject damImg = objectFulling.MakeObj(damageString[i] - '0');
            damImg.transform.position = damageStartPos + Vector3.right * uiManager.damageImage[0].bounds.size.x * i * 0.5f;
        }
        KeyInputSystem.orderSortNum += 1;
    }

    /// <summary>
    /// ũ�� ������ ���̱�
    /// </summary>
    /// <param name="Damage">������</param>
    /// <param name="monsterPos">���� ��ġ</param>
    void ShowCriticalDamageAsSkin(long Damage, GameObject monsterPos)
    {
        string damageString = Damage.ToString();
        float damageLength = uiManager.criticalDamageImage[0].bounds.size.x * damageString.Length;
        Bounds bounds = monsterPos.GetComponent<MeshRenderer>().bounds;
        Vector3 damageStartPos = bounds.center + Vector3.up * (bounds.size.y * 0.5f + 1) + damageLength * Vector3.left * 0.25f;
        damageStartPos += Vector3.up * hitNumber * uiManager.criticalDamageImage[0].bounds.size.y * 0.5f;

        for (int i = 0; i < damageString.Length; i++)
        {
            GameObject damImg = objectFulling.MakeObj((damageString[i] - '0') + 10);
            damImg.transform.position = damageStartPos + Vector3.right * uiManager.criticalDamageImage[0].bounds.size.x * i * 0.5f;
        }
        KeyInputSystem.orderSortNum += 1;
    }
}
