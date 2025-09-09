using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace MdiAvalonia.Markup;

/// <summary>
/// A markup extension that provides icon sources for Avalonia applications.
/// This extension allows XAML to easily reference and create drawing images for Material Design Icons.
/// </summary>
public class IconSourceExtension : MarkupExtension
{
    /// <summary>
    /// Gets or sets the brush used to color the icon.
    /// Defaults to black if not specified.
    /// </summary>
    public IBrush Brush { get; set; } = Brushes.Black;

    /// <summary>
    /// Gets or sets the specific icon to display from the Material Design Icons collection.
    /// This property determines which icon shape will be rendered.
    /// </summary>
    public Icon Icon { get; set; }

    /// <summary>
    /// Provides the actual drawing image value when the markup extension is processed by the XAML parser.
    /// Creates and returns a drawing image using the specified icon and brush.
    /// </summary>
    /// <param name="serviceProvider">The service provider from the XAML context (not used in this implementation)</param>
    /// <returns>A DrawingImage object representing the specified icon with the applied brush</returns>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        // Create a factory instance to generate the icon drawing image
        var factory = new IconsFactory();
        return factory.CreateDrawingImage(Icon, Brush);
    }
}