using Blazor.SvgDiagram.Data;
using Microsoft.JSInterop;

namespace Blazor.SvgDiagram
{
    public static class DiagramHelper
    {
        private static Pages.Index? _indexComponent;
        public static void Initialize(Pages.Index indexComponent)
        {
            _indexComponent = indexComponent;
        }

        [JSInvokable]
        public static void SelectElement(ElementInfo elementInfo)
        {
            if (_indexComponent != null)
            {
                _indexComponent!.SelectElementInternal(elementInfo);
            }
            else
            {
                throw new Exception("Ошибка: Ссылка на компонент Index не установлена.");
            }
        }
    }
}
