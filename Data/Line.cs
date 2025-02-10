using Blazor.SvgDiagram.Extension;
using Blazor.SvgDiagram.Interfaces;
using Microsoft.JSInterop;

namespace Blazor.SvgDiagram.Data
{
    public class Line : ILine
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly double _x1, _y1, _x2, _y2;
        private readonly string _color;

        public Line(IJSRuntime jsRuntime, double x1, double y1, double x2, double y2, string color)
        {
            _jsRuntime = jsRuntime;
            _x1 = x1;
            _y1 = y1;
            _x2 = x2;
            _y2 = y2;
            _color = color;
        }

        public async Task Add(IJSObjectReference? _svgModul)
        {
            await _jsRuntime.InvokeSvgMethod(_svgModul, "addLine", _x1, _y1, _x2, _y2, _color);
        }
    }
}
