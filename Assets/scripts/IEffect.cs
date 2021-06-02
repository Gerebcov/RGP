public interface IEffect
{
    float Time { get; }

    void Activate();

    void Deactivate();
}
