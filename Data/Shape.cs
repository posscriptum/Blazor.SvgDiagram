using Microsoft.JSInterop;

namespace Blazor.SvgDiagram.Data
{
    public abstract class Shape
    {
        protected readonly IJSRuntime _jsRuntime;

        protected Shape(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public abstract Task AddAsync(); 
    }
}
