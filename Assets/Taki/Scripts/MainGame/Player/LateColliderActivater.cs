using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateColliderActivater : MonoBehaviour
{

    [SerializeField] GameObject child;
    [SerializeField] int activateCount;//有効化するまでのカウント
    [SerializeField] int inactivateCount;//無効化するまでのカウント
    int count;

    private void Awake()
    {
        child.SetActive(false);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(count == activateCount)
        {
            count = count + 1;
            child.SetActive(true);
        }
        else if(count == inactivateCount)
        {
            child.SetActive(false);
        }
        else
        {
            count = count + 1;

        }
    }
}
