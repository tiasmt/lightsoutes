namespace App.Contracts.Models.Interfaces.Entities
{
    public interface IEvent
    {
        string UserId {get;}
        string GameId {get;}
        string EventType { get; }
    }
}