using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatLabCompiler.LexicAnalyser
{
    public class LexicAnalyser2
    {
        #region Constructors

        public LexicAnalyser2()
        {
            this.Initialize();
        }

        #endregion

        #region Attributes and Properties

        private State _state0;

        private List<string> _reservedWords;
        public List<string> ReservedWords
        {
            get
            {
                return _reservedWords ?? (_reservedWords = new List<string>()
                    {
                        "while",
                        "if",
                        "switch",
                        "for",
                        "else",
                        "elseif",
                    });
            }
        }
        public ObservableCollection<Token> Tokens { get; set; }
        public ObservableCollection<LexicError> Errors { get; set; }

        #endregion

        #region Private Mehtods

        private void Initialize()
        {
            this.Tokens = new ObservableCollection<Token>();
            this.Errors = new ObservableCollection<LexicError>();
            _state0 = new State(false);
            var state1 = new State();
            var state2 = new State();
        }

	    #endregion

        #region Public Methods

        public void Analyze(string text)
        {

        }

        #endregion
    }
}
