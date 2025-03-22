using System;
using System.Collections.Generic;
using StaticData.Heroes;
using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "LevelStaticData", menuName = "StaticData/Level", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelName;
        public int LevelId;
        public LevelTypeId LevelTypeId;
        public List<HeroTypeId> Enemies;
    }

    [Serializable]
    public enum LevelTypeId
    {
        Regular = 0, 
        Special = 1,
        Bonus = 2,
    }
    
}