using Blazor.SvgDiagram.Interfaces;
using Microsoft.JSInterop;

namespace Blazor.SvgDiagram.Data
{
    public class Circle : ICircle
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly double _cx, _cy, _r;
        private readonly string _color;

        public Circle(IJSRuntime jsRuntime, double cx, double cy, double r, string color)
        {
            _jsRuntime = jsRuntime;
            _cx = cx;
            _cy = cy;
            _r = r;
            _color = color;
        }

        public async Task Add()
        {
            await _jsRuntime.InvokeVoidAsync("addCircle", _cx, _cy, _r, _color);
        }
    }
}
