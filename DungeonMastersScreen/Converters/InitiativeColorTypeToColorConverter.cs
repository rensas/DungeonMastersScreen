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
    public class InitiativeColorTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            InitiativeColorType dType = (InitiativeColorType)value;
            SolidColorBrush color = new SolidColorBrush(Colors.White);
            switch (dType)
            {
                case InitiativeColorType.R:
                    {
                        color = new SolidColorBrush(Colors.Red);
                        break;
                    }
                case InitiativeColorType.G:
                    {
                        color = new SolidColorBrush(Colors.DarkSlateGray);
                        break;
                    }
                case InitiativeColorType.B:
                    {
                        color = new SolidColorBrush(Colors.Blue);
                        break;
                    }
                case InitiativeColorType.Y:
                    {
                        color = new SolidColorBrush(Colors.Yellow);
                        break;
                    }
                case InitiativeColorType.LG:
                    {
                        color = new SolidColorBrush(Colors.LightGreen);
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
