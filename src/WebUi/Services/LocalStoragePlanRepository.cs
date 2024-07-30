using BenchPressPlus20Kg.Domain;
using Blazored.LocalStorage;

namespace WebUi.Services;

public class LocalStoragePlanRepository(IServiceProvider provider) : IPlanRepository
{
    public Plan? Plan { get; private set; }
    public Weight.Unit? Unit { get; private set; }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();

    public async Task<Plan?> GetPlan()
    {
        await using var scope = provider.CreateAsyncScope();
        var localStorage = scope.ServiceProvider.GetRequiredService<ILocalStorageService>();
        return Plan ??= await localStorage.GetItemAsync<Plan>("plan");
    }

    public async Task SavePlan(Plan plan)
    {
        var localStorage = await GetLocalStorage();
        await localStorage.SetItemAsync("plan", plan);
        Plan = plan;
        NotifyStateChanged();
    }

    public async Task DeletePlan()
    {
        var localStorage = await GetLocalStorage();
        await localStorage.RemoveItemAsync("plan");
        Plan = null!;
        NotifyStateChanged();
    }

    public async Task<Weight.Unit?> GetUnit()
    {
        var localStorage = await GetLocalStorage();
        return Unit ??= await localStorage.GetItemAsync<Weight.Unit>("unit");
    }
    
    public async Task SaveUnit(Weight.Unit unit)
    {
        var localStorage = await GetLocalStorage();
        await localStorage.SetItemAsync("unit", unit);
        Unit = unit;
        NotifyStateChanged();
    }
    
    private async Task<ILocalStorageService> GetLocalStorage()
    {
        await using var scope = provider.CreateAsyncScope();
        return scope.ServiceProvider.GetRequiredService<ILocalStorageService>();
    }
}
