using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/CreateSkillData")]
public class SkillData : ScriptableObject
{
    [SerializeField] public string skillName;
    [SerializeField,TextArea(3,4)] public string skillExpraination;
}
