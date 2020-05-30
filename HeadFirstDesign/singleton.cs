public class Singleton
{
    private static Singleton unique;

    private Singleton() { }

    public static Singleton GetSingleton()
    {
        if (unique == null)
            return new Singleton();

        return unique;
    }
}

// consider synchrozined

// or eagerly create an instance
public class SingletonEager
{
    private static SingletonEager unique = new SingletonEager();

    private Singleton() { }

    public static SingletonEager GetSingleton()
    {
        return unique;
    }
}
