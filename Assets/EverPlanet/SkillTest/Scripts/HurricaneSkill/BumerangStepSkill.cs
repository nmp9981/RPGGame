using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BumerangStepSkill : MonoBehaviour
{
    [SerializeField]
    GameObject knifeObject;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject dirToPlayer;

    const float maxBumerangStepDist = 15f;

    /// <summary>
    /// �θ޶� ����
    /// </summary>
    /// <returns></returns>
    public IEnumerator BumerangStep()
    {
        BumerangStepFlow(0);
        yield return new WaitForSeconds(0.5f);
    }

    /// <summary>
    /// �θ޶����� Flow
    /// </summary>
    public void BumerangStepFlow(int hitNum)
    {
        //�ܰ��� ĳ���� ��ġ�� ����
        knifeObject.SetActive(true);

        //ȭ���� �� �������� ���ؾ���
        Knife knife = knifeObject.GetComponent<Knife>();
        knifeObject.transform.position = player.transform.position + 0.2f * (hitNum - 1.5f) * Vector3.up;

        //�÷��̾� ���� ����
        Vector3 playerDir = (dirToPlayer.transform.position - player.transform.position).normalized;

        knife.InitKnifeInfo(maxBumerangStepDist, playerDir, hitNum, 500);
    }
}
