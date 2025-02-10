using Blazor.SvgDiagram.Data;
using Microsoft.JSInterop;

namespace Blazor.SvgDiagram.Interfaces
{
    public interface IDiagram
    {
        Task InitializeAsync(string containerId, int width, int height);
        Task AddShapeAsync(IShape shape);
        ValueTask DisposeAsync();
        public IJSObjectReference? GetSvgModule();
    }
}
