using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;

namespace MatLabCompiler
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }

        private void SaveFile(string text, string filter)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = filter;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                FileManager.Instance.Save(text, dialog.FileName);
        }

        private void closeFileMenutItem_Click(object sender, RoutedEventArgs e)
        {
            string message = "Deseja Salvar ?";
            if (!string.IsNullOrEmpty(this.TextEditor.Text))
            {
                if (MessageBox.Show(message, "Salvar", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    this.SaveFile(this.TextEditor.Text, "MatLab Files|*.m");
                this.TextEditor.Text = string.Empty;
            }
        }

        private void openFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "MatLab Files|*.m";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.TextEditor.Text = FileManager.Instance.Load(dialog.FileName);
        }

        private void saveFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.SaveFile(this.TextEditor.Text, "MatLab Files|*.m");
        }

        private void saveAnalysisMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = this.DataContext as MainWindowViewModel;
            StringBuilder text = new StringBuilder();
            text.AppendLine("----------Tokens----------");
            foreach (var token in viewModel.LexicAnaliyzer.Tokens)
                text.AppendLine(token.ToString());

            text.AppendLine();
            text.AppendLine("----------Errors----------");
            foreach (var error in viewModel.LexicAnaliyzer.Errors)
                text.AppendLine(error.ToString());

            this.SaveFile(text.ToString(), "Analysis file|*.lex");
        }
    }
}