using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MatLabCompiler.Lexic
{
    public class LexicAnalyzer
    {
        #region Constructors

        public LexicAnalyzer()
        {
            this.Tokens = new ObservableCollection<Token>();
            this.Errors = new ObservableCollection<LexicError>();
        }

        #endregion

        #region Attributes and Properties

        private List<string> _reservedWords;
        public List<string> ReservedWords
        {
            get
            {
                return _reservedWords ?? (_reservedWords = new List<string>()
                    {
                        "while",
                        "for",
                        "do",
                        "end",
                        "if",
                        "elseif",
                        "else",
                        "switch",
                        "case",
                        "otherwise",
                    });
            }
        }
        public ObservableCollection<Token> Tokens { get; set; }
        public ObservableCollection<LexicError> Errors { get; set; }

        #endregion

        #region Private Methods

        private Token CreateToken(string lexeme, int row, int column, eTokenType type)
        {
            Token token;
            column--;

            if (this.ReservedWords.Contains(lexeme))
                token = new Token(lexeme, row, column, eTokenType.ReservedWord);
            else
                token = new Token(lexeme, row, column, type);

            return token;
        }

        #endregion

        #region Public Methods

        public void Analyze(string text)
        {
            this.Tokens.Clear();
            this.Errors.Clear();
            if (string.IsNullOrEmpty(text)) return;

            StringBuilder lexeme = new StringBuilder();
            Token lastAddedToken = null;
            char[] textArray = text.ToArray();
            int currentPosition = 0;
            int currentRow = 1;
            int currentColumn = 0;
            int currentState = 0;
            char currentChar;

            Func<char, bool> IsNumber = (charToCompare) =>
            {
                return charToCompare >= '0' && charToCompare <= '9';
            };

            Action<eTokenType> AddToken = (tokenType) =>
            {
                var token = this.CreateToken(lexeme.ToString(), currentRow, currentColumn, tokenType);
                this.Tokens.Add(token);
                lastAddedToken = token;
            };

            Action<eTokenType> AddTokenAndRemoveLastAdded = (tokenType) =>
            {
                this.Tokens.Remove(lastAddedToken);
                AddToken(tokenType);
            };

            Action EndPartialAnalysis = () =>
            {
                lexeme.Clear();
                currentState = 0;
                currentPosition--;
                currentColumn--;
            };

            Action AddError = () =>
            {
                var error = new LexicError(lexeme.ToString(), currentRow, currentColumn);
                this.Errors.Add(error);
                lexeme.Clear();
                currentState = 0;
            };

            do
            {
                currentChar = textArray[currentPosition];
                currentPosition++;
                currentColumn++;

                if (currentChar == '\n')
                {
                    currentColumn = 0;
                    currentRow++;
                    continue;
                }

                if ((currentChar == ' ' && currentState != 20)|| currentChar == '\r' || currentChar == '\t')
                {
                    if (string.IsNullOrEmpty(lexeme.ToString())) continue;

                    lexeme.Clear();
                    currentState = 0;
                    continue;
                }

                lexeme.Append(currentChar);
                switch (currentState)
                {
                    case 0:
                        if (char.IsLetter(currentChar) || currentChar == '_')
                        {
                            currentState = 1;
                            AddToken(eTokenType.Identifier);
                            break;
                        }
                        if (IsNumber(currentChar))
                        {
                            currentState = 2;
                            AddToken(eTokenType.Constant);
                            break;
                        }
                        if (currentChar == '.')
                        {
                            currentState = 3;
                            break;
                        }
                        if (currentChar == '&')
                        {
                            currentState = 7;
                            AddToken(eTokenType.LogicOperator);
                            break;
                        }
                        if (currentChar == '|')
                        {
                            currentState = 9;
                            AddToken(eTokenType.LogicOperator);
                            break;
                        }
                        if (currentChar == '>' || currentChar == '<')
                        {
                            currentState = 11;
                            AddToken(eTokenType.RelationalOperator);
                            break;
                        }
                        if (currentChar == '~')
                        {
                            currentState = 12;
                            break;
                        }
                        if (currentChar == '+' || currentChar == '-' ||
                            currentChar == '*' || currentChar == '/' ||
                            currentChar == '^')
                        {
                            currentState = 14;
                            AddToken(eTokenType.ArithmeticOperator);
                            break;
                        }
                        if (currentChar == '(')
                        {
                            currentState = 15;
                            AddToken(eTokenType.OpenParenthesis);
                            break;
                        }
                        if (currentChar == ')')
                        {
                            currentState = 16;
                            AddToken(eTokenType.CloseParenthesis);
                            break;
                        }
                        if (currentChar == ',')
                        {
                            currentState = 17;
                            AddToken(eTokenType.Comma);
                            break;
                        }
                        if (currentChar == '=')
                        {
                            currentState = 18;
                            AddToken(eTokenType.Attribution);
                            break;
                        }
                        if (currentChar == ';')
                        {
                            currentState = 19;
                            AddToken(eTokenType.SemiColon);
                            break;
                        }
                        if (currentChar == '\'')
                        {
                            currentState = 20;
                            AddToken(eTokenType.Apostrophe);
                            break;
                        }
                        if (currentChar == ':')
                        {
                            currentState = 22;
                            AddToken(eTokenType.Colon);
                            break;
                        }
                        AddError();
                        break;
                    case 1:
                        if (char.IsLetter(currentChar) || currentChar == '_' || IsNumber(currentChar))
                        {
                            AddTokenAndRemoveLastAdded(eTokenType.Identifier);
                            break;
                        }
                        EndPartialAnalysis();
                        break;
                    case 2:
                        if (IsNumber(currentChar))
                        {
                            AddTokenAndRemoveLastAdded(eTokenType.Constant);
                            break;
                        }
                        if (currentChar == '.')
                        {
                            currentState = 3;
                            break;
                        }
                        if (currentChar == 'E' || currentChar == 'e')
                        {
                            currentState = 5;
                            break;
                        }
                        EndPartialAnalysis();
                        break;
                    case 3:
                        if (IsNumber(currentChar))
                        {
                            currentState = 4;
                            AddTokenAndRemoveLastAdded(eTokenType.Constant);
                            break;
                        }
                        AddError();
                        break;
                    case 4:
                        if (IsNumber(currentChar))
                        {
                            AddTokenAndRemoveLastAdded(eTokenType.Constant);
                            break;
                        }
                        if (currentChar == 'E' || currentChar == 'e')
                        {
                            currentState = 5;
                            break;
                        }
                        EndPartialAnalysis();
                        break;
                    case 5:
                        if (IsNumber(currentChar))
                        {
                            currentState = 6;
                            AddTokenAndRemoveLastAdded(eTokenType.Constant);
                            break;
                        }
                        AddError();
                        break;
                    case 6:
                        if (IsNumber(currentChar))
                        {
                            AddTokenAndRemoveLastAdded(eTokenType.Constant);
                            break;
                        }
                        EndPartialAnalysis();
                        break;
                    case 7:
                        if (currentChar == '&')
                        {
                            currentState = 8;
                            AddTokenAndRemoveLastAdded(eTokenType.LogicOperator);
                            break;
                        }
                        EndPartialAnalysis();
                        break;
                    case 9:
                        if (currentChar == '|')
                        {
                            currentState = 10;
                            AddTokenAndRemoveLastAdded(eTokenType.LogicOperator);
                            break;
                        }
                        EndPartialAnalysis();
                        break;
                    case 11:
                        if (currentChar == '=')
                        {
                            currentState = 13;
                            AddTokenAndRemoveLastAdded(eTokenType.RelationalOperator);
                            break;
                        }
                        EndPartialAnalysis();
                        break;
                    case 12:
                        if (currentChar == '=')
                        {
                            currentState = 13;
                            AddToken(eTokenType.RelationalOperator);
                            break;
                        }
                        AddError();
                        break;
                    case 8:
                    case 10:
                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 19:
                    case 21:
                    case 22:
                        EndPartialAnalysis();
                        break;
                    case 18:
                        if (currentChar == '=')
                        {
                            AddTokenAndRemoveLastAdded(eTokenType.RelationalOperator);
                            break;
                        }
                        EndPartialAnalysis();
                        break;
                    case 20:
                        if (currentChar == '\'')
                        {
                            AddTokenAndRemoveLastAdded(eTokenType.String);
                            currentState = 21;
                        }

                        break;
                }
            }
            while (currentChar != '\0' && currentPosition < textArray.Count());

            if (currentState == 3 || currentState == 5 || currentState == 12 || currentState == 20)
                AddError();

            this.Tokens.Add(new Token("$", currentRow, currentColumn, eTokenType.EndOfSentence));
        }

        #endregion
    }
}