namespace Blazor.SvgDiagram.Data
{
    using Blazor.SvgDiagram.Interfaces;
    using Microsoft.JSInterop;

    public class ShapeFactory : IShapeFactory
    {
        public IShape CreateShape(string type, IJSRuntime jsRuntime, params object[] parameters)
        {
            switch (type.ToLower())
            {
                case "rectangle":
                    if (parameters.Length != 5)
                        throw new ArgumentException("Неверное количество параметров для прямоугольника");
                    return new Rectangle(jsRuntime, (double)parameters[0], (double)parameters[1], (double)parameters[2], (double)parameters[3], (string)parameters[4]);

                case "circle":
                    if (parameters.Length != 4)
                        throw new ArgumentException("Неверное количество параметров для круга");
                    return new Circle(jsRuntime, (double)parameters[0], (double)parameters[1], (double)parameters[2], (string)parameters[3]);

                case "line":
                    if (parameters.Length != 5)
                        throw new ArgumentException("Неверное количество параметров для линии");
                    return new Line(jsRuntime, (double)parameters[0], (double)parameters[1], (double)parameters[2], (double)parameters[3], (string)parameters[4]);
                case "image":
                    if (parameters.Length != 5)
                        throw new ArgumentException("Неверное количество параметров для изображения");
                    return new ImageShape(jsRuntime, (string)parameters[0], (double)parameters[1], (double)parameters[2], (double)parameters[3], (double)parameters[4]);
                default:
                    throw new ArgumentException($"Неизвестный тип фигуры: {type}");
            }
        }
    }
}
