public interface IPizzaStore
{
    void CreatePizza();
}

public abstract class NYPizzaStore : IPizzaStore{

}

public abstract class ChicagoPizzaStore : IPizzaStore{

}

public class ChicagoCheesePizza : NYPizzaStore
{

}

public class ChicagoClamPizza : NYPizzaStore{
    
}

public class NYCheesePizza : NYPizzaStore
{

}

public class NYClamPizza : NYPizzaStore{
    
}
