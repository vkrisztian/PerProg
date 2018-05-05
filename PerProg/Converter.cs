using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PerProg
{
    class ConverterBlack : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int[,] tabla = (int[,])value;
            GeometryGroup gg = new GeometryGroup();
            for (int i = 0; i < tabla.GetLength(0); i=i+2)
            {
                for (int j = 1; j < tabla.GetLength(1); j=j+2)
                {
                    
                    
                    gg.Children.Add(new RectangleGeometry(new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                }
            }
            for (int i = 1; i < tabla.GetLength(0); i = i + 2)
            {
                for (int j = 0; j < tabla.GetLength(1); j = j + 2)
                {


                    gg.Children.Add(new RectangleGeometry(new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                }
            }
            return gg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConverterWhite : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int[,] tabla = (int[,])value;
            GeometryGroup gg = new GeometryGroup();
            for (int i = 0; i < tabla.GetLength(0); i = i + 2)
            {
                for (int j = 0; j < tabla.GetLength(1); j = j + 2)
                {


                    gg.Children.Add(new RectangleGeometry(new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                }
            }
            for (int i = 1; i < tabla.GetLength(0); i = i + 2)
            {
                for (int j = 1; j < tabla.GetLength(1); j = j + 2)
                {


                    gg.Children.Add(new RectangleGeometry(new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                }
            }
            return gg;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    class ConvertPawns : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int[,] tabla = (int[,])value;
            DrawingGroup dg = new DrawingGroup();
            GeometryDrawing gd = new GeometryDrawing();
            GeometryDrawing gdfekete = new GeometryDrawing();
            GeometryGroup ggfekete = new GeometryGroup();
            GeometryGroup ggfeher = new GeometryGroup();
            for (int i = 0; i < tabla.GetLength(0); i = i + 2)
            {
                for (int j = 1; j < tabla.GetLength(1); j = j + 2)
                {


                    ggfekete.Children.Add(new RectangleGeometry(new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                }
            }
            for (int i = 1; i < tabla.GetLength(0); i = i + 2)
            {
                for (int j = 0; j < tabla.GetLength(1); j = j + 2)
                {


                    ggfekete.Children.Add(new RectangleGeometry(new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                }
            }
            for (int i = 0; i < tabla.GetLength(0); i = i + 2)
            {
                for (int j = 0; j < tabla.GetLength(1); j = j + 2)
                {


                    ggfeher.Children.Add(new RectangleGeometry(new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                }
            }
            for (int i = 1; i < tabla.GetLength(0); i = i + 2)
            {
                for (int j = 1; j < tabla.GetLength(1); j = j + 2)
                {


                    ggfeher.Children.Add(new RectangleGeometry(new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                }
            }
            gd.Geometry = ggfeher;
            gd.Pen = new Pen(Brushes.Black, 1);
            gd.Brush = Brushes.White;
            gdfekete.Geometry = ggfekete;
            gdfekete.Pen = new Pen(Brushes.Black, 1);
            gdfekete.Brush = Brushes.Gray;
            dg.Children.Add(gdfekete);
            dg.Children.Add(gd);
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
                {
                    switch (tabla[i,j])
                    {
                        case -1:
                            dg.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"..\..\Babuk_img\gyalog_b.png", UriKind.Relative)), new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                            break;
                        case 1:
                            dg.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"..\..\Babuk_img\gyalog_f.png", UriKind.Relative)), new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                            break;
                        case -2:
                            dg.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"..\..\Babuk_img\bastya_b.png", UriKind.Relative)), new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                            break;
                        case 2:
                            dg.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"..\..\Babuk_img\bastya_f.png", UriKind.Relative)), new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                            break;
                        case -3:
                            dg.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"..\..\Babuk_img\futo_b.png", UriKind.Relative)), new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                            break;
                        case 3:
                            dg.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"..\..\Babuk_img\futo_f.png", UriKind.Relative)), new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                            break;
                        case -4:
                            dg.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"..\..\Babuk_img\lo_b.png", UriKind.Relative)), new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                            break;
                        case 4:
                            dg.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"..\..\Babuk_img\lo_f.png", UriKind.Relative)), new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                            break;
                        case -5:
                            dg.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"..\..\Babuk_img\kiraly_b.png", UriKind.Relative)), new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                            break;
                        case 5:
                            dg.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"..\..\Babuk_img\kiraly_f.png", UriKind.Relative)), new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                            break;
                        case -6:
                            dg.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"..\..\Babuk_img\kiralyno_b.png", UriKind.Relative)), new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                            break;
                        case 6:
                            dg.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"..\..\Babuk_img\kiralyno_f.png", UriKind.Relative)), new Rect(j * ViewModel.csempeMeret, i * ViewModel.csempeMeret, ViewModel.csempeMeret, ViewModel.csempeMeret)));
                            break;
                        default:
                            break;
                    }
                }
            }
       
   
            return dg.Children;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
