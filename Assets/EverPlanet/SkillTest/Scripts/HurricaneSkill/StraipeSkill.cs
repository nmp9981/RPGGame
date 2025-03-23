using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraipeSkill : MonoBehaviour
{
    [SerializeField]
    ObjectFullingInTest objectFulling;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject target;
    [SerializeField]
    GameObject dirToPlayer;

    const int straipeHitNumber = 6;
    /// <summary>
    /// ��Ʈ������ ���
    /// </summary>
    /// <returns></returns>
    public IEnumerator StraipeShot()
    {
        for(int i = 0; i < straipeHitNumber; i++)
        {
            StraifeFlow(i);
            yield return new WaitForSeconds(0.05f);
        }
    }

    /// <summary>
    /// ��Ʈ������ Flow
    /// </summary>
    public void StraifeFlow(int hitNum)
    {
        //ȭ���� ĳ���� ��ġ�� ����
        GameObject arrow = objectFulling.MakeObj(20);

        //ȭ���� �� �������� ���ؾ���
        Projective projective = arrow.GetComponent<Projective>();
        arrow.transform.position = player.transform.position+0.2f*(hitNum-1.5f)*Vector3.up;

        //�������� ��°�?
        bool isRange = IsRangeViewAngle();
        //�÷��̾� ���� ����
        Vector3 playerDir = (dirToPlayer.transform.position - player.transform.position).normalized;

        projective.InitArrowInfo(target.transform.position, playerDir, isRange, hitNum,180);
    }

    /// <summary>
    /// ���� �þ߹������� ���°�?
    /// </summary>
    /// <returns></returns>
    bool IsRangeViewAngle()
    {
        //�÷��̾� ���� ����
        Vector3 playerDir = (dirToPlayer.transform.position - player.transform.position).normalized;
        //Ÿ�ٱ����� ����
        Vector3 targetDir = (target.transform.position - player.transform.position).normalized;

        //����
        float angle = Vector3.Dot(playerDir, targetDir);

        //�������� ���
        if (angle > 0)
        {
            return true;
        }
        //�������� ���� ����
        return false;
    }
}
