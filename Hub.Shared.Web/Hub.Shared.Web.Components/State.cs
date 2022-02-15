namespace Hub.Shared.Web.Components;

public class State
{
    public event Action? OnChange;

    public bool Working { get; private set; }

    public void SetWorking(bool working)
    {
        Working = working;

        OnChange?.Invoke();
    }
}