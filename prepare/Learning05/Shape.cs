public class Shape
{
    private string _color;
    public string Color
    {
        get { return _color;}
        set {_color = value;}
    }

    public Shape()
    {
        _color = "shape";
    }

    public Shape(string color)
    {
        _color = color;
    }

    public virtual double GetArea()
    {
        return 0;
    }
}
