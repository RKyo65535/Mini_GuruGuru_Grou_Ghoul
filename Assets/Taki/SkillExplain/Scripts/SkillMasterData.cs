using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/CreateSkillMaster")]
public class SkillMasterData : ScriptableObject
{
    public List<SkillData> skills;
}
