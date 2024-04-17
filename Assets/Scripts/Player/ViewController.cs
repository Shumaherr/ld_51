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
}