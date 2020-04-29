using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfObservableCollectionVisibility
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty MenuItemsProperty =
            DependencyProperty.Register(
                "MenuItems", typeof(ObservableCollection<MyItem>),
                typeof(MainWindow),
                new FrameworkPropertyMetadata(default (ObservableCollection<MyItem>),FrameworkPropertyMetadataOptions.BindsTwoWayByDefault)
            );

        public MainWindow()
        {
            InitializeComponent();
            
            MenuItems = new ObservableCollection<MyItem>();
            MenuItems.Add(new MyItem("Entry 1"));
            MenuItems.Add(new MyItem("Entry 2"));
            DataContext = this;
        }

        public ObservableCollection<MyItem> MenuItems
        {
            get => (ObservableCollection<MyItem>) GetValue(MenuItemsProperty);
            set => SetValue(MenuItemsProperty, value);
        }
    }

    public class MyItem
    {
        public MyItem(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }

    public class EmptyListVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Trace.WriteLine($"Do conversion");
            if (value == null)
            {
                Trace.WriteLine($"Conversion value was null");
                return Visibility.Collapsed;
            }
            else
            {
                ICollection list = value as ICollection;
                if (list != null)
                {
                    if (list.Count == 0)
                    {
                        Trace.WriteLine($"Count is zero");
                        return Visibility.Hidden;
                    }
                    else
                    {
                        Trace.WriteLine($"Count is greater than zero");
                        return Visibility.Visible;
                    }
                }
                else
                {
                    Trace.WriteLine($"Was not a collection");
                    return Visibility.Visible;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)

        {
            throw new NotImplementedException();
        }
    }

    public class ListCountVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value == null)
            {
                return Visibility.Collapsed;
            }

            if (value is int num)
            {
                if (num > 0)
                {
                    return Visibility.Visible;
                }
                    
                
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)

        {
            throw new NotImplementedException();
        }
    }
}