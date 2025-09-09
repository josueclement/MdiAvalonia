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
        var source = factory.CreateDrawingImage(MdiAvalonia.Icon.@null, Brushes.White);
        var test = MdiAvalonia.Icon.@null.ToString();
        // MyImage.Source = source;
    }
}