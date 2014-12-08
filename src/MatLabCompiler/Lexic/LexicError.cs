namespace MatLabCompiler.Lexic
{
    public class LexicError
    {
        public LexicError(string lexeme, int row, int column)
        {
            this.Lexeme = lexeme;
            this.Row = row;
            this.Column = column;
        }

        public string Lexeme { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public override string ToString()
        {
            return string.Format("[{0}, {1}] Unexpected Character \"{2}\"", this.Row, this.Column, this.Lexeme);
        }
    }
}