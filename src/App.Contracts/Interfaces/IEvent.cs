namespace App.Contracts.Interfaces
{
    public interface IEvent
    {
        string PlayerName {get;}
        string GameName {get;}
        string EventType { get; }
    }
}