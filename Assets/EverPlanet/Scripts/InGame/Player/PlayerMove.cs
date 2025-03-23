using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    int curJumpCount = 0;
    float rotateSpeed = 15f;
    Rigidbody rigid = null;

    float distance;
    public Vector3 moveVec;
    [SerializeField] LayerMask layerMask = 0;
    [SerializeField] Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!GameManager.Instance.IsCharacterDie)
        {
            Move();
            TryJump();
            CheckGround();
            PlayerYPosCheck();
        }
    }
    private void Move()
    {
        float hAxis = Input.GetAxisRaw("Horizontal");
        float vAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(hAxis, 0, vAxis).normalized;//�̵� ����, ����ȭ(�밢������ �� �������°� ����)
        
        if (hAxis!=0 || vAxis != 0)
        {
            transform.position += moveVec * GameManager.Instance.PlayerMoveSpeed * Time.deltaTime;//��ǥ �̵�
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveVec), Time.deltaTime *rotateSpeed);//ĳ���Ͱ� �ٶ󺸴� �������� ȸ��
        }
        MoveAnimation(hAxis,vAxis);
    }
    void MoveAnimation(float hAxis, float vAxis)
    {
        if(hAxis == 0 && vAxis == 0)//����
        {
            anim.SetBool("Front", false);
            anim.SetBool("Back", false);
            anim.SetBool("Right", false);
            anim.SetBool("Left", false);
        }
        if (hAxis > 0)//������
        {
            anim.SetBool("Right", true);
            anim.SetBool("Left", false);
        }
        if (hAxis < 0)//����
        {
            anim.SetBool("Left", true);
            anim.SetBool("Right", false);
        }
        if (vAxis > 0)//��
        {
            anim.SetBool("Front", true);
            anim.SetBool("Back", false);
        }
        if (vAxis > 0)//��
        {
            anim.SetBool("Front", false);
            anim.SetBool("Back", true);
        }
    }
    void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (curJumpCount < GameManager.Instance.MaxJumpCount)//���� Ƚ�� ����
            {
                SoundManager._sound.PlaySfx(3);
                curJumpCount++;
                rigid.velocity = Vector3.up * GameManager.Instance.PlayerJumpSpeed;
            }
        }
    }
    void CheckGround()
    {
        distance = GetComponent<BoxCollider>().size.y * 0.6f;
        if (rigid.velocity.y < 0)//�����Ҷ���
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, distance, layerMask))//���̸� ���
            {
                if (hit.transform.CompareTag("Ground"))//�߹ؿ� ���� ������ ���� �ʱ�ȭ
                {
                    curJumpCount = 0;
                }
            }
        }
    }
    //���� ó��
    void PlayerYPosCheck()
    {
        if (gameObject.transform.position.y < -200)
        {
            gameObject.GetComponent<PlayerHit>().Resurrection();
        }
    }
}
