using UnityEngine;

public class DamageSpriteFunction : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        Invoke("EraseDamageImage", 0.5f);
        spriteRenderer.sortingOrder = KeyInputSystem.orderSortNum;
    }
    /// <summary>
    /// ������ ����� : ������ 1�ʵڿ� ����
    /// </summary>
    void EraseDamageImage()
    {
        gameObject.SetActive(false);
    }
}
