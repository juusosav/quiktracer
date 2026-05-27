using QuikTracer.Vsix.ViewModels;
using System.Windows;
using System.Windows.Controls;
using QuikTracer.Vsix.Models;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System.IO;
using System.Threading.Tasks;

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
                IsError = true,
                FilePath = @"C:\Users\jurox\source\repos\QuikTracer\QuikTracer.SandboxApp\Components\Pages\Weather.razor",
                LineNumber = 42,
                Details = "Object reference not set to an instance of an object."
            });

            DataContext = ViewModel;
        }

        /// <summary>
        /// Handles click on the button by displaying a message box.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event args.</param>
#pragma warning disable VSTHRD100 // Avoid async void methods
        private async void TraceNode_Click(object sender, RoutedEventArgs e)
#pragma warning restore VSTHRD100 // Avoid async void methods
        {
            if (sender is Button button &&
                button.DataContext is TraceNode node)
            {
                SelectNode(node);

                await OpenSourceAsync(node);
            }
        }

        private void SelectNode(TraceNode node)
        {
            ViewModel.SelectedNode = node;
            DataContext = null;
            DataContext = ViewModel;
        }

        private async Task OpenSourceAsync(TraceNode node)
        {
            if (!string.IsNullOrWhiteSpace(node.Name) &&
                File.Exists(node.FilePath))
            {
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                var dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2;

                if (dte != null)
                {
                    EnvDTE.Window window = dte.ItemOperations.OpenFile(node.FilePath);
                    window.Visible = true;
                    if (node.LineNumber.HasValue)
                    {
                        TextSelection selection = (TextSelection)dte.ActiveDocument.Selection;
                        selection.GotoLine(node.LineNumber.Value, true);
                    }
                }
            }
            else
            {
                MessageBox.Show(
                    "No valid source file available for this node.",
                    "QuickTrace",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }
}