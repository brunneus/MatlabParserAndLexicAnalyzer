using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatLabCompiler.LexicAnalyser
{
    public class State
    {
        public State(bool isFinal)
        {
            this.IsFinal = isFinal;
        }

        public bool IsFinal { get; set; }
        public List<Transition> _transitions = new List<Transition>();

        public void AddTransition(State state, char symbol)
        {
            _transitions.Add(new Transition(symbol, state));
        }

        public void Resolve(string text, int currentPos)
        {
            var currentChar = text.ElementAt(currentPos);

            foreach (var transition in _transitions)
            {
                if (currentChar == transition.Symbol)
                    transition.State.Resolve(text, ++currentPos);
            }

            if(!this.IsFinal)
                throw new TokenNotRecognizedException();
        }
    }
}
