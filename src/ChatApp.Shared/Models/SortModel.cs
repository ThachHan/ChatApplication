namespace ChatApp.Common.Models;

public class SortModel
{
    public SortModel() { }

    public SortModel(string field, string sort)
    {
        Field = field;
        Sort = sort;
    }

    public string? Field { get; set; } = string.Empty;
    public string? Sort { get; set; } = string.Empty;
}
