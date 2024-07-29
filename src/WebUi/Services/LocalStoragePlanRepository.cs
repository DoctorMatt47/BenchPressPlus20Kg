using BenchPressPlus20Kg.Domain;
using Blazored.LocalStorage;

namespace WebUi.Services;

public class LocalStoragePlanRepository(ILocalStorageService localStorage) : IPlanRepository
{
    public async Task<Plan?> GetPlan()
    {
        return await localStorage.GetItemAsync<Plan>("plan");
    }

    public async Task SavePlan(Plan plan)
    {
        await localStorage.SetItemAsync("plan", plan);
    }
}
