using Avalonia.Controls;
using Avalonia.Media;
using MdiAvalonia;

namespace TesterApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        
        var factory = new IconsFactory();
        var source = factory.CreateDrawingImage(IconNames.@null, Brushes.White);
        var test = IconNames.@null.ToString();
        // MyImage.Source = source;
    }
}