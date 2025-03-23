using System.Collections;
using UnityEngine;

public class Projective : MonoBehaviour
{
    public float arrowMoveSpeed= 3;
    public Vector3 arrowMoveDir { get; set; }
    public float arrowMoveDistance { get; set; }

    private float arrowMaxMoveDistance = 15;

    //���� Ÿ��
    public float hitNumber = 0;
    //��ų������
    public long skillDamageRate;

    UIManager uiManager;
    ObjectFullingInTest objectFulling;

    void Awake()
    {
        arrowMoveDistance = 0;
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        objectFulling = GameObject.Find("ObjectFulling").GetComponent<ObjectFullingInTest>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveArrow();
        DestroyArrow();
    }
    /// <summary>
    /// ȭ�� ���� �ʱ�ȭ
    /// </summary>
    public void InitArrowInfo(Vector3 targetPos, Vector3 moveDir, bool isRange, int hitNum, long skillDamage)
    {
        //�þ� �������� ���� Ÿ���� ���ϰ�
        if (isRange)
        {
            transform.LookAt(targetPos);
            arrowMoveDir = targetPos - gameObject.transform.position;
            arrowMoveSpeed = 3;
        }
        else//�׷��� ������ ��������
        {
            transform.LookAt(moveDir);
            arrowMoveDir = moveDir;
            arrowMoveSpeed = 15;
        }
        
        arrowMoveDistance = 0;
        hitNumber = hitNum;
        skillDamageRate = skillDamage;
    }
    /// <summary>
    /// ȭ�� �̵�
    /// </summary>
    void MoveArrow()
    {
        gameObject.transform.position += arrowMoveSpeed * arrowMoveDir * Time.deltaTime;
        arrowMoveDistance += arrowMoveSpeed*Time.deltaTime;
    }
    /// <summary>
    /// ȭ�� �ı�
    /// </summary>
    void DestroyArrow()
    {
        if(arrowMoveDistance >= arrowMaxMoveDistance)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Monster"))
        {
            long maxDamage = (PlayerInfo.maxAttackDamage* skillDamageRate)/100;
            long minDamage = (maxDamage * PlayerInfo.workmanship) / 100;
            long damage = (long)Random.Range(minDamage,maxDamage);

            int criticalValue = Random.Range(0, 100);
            if(criticalValue < PlayerInfo.criticalRate)//ũ�� ����
            {
                damage *= PlayerInfo.criticalDamageRate;
            }

            uiManager.ShowDamage(damage);
            if(criticalValue < PlayerInfo.criticalRate)//ũ�� ����
            {
                ShowCriticalDamageAsSkin(damage, other.gameObject);
            }
            else
            {
                ShowDamageAsSkin(damage, other.gameObject);
            }
           
            gameObject.SetActive(false);
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
        Vector3 damageStartPos = bounds.center + Vector3.up * (bounds.size.y *0.5f + 1)+damageLength*Vector3.left*0.25f;
        damageStartPos += Vector3.up * hitNumber* uiManager.damageImage[0].bounds.size.y*0.55f;

        for (int i = 0; i < damageString.Length; i++)
        {
            GameObject damImg = objectFulling.MakeObj(damageString[i]-'0');
            damImg.transform.position = damageStartPos+Vector3.right * uiManager.damageImage[0].bounds.size.x*i*0.5f;
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
        Vector3 damageStartPos = bounds.center + Vector3.up * (bounds.size.y*0.5f+ 1) + damageLength * Vector3.left * 0.25f;
        damageStartPos += Vector3.up * hitNumber * uiManager.criticalDamageImage[0].bounds.size.y*0.5f;

        for (int i = 0; i < damageString.Length; i++)
        {
            GameObject damImg = objectFulling.MakeObj((damageString[i] - '0')+10);
            damImg.transform.position = damageStartPos + Vector3.right * uiManager.criticalDamageImage[0].bounds.size.x * i * 0.5f;
        }
        KeyInputSystem.orderSortNum += 1;
    }
}
