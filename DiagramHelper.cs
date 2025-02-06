﻿using Microsoft.JSInterop;

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
        public static void SelectElement(string type, double x, double y, double width = 0, double height = 0, double r = 0, double x1 = 0, double y1 = 0, double x2 = 0, double y2 = 0)
        {
            if (_indexComponent != null)
            {
                _indexComponent!.SelectElementInternal(type, x, y, width, height, r, x1, y1, x2, y2);
            }
            else
            {
                throw new Exception("Ошибка: Ссылка на компонент Index не установлена.");
            }
        }
    }
}
