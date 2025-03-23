using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapManagement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI mapName;
    [SerializeField] string[] mapNameList = new string[9];
    void Start()
    {
        GameManager.Instance.PlayerBGMNumber = 0;
        SoundManager._sound.PlayBGM(GameManager.Instance.PlayerBGMNumber);//��ó������ �������� ���� -> ���� bgm
        mapName.text = $"{mapNameList[0]}";
    }

    void Update()
    {
        ShowMapName();
    }
    //�� �̸� ǥ��
    void ShowMapName()
    {
        mapName.text = $"{mapNameList[GameManager.Instance.PlayerCurrentMap]}";
    }
}
