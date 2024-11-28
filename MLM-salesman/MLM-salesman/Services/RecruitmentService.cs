using MLM_salesman.Models;

namespace MLM_salesman.Services;

public class RecruitmentService
{
    private readonly Neighborhood _neighborhood;
    private readonly Random _random = new Random();
    private List<HistorySnapshot> History { get; set; } = [];
    
    public RecruitmentService(int rows, int columns)
    {
        _neighborhood = new Neighborhood
        {
            Houses = new House[rows, columns]
        };

        InitializeHouses(rows, columns);
        InitializeFirstSalesman();
        SaveState();
    }
    
    private void InitializeHouses(int rows, int columns)
    {
        for (var y = 0; y < rows; y++)
        {
            for (var x = 0; x < columns; x++)
            {
                _neighborhood.Houses[y, x] = new House();
            }
        }
    }
    private void InitializeFirstSalesman()
    {
        _neighborhood.Salesmen.Add(new Salesman { X = 0, Y = 0 });
        RecruitPerson(0, 0);
    }
    
    public (int hours, List<HistorySnapshot> history) SimulateRecruitment()
    {
        var hours = 0;

        while (_neighborhood.Houses.Cast<House>().Any(h => h.HasUnemployedPerson))
        {
            foreach (var salesman in _neighborhood.Salesmen.ToList())
            {
                MoveSalesman(salesman);
            }

            hours++;
            SaveState();
        }

        return (hours, History);
    }

    private void MoveSalesman(Salesman salesman)
    {
        var neighbors = GetNeighbors(salesman.X, salesman.Y);

        if (neighbors.Count <= 0) return;
        
        var (newX, newY) = neighbors[_random.Next(neighbors.Count)];
        salesman.X = newX;
        salesman.Y = newY;

        RecruitPerson(newX, newY);
    }

    private void RecruitPerson(int x, int y)
    {
        var house = _neighborhood.Houses[y, x];
        if (!house.HasUnemployedPerson) return;
        
        house.HasUnemployedPerson = false;
        _neighborhood.Salesmen.Add(new Salesman { X = x, Y = y });
    }

    private List<(int x, int y)> GetNeighbors(int x, int y)
    {
        var neighbors = new List<(int x, int y)>();

        if (x > 0) neighbors.Add((x - 1, y));
        if (x < _neighborhood.Columns - 1) neighbors.Add((x + 1, y));
        if (y > 0) neighbors.Add((x, y - 1));
        if (y < _neighborhood.Rows - 1) neighbors.Add((x, y + 1));

        return neighbors;
    }
    
    private void SaveState()
    {
        var houses = _neighborhood.Houses
            .Cast<House>()
            .Select((house, index) => new House
            {
                X = index % _neighborhood.Columns,
                Y = index / _neighborhood.Columns,
                HasUnemployedPerson = house.HasUnemployedPerson
            })
            .ToList();

        var salesmen = _neighborhood.Salesmen.Select(s => new Salesman { X = s.X, Y = s.Y }).ToList();
        
        History.Add(new HistorySnapshot
        {
            Houses = houses,
            Salesmen = salesmen
        });
    }
}
