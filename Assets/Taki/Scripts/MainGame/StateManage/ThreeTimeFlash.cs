using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThreeTimeFlash : MonoBehaviour
{

    TextMeshProUGUI text;
    int count;
    
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        count = 25;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count++;
        float alpha = Mathf.Abs((count % 50)-25) / 25f;
        text.color = new Color(1, 1, 1, alpha);
        if(count > 175)
        {
            gameObject.SetActive(false);
        }
    }
}
