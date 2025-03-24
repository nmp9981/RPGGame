using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HurricaneSkill : MonoBehaviour
{
    [SerializeField]
    ObjectFullingInTest objectFulling;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject dirToPlayer;

    Vector3 targetPosition;
    List<Vector3> targetList = new List<Vector3>();

    const float angleRange = 45;
    const float radToDeg = 57.2958f;

    private void Awake()
    {
        GameObject monsterSet = GameObject.Find("MonsterSet");
        foreach (Transform monster in monsterSet.GetComponentsInChildren<Transform>()) 
        {
            if(monster.gameObject.tag == "Monster")
            {
                targetList.Add(monster.position);
            }
        }
    }

    /// <summary>
    /// ��ǳ�� �� Flow
    /// </summary>
    public void HurricaneFlow()
    {
        //ȭ���� ĳ���� ��ġ�� ����
        GameObject arrow = objectFulling.MakeObj(20);
        
        //ȭ���� �� �������� ���ؾ���
        Projective projective = arrow.GetComponent<Projective>();
        arrow.transform.position = player.transform.position;

        //Ÿ�� ����
        targetPosition = FindNearestTarget();

        //�������� ��°�?
        bool isRange = IsRangeViewAngle();
        //�÷��̾� ���� ����
        Vector3 playerDir = (dirToPlayer.transform.position - player.transform.position).normalized;

        projective.InitArrowInfo(targetPosition, playerDir,isRange,0,100);
    }

    /// <summary>
    /// ���� �þ߹������� ���°�?
    /// </summary>
    /// <returns></returns>
    bool IsRangeViewAngle()
    {
        //�÷��̾� ���� ����
        Vector3 playerDir = (dirToPlayer.transform.position- player.transform.position).normalized;
        //Ÿ�ٱ����� ����
        Vector3 targetDir = (targetPosition - player.transform.position).normalized;
       
        //����
        float angle = Vector3.Dot(playerDir, targetDir);

        //�������� ���
        if (angle>0)
        {
            return true;
        }
        //�������� ���� ����
        return false;
    }
    /// <summary>
    /// Ÿ�� ã��
    /// </summary>
    /// <returns></returns>
    Vector3 FindNearestTarget()
    {
        Vector3 targetPos = Vector3.zero;
        float dist = int.MaxValue;
        foreach(var target in targetList)
        {
            float curDist = Vector3.Distance(player.transform.position, target);
            if(curDist < dist)
            {
                dist = curDist;
                targetPos = target;
            }
        }
        return targetPos;
    }
}
