using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace INFOIBV.Presentation
{
    /// <summary>
    /// Interaction logic for FilterSelectorWindow.xaml
    /// </summary>
    public partial class FilterSelectorWindow : Window
    {
        public FilterSelectorWindow()
        {
            InitializeComponent();
        }

        private void Prevent_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
