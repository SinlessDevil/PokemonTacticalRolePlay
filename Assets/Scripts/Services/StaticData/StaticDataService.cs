using System;
using System.Collections.Generic;
using System.Linq;
using Services.Factories.Paths;
using StaticData;
using StaticData.Heroes;
using StaticData.Levels;
using StaticData.Skills;
using UnityEngine;
using Window;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private GameStaticData _gameStaticData;
        private BalanceStaticData _balanceStaticData;
        private Dictionary<WindowTypeId, WindowConfig> _windowConfigs;
        private List<ChapterStaticData> _chapterStaticDatas = new();
        private Dictionary<HeroTypeId, HeroConfig> _heroConfigs = new();
        
        public GameStaticData GameConfig => _gameStaticData;
        public BalanceStaticData Balance => _balanceStaticData;
        public List<ChapterStaticData> Chapters => _chapterStaticDatas;
        
        public void LoadData()
        {
            _gameStaticData = Resources
                .Load<GameStaticData>(ResourcePath.GameConfigPath);
            
            _balanceStaticData = Resources
                .Load<BalanceStaticData>(ResourcePath.GameBalancePath);
            
            _windowConfigs = Resources
                .Load<WindowStaticData>(ResourcePath.WindowsStaticDataPath)
                .Configs.ToDictionary(x => x.WindowTypeId, x => x);
            
            _chapterStaticDatas = Resources
                .LoadAll<ChapterStaticData>(ResourcePath.ChaptersStaticDataPath)
                .ToList();
            
            _heroConfigs = Resources
                .LoadAll<HeroConfig>(ResourcePath.HeroConfigFolderPath)
                .ToDictionary(x => x.TypeId, x => x);
        }

        public WindowConfig ForWindow(WindowTypeId windowTypeId) => _windowConfigs[windowTypeId];

        public LevelStaticData ForLevel(int chapterId, int levelId)
        {
            if (_chapterStaticDatas.Count == 0)
                throw new InvalidOperationException("No chapters available.");

            int realChapterIndex = (chapterId - 1) % _chapterStaticDatas.Count;
            var chapter = _chapterStaticDatas[realChapterIndex];

            if (chapter.Levels.Count == 0)
                throw new InvalidOperationException($"Chapter {realChapterIndex + 1} has no levels.");

            if (levelId < 1 || levelId > chapter.Levels.Count)
                throw new ArgumentOutOfRangeException(nameof(levelId), $"Level {levelId} is out of range: 1 - {chapter.Levels.Count}");

            var level = chapter.Levels[levelId - 1];
            return level;
        }
        
        public ChapterStaticData ForChapter(int chapterId)
        {
            if (_chapterStaticDatas.Count == 0)
                throw new InvalidOperationException("No chapters available.");

            int realChapterIndex = (chapterId - 1) % _chapterStaticDatas.Count;

            return _chapterStaticDatas[realChapterIndex];
        }
        
        public HeroConfig HeroConfigFor(HeroTypeId typeId)
        {
            if (_heroConfigs.TryGetValue(typeId, out HeroConfig config))
                return config;
      
            throw new KeyNotFoundException($"No config found for {typeId}");
        }

        public HeroSkill HeroSkillFor(SkillTypeId typeId, HeroTypeId heroTypeId)
        {
            HeroConfig heroConfig = HeroConfigFor(heroTypeId);
            HeroSkill heroSkill = heroConfig.Skills.Find(x => x.TypeId == typeId);
            if (heroSkill != null)
                return heroSkill;
      
            throw new KeyNotFoundException($"No hero skill config found for {typeId} on {heroTypeId}");
        }
    }
}