using Microsoft.JSInterop;

namespace Blazor.SvgDiagram.Interfaces
{
    public interface IShape
    {
        Task Add(IJSObjectReference? _svgModule);
    }

    public interface IRectangle : IShape { }
    public interface ICircle : IShape { }
    public interface ILine : IShape { }
    public interface IImageShape : IShape { }
}
