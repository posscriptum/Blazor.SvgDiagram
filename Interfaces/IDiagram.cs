using Blazor.SvgDiagram.Data;

namespace Blazor.SvgDiagram.Interfaces
{
    public interface IDiagram
    {
        Task Initialize(string containerId, int width, int height);
        Task AddShape(IShape shape);
        ValueTask DisposeAsync();
    }
}
