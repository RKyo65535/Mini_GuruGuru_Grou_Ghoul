using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredictAttackRangeAlphaMotion : MonoBehaviour
{
    [SerializeField] int spanFlame;
    int count;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count = (count + 1) % spanFlame;

        float alpha =(Mathf.Abs(count - spanFlame / 2f))/ (spanFlame / 2f) / 4f;

        spriteRenderer.color = new Color(1 / 5f, 1 / 5f, 1 / 5f, alpha + 0.25f);

    }
}
