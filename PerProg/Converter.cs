using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PerProg
{
    class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int[,] tabla = (int[,])value;
            GeometryGroup gg = new GeometryGroup();
            for (int i = 0; i < tabla.GetLength(0); i++)
            {
                for (int j = 0; j < tabla.GetLength(1); j++)
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
}
