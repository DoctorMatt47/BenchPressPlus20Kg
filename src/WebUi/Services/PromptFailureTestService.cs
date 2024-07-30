using BenchPressPlus20Kg.Domain;
using Microsoft.JSInterop;

namespace WebUi.Services;

public class PromptFailureTestService(IJSRuntime jsRuntime) : IFailureTestService
{
    public async Task<int> GetFailureTestReps()
    {
        var prompted = await jsRuntime.InvokeAsync<string>("prompt", "Take some input:");
        return int.Parse(prompted);
    }
}
