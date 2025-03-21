namespace CodeBase.Gameplay.Cooldowns
{
    public interface ICooldownService
    {
        void CooldownTick(float deltaTime);
    }
}