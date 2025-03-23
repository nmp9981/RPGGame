using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFullingInTest : MonoBehaviour
{
    //������ �غ�
    const int blockMaxCount = 30;
    const int blockKinds = 21;
    public GameObject[] blockPrefabs;

    //������Ʈ �迭
    GameObject[][] blocks;
    GameObject[] targetPool;

    void Awake()
    {
        blocks = new GameObject[blockKinds][]
        {
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
            new GameObject[blockMaxCount],
            new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
            new GameObject[blockMaxCount],
            new GameObject[blockMaxCount],
            new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
            new GameObject[blockMaxCount],
            new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
             new GameObject[blockMaxCount],
            new GameObject[blockMaxCount],
            new GameObject[blockMaxCount]
        };
        Generate();
    }
    void Generate()
    {
        //���
        for (int i = 0; i < blockKinds; i++)
        {
            for (int j = 0; j < blockMaxCount; j++)
            {
                blocks[i][j] = Instantiate(blockPrefabs[i]);
                blocks[i][j].SetActive(false);
            }
        }
    }
    //������Ʈ ����
    public GameObject MakeObj(int num)
    {
        targetPool = blocks[num];

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);//Ȱ��ȭ �� �ѱ�
                return targetPool[i];
            }
        }
        return null;//������ �� ��ü
    }
    //������Ʈ ����
    public GameObject MakeDragObj(int num, int idx)
    {
        targetPool = blocks[num];

        for (int i = idx; i < targetPool.Length; i++)
        {
            if (!targetPool[i].activeSelf)
            {
                targetPool[i].SetActive(true);//Ȱ��ȭ �� �ѱ�
                return targetPool[i];
            }
        }
        return null;//������ �� ��ü
    }
    //������Ʈ ���翩�� Ȯ��
    public bool IsActiveObj(int num)
    {
        targetPool = blocks[num];

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (targetPool[i].activeSelf) return true;//Ȱ��ȭ�Ȱ� ������
        }
        return false;//������ �� ��ü
    }
    //������Ʈ �����ϸ� ��������
    public GameObject GetObj(int num)
    {
        targetPool = blocks[num];

        for (int i = 0; i < targetPool.Length; i++)
        {
            if (targetPool[i].activeSelf) return targetPool[i];//Ȱ��ȭ�Ȱ� ������
        }
        return null;//������ �� ��ü
    }
    //������Ʈ �迭 ��������
    public GameObject[] GetPool(int num)//������ ������Ʈ Ǯ ��������
    {
        targetPool = blocks[num];
        return targetPool;
    }
    //������Ʈ�� ��Ȱ��ȭ
    public void OffObj()
    {
        for (int i = 0; i < blockKinds; i++)
        {
            for (int j = 0; j < blockMaxCount; j++)
            {
                if (blocks[i][j].activeSelf) blocks[i][j].SetActive(false);
            }
        }
    }
}
