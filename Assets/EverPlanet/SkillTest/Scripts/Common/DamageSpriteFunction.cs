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
    /// 데미지 지우기 : 생성후 1초뒤에 실행
    /// </summary>
    void EraseDamageImage()
    {
        gameObject.SetActive(false);
    }
}
