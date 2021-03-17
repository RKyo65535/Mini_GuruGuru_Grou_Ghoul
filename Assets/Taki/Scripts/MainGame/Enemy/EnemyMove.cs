using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    Transform TF;

    [SerializeField] float minRadius;//最低距離
    [SerializeField] float maxRadius;//最低距離
    [SerializeField] float minAngle;//最低角度
    [SerializeField] float maxAngle;//最高角度
    public float angularVelocity;//角速度

    float posOfRadius;//中心からの距離
    float posOflAngle;//角度

    private void Awake()
    {
        TF = transform;
        SetInitialPosition();

    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }


    void SetInitialPosition()
    {
        posOfRadius = Random.Range(minRadius, maxRadius);
        posOflAngle = Random.Range(minAngle, maxAngle);
        TF.position = new Vector3(Mathf.Cos(posOflAngle * Mathf.Deg2Rad), Mathf.Sin(posOflAngle * Mathf.Deg2Rad), 0) * posOfRadius;
    }

    void UpdatePosition()
    {
        posOflAngle = (posOflAngle + angularVelocity) % 360;
        TF.position = new Vector3(Mathf.Cos(posOflAngle * Mathf.Deg2Rad), Mathf.Sin(posOflAngle * Mathf.Deg2Rad), 0) * posOfRadius;
        if(posOflAngle >=0 && posOflAngle <= 180)
        {
            TF.localScale = new Vector3(Mathf.Abs(TF.localScale.x), TF.localScale.y, TF.localScale.z);
        }
        else
        {
            TF.localScale = new Vector3(-Mathf.Abs(TF.localScale.x), TF.localScale.y, TF.localScale.z);

        }
    }


    public void SetRadius(float radius)
    {
        posOfRadius = radius;
    }

    public void SetAngle(float angle)
    {
        posOflAngle = angle;
    }

}
