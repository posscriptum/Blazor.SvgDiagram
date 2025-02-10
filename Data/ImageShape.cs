using Blazor.SvgDiagram.Extension;
using Blazor.SvgDiagram.Interfaces;
using Microsoft.JSInterop;
using System.Drawing;

namespace Blazor.SvgDiagram.Data
{
    public class ImageShape : IImageShape
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly string _url;
        private readonly double _x, _y, _width, _height;

        public ImageShape(IJSRuntime jsRuntime, string url, double x, double y, double width, double height)
        {
            _jsRuntime = jsRuntime;
            _url = url;
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public async Task Add(IJSObjectReference? _svgModul)
        {
            await _jsRuntime.InvokeSvgMethod(_svgModul, "addImage", _url, _x, _y, _width, _height);
        }
    }
}
