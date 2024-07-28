using System.Collections;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    [SerializeField] private Sprite normalView;
    [SerializeField] private Sprite fatView;
    [SerializeField] private RuntimeAnimatorController normalAnimator;
    [SerializeField] private RuntimeAnimatorController fatAnimator;

    private SpriteRenderer _spriteRenderer;
    private Animator _animatorComponent;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animatorComponent = GetComponentInChildren<Animator>();
    }


    public void ChangeToNormalView()
    {
        _spriteRenderer.sprite = normalView;
        _animatorComponent.runtimeAnimatorController = normalAnimator;
    }

    public void ChangeToFatView()
    {
        _spriteRenderer.sprite = fatView;
        _animatorComponent.runtimeAnimatorController = fatAnimator;
    }
    
    public void MakeInvisible()
    {
        //Make the player invisible by changing transparency every 0.5 seconds
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Color color = spriteRenderer.color;
        while (color.a > 0)
        {
            color.a -= 0.1f;
            spriteRenderer.color = color;
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    public void MakeVisible()
    {
        //Reset the sprite's transparency when the effect is removed
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        Color color = spriteRenderer.color;
        color.a = 1;
        spriteRenderer.color = color;
    }
}