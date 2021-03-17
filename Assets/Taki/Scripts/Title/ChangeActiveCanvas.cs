using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeActiveCanvas : MonoBehaviour
{
    [SerializeField] GameObject Object1;
    [SerializeField] GameObject Object2;


    public void ChangeActive()
    {
        if (Object1.activeSelf)
        {
            Object2.SetActive(true);
            Object1.SetActive(false);
        }
        else
        {
            Object1.SetActive(true);
            Object2.SetActive(false);
        }
    }


}
