namespace App.Contracts.Interfaces
{
    public interface IEvent
    {
        string GameName {get;}
        string EventType { get; }
    }
}