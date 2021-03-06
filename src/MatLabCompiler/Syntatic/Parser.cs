﻿using System.Collections.Generic;
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

        public bool P(ref string cod)
        {
            cod += "Inicio\n";

            if (B(ref cod))
            {
                return _currentToken.Lexeme == "$";
            }

            return false;
        }

        private bool B(ref string BCOD)
        {
            if (_currentToken.Lexeme == "if")
            {
                string B5COD = string.Empty;
                string B_COD = string.Empty;
                string BR_COD = string.Empty;
                string B1COD = string.Empty;
                string B2COD = string.Empty;
                this.NextToken();

                if (this.B5(ref B5COD))
                {
                    if (this.B(ref B_COD))
                    {
                        if (this.B1(ref B1COD))
                        {
                            if (this.B2(ref B2COD))
                            {
                                if (_currentToken.Lexeme == "end")
                                {
                                    this.NextToken();
                                    if(this.B(ref BR_COD))
                                    {
                                        BCOD += string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", "se ", B5COD, "entao\n", B_COD, B1COD, B2COD, BR_COD, "\nfimse");
                                        return true;
                                    }

                                    return false;
                                }
                            }
                        }
                    }
                }

                return false;
            }
            else if (_currentToken.Lexeme == "while")
            {
                string B5COD = string.Empty;
                string B_COD = string.Empty;
                string BR_COD = string.Empty;

                this.NextToken();

                if (this.B5(ref B5COD))
                {
                    if (this.B(ref B_COD))
                    {
                        if (_currentToken.Lexeme == "end")
                        {
                            this.NextToken();

                            if(this.B(ref BR_COD))
                            {
                                BCOD += string.Format("enquanto {0} faca\n {1} \nfimenquanto {2}", B5COD, B_COD, BR_COD);
                                return true;
                            }

                            return false;
                        }
                    }
                }

                return false;
            }
            else if (_currentToken.Lexeme == "for")
            {
                this.NextToken();

                string B5COD = string.Empty;
                string E1COD = string.Empty;
                string B_COD = string.Empty;
                string BRCOD = string.Empty;

                if (_currentToken.Type == eTokenType.Identifier)
                {
                    string identifier = _currentToken.Lexeme;

                    this.NextToken();
                    if (_currentToken.Lexeme == "=")
                    {
                        this.NextToken();
                        if (this.B5(ref B5COD))
                        {
                            if (_currentToken.Lexeme == ":")
                            {
                                this.NextToken();
                                if (this.E1(ref E1COD))
                                {
                                        if (this.B(ref B_COD))
                                        {
                                            if (_currentToken.Lexeme == "end")
                                            {
                                                this.NextToken();
                                                if (this.B(ref BRCOD))
                                                {
                                                    BCOD += string.Format("para de {0} = {1} ate {2} faca {3} \nfimpara {4}", identifier, B5COD, E1COD, B_COD, BRCOD);
                                                    return true;
                                                }

                                                return false;
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
                var identifier = _currentToken.Lexeme;

                string B4COD = string.Empty;
                string B3COD = string.Empty;
                string B4_COD = string.Empty;
                string B_COD = string.Empty;
                string BRCOD = string.Empty;

                if (this.B4(ref B4COD))
                {
                    if (_currentToken.Lexeme == "case")
                    {
                        this.NextToken();
                        if (this.B4(ref B4_COD))
                        {
                            if (this.B(ref B_COD))
                            {
                                if (this.B3(ref B3COD, identifier))
                                {
                                    if (_currentToken.Lexeme == "end")
                                    {
                                        this.NextToken();
                                        if (this.B(ref BRCOD))
                                        {
                                            BCOD += string.Format("se {0} == {1} {2} {3} fimse {4}", B4COD.TrimEnd(), B4_COD, B_COD, B3COD, BRCOD);
                                            return true;
                                        }
                                        return false;
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
                string B6Cod = string.Empty;
                string B_1Cod = string.Empty;
                string id = _currentToken.Lexeme;

                this.SaveToken();
                this.NextToken();

                if (this.B6(ref B6Cod))
                {
                    if (this.B(ref B_1Cod))
                    {
                        BCOD = string.Concat(BCOD, id, B6Cod, B_1Cod);
                    }
                }

                this.RestoreToken();
            }
            SaveToken();
            if (this.E1(ref BCOD))
            {
                if (this.B7(ref BCOD))
                    return this.B(ref BCOD);

                return false;
            }
            RestoreToken();
            return true;
        }

        private bool B1(ref string B1COD)
        {
            string B5COD = string.Empty;
            string BCOD = string.Empty;
            string B1_COD = string.Empty;

            if (_currentToken.Lexeme == "elseif")
            {
                this.NextToken();
                if (this.B5(ref B5COD))
                {
                    if (this.B(ref BCOD))
                    {
                        if(this.B1(ref B1_COD))
                        {
                            B1COD = string.Format("senao se {0} {1} {2}", B5COD, BCOD, B1_COD);
                            return true;
                        }

                        return false;
                    }
                }

                return false;
            }

            return true;
        }

        private bool B2(ref string B2COD)
        {
            string BCOD = string.Empty;

            if (_currentToken.Lexeme == "else")
            {
                this.NextToken();

                if(this.B(ref BCOD))
                {
                    B2COD = string.Format("senao {0}", BCOD);
                }
            }

            return true;
        }

        private bool B3(ref string B3COD, string identifier)
        {
            string B4COD = string.Empty;
            string B3_COD = string.Empty;
            string BCOD = string.Empty;

            if (_currentToken.Lexeme == "case")
            {
                this.NextToken();
                if (this.B4(ref B4COD))
                {
                    if (this.B(ref BCOD))
                    {
                        if(this.B3(ref B3_COD, identifier))
                        {
                            B3COD = string.Format("senão se "+ identifier +" == {0} {1} {2}", B4COD, BCOD, B3_COD);
                            return true;
                        }
                        return false;
                    }
                }

                return false;
            }
            else if (_currentToken.Lexeme == "otherwise")
            {
                this.NextToken();
                if (this.B(ref BCOD))
                {
                    B3COD = string.Format("senao\n{0}", BCOD);
                    return true;
                }

                return false;
            }

            return true;
        }

        private bool B4(ref string cod)
        {
            if (_currentToken.Type == eTokenType.String)
            {
                this.NextToken();
                return true;
            }
            else if (this.B5(ref cod))
            {
                return true;
            }

            return false;
        }

        private bool B5(ref string cod)
        {
            if (_currentToken.Type == eTokenType.Identifier)
            {
                this.SaveToken();
                this.NextToken();
                if (this.F(ref cod))
                    return true;

                this.RestoreToken();
            }
            if (this.E1(ref cod))
            {
                return true;
            }

            return false;
        }

        private bool B6(ref string B6Cod)
        {
            string B4Cod = string.Empty;
            string B7Cod = string.Empty;
            string FCod = string.Empty;

            if (_currentToken.Lexeme == "=")
            {
                var convertedToken = SimbolConverter.Converter(_currentToken.Lexeme);
                this.NextToken();
                if (this.B4(ref B4Cod))
                {
                    if (this.B7(ref B7Cod))
                    {
                        B6Cod = string.Concat(convertedToken, B4Cod, B7Cod);
                        return true;
                    }
                }
            }
            else if (this.F(ref FCod))
            {
                if (this.B7(ref B7Cod))
                {
                    B6Cod = string.Concat(FCod, B7Cod);
                    return true;
                }
            }

            return false;
        }

        private bool B7(ref string cod)
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

        private bool F(ref string cod)
        {
            if (_currentToken.Lexeme == "(")
            {
                this.NextToken();
                if (this.F1(ref cod))
                    return true;
            }

            return false;
        }

        private bool F1(ref string cod)
        {
            if (_currentToken.Lexeme == ")")
            {
                this.NextToken();
                return true;
            }
            else if (this.F2(ref cod))
            {
                if (_currentToken.Lexeme == ")")
                {
                    this.NextToken();
                    return true;
                }
            }

            return false;
        }

        private bool F2(ref string cod)
        {
            if (this.B4(ref cod))
            {
                if (this.F3(ref cod))
                    return true;
            }

            return false;
        }

        private bool F3(ref string cod)
        {
            if (_currentToken.Lexeme == ",")
            {
                this.NextToken();
                if (this.F2(ref cod))
                    return true;

                return false;
            }

            return true;
        }

        #endregion

        #region Expression

        public bool E1(ref string E1COD)
        {
            string E2Cod = string.Empty;
            string E1_Cod = string.Empty;

            if (this.E2(ref E2Cod))
            {
                if (this.E1_(ref E1_Cod))
                {
                    string newLine = "\n";
                    if (this.HasUnclosedBracesUntilCurrentPosition())
                        newLine = "";

                    E1COD = string.Concat(E1COD, E2Cod, E1_Cod, newLine);
                    return true;
                }
            }

            return false;
        }

        private bool HasUnclosedBracesUntilCurrentPosition()
        {
            int openBracesCount = 0;
            int closeBracesCount = 0;
            int index = 0;
            foreach (var token in _sentence)
            {
                index++;

                if (token.Lexeme == "(")
                    openBracesCount++;
                else if (token.Lexeme == ")")
                    closeBracesCount++;

                if (index >= _currentIndex)
                    break;
            }

            return openBracesCount != closeBracesCount;
        }

        public bool E1_(ref string E1_Cod)
        {
            string E2Cod = string.Empty;
            string E1_1Cod = string.Empty;

            if (this.CompareStringToCurrentToken("|"))
            {
                this.NextToken();

                if (this.E2(ref E2Cod))
                {
                    if (this.E1_(ref E1_1Cod))
                    {
                        E1_Cod = string.Concat("|", E2Cod, E1_1Cod);
                        return true;
                    }
                }

                return false;
            }

            return true;
        }

        public bool E2(ref string E2Cod)
        {
            string E2_Cod = string.Empty;
            string E3Cod = string.Empty;

            if (this.E3(ref E3Cod))
            {
                if (this.E2_(ref E2_Cod))
                {
                    E2Cod = string.Concat(E3Cod, E2_Cod);
                    return true;
                }
            }

            return false;
        }

        private bool E2_(ref string E2_Cod)
        {
            string E3Cod = string.Empty;
            string E2_1Cod = string.Empty;

            if (this.CompareStringToCurrentToken("&"))
            {
                this.NextToken();
                if (this.E3(ref E3Cod))
                {
                    if (this.E2_(ref E2_1Cod))
                    {
                        E2_Cod = string.Concat("&", E3Cod, E2_1Cod);
                        return true;
                    }
                }

                return false;
            }

            return true;
        }

        private bool E3(ref string E3Cod)
        {
            string E4Cod = string.Empty;
            string E3_Cod = string.Empty;

            if (this.E4(ref E4Cod))
            {
                if (this.E3_(ref E3_Cod))
                {
                    E3Cod = string.Concat(E4Cod, E3_Cod);
                    return true;
                }
            }

            return false;
        }

        private bool E3_(ref string E3_Cod)
        {
            string E4Cod = string.Empty;
            string E3_1Cod = string.Empty;

            if (this.CompareStringToCurrentToken("||") || this.CompareStringToCurrentToken("&&"))
            {
                string currentToken = SimbolConverter.Converter(_currentToken.Lexeme);
                this.NextToken();
                if (this.E4(ref E4Cod))
                {
                    if (this.E3_(ref E3_1Cod))
                    {
                        E3_Cod = string.Concat(currentToken, E4Cod, E3_1Cod);
                        return true;
                    }
                }

                return false;
            }

            return true;
        }

        public bool E4(ref string E4Cod)
        {
            string E5Cod = string.Empty;
            string E4_Cod = string.Empty;

            if (this.E5(ref E5Cod))
            {
                if (this.E4_(ref E4_Cod))
                {
                    E4Cod = string.Concat(E5Cod, E4_Cod);
                    return true;
                }
            }

            return false;
        }

        private bool E4_(ref string E4_Cod)
        {
            string E4_1Cod = string.Empty;
            string E5Cod = string.Empty;

            if (this.CompareStringToCurrentToken("==") || this.CompareStringToCurrentToken("~=") || this.CompareStringToCurrentToken(">=") ||
                this.CompareStringToCurrentToken("<=") || this.CompareStringToCurrentToken("<") || this.CompareStringToCurrentToken(">"))
            {
                string convertedToken = SimbolConverter.Converter(_currentToken.Lexeme);
                this.NextToken();

                if (this.E5(ref E5Cod))
                {
                    if (this.E4_(ref E4_1Cod))
                    {
                        E4_Cod = string.Concat(convertedToken, E5Cod, E4_1Cod);
                        return true;
                    }
                }

                return false;
            }

            return true;
        }

        private bool E5(ref string E5Cod)
        {
            string E6Cod = string.Empty;
            string E5_Cod = string.Empty;

            if (this.E6(ref E6Cod))
            {
                if (this.E5_(ref E5_Cod))
                {
                    E5Cod = string.Concat(E6Cod, E5_Cod);
                    return true;
                }
            }

            return false;
        }

        private bool E5_(ref string E5_Cod)
        {
            string E6Cod = string.Empty;
            string E5_1Cod = string.Empty;

            if (this.CompareStringToCurrentToken("+") || this.CompareStringToCurrentToken("-"))
            {
                var convertedToken = SimbolConverter.Converter(_currentToken.Lexeme);
                this.NextToken();

                if (this.E6(ref E6Cod))
                {
                    if (this.E5_(ref E5_1Cod))
                    {
                        E5_Cod = string.Concat(convertedToken, E6Cod, E5_1Cod);
                        return true;
                    }
                }

                return false;
            }

            return true;
        }

        private bool E6(ref string E6Cod)
        {
            string E7Cod = string.Empty;
            string E6_Cod = string.Empty;

            if (this.E7(ref E7Cod))
            {
                if (this.E6_(ref E6_Cod))
                {
                    E6Cod = string.Concat(E7Cod, E6_Cod);
                    return true;
                }
            }

            return false;
        }

        private bool E6_(ref string E6_Cod)
        {
            string E7Cod = string.Empty;
            string E6_1Cod = string.Empty;

            if (this.CompareStringToCurrentToken("*") || this.CompareStringToCurrentToken("/"))
            {
                var convertedToken = SimbolConverter.Converter(_currentToken.Lexeme);
                this.NextToken();

                if (this.E7(ref E7Cod))
                {
                    if (this.E6_(ref E6_1Cod))
                    {
                        E6_Cod = string.Concat(convertedToken, E7Cod, E6_1Cod);
                        return true;
                    }
                }

                return false;
            }

            return true;
        }

        private bool E7(ref string E7Cod)
        {
            string E8Cod = string.Empty;
            string E7_Cod = string.Empty;

            if (this.E8(ref E8Cod))
            {
                if (E7_(ref E7_Cod))
                {
                    E7Cod = string.Concat(E8Cod, E7_Cod);
                    return true;
                }
            }

            return false;
        }

        private bool E7_(ref string E7_Cod)
        {
            string E7Cod = string.Empty;

            if (this.CompareStringToCurrentToken("^"))
            {
                var convertedToken = SimbolConverter.Converter(_currentToken.Lexeme);
                this.NextToken();

                if (this.E7(ref E7Cod))
                {
                    E7_Cod = string.Concat(convertedToken, E7Cod);
                    return true;
                }
            }

            return true;
        }

        private bool E8(ref string E8Cod)
        {
            string E9Cod = string.Empty;

            if (this.CompareStringToCurrentToken("+") || this.CompareStringToCurrentToken("-"))
            {
                var convertedToken = SimbolConverter.Converter(_currentToken.Lexeme);
                this.NextToken();

                if (this.E9(ref E9Cod))
                {
                    E8Cod = string.Concat(convertedToken, E9Cod);
                    return true;
                }
            }
            else if (this.E9(ref E9Cod))
            {
                E8Cod = E9Cod;
                return true;
            }

            return false;
        }

        private bool E9(ref string E9Cod)
        {
            string E1Cod = string.Empty;

            if (this.CompareStringToCurrentToken(eTokenType.Identifier) || this.CompareStringToCurrentToken(eTokenType.Constant))
            {
                E9Cod = _currentToken.Lexeme;
                this.NextToken();
                return true;
            }
            else if (this.CompareStringToCurrentToken("("))
            {
                this.NextToken();
                if (this.E1(ref E1Cod))
                {
                    if (this.CompareStringToCurrentToken(")"))
                    {
                        this.NextToken();
                        E9Cod = string.Format("({0})", E1Cod);
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

        private void GenerateInitialCode(ref string cod, IEnumerable<Token> tokens)
        {
            if (tokens.Count() == 0) return;

            var identifiersCode = "Var \n";

            var identifiers = tokens.Where(t => t.Type == eTokenType.Identifier).Select(t => t.Lexeme).Distinct();

            for (int i = 0; i < identifiers.Count(); i ++)
                identifiersCode += i == 0 ? identifiers.ElementAt(i) : string.Format(",{0}", identifiers.ElementAt(i));
            
            identifiersCode += ": Real";
            cod += string.Format("\n{0}\n" ,identifiersCode);
        }

        #endregion

        #region Public Methods

        public void Analyze(IEnumerable<Token> tokens, ref string cod, ref string errors)
        {
            this.SyntaticResults.Clear();
            if (tokens.Count() <= 1)
                return;

            _sentence = tokens;
            _currentIndex = 0;
            _currentToken = tokens.First();

            cod += "Algoritmo";

            this.GenerateInitialCode(ref cod, tokens);

            if (this.P(ref cod))
                this.SyntaticResults.Add("Success");
            else
                this.SyntaticResults.Add("Unexpected token " + _currentToken);

            cod += "\nfimalgoritmo";
        }

        #endregion
    }
}