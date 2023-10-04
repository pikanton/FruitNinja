using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitAnimation : MonoBehaviour
{
    public List<Sprite> fruitSprites;
    public float fruirRotateAngle = 2.5f;
    private void Start()
    {
        SetRandomFruitSprite();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * fruirRotateAngle);
    }

    private void SetRandomFruitSprite()
    {
        int randomSpriteIndex = Random.Range(0, fruitSprites.Count);
        Sprite selectedSprite = fruitSprites[randomSpriteIndex];
        gameObject.GetComponent<SpriteRenderer>().sprite = selectedSprite;
    }
}
