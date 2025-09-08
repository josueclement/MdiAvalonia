using System.Reflection;
using System.Xml;
using Avalonia.Media;

namespace MdiAvalonia;

public class IconsFactory
{
    private static readonly Assembly Assembly = typeof(IconsFactory).Assembly;

    private static string GetIconName(IconNames icon)
        => $"{icon}".Replace("_", "-")[5..];

    private static string GetIconStreamName(IconNames icon)
    {
        var iconName = GetIconName(icon);
        return $"MdiAvalonia.svg.{iconName}.svg";
    }

    public Geometry CreateIconGeometry(IconNames icon)
    {
        using var stream = Assembly.GetManifestResourceStream(GetIconStreamName(icon))
                           ?? throw new InvalidOperationException($"Icon '{icon}' not found");
        using var sr = new StreamReader(stream);
        var content = sr.ReadToEnd();

        var xml = new XmlDocument();
        xml.LoadXml(content);
        var xnm = new XmlNamespaceManager(xml.NameTable);
        xnm.AddNamespace("std", "http://www.w3.org/2000/svg");
        var node = xml.SelectSingleNode("/std:svg/std:path", xnm);

        if (node?.Attributes?["d"]?.Value is { } val)
            return Geometry.Parse(val);

        throw new InvalidOperationException($"Cannot read icon '{GetIconName(icon)}'");
    }

    public DrawingImage CreateDrawingImage(IconNames icon, IBrush brush)
    {
        var geometry = CreateIconGeometry(icon);

        var drawingImage = new DrawingImage(new GeometryDrawing
        {
            Geometry = geometry,
            Brush = brush
        });
        
        return drawingImage;
    }
    
}