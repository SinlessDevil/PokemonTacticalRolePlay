using System.Collections.Generic;
using Services.Storage;
using StaticData.Heroes;
using StaticData.Levels;
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
        public List<Currency> Rewards;
    }
}