public interface IQuackBehavior
{
    void quack();
}

public class Quack : IQuackBehavior
{
    public void quack()
    {
        System.Console.WriteLine("Quack");
    }
}

public class MuteQuack : IQuackBehavior
{
    public void quack()
    {
        System.Console.WriteLine("Silence");
    }
}

public class Squeak : IQuackBehavior
{
    public void quack()
    {
        System.Console.WriteLine("Squeak");
    }
}

public interface IFlyBehavior
{
    void fly();
}

public class FlyWithWings : IFlyBehavior
{
    public void fly()
    {
        System.Console.WriteLine("Flying with wings");
    }
}

public class FlyNoWay : IFlyBehavior
{
    public void fly()
    {
        System.Console.WriteLine("I can't fly...");
    }
}

public abstract class Duck
{
    public IFlyBehavior flyBehavior;
    public IQuackBehavior quackBehavior;

    public Duck()
    {

    }

    public Duck(IQuackBehavior qb, IFlyBehavior fb)
    {
        quackBehavior = qb;
        flyBehavior = fb;
    }

    public abstract void display();

    public void performFly()
    {
        flyBehavior.fly();
    }

    public void performQuack()
    {
        quackBehavior.quack();
    }

    public void swim()
    {
        System.Console.WriteLine("Yeah, all ducks can swim.");
    }

}

public class ModelDuck : Duck
{
    public ModelDuck(IFlyBehavior f, IQuackBehavior q)
    {
        flyBehavior = f;
        quackBehavior = q;
    }

    public override void display()
    {
        System.Console.WriteLine("Model duck");
    }
}

public class Program
{

    public static void Main(string[] args)
    {
        var f = new FlyWithWings();
        var q = new Squeak();
        var duck = new ModelDuck(f, q);
        duck.performFly();
        duck.display();
        duck.performQuack();
        //Your code goes here
        Console.WriteLine("Hello, world!");
    }
}

