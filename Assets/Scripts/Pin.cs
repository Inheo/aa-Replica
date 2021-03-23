using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    [Range(10, 40)]
    public float Speed = 20f;

    public Vector3 Target;

    public GameManager GameManager;

    public CircleCollider2D PinCollider;
    public BoxCollider2D SpearCOllider;

    public SpriteRenderer[] SpriteRenderers;

    private bool isPinned;
    private bool hide;

    private void Update()
    {
        if (hide)
        {
            for (int i = 0; i < SpriteRenderers.Length; i++)
            {
                Color spriteColor = Vector4.MoveTowards(SpriteRenderers[i].color, new Vector4(0, 0, 0, 0), Time.deltaTime * 5);
                SpriteRenderers[i].color = spriteColor;
            }
        }
        if (!isPinned)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime * GameManager.TimeForSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Rotator")
        {
            transform.SetParent(collision.transform);
            GameManager.UpdateUI(1);
            isPinned = true;
        }
        else if(!isPinned)
        {
            GameManager.EndGame?.Invoke();
        }
    }

    public void HidePin()
    {
        hide = true;
    }
}
