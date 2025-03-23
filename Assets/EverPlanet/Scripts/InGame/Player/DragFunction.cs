using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DragFunction : MonoBehaviour
{
    PlayerAttack playerAttack;
    GameObject player;
    GameObject target;
    public Transform monsterTarget;//���� ����� ������ ��ġ
    float moveDist;//ǥâ �̵��Ÿ�
    float liveTime;//ǥâ ���� �� �ð�
    Vector3 moveVec;
    Vector3 initPos;//�ʱ� ��ġ

    [SerializeField] TextMeshProUGUI DamegeText;
    public bool isShadow;
    public long attackDamage;
    public float criticalNum;
    public bool isCritical;
    public int skillDigit;

    void Awake()
    {
        playerAttack = GameObject.Find("Player").GetComponent<PlayerAttack>();
        player = GameObject.Find("Body05");
        target = GameObject.Find("DragTarget");
    }
    public void OnEnableDrag()
    {
        liveTime = 0;
        moveDist = 0f;

        if (monsterTarget == null)
        {
            moveVec = (target.transform.position - player.transform.position).normalized;
            moveVec.y = 0f;
        }
        else
        {
            moveVec = (monsterTarget.transform.position - player.transform.position).normalized;
        }

        initPos = gameObject.transform.position;
        gameObject.transform.rotation = Quaternion.Euler(0, DotAngle(), DotZAngle());
        criticalNum = Random.Range(0, 100);
        if (criticalNum < GameManager.Instance.CriticalRate) isCritical = true;
        else isCritical = false;
    }
    void Update()
    {
        DragMove();
        TimeChecker();
    }
    void TimeChecker()
    {
        liveTime += Time.deltaTime;
        if (liveTime >= 7.0f) gameObject.SetActive(false);
    }
    //ǥâ �̵�
    void DragMove()
    {
        if (monsterTarget == null)
        {
            gameObject.transform.position += moveVec * GameManager.Instance.PlayerDragSpeed * Time.deltaTime;
            moveDist = (gameObject.transform.position - initPos).magnitude;
        }
        else
        { 
            gameObject.transform.position = Vector3.MoveTowards(this.transform.position, monsterTarget.position, GameManager.Instance.PlayerDragSpeed * Time.deltaTime);
            moveDist = (gameObject.transform.position - initPos).magnitude;
        }
        //���� �Ÿ� �ʰ�
        if (moveDist > GameManager.Instance.ThrowDist)
        {
            gameObject.SetActive(false);
        }
    }
    //ǥâ y�� ȸ�� ����
    float DotAngle()
    {
        float dot = -moveVec.x;
        float cosTheta = dot / moveVec.magnitude;
        float theta = -Mathf.Acos(cosTheta) * 180 / Mathf.PI;
        return theta;
    }
    //ǥâ z�� ȸ�� ����
    float DotZAngle()
    {
        if (monsterTarget == null) return 90;
        float dot = moveVec.y;
        float cosTheta = dot / moveVec.magnitude;
        float theta = Mathf.Acos(cosTheta) * 180 / Mathf.PI;
        return theta;
    }
    //�ǰ�
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Bear"|| collision.gameObject.tag == "SandBag")//���� ����
        {
            SoundManager._sound.PlaySfx(5);
            attackDamage = AttackDamage();
            if (isShadow) attackDamage = (long)((float)attackDamage*GameManager.Instance.ShadowAttack/100f);

            if (collision.gameObject.tag == "Bear" && (collision.gameObject.name.Contains("Bear") 
                || collision.gameObject.name.Contains("Human_Mutant") || collision.gameObject.name.Contains("Rhino_PBR")))
            {
                collision.gameObject.GetComponent<BearBossFunction>().monsterHP -= attackDamage;
            }else if(collision.gameObject.tag == "SandBag") collision.gameObject.GetComponent<SandBag>().csumDamage+=attackDamage;
            else
            {
                collision.gameObject.GetComponent<MonsterFunction>().monsterHP -= attackDamage;
            }
        }
    }
    //���� ������
    public long AttackDamage()
    {
        if (skillDigit == 1)//�Ϲ� ����
        {
            return GameManager.Instance.PlayerAttack * (long)Random.Range(GameManager.Instance.Proficiency, 100)/100;
        }

        long attackMaxDamage = skillDigit==2?((long)(GameManager.Instance.PlayerAttack * GameManager.Instance.LuckySevenCoefficient/100f))
            : (long)((float)(GameManager.Instance.PlayerAttack * GameManager.Instance.TripleThrowCoefficient/100f));
        int attackRate = Random.Range(GameManager.Instance.Proficiency, 100);
        if (isCritical) attackMaxDamage = (long)((float)attackMaxDamage*GameManager.Instance.CriticalDamage/100f);//ũ�� ������
        return attackMaxDamage * (long)attackRate / 100;
    }
}
