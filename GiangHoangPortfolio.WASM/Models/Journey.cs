namespace GiangHoangPortfolio.WASM.Models;

public class JourneyItem
{
    public string Title { get; set; }
    public string Date { get; set; }
    public string Status { get; set; }
    public string Type { get; set; } // Course, Project, Milestone
    public string? Score { get; set; }
    public List<string> Tags { get; set; }
    public string? Description { get; set; }
}