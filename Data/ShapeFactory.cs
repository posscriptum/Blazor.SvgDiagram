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
                    return new Rectangle(jsRuntime, double.Parse(parameters[0].ToString()!), double.Parse(parameters[1].ToString()!), double.Parse(parameters[2].ToString()!), double.Parse(parameters[3].ToString()!), parameters[4].ToString()!);

                case "circle":
                    if (parameters.Length != 4)
                        throw new ArgumentException("Неверное количество параметров для круга");
                    return new Circle(jsRuntime, double.Parse(parameters[0].ToString()!), double.Parse(parameters[1].ToString()!), double.Parse(parameters[2].ToString()!), parameters[3].ToString()!);

                case "line":
                    if (parameters.Length != 5)
                        throw new ArgumentException("Неверное количество параметров для линии");
                    return new Line(jsRuntime, double.Parse(parameters[0].ToString()!), double.Parse(parameters[1].ToString()!), double.Parse(parameters[2].ToString()!), double.Parse(parameters[3].ToString()!), parameters[4].ToString()!);
                case "image":
                    if (parameters.Length != 5)
                        throw new ArgumentException("Неверное количество параметров для изображения");
                    return new ImageShape(jsRuntime, parameters[0].ToString()!, double.Parse(parameters[1].ToString()!), double.Parse(parameters[2].ToString()!), double.Parse(parameters[3].ToString()!), double.Parse(parameters[4].ToString()!));
                default:
                    throw new ArgumentException($"Неизвестный тип фигуры: {type}");
            }
        }
    }
}
