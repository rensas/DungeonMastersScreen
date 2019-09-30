using DungeonMastersScreen.Model.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace DungeonMastersScreen.Converters
{
    public class DispositionTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DispositionType dType = (DispositionType)value;
            SolidColorBrush color = new SolidColorBrush(Colors.White);
            switch (dType)
            {
                case DispositionType.Friendly:
                    {
                        color = new SolidColorBrush(Colors.LightGreen);
                        break;
                    }
                case DispositionType.Neutral:
                    {
                        color = new SolidColorBrush(Colors.LightGray);
                        break;
                    }
                case DispositionType.Hostile:
                    {
                        color = new SolidColorBrush(Colors.Red);
                        break;
                    }
            }
            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
