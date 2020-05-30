using System;
using System.Collections.Generic;
using System.Threading;


public interface IObserver
{
    // Receive update from subject
    void Update(ISubject subject);
}

public interface ISubject
{
    // Add an observer to the subject.
    void AddOberver(IObserver observer);

    //Delete an observer from the subject.
    void DeleteOberver(IObserver observer);

    // Notify all observers about an event.
    void Notify();
}

public class Subject : ISubject
{
    public int State { get; set; }
    private List<IObserver> _observers = new List<IObserver>();

    public void AddOberver(IObserver observer)
    {
        Console.WriteLine("Add an observer.");
        this._observers.Add(observer);
    }

    public void DeleteOberver(IObserver observer)
    {
        this._observers.Remove(observer);
        Console.WriteLine("Delete an observer.");
    }

    // Trigger an update in each subscriber.
    public void Notify()
    {
        Console.WriteLine("Subject: Notifying observers...");

        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
    }

    // Usually, the subscription logic is only a fraction of what a Subject
    // can really do. Subjects commonly hold some important business logic,
    // that triggers a notification method whenever something important is
    // about to happen (or after it).
    public void SomeBusinessLogic()
    {
        Console.WriteLine("\nSubject: I'm doing something important.");
        this.State = new Random().Next(0, 10);

        Thread.Sleep(15);

        Console.WriteLine("Subject: My state has just changed to: " + this.State);
        this.Notify();
    }
}

class ConcreteObserverA : IObserver
{
    public void Update(ISubject subject)
    {
        if ((subject as Subject).State < 3)
        {
            Console.WriteLine("ConcreteObserverA: Reacted to the event.");
        }
    }
}

class ConcreteObserverB : IObserver
{
    public void Update(ISubject subject)
    {
        if ((subject as Subject).State == 0 || (subject as Subject).State >= 2)
        {
            Console.WriteLine("ConcreteObserverB: Reacted to the event.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // The client code.
        var subject = new Subject();
        var observerA = new ConcreteObserverA();
        subject.AddOberver(observerA);

        var observerB = new ConcreteObserverB();
        subject.AddOberver(observerB);

        subject.SomeBusinessLogic();
        subject.SomeBusinessLogic();

        subject.DeleteOberver(observerB);

        subject.SomeBusinessLogic();
    }
}
