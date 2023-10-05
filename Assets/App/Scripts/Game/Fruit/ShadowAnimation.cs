using UnityEngine;

public class ShadowAnimation : MonoBehaviour
{
    public GameObject fruit;

    private Vector3 _initialShadowOffset;
    private Vector3 _initialFruitScale;

    private void Start()
    {
        Sprite sprite = fruit.GetComponent<SpriteRenderer>().sprite;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        _initialShadowOffset = transform.localPosition;
        _initialFruitScale = fruit.transform.localScale;
    }

    private void Update()
    {
        Vector3 fruitScale = fruit.transform.localScale;
        transform.localScale = fruitScale;
        Vector3 newOffset = new Vector3(_initialShadowOffset.x + fruitScale.x - _initialFruitScale.x,
            _initialShadowOffset.y - fruitScale.y + _initialFruitScale.y, 0);
        transform.localPosition = newOffset;
        
        transform.rotation = fruit.transform.rotation;
    }
}
