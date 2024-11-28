namespace MLM_salesman.Models;

public struct Simulation(int hoursPassed, List<HistorySnapshot> history)
{
    public int HoursPassed { get; set; } = hoursPassed;
    public List<HistorySnapshot> History { get; set; } = history;
}

public class HistorySnapshot
{
    public List<House> Houses { get; set; }
    public List<Salesman> Salesmen { get; set; }
    public HistorySnapshot()
    {
        Houses = new List<House>();
        Salesmen = new List<Salesman>();
    }
}

public class Neighborhood
{
    public House[,] Houses { get; init; }
    public List<Salesman> Salesmen { get; set; } = new List<Salesman>();
    public int Rows => Houses.GetLength(0);
    public int Columns => Houses.GetLength(1);
}

public class House
{
    public bool HasUnemployedPerson { get; set; } = true;
    public int X { get; set; }
    public int Y { get; set; }
}


public class Salesman
{
    public int X { get; set; }
    public int Y { get; set; }
}

