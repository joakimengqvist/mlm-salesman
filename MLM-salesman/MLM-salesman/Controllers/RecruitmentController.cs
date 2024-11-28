using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MLM_salesman.ServiceConnector;

namespace MLM_salesman.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecruitmentController(RecruitmentConnector recruitmentConnector) : ControllerBase
{
    [HttpGet("simulate")]
    public async Task<IActionResult> SimulateRecruitment(int rows, int columns, int quantity)
    {
        if (rows <= 0 || columns <= 0 || quantity <= 0)
        {
            return BadRequest("Rows, columns, and quantity must all be greater than 0.");
        }
        
        var (averageHours, simulations) = await recruitmentConnector.HandleRecruitmentAsync(rows, columns, quantity);
        
        return Ok(new
        {
            AverageHours = averageHours,
            Simulations = simulations
        });
    }
}
