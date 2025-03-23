using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace StaticData.Heroes
{
    [CreateAssetMenu(menuName = "UtilityBattler/Hero Config")]
    public class HeroConfig : ScriptableObject
    {
        public HeroTypeId TypeId;
        public float Hp;
        public float Armor;
        public float Initiative;

        public List<HeroSkill> Skills;

        public Sprite IconImage;
        [FormerlySerializedAs("bgSprite")] public Sprite BgSprite;
        public GameObject Prefab;
    }
}