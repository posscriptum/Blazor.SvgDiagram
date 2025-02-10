using Blazor.SvgDiagram.Extension;
using Blazor.SvgDiagram.Interfaces;
using Microsoft.JSInterop;

namespace Blazor.SvgDiagram.Data
{
    public class Rectangle : IRectangle
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly double _x;
        private readonly double _y;
        private readonly double _width;
        private readonly double _height;
        private readonly string _color;

        public Rectangle(IJSRuntime jsRuntime, double x, double y, double width, double height, string color)
        {
            _jsRuntime = jsRuntime;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
            _color = color;
        }

        public async Task Add(IJSObjectReference? _svgModul)
        {
            await _jsRuntime.InvokeSvgMethod(_svgModul, "addRectangle", _x, _y, _width, _height, _color);
        }
    }
}
