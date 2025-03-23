using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInputSystem : MonoBehaviour
{
    //��ų
    [SerializeField]
    HurricaneSkill hurricaneSkill;
    [SerializeField]
    StraipeSkill straipeSkill;

    //��ų ��Ÿ��
    float coolTimeInHurricane = 0.15f;
    float curTimeInHurricane = 0;

    float coolTimeInStraipe = 0.5f;
    float curTimeInStraipe = 0;

    //������ UI����
    public static int orderSortNum { get; set; }
   
    private void Awake()
    {
        orderSortNum = 1;
    }
    void Update()
    {
        //���̾� ��ȣ �ʱ�ȭ
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.X))
        {
            orderSortNum = 1;
        }
        //��ǳ�� ��
        if (Input.GetKey(KeyCode.Z))
        {
            if(CheckCooltime(curTimeInHurricane, coolTimeInHurricane))
            {
                curTimeInHurricane = 0;
                hurricaneSkill.HurricaneFlow();
            }
        }
        //��Ʈ������
        if (Input.GetKey(KeyCode.X))
        {
            if (CheckCooltime(curTimeInStraipe, coolTimeInStraipe))
            {
                orderSortNum = 1;
                curTimeInStraipe = 0;
                StartCoroutine(straipeSkill.StraipeShot());
            }
        }

        TimeFlow();
    }
    /// <summary>
    /// �ð� �帧
    /// </summary>
    void TimeFlow()
    {
        curTimeInHurricane += Time.deltaTime;
        curTimeInStraipe += Time.deltaTime;
    }
  
    /// <summary>
    /// ��Ÿ�� �������� �˻�
    /// </summary>
    /// <param name="curTime">���� �ð�</param>
    /// <param name="goalTime">�� Ÿ��</param>
    /// <returns></returns>
    bool CheckCooltime(float curTime, float goalTime)
    {
        if (curTime >= goalTime)
        {
            return true;
        }
        return false;
    }
}
