using YardenProject.Models;
using YardenProject.Data;

namespace YardenProject.Services
{
    public class OperationService : IOperationService
    {
        private readonly GaiaDbContext _context;

        public OperationService(GaiaDbContext context)
        {
            _context = context;
        }

        public OperationResult Calculate(OperationRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.FieldA) || string.IsNullOrEmpty(request.FieldB) || string.IsNullOrEmpty(request.Operation))
                    return new OperationResult { Success = false, Message = "Missing input fields." };

                string result = request.Operation.ToUpper() switch
                {
                    "ADD" => (double.Parse(request.FieldA) + double.Parse(request.FieldB)).ToString(),
                    "SUBTRACT" => (double.Parse(request.FieldA) - double.Parse(request.FieldB)).ToString(),
                    "MULTIPLY" => (double.Parse(request.FieldA) * double.Parse(request.FieldB)).ToString(),
                    "DIVIDE" => (double.Parse(request.FieldB) == 0) ? "Cannot divide by zero" : (double.Parse(request.FieldA) / double.Parse(request.FieldB)).ToString(),
                    "CONCATENATE" => request.FieldA + request.FieldB,
                    "CONTAINS" => request.FieldA.Contains(request.FieldB).ToString(),
                    _ => null
                };

                if (result == null)
                    return new OperationResult { Success = false, Message = "Invalid operation requested." };

                var history = new OperationHistory
                {
                    FieldA = request.FieldA,
                    FieldB = request.FieldB,
                    Operation = request.Operation,
                    Result = result,
                    OperationDate = DateTime.Now
                };

                _context.OperationHistories.Add(history);
                _context.SaveChanges();

                return new OperationResult { Success = true, Message = "Operation successful.", Data = result };
            }
            catch (Exception ex)
            {
                return new OperationResult { Success = false, Message = "Error: " + ex.Message };
            }
        }

        public List<OperationHistory> GetLastOperations(string operation)
        {
            return _context.OperationHistories
                .Where(o => o.Operation.ToUpper() == operation.ToUpper())
                .OrderByDescending(o => o.OperationDate)
                .Skip(1) // מדלגים על הפעולה האחרונה שהרגע בוצעה
                .Take(3) // לוקחים את שלושת הבאים
                .ToList();
        }


        public int GetMonthlyOperationCount(string operation)
        {
            var now = DateTime.Now;
            return _context.OperationHistories
                .Where(o => o.Operation.ToUpper() == operation.ToUpper()
                         && o.OperationDate.Month == now.Month
                         && o.OperationDate.Year == now.Year)
                .Count();
        }
    }
}
