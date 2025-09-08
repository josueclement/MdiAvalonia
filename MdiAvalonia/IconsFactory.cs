using System.Reflection;
using System.Xml;
using Avalonia.Media;

namespace MdiAvalonia;

/// <summary>
/// Factory class for creating Material Design Icons (MDI) as Avalonia geometries and drawing images.
/// This class loads SVG icons from embedded resources and converts them to usable Avalonia graphics objects.
/// </summary>
public class IconsFactory
{
    /// <summary>
    /// Assembly reference used to access embedded SVG icon resources
    /// </summary>
    private static readonly Assembly Assembly = typeof(IMdiAvaloniaMarker).Assembly;

    /// <summary>
    /// Converts an IconNames enum value to the corresponding icon name string format.
    /// Removes the "icon_" prefix and replaces underscores with hyphens.
    /// </summary>
    /// <param name="icon">The icon enum value to convert</param>
    /// <returns>The formatted icon name string</returns>
    private static string GetIconName(IconNames icon)
        => $"{icon}".Replace("_", "-")[5..]; // Remove "icon_" prefix and replace underscores

    /// <summary>
    /// Generates the full resource stream name for an embedded SVG icon file.
    /// </summary>
    /// <param name="icon">The icon enum value</param>
    /// <returns>The complete resource stream path for the SVG file</returns>
    private static string GetIconStreamName(IconNames icon)
    {
        var iconName = GetIconName(icon);
        return $"MdiAvalonia.svg.{iconName}.svg";
    }

    /// <summary>
    /// Creates an Avalonia Geometry object from an SVG icon by parsing the SVG path data.
    /// </summary>
    /// <param name="icon">The icon to create geometry for</param>
    /// <returns>A Geometry object representing the icon's vector path</returns>
    /// <exception cref="InvalidOperationException">Thrown when the icon is not found or cannot be parsed</exception>
    public Geometry CreateGeometry(IconNames icon)
    {
        // Load the SVG file from embedded resources
        using var stream = Assembly.GetManifestResourceStream(GetIconStreamName(icon))
                           ?? throw new InvalidOperationException($"Icon '{icon}' not found");
        using var sr = new StreamReader(stream);
        var content = sr.ReadToEnd();

        // Parse the SVG XML content
        var xml = new XmlDocument();
        xml.LoadXml(content);

        // Set up namespace manager for SVG namespace
        var xnm = new XmlNamespaceManager(xml.NameTable);
        xnm.AddNamespace("std", "http://www.w3.org/2000/svg");

        // Extract the path element which contains the vector data
        var node = xml.SelectSingleNode("/std:svg/std:path", xnm);

        // Parse the 'd' attribute which contains the path data
        if (node?.Attributes?["d"]?.Value is { } val)
            return Geometry.Parse(val);

        throw new InvalidOperationException($"Cannot read icon '{GetIconName(icon)}'");
    }

    /// <summary>
    /// Creates a DrawingImage from an icon with the specified brush for styling.
    /// This is useful for displaying icons in Avalonia UI controls.
    /// </summary>
    /// <param name="icon">The icon to create the drawing image for</param>
    /// <param name="brush">The brush to use for filling the icon (color, gradient, etc.)</param>
    /// <returns>A DrawingImage that can be used in Avalonia Image controls</returns>
    public DrawingImage CreateDrawingImage(IconNames icon, IBrush brush)
    {
        // Get the vector geometry for the icon
        var geometry = CreateGeometry(icon);

        // Create a drawing image with the geometry and specified brush
        var drawingImage = new DrawingImage(new GeometryDrawing
        {
            Geometry = geometry,
            Brush = brush
        });
        
        return drawingImage;
    }
}