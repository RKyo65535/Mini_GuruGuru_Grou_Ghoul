using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KakudaisyukusyouWhenSelected : MonoBehaviour
{
    Transform TF;
    Vector3 originalSize;
    [SerializeField] int loopSpan = 150;
    [SerializeField] float strength = 0.2f;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        TF = GetComponent<Transform>();
        originalSize = TF.localScale;
    }


    private void FixedUpdate()
    {
        
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            count = (count + 1) % loopSpan;
            float sizeRate = ((Mathf.Sin(count / (float)loopSpan * 360 * Mathf.Deg2Rad) + 1) /2f) * strength;
            TF.localScale = originalSize * (sizeRate + 1);
        }
        else
        {
            count = 0;
            TF.localScale = originalSize;
        }
    }


}
