using UnityEngine;

public class ShadowAnimation : MonoBehaviour
{
    public Transform blockTransform;
    public SpriteRenderer blockSpriteRenderer;
    public SpriteRenderer shadowSpriteRenderer;

    private Vector3 _initialShadowOffset;
    private Vector3 _initialBlockScale;

    private void Start()
    {
        shadowSpriteRenderer.sprite = blockSpriteRenderer.sprite;
        _initialShadowOffset = transform.localPosition;
        _initialBlockScale = blockTransform.localScale;
    }

    private void Update()
    {
        Vector3 fruitScale = blockTransform.localScale;
        transform.localScale = fruitScale;
        Vector3 newOffset = new Vector3(_initialShadowOffset.x + fruitScale.x - _initialBlockScale.x,
            _initialShadowOffset.y - fruitScale.y + _initialBlockScale.y, 0);
        transform.localPosition = newOffset;
        
        transform.rotation = blockTransform.rotation;
    }
}
