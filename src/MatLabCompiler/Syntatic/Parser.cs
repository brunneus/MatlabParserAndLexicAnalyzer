using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MatLabCompiler.Syntatic
{
    public class Parser
    {
        #region Constructor
        public Parser()
        {
            this.SyntaticResults = new ObservableCollection<string>();
        }
        
        #endregion

        #region Attributes and Properties

        public ObservableCollection<string> SyntaticResults { get; set; }

        private IEnumerable<Token> _sentence;
        private Token _currentToken;
        private int _lastSavedIndex = 0;
        private int _currentIndex = 0;

        #endregion

        #region Grammar

        #region Blocks

        public bool P()
        {
            if (B())
                return _currentToken.Lexeme == "$";

            return false;
        }

        private bool B()
        {
            if (_currentToken.Lexeme == "if")
            {
                this.NextToken();

                if (this.B5())
                {
                    if (this.B())
                    {
                        if (this.B1())
                        {
                            if (this.B2())
                            {
                                if (_currentToken.Lexeme == "end")
                                {
                                    this.NextToken();
                                    return this.B();
                                }
                            }
                        }
                    }
                }

                return false;
            }
            else if (_currentToken.Lexeme == "while")
            {
                this.NextToken();
                if (this.B5())
                {
                    if (_currentToken.Lexeme == "do")
                    {
                        this.NextToken();
                        if (this.B())
                        {
                            if (_currentToken.Lexeme == "end")
                            {
                                this.NextToken();
                                return this.B();
                            }
                        }
                    }
                }

                return false;
            }
            else if (_currentToken.Lexeme == "for")
            {
                this.NextToken();
                if (_currentToken.Type == eTokenType.Identifier)
                {
                    this.NextToken();
                    if (_currentToken.Lexeme == "=")
                    {
                        this.NextToken();
                        if (this.B5())
                        {
                            if (_currentToken.Lexeme == ":")
                            {
                                this.NextToken();
                                if (this.E1())
                                {
                                    if (this.B())
                                    {
                                        if (_currentToken.Lexeme == "end")
                                        {
                                            this.NextToken();
                                            return this.B();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return false;
            }
            else if (_currentToken.Lexeme == "switch")
            {
                this.NextToken();
                if (this.B4())
                {
                    if (_currentToken.Lexeme == "case")
                    {
                        this.NextToken();
                        if (this.B4())
                        {
                            if (this.B())
                            {
                                if (this.B3())
                                {
                                    if (_currentToken.Lexeme == "end")
                                    {
                                        this.NextToken();
                                        return this.B();
                                    }
                                }
                            }
                        }
                    }
                }

                return false;
            }
            else if (_currentToken.Type == eTokenType.Identifier)
            {
                this.SaveToken();
                this.NextToken();

                if (this.B6())
                    return this.B();

                this.RestoreToken();
            }
            SaveToken();
            if (this.E1())
            {
                if (this.B7())
                    return this.B();

                return false;
            }
            RestoreToken();
            return true;
        }

        private bool B1()
        {
            if (_currentToken.Lexeme == "elseif")
            {
                this.NextToken();
                if (this.B5())
                {
                    if (this.B())
                        return this.B1();
                }

                return false;
            }

            return true;
        }

        private bool B2()
        {
            if (_currentToken.Lexeme == "else")
            {
                this.NextToken();
                return this.B();
            }

            return true;
        }

        private bool B3()
        {
            if (_currentToken.Lexeme == "case")
            {
                this.NextToken();
                if (this.B4())
                {
                    if (this.B())
                    {
                        return this.B3();
                    }
                }

                return false;
            }
            else if (_currentToken.Lexeme == "otherwise")
            {
                this.NextToken();
                if (this.B())
                    return true;

                return false;
            }

            return true;
        }

        private bool B4()
        {
            if (_currentToken.Type == eTokenType.String)
            {
                this.NextToken();
                return true;
            }
            else if (this.B5())
            {
                return true;
            }

            return false;
        }

        private bool B5()
        {
            if (_currentToken.Type == eTokenType.Identifier)
            {
                this.SaveToken();
                this.NextToken();
                if (this.F())
                    return true;

                this.RestoreToken();
            }
            if (this.E1())
            {
                return true;
            }

            return false;
        }

        private bool B6()
        {
            if (_currentToken.Lexeme == "=")
            {
                this.NextToken();
                if (this.B4())
                {
                    if (this.B7())
                    {
                        return true;
                    }
                }
            }
            else if (this.F())
            {
                if (this.B7())
                {
                    return true;
                }
            }

            return false;
        }

        private bool B7()
        {
            if (_currentToken.Lexeme == ";")
            {
                this.NextToken();
                return true;
            }

            return true;
        }

        #endregion

        #region Functions

        private bool F()
        {
            if (_currentToken.Lexeme == "(")
            {
                this.NextToken();
                if (this.F1())
                    return true;
            }

            return false;
        }

        private bool F1()
        {
            if (_currentToken.Lexeme == ")")
            {
                this.NextToken();
                return true;
            }
            else if (this.F2())
            {
                if (_currentToken.Lexeme == ")")
                {
                    this.NextToken();
                    return true;
                }
            }

            return false;
        }

        private bool F2()
        {
            if (this.B4())
            {
                if (this.F3())
                    return true;
            }

            return false;
        }

        private bool F3()
        {
            if (_currentToken.Lexeme == ",")
            {
                this.NextToken();
                if (this.F2())
                    return true;

                return false;
            }

            return true;
        }

        #endregion

        #region Expression

        public bool E1()
        {
            if (this.E2())
                return this.E1_();

            return false;
        }

        public bool E1_()
        {
            if (this.CompareStringToCurrentToken("|"))
            {
                this.NextToken();
                if (this.E2())
                    return this.E1_();

                return false;
            }

            return true;
        }

        public bool E2()
        {
            if (this.E3())
                return this.E2_();

            return false;
        }

        private bool E2_()
        {
            if (this.CompareStringToCurrentToken("&"))
            {
                this.NextToken();
                if (this.E3())
                    return this.E2_();
                
                return false;
            }

            return true;
        }

        private bool E3()
        {
            if (this.E4())
                return this.E3_();

            return false;
        }

        private bool E3_()
        {
            if (this.CompareStringToCurrentToken("||") || this.CompareStringToCurrentToken("&&"))
            {
                this.NextToken();
                if (this.E4())
                    return this.E3_();

                return false;
            }

            return true;
        }

        public bool E4()
        {
            if (this.E5())
                return this.E4_();

            return false;
        }

        private bool E4_()
        {
            if (this.CompareStringToCurrentToken("==") || this.CompareStringToCurrentToken("~=") || this.CompareStringToCurrentToken(">=") ||
                this.CompareStringToCurrentToken("<=") || this.CompareStringToCurrentToken("<") || this.CompareStringToCurrentToken(">"))
            {
                this.NextToken();
                if (this.E5())
                    return this.E4_();

                return false;
            }

            return true;
        }

        private bool E5()
        {
            if (this.E6())
                return this.E5_();

            return false;
        }

        private bool E5_()
        {
            if (this.CompareStringToCurrentToken("+") || this.CompareStringToCurrentToken("-"))
            {
                this.NextToken();
                if (this.E6())
                    return this.E5_();

                return false;
            }

            return true;
        }

        private bool E6()
        {
            if (this.E7())
                return this.E6_();

            return false;
        }

        private bool E6_()
        {
            if (this.CompareStringToCurrentToken("*") || this.CompareStringToCurrentToken("/"))
            {
                this.NextToken();
                if (this.E7())
                    return this.E6_();

                return false;
            }

            return true;
        }

        private bool E7()
        {
            if (this.E8())
                return E7_();

            return false;
        }

        private bool E7_()
        {
            if (this.CompareStringToCurrentToken("^"))
            {
                this.NextToken();
                return this.E7();
            }

            return true;
        }

        private bool E8()
        {
            if (this.CompareStringToCurrentToken("+"))
            {
                this.NextToken();
                if (this.E9())
                    return true;
            }
            else if (this.CompareStringToCurrentToken("-"))
            {
                this.NextToken();
                if (this.E9())
                    return true;
            }
            else if (this.E9())
                return true;

            return false;
        }

        private bool E9()
        {
            if (this.CompareStringToCurrentToken(eTokenType.Identifier) || this.CompareStringToCurrentToken(eTokenType.Constant))
            {
                this.NextToken();
                return true;
            }
            else if (this.CompareStringToCurrentToken("("))
            {
                this.NextToken();
                if (this.E1())
                {
                    if (this.CompareStringToCurrentToken(")"))
                    {
                        this.NextToken();
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion

        #endregion

        #region Private Methods

        private void NextToken()
        {
            _currentIndex++;
            _currentToken = _sentence.ElementAt(_currentIndex);
        }

        private bool CompareStringToCurrentToken(string stringToCompare)
        {
            return _currentToken.Lexeme.Equals(stringToCompare);
        }

        private bool CompareStringToCurrentToken(eTokenType tokenType)
        {
            return _currentToken.Type == tokenType;
        }

        private void SaveToken()
        {
            _lastSavedIndex = _currentIndex;
        }

        private void RestoreToken()
        {
            _currentIndex = _lastSavedIndex;
            _currentToken = _sentence.ElementAt(_currentIndex);
        }

        #endregion

        #region Public Methods

        public void Analyze(IEnumerable<Token> tokens)
        {
            this.SyntaticResults.Clear();
            if (tokens.Count() <= 1)
                return;

            _sentence = tokens;
            _currentIndex = 0;
            _currentToken = tokens.First();
            if (this.P())
                this.SyntaticResults.Add("Success");
            else
                this.SyntaticResults.Add("Unexpected token " + _currentToken);
        }
        
        #endregion
    }
}