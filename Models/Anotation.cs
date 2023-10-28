using System.ComponentModel.DataAnnotations;

namespace MyScriptureJournal.Models;

public class Anotation
{
    public int Id { get; set; }
    public string? Record { get; set; }
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    public string? Book {  get; set; }
}
