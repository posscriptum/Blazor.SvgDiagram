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
            StateHasChanged();
        }
        private async Task InitializeSvg()
        {
            if (_svgModule != null)
            {
                await _svgModule.InvokeVoidAsync("createSvg", "svg-container", Width, Height);
                await _svgModule.InvokeVoidAsync("drawGrid", Width, Height, 20);
            }
        }

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
