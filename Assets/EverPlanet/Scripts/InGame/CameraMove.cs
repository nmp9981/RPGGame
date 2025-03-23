using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject target;//Ÿ�� : �÷��̾� ������Ʈ

    private float turnSpeed = 15f;//ȸ�� �ӵ�
    //���콺 �̵���
    private float mouseX;
    private float mouseY;

    
    //ī�޶� ������ ���⼭
    private void LateUpdate()
    {
        CameraFollow();
    }
   
    //ī�޶� ��ġ ����
    private void CameraFollow()
    {
        //ī�޶�� ������Ʈ�� �����Ÿ� ������ �־����
        Vector3 Distance = new Vector3(0.0f, -2.0f, 10.0f); // ī�޶� �ٶ󺸴� �չ����� Z ��, �̵����� ���� Z ������� ���͸� ���մϴ�.
        transform.position = target.transform.position - gameObject.transform.rotation * Distance; // �÷��̾��� ��ġ���� ī�޶� �ٶ󺸴� ���⿡ ���Ͱ��� ������ ��� ��ǥ�� ���ش�.

    }
    void CameraRotate()
    {
        //���콺 ��Ŭ�� �߿��� ����
        if (Input.GetMouseButton(0))
        {
            mouseX += Input.GetAxis("Mouse X") * turnSpeed;// ���콺�� �¿� �̵���
            mouseY -= Input.GetAxis("Mouse Y") * turnSpeed;// ���콺�� ���� �̵���
        }

        gameObject.transform.localRotation = Quaternion.Euler(mouseY, mouseX, 0);//�̵����� ���� ī�޶� ���� ����
        target.transform.localRotation = Quaternion.Euler(mouseY, mouseX, 0);//�̵����� ���� ������Ʈ�� ���� ����

    }

}
