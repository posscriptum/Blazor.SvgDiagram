using Microsoft.JSInterop;

namespace Blazor.SvgDiagram.Interfaces
{
    public interface IShapeFactory
    {
        IShape CreateShape(string type, IJSRuntime jsRuntime, params object[] parameters);
    }
}
