using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using MatLabCompiler.Lexic;
using MatLabCompiler.Syntatic;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Xml;

namespace MatLabCompiler
{
    public class MainWindowViewModel : DependencyObject
    {
        #region Constructors

        public MainWindowViewModel()
        {
            _lexicAnaliyzer = new LexicAnalyzer();
            _parser = new Parser();
        }

        #endregion

        #region Attributes and Properties

        private LexicAnalyzer _lexicAnaliyzer;

        public LexicAnalyzer LexicAnaliyzer
        {
            get { return _lexicAnaliyzer; }
            set { _lexicAnaliyzer = value; }
        }

        private Parser _parser;

        public Parser Parser
        {
            get { return _parser; }
            set { _parser = value; }
        }

        public string GeneratedCode
        {
            get { return (string)GetValue(GeneratedCodeProperty); }
            set { SetValue(GeneratedCodeProperty, value); }
        }

        public static readonly DependencyProperty GeneratedCodeProperty =
                DependencyProperty.Register("GeneratedCode", typeof(string), typeof(MainWindowViewModel));


        #endregion

        #region Commands

        private RelayCommand _textChangedCommand;
        public RelayCommand TextChangedCommand
        {
            get
            {
                if (_textChangedCommand == null)
                    this.CreateTextChangedCommand();

                return _textChangedCommand;
            }
            set { _textChangedCommand = value; }
        }

        private RelayCommand _onLoadCommand;
        public RelayCommand OnLoadCommand
        {
            get
            {
                if (_onLoadCommand == null)
                    this.CreateOnLoadCommand();

                return _onLoadCommand;
            }
            set { _onLoadCommand = value; }
        }

        #endregion

        #region Private Methods

        private void CreateOnLoadCommand()
        {
            _onLoadCommand = new RelayCommand
            (
                param =>
                {
                    this.LoadDefaultHighlightingDefinition(param as TextEditor);
                }
            );
        }

        private void CreateTextChangedCommand()
        {
            _textChangedCommand = new RelayCommand
            (
                param =>
                {
                    var textEditor = param as TextEditor;
                    this.Analyze(textEditor.Text);
                }
            );
        }

        private void LoadDefaultHighlightingDefinition(TextEditor textEditor)
        {
            IHighlightingDefinition cSharpHighlighting;
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("MatLabCompiler.SyntaxDefinitions.MatLab.xshd"))
            {
                using (XmlReader reader = new XmlTextReader(s))
                    cSharpHighlighting = HighlightingLoader.Load(reader, HighlightingManager.Instance);
            }

            HighlightingManager.Instance.RegisterHighlighting("MatLab Highlighting", new string[] { ".m" }, cSharpHighlighting);
            textEditor.SyntaxHighlighting = cSharpHighlighting;
        }

        private void Analyze(string text)
        {
            string generatedCod = string.Empty;
            string errors = string.Empty;
            _lexicAnaliyzer.Analyze(text);
            _parser.Analyze(_lexicAnaliyzer.Tokens, ref generatedCod, ref errors);

            this.GeneratedCode = generatedCod;
        }

        #endregion
    }
}