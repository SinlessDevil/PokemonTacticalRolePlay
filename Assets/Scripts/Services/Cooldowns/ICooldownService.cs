namespace Services.Cooldown
{
    public interface ICooldownService
    {
        void CooldownTick(float deltaTime);
    }
}