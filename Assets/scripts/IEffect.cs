public interface IEffect
{
    float Duration { get; }

    void Activate();

    void Deactivate();
}
