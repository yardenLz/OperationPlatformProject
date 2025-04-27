using YardenProject.Models;

namespace YardenProject.Services
{
    public interface IOperationService
    {
        OperationResult Calculate(OperationRequest request);
        List<OperationHistory> GetLastOperations(string operation);
        int GetMonthlyOperationCount(string operation);
    }
}

