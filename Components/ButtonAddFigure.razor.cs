using Blazor.SvgDiagram.Extension;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Primitives;
using Microsoft.JSInterop;
using System.Collections;

namespace Blazor.SvgDiagram.Components
{
    public partial class ButtonAddFigure
    {
        [Parameter] public IJSObjectReference? SvgModule { private get; set; }
        [Parameter] public string FigureName { get; set; } = string.Empty;
        [Parameter] public object[] Parameters { get; set; } = default!;
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }
        private string[]? _functionNamesInJsModule;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _functionNamesInJsModule = await SvgModule!.InvokeAsync<string[]>("getExportedFunctionNames");
            }
        }

        private async Task Add()
        {
            string? methodName = _functionNamesInJsModule!.Where(p => p.Contains(FigureName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            try
            {
                await JSRuntime!.InvokeSvgMethod(SvgModule, methodName!, Parameters);
            }
            catch (Exception ex)
            {
                throw new Exception(methodName == null ? $"The JS module does not implement a method for the shape {FigureName}." : $"{ex}");
            }
        }
    }
}
