﻿using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace O7.EasyImage;

/// <summary>
/// Image extensions class for .Net. Here you have all the resize and crop functions for Image class, Have fun!.
/// </summary>
public static class ImageExtensions
{
    /// <summary>
    /// Crop an image. The new width and height of image will be: Width = image.Width - (x1 + x2), Height = image.Height - (y1 + y2).
    /// <code>
    /// <para>How to use Crop function:</para>
    /// <para>var image = Image.FromFile("C:\\images\\bar.jpg");</para>
    /// <para>image.Crop(100, 90, 30, 20);</para>
    /// </code>
    /// </summary>
    /// <param name="image">The image to be cropped.</param>
    /// <param name="x1">The left coordinate x in pixels (width spacing).</param>
    /// <param name="x2">The right coordinate x in pixels (width spacing).</param>
    /// <param name="y1">The left coordinate y in pixels (height spacing).</param>
    /// <param name="y2">The right coordinate y in pixels (height spacing).</param>
    /// <returns>The new cropped image.</returns>
    public static Bitmap Crop(this Image image, int x1, int x2, int y1, int y2)
    {
        var width = image.Width - (x1 + x2);
        var height = image.Height - (y1 + y2);
        Bitmap bitmap = new(width, height);
        var horizontalResolution = image.HorizontalResolution;
        var verticalResolution = image.VerticalResolution;
        bitmap.SetResolution(horizontalResolution, verticalResolution);
        
        using var graphics = Graphics.FromImage(bitmap);
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.CompositingMode = CompositingMode.SourceCopy;
        graphics.CompositingQuality = CompositingQuality.HighQuality;
        
        Rectangle rectangle = new(0, 0, width, height);
        graphics.DrawImage(image, rectangle, x1, y1, width, height, GraphicsUnit.Pixel);
        
        return bitmap;
    }

    /// <summary>
    /// Crop an image. The new width and height of image will be: Width = image.Width - x * 2, Height = image.Height - y * 2.
    /// <code>
    /// <para>How to use Crop function:</para>
    /// <para>var image = Image.FromFile("C:\\images\\bar.jpg");</para>
    /// <para>image.Crop(100, 30);</para>
    /// </code>
    /// </summary>
    /// <param name="image">The image to be cropped.</param>
    /// <param name="x">The coordinate x in pixels (width spacing).</param>
    /// <param name="y">The coordinate y in pixels (height spacing).</param>
    /// <returns>The new cropped image.</returns>
    public static Bitmap Crop(this Image image, int x, int y)
    {
        return image.Crop(x, x, y, y);
    }

    /// <summary>
    /// Resize the image based on height. Keeps width proportional.
    /// <code>
    /// <para>How to use HeightResize function:</para>
    /// <para>var image = Image.FromFile("C:\\images\\bar.jpg");</para>
    /// <para>image.HeightResize(500);</para>
    /// </code>
    /// </summary>
    /// <param name="image">The image to be resized.</param>
    /// <param name="height">The new height of image.</param>
    /// <returns>The new resized image.</returns>
    public static Bitmap HeightResize(this Image image, int height)
    {

        var width = (height * image.Width) / image.Height;
        return image.Resize(width, height);
    }

    /// <summary>
    /// Resize the image.
    /// <code>
    /// <para>How to use Resize function:</para>
    /// <para>var image = Image.FromFile("C:\\images\\foo-bar.jpg");</para>
    /// <para>image.Resize(400, 500);</para>
    /// </code>
    /// </summary>
    /// <param name="image">The image to be resized.</param>
    /// <param name="width">The new width of image.</param>
    /// <param name="height">The new height of image.</param>
    /// <returns>The new resized image.</returns>
    public static Bitmap Resize(this Image image, int width, int height)
    {
        Bitmap bitmap = new(width, height);
        var horizontalResolution = image.HorizontalResolution;
        var verticalResolution = image.VerticalResolution;
        bitmap.SetResolution(horizontalResolution, verticalResolution);
        
        using var graphics = Graphics.FromImage(bitmap);
        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.SmoothingMode = SmoothingMode.HighQuality;
        graphics.CompositingMode = CompositingMode.SourceCopy;
        graphics.CompositingQuality = CompositingQuality.HighQuality;

        using ImageAttributes imageAttributes = new();
        var widthStart = 0;
        var heightStart = 0;
        imageAttributes.SetWrapMode(WrapMode.TileFlipXY);
        
        Rectangle rectangle = new(widthStart, heightStart, width, height);
        graphics.DrawImage(image, rectangle, widthStart, heightStart, image.Width, image.Height, GraphicsUnit.Pixel, imageAttributes);

        return bitmap;
    }

    /// <summary>
    /// Resize the image based on width. Keeps height proportional.
    /// <code>
    /// <para>How to use WidthResize function:</para>
    /// <para>var image = Image.FromFile("C:\\images\\foo.jpg");</para>
    /// <para>image.WidthResize(400);</para>
    /// </code>
    /// </summary>
    /// <param name="image">The image to be resized.</param>
    /// <param name="width">The new width of image.</param>
    /// <returns>The new resized image.</returns>
    public static Bitmap WidthResize(this Image image, int width)
    {
        var height = (width * image.Height) / image.Width;
        return image.Resize(width, height);
    }
}
