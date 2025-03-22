using System.Collections.Generic;
using StaticData;
using StaticData.Heroes;
using StaticData.Levels;
using StaticData.Skills;
using Window;

namespace Services.StaticData
{
    public interface IStaticDataService
    {
        GameStaticData GameConfig { get; }
        BalanceStaticData Balance { get; }
        List<ChapterStaticData> Chapters { get; }
        void LoadData();
        WindowConfig ForWindow(WindowTypeId windowTypeId);
        LevelStaticData ForLevel(int chapterId, int levelId);
        ChapterStaticData ForChapter(int chapterId);
        HeroConfig HeroConfigFor(HeroTypeId typeId);
        HeroSkill HeroSkillFor(SkillTypeId typeId, HeroTypeId heroTypeId);
    }
}