using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI accumulateDamageText;

    public List<Sprite> damageImage = new List<Sprite>();
    public List<Sprite> criticalDamageImage = new List<Sprite>();
    //���� ������
    long accumulateDamage = 0;
    
    public void ShowDamage(long damage)
    {
        accumulateDamage += damage;
        accumulateDamageText.text = accumulateDamage.ToString();
    }
}
