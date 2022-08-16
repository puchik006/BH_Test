public interface ISprintable
{
    bool IsSprinting { get; }

    void Sprint(bool mouseDown);
}