using System.Collections.Generic;

public class Room
{
    private HashSet<IRoomObserver> roomObservers = new HashSet<IRoomObserver>();

    public void AddObserver(IRoomObserver observer) => roomObservers.Add(observer);
    public void RemoveObservers() => roomObservers.Clear();

    public void NotifyObservers()
    {
        foreach (IRoomObserver roomObserver in roomObservers)
            roomObserver.Execute();
    }
}
