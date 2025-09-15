# MdiAvalonia

MdiAvalonia is an avalonia vector icon library using MaterialDesign open source icons.

The icons are embedded in the library in svg format and their data are parsed with the `IconsFactory` class.

The markup extensions `IconSource` and `IconGeometry` make the usage in XAML as easy as possible.

[Icons license](https://github.com/Templarian/MaterialDesign/blob/master/LICENSE)

[Material Design Icons GitHub repo](https://github.com/Templarian/MaterialDesign)

[Pictogrammers website](https://pictogrammers.com/library/mdi/)

## Examples

Import namespace in XAML :

```xaml
xmlns:mdi="using:MdiAvalonia.Markup"
```

Example of `IconSource` markup extension with an image :

```xaml
<Image Source="{mdi:IconSource Icon=switch, Brush=AliceBlue}" />
```

Example of `IconGeometry` markup extension with a `PathIcon` control :

```xaml
<PathIcon Data="{mdi:IconGeometry Icon=decimal}" Foreground="Red" />
```

Copyright (c) 2025 Josué Clément