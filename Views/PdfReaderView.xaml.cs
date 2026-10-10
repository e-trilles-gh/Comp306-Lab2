using _301434046_eskim__Lab2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _301434046_eskim__Lab2.Views
{
    /// <summary>
    /// Interaction logic for PdfReaderView.xaml
    /// </summary>
    public partial class PdfReaderView : UserControl
    {
        public PdfReaderView()
        {
            InitializeComponent();
        }

        private void pdfViewer_CurrentPageChanged(object sender, EventArgs args)
        {
            if (DataContext is PdfReaderViewModel viewModel)
            {
                viewModel.UpdateLastReadPage(pdfViewer.CurrentPage);
            }
        }

        private void pdfViewer_DocumentLoaded(object sender, EventArgs args)
        {
            if (DataContext is PdfReaderViewModel viewModel)
            {
                if (viewModel.PageToRestore > 1)
                {
                    pdfViewer.GotoPage(viewModel.PageToRestore);
                }
            }
        }

    }
}
