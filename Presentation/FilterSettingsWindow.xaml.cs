using System.Windows;

namespace INFOIBV.Presentation
{
    /// <summary>
    /// Interaction logic for FilterSettingsWindow.xaml
    /// </summary>
    public partial class FilterSettingsWindow : Window
    {
        public FilterSettingsWindow()
        {
            InitializeComponent();
        }

        private void Prevent_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void CheckIfInputIsNumeric(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsNumeric(e.Text);
        }

        private void TextBox_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(string)))
            {
                string text = (string)e.DataObject.GetData(typeof(string));
                if (!IsNumeric(text)) 
                    e.CancelCommand();
            }
            else 
                e.CancelCommand(); 
        }

        private bool IsNumeric(string input)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^\d+$");
        }
    }
}
