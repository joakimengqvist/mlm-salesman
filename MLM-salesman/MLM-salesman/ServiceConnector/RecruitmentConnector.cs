using MLM_salesman.Models;
using MLM_salesman.Services;
using System.Collections.Concurrent;

namespace MLM_salesman.ServiceConnector;

public class RecruitmentConnector
{
    public async Task<(int averageHours, List<Simulation> history)> HandleRecruitmentAsync(int rows, int columns, int quantity)
    {
        var simulations = new ConcurrentBag<Simulation>();
        var batchSize = 100;
        
        var batches = Enumerable.Range(0, (quantity + batchSize - 1) / batchSize)
            .Select(i => Math.Min(batchSize, quantity - i * batchSize));
        
        await Parallel.ForEachAsync(batches, async (batchSize, _) =>
        {
            for (int i = 0; i < batchSize; i++)
            {
                var recruitmentService = new RecruitmentService(rows, columns);
                var (hours, history) = recruitmentService.SimulateRecruitment();
                simulations.Add(new Simulation(hours, history));
            }
        });

        var totalHours = simulations.Sum(simulation => simulation.HoursPassed);
        var averageHours = simulations.Any() ? totalHours / simulations.Count : 0;

        return (averageHours, simulations.ToList());
    }
}