using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatLabCompiler
{
    public class Token
    {
        public Token(string lexeme, int row, int column, eTokenType type)
        {
            this.Lexeme = lexeme;
            this.Row = row;
            this.Column = column;
            this.Type = type;
        }

        public string Lexeme { get; set; }
        public int Column { get; set; }
        public int Row { get; set; }
        public eTokenType Type { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]  {2}  \"{3}\"", this.Row, this.Column, this.Lexeme, this.Type.ToString());
        }

        public int CompareTo(Token other)
        {
            if(other.Lexeme == this.Lexeme)
                return 0;

            return -1;
        }
    }
}