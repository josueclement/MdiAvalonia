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
    /// Converts an Icon enum value to the corresponding icon name string format.
    /// </summary>
    /// <param name="icon">The icon enum value to convert</param>
    /// <returns>The formatted icon name string</returns>
    private static string GetIconName(Icon icon)
        => $"{icon}".Replace("_", "-");

    /// <summary>
    /// Generates the full resource stream name for an embedded SVG icon file.
    /// </summary>
    /// <param name="icon">The icon enum value</param>
    /// <returns>The complete resource stream path for the SVG file</returns>
    private static string GetIconStreamName(Icon icon)
        => $"MdiAvalonia.svg.{GetIconName(icon)}.svg";

    /// <summary>
    /// Retrieves the embedded resource stream for the specified SVG icon file.
    /// </summary>
    /// <param name="icon">The icon enum value to get the stream for</param>
    /// <returns>A Stream containing the SVG icon data, or null if the icon resource is not found</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public Stream? GetIconStream(Icon icon)
        => Assembly.GetManifestResourceStream(GetIconStreamName(icon));

    /// <summary>
    /// Extracts the SVG path data string from an embedded SVG icon resource.
    /// This method loads the SVG file, parses its XML content, and retrieves the 'd' attribute
    /// from the path element which contains the vector path data used for rendering the icon.
    /// </summary>
    /// <param name="icon">The icon enum value to extract path data for</param>
    /// <returns>The SVG path data string that defines the icon's vector shape</returns>
    /// <exception cref="InvalidOperationException">Thrown when the icon resource is not found or the SVG structure is invalid</exception>
    // ReSharper disable once MemberCanBePrivate.Global
    public string GetIconData(Icon icon)
    {
        // Load the SVG file from embedded resources
        using var stream = GetIconStream(icon)
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
            return val;

        throw new InvalidOperationException($"Cannot read icon '{GetIconName(icon)}'");
        
    }

    /// <summary>
    /// Creates an Avalonia Geometry object from an SVG icon by parsing the SVG path data.
    /// </summary>
    /// <param name="icon">The icon to create geometry for</param>
    /// <returns>A Geometry object representing the icon's vector path</returns>
    /// <exception cref="InvalidOperationException">Thrown when the icon is not found or cannot be parsed</exception>
    public Geometry CreateGeometry(Icon icon)
    {
        // Get the icon data
        var data = GetIconData(icon);
        
        // Parse the icon data into a Geometry object
        return Geometry.Parse(data);
    }

    /// <summary>
    /// Creates a DrawingImage from an icon with the specified brush for styling.
    /// This is useful for displaying icons in Avalonia UI controls.
    /// </summary>
    /// <param name="icon">The icon to create the drawing image for</param>
    /// <param name="brush">The brush to use for filling the icon (color, gradient, etc.)</param>
    /// <returns>A DrawingImage that can be used in Avalonia Image controls</returns>
    public DrawingImage CreateDrawingImage(Icon icon, IBrush brush)
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