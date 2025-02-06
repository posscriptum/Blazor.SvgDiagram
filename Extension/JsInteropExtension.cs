using Microsoft.JSInterop;

namespace Blazor.SvgDiagram.Extension
{
    public static class JsInteropExtension
    {
        public static async Task InvokeSvgMethod(this IJSRuntime jsRuntime, IJSObjectReference? svgModule, string methodName, params object[] args)
        {
            if (svgModule != null)
            {
                try
                {
                    await svgModule.InvokeVoidAsync(methodName, args);
                }
                catch (JSException ex)
                {
                    Console.Error.WriteLine($"Error calling JavaScript function '{methodName}': {ex.Message}");
                    throw; // logging if need
                }
            }
            else
            {
                Console.WriteLine("Svg module not loaded yet.");
            }
        }
    }
}
