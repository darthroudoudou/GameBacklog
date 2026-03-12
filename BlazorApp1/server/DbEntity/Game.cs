namespace BlazorApp1.server.DbEntity;

public class Game
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public bool IsCompleted { get; set; }

    public bool IsTodo { get; set; }

    public Platform Platform { get; set; }
}
