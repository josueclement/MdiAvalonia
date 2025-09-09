using Avalonia.Markup.Xaml;

namespace MdiAvalonia.Markup;

/// <summary>
/// A markup extension that provides geometry data for Material Design Icons (MDI) in Avalonia applications.
/// This extension allows you to use MDI icons directly in XAML by specifying the icon name.
/// </summary>
public class IconGeometryExtension : MarkupExtension
{
    /// <summary>
    /// Gets or sets the name of the Material Design Icon to be rendered.
    /// This property specifies which icon geometry should be created.
    /// </summary>
    /// <value>The icon from the Icon enumeration.</value>
    public Icon Icon { get; set; }

    /// <summary>
    /// Provides the geometry data for the specified Material Design Icon.
    /// This method is called by the XAML parser to resolve the markup extension.
    /// </summary>
    /// <param name="serviceProvider">A service provider that can provide services for the markup extension.</param>
    /// <returns>A geometry object representing the specified Material Design Icon that can be used in Path elements.</returns>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        // Create a factory instance to generate the icon geometry
        var factory = new IconsFactory();
        // Return the geometry data for the requested icon
        return factory.CreateGeometry(Icon);
    }
}