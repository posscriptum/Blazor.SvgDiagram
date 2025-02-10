using Blazor.SvgDiagram.Extension;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Blazor.SvgDiagram.Interfaces;

namespace Blazor.SvgDiagram.Components
{
    public partial class SvgDiagram: ISvgDiagram
    {
        [Parameter] public int Width { get; set; } = 800;
        [Parameter] public int Height { get; set; } = 600;
        private IJSObjectReference? _svgModule;
        private ElementInfo? _selectedElement;
        [Inject]
        private IJSRuntime? JSRuntime {get; set;}

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _svgModule = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", "./js/svgInterop.js");
                await InitializeSvg();
                DiagramHelper.Initialize(this);
            }
        }
        private async Task InitializeSvg()
        {
            if (_svgModule != null)
            {
                await _svgModule.InvokeVoidAsync("createSvg", "svg-container", Width, Height);
                await _svgModule.InvokeVoidAsync("drawGrid", Width, Height, 20);
            }
        }

        private async Task AddRectangle() => await JSRuntime!.InvokeSvgMethod(_svgModule, "addRectangle", 50, 50, 100, 80, "red");

        private async Task AddCircle() => await JSRuntime!.InvokeSvgMethod(_svgModule, "addCircle", 150, 150, 50, "blue");

        private async Task AddLine() => await JSRuntime!.InvokeSvgMethod(_svgModule, "addLine", 20, 20, 200, 100, "green");

        private async Task AddImage() => await JSRuntime!.InvokeSvgMethod(_svgModule, "addImage", "https://opencart.club/uploads/monthly_2022_04/12e706e67.png.e826c2e86cba6107b041e6e0abe32780.png", 250, 250, 100, 100);

        public void SelectElementInternal(ElementInfo elementInfo)
        {
            _selectedElement = elementInfo;
            StateHasChanged();
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
