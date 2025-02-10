using Blazor.SvgDiagram.Interfaces;
using Microsoft.JSInterop;

namespace Blazor.SvgDiagram.Data
{
    public class Diagram : IDiagram
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference? _svgModule;

        public Diagram(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task InitializeAsync(string containerId, int width, int height)
        {
            _svgModule = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/svgInterop.js");
            await _svgModule.InvokeVoidAsync("createSvg", containerId, width, height);
            await _svgModule.InvokeVoidAsync("drawGrid", width, height, 20);
        }

        public IJSObjectReference? GetSvgModule() => _svgModule;

        public async Task AddShapeAsync(IShape shape)
        {
            await shape.Add(_svgModule);
        }

        public async ValueTask DisposeAsync()
        {
            if (_svgModule != null)
            {
                await _svgModule.DisposeAsync();
            }
        }
    }
}
