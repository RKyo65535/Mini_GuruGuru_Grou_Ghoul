using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeInfinity : MonoBehaviour
{

    [SerializeField] float strength;
    [SerializeField] int span;
    Transform TF;
    Vector3 initialPos;
    int count;

    // Start is called before the first frame update
    void Start()
    {
        TF = GetComponent<Transform>();
        initialPos = TF.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        count = (count + 1) % span;
        float angle = (count / (float)span) * 360 * Mathf.Deg2Rad;
        TF.position = initialPos + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0)* strength;
    }
}
