using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Heroes
{
  [CreateAssetMenu(menuName = "UtilityBattler/Hero Config")]
  public class HeroConfig : ScriptableObject
  {
    public HeroTypeId TypeId;
    public float Hp;
    public float Armor;
    public float Initiative;

    public List<HeroSkill> Skills;

    public GameObject Prefab;
  }
}