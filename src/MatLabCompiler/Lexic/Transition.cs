namespace MatLabCompiler.LexicAnalyser
{
    public class Transition
    {
        public Transition(char symbol, State state)
        {
            this.Symbol = symbol;
            this.State = state;
        }

        public char Symbol { get; set; }
        public State State { get; set; }
    }
}
