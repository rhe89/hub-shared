namespace Hub.Shared.Web.Components;

public class State
{
    public event Action? OnChange;

    public bool Saving { get; private set; }

    public void SetWorking(bool saving)
    {
        Saving = saving;

        OnChange?.Invoke();
    }
}