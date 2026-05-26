using QuikTracer.Vsix.ViewModels;
using System.Windows;
using System.Windows.Controls;
using QuikTracer.Vsix.Models;

namespace QuikTracer.Vsix
{
    /// <summary>
    /// Interaction logic for QuickTracerWindowControl.
    /// </summary>
    public partial class QuickTracerWindowControl : UserControl
    {

        public TraceViewModel ViewModel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="QuickTracerWindowControl"/> class.
        /// </summary>
        public QuickTracerWindowControl()
        {
            this.InitializeComponent();

            ViewModel = new TraceViewModel();

            ViewModel.TraceNodes.Add(new TraceNode
            {
                Name = "Dashboard.razor",
            });
            ViewModel.TraceNodes.Add(new TraceNode
            {
                Name = "WeatherService",
            });
            ViewModel.TraceNodes.Add(new TraceNode
            {
                Name = "NullReferenceException",
                IsError = true
            });

            DataContext = ViewModel;
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                string.Format(System.Globalization.CultureInfo.CurrentUICulture, "Invoked '{0}'", this.ToString()),
                "QuikTracerWindow");
        }
    }
}