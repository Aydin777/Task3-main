namespace Task3.Services;

public class CalculateService : ICalculateService
{
    private int _value = 100;

    public int Calculate(int num1, int num2)
    {
        _value += 100;
        return _value;
    }
}
