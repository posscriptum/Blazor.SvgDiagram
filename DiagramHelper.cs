using Microsoft.JSInterop;
using Blazor.SvgDiagram.Interfaces;

namespace Blazor.SvgDiagram
{
    public static class DiagramHelper
    {
        private static ISvgDiagram? _indexComponent;
        public static void Initialize(ISvgDiagram indexComponent)
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
