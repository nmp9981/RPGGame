using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AvengerSkill : MonoBehaviour
{
    MonsterSpawner monsterSpawner;
    GameObject player;
    GameObject target;

    float liveTime;//ǥâ ���� �ð�
    float moveDist;//ǥâ �̵��Ÿ�
    Vector3 moveVec;
    Vector3 initPos;//�ʱ� ��ġ

    public bool isShadow;
    public int hitCount;
    public long attackDamage;
    public float criticalNum;
    public bool isCritical;

    void Awake()
    {
        monsterSpawner = GameObject.Find("MonsterSpawn").GetComponent<MonsterSpawner>();
        player = GameObject.Find("Body05");
        target = GameObject.Find("DragTarget");
    }
    public void OnEnableThrow()
    {
        liveTime = 0;
        moveDist = 0f;
        hitCount = 0;

        moveVec = (target.transform.position - player.transform.position).normalized;
        moveVec.y = 0f;
        initPos = gameObject.transform.position;

        criticalNum = Random.Range(0, 100);
        if (criticalNum >= GameManager.Instance.CriticalRate) isCritical = true;
        else isCritical = false;
    }
    void Update()
    {
        SizeUp();
        DragMove();
        TimeChecker();
    }
    void SizeUp()
    {
        gameObject.transform.localScale = Vector3.Lerp(gameObject.transform.localScale, new Vector3(22,13,22), 4*Time.deltaTime);
    }
    void TimeChecker()
    {
        liveTime += Time.deltaTime;
        if (liveTime >= 8.0f) gameObject.SetActive(false);
    }
    //ǥâ �̵�
    void DragMove()
    {
        gameObject.transform.position += moveVec * GameManager.Instance.PlayerDragSpeed * Time.deltaTime;
        moveDist  = (gameObject.transform.position - initPos).magnitude;

        //���� �Ÿ� �ʰ�
        if (moveDist > GameManager.Instance.ThrowDist)
        {
            gameObject.SetActive(false);
        }
    }
    //�ǰ�
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Bear" || collision.gameObject.tag == "SandBag")//���� ����
        {
            hitCount++;
            SoundManager._sound.PlaySfx(5);
            attackDamage = AttackDamage();
            if (isShadow) attackDamage = (long)((float)attackDamage * GameManager.Instance.ShadowAttack/100f);
            if(collision.gameObject.tag == "Bear") collision.gameObject.GetComponent<BearBossFunction>().monsterHP -= attackDamage;
            else if (collision.gameObject.tag == "SandBag") collision.gameObject.GetComponent<SandBag>().csumDamage += attackDamage;
            else collision.gameObject.GetComponent<MonsterFunction>().monsterHP -= attackDamage;

            if (hitCount >= 6) gameObject.SetActive(false);
        }
    }
    public long AttackDamage()
    {
        long attackMaxDamage = (long)((float)GameManager.Instance.PlayerAttack * GameManager.Instance.AvengerCoefficient/100f);
        int attackRate = Random.Range(GameManager.Instance.Proficiency,100);
        if (isCritical) attackMaxDamage = attackMaxDamage * GameManager.Instance.CriticalDamage / 100;//ũ�� ������
        return attackMaxDamage * (long)attackRate / 100;
    }
}
