using System.Collections.Generic;
using UnityEngine;

public class FruitAnimation : MonoBehaviour
{
    public List<Sprite> fruitSprites;
    public float rotateAngle = 2.0f;
    
    public float targetIncreaseScale = 1.2f;
    public float targetDecreaseScale = 0.9f;
    public float scalingDuration = 5.0f;

    private Vector3 _initialScale;
    private float _targetScale;
    private float _scalingStartTime;
    private int _rotationDirection;
    private int _randomAnimationValue;
    
    private void Start()
    {
        SetRandomFruitSprite();

        _initialScale = transform.localScale;
        _scalingStartTime = Time.time;
        _rotationDirection = Random.Range(0, 2) == 0 ? -1 : 1;
        _targetScale = Random.Range(0, 2) == 0 ? targetIncreaseScale : targetDecreaseScale;
        _randomAnimationValue = Random.Range(0, 3);
    }

    private void Update()
    {
        SelectRandomAnimation();        
    }

    private void SetRandomFruitSprite()
    {
        int randomSpriteIndex = Random.Range(0, fruitSprites.Count);
        Sprite selectedSprite = fruitSprites[randomSpriteIndex];
        gameObject.GetComponent<SpriteRenderer>().sprite = selectedSprite;
    }

    private void Scale()
    {
        float elapsedTime = Time.time - _scalingStartTime;
        float scaleProgress = Mathf.Clamp01(elapsedTime / scalingDuration);

        Vector3 newScale = Vector3.Lerp(_initialScale, Vector3.one * _targetScale, scaleProgress);
        transform.localScale = newScale;
    }

    private void Rotate()
    {
        float finalRotationAngle = rotateAngle * _rotationDirection;  
        transform.Rotate(Vector3.forward * finalRotationAngle);
    }

    private void SelectRandomAnimation()
    {
        switch (_randomAnimationValue)
        {
            case 0:
                Rotate();
                break;
            case 1:
                Scale();
                break;
            case 2:
                Scale();
                Rotate();
                break;
        }
    }
}
