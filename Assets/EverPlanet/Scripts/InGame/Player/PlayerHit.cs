using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hitDamageText;
    [SerializeField] GameObject tombStone;
    [SerializeField] GameObject tombStoneMessage;

    void Awake()
    {
        hitDamageText.text = "";
        tombStone.SetActive(false);
        tombStoneMessage.SetActive(false);
    }
    private void Update()
    {
        PlayerDie();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.Instance.IsInvincibility)
        {
            if (collision.gameObject.tag == "Monster" || collision.gameObject.tag == "Bear")
            {
                int avoidRandom = Random.Range(0, 100);
                int hit = 0;

                if (collision.gameObject.tag == "Bear")
                {
                    if (collision.gameObject.GetComponent<BearBossFunction>()) hit = collision.gameObject.GetComponent<BearBossFunction>().monsterHitDamage;
                    else hit = 2500;
                }
                else hit = collision.gameObject.GetComponent<MonsterFunction>().monsterHitDamage;
                int finalHit = (avoidRandom < GameManager.Instance.PlayerAvoid) ? 0 : Random.Range(hit * 90 / 100, hit * 110 / 100);
                GameManager.Instance.PlayerHP -= finalHit;
                StartCoroutine(ShowDamage(finalHit));
            }
        }
    }
    //���
    void PlayerDie()
    {
        if (GameManager.Instance.PlayerHP <= 0)//ĳ���� ���
        {
            GameManager.Instance.IsCharacterDie = true;
            tombStone.SetActive(true);
            tombStoneMessage.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))//��� Ȯ�� ��ư
        {
            if (tombStoneMessage.activeSelf) Resurrection();
        }
    }
    //������ �����ֱ� �� ����ȿ�� ó��
    public IEnumerator ShowDamage(int finalHit)
    {
        hitDamageText.transform.position = Camera.main.WorldToScreenPoint(this.gameObject.transform.position + new Vector3(0, 2f, 0));
        hitDamageText.text = (finalHit==0)?"MISS": finalHit.ToString();
        GameManager.Instance.IsInvincibility = true;//���� ȿ�� �ߵ�
        yield return new WaitForSecondsRealtime(0.3f);
        hitDamageText.text = "";
        yield return new WaitForSecondsRealtime(1.0f);
        GameManager.Instance.IsInvincibility = false;
    }
    //��Ȱ
    public void Resurrection()
    {
        GameManager.Instance.IsCharacterDie = false;
        GameManager.Instance.PlayerHP = GameManager.Instance.PlayerMaxHP / 2;
        GameManager.Instance.PlayerEXP = Mathf.Max(0, (int)GameManager.Instance.PlayerEXP-(int)GameManager.Instance.PlayerReqExp /20);

        this.gameObject.transform.position = PortalManager.PortalInstance.portalist[0].transform.position;//������ �̵�

        SoundManager._sound.StopBGM(GameManager.Instance.PlayerBGMNumber);
        GameManager.Instance.PlayerBGMNumber = 0;
        GameManager.Instance.PlayerCurrentMap = 0;
        SoundManager._sound.PlayBGM(GameManager.Instance.PlayerBGMNumber);

        tombStone.SetActive(false);
        tombStoneMessage.SetActive(false);
    }
}
