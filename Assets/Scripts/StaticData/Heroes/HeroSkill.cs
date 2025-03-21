using System;
using CodeBase.StaticData.Skills;
using UnityEngine;

namespace CodeBase.StaticData.Heroes
{
  [Serializable]
  public class HeroSkill
  {
    public SkillTypeId TypeId;
    public SkillKind Kind;
    public string Name;
    public Color Color;
    public TargetType TargetType;
    public float Value;
    public float Cooldown;
    public SkillAnimation Animation;
    public GameObject CustomTargetFx;
  }
}