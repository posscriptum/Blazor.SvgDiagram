namespace Blazor.SvgDiagram.Interfaces
{
    public interface IShape
    {
        Task Add();
    }

    public interface IRectangle : IShape { }
    public interface ICircle : IShape { }
    public interface ILine : IShape { }
    public interface IImageShape : IShape { }
}
