using Blazor.SvgDiagram.Extension;
using Blazor.SvgDiagram.Interfaces;
using Microsoft.JSInterop;

namespace Blazor.SvgDiagram.Data
{
    public class Rectangle : IRectangle
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly Rectangle _bounds;
        private double _x, _y, _width, _height;
        private string _color;

        public Rectangle(IJSRuntime jsRuntime) => _jsRuntime = jsRuntime;

        public Rectangle SetParameters(double x, double y, double width, double height, string color)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _color = color;
            return this;
        }

        public async Task Add(IJSObjectReference? _svgModul)
        {
            await _jsRuntime.InvokeSvgMethod(_svgModul, "addRectangle", _x, _y, _width, _height, _color);
        }
    }
}
