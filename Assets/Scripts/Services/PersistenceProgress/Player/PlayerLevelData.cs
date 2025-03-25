using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Services.PersistenceProgress.Player
{
    [Serializable]
    public class PlayerLevelData
    {
        public LevelContainer LastProgress = new LevelContainer();
        public LevelContainer CurrentProgress = new LevelContainer();
        public List<LevelContainer> LevelsComleted = new List<LevelContainer>();
    }
    
    [Serializable]
    public class LevelContainer
    {
        public int ChapterId = 1;
        public int LevelId = 1;
        public int CountStart = 0;
        public float Time = 0f;
    }
}