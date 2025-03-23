using UnityEngine;

public class HurricaneSkill : MonoBehaviour
{
    [SerializeField]
    ObjectFullingInTest objectFulling;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject target;
    [SerializeField]
    GameObject dirToPlayer;

    const float angleRange = 45;
    const float radToDeg = 57.2958f;

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

        //�������� ��°�?
        bool isRange = IsRangeViewAngle();
        //�÷��̾� ���� ����
        Vector3 playerDir = (dirToPlayer.transform.position - player.transform.position).normalized;

        projective.InitArrowInfo(target.transform.position, playerDir,isRange,0,100);
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
        Vector3 targetDir = (target.transform.position- player.transform.position).normalized;
       
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
}
