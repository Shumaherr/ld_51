using UnityEngine;

public class EffectsApplier : MonoBehaviour
{
    private ViewController viewController;
    private CollisionController collisionController;

    private void Awake()
    {
        viewController = gameObject.GetComponent<ViewController>();
        collisionController = gameObject.GetComponent<CollisionController>();
    }

    public void ApplyBloatEffect()
    {
        viewController.ChangeToFatView();
        collisionController.ChangeToFatCollider();
    }

    public void RemoveBloatEffect()
    {
        viewController.ChangeToNormalView();
        collisionController.ChangeToNormalCollider();
    }
}