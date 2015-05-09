using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatLabCompiler.Syntatic;
using System.Linq;
using MatLabCompiler.Lexic;

namespace MatLabCompilerTests
{
    [TestClass]
    public class UnitTest1
    {
        private Parser _parserAnalyzer = new Parser();
        private LexicAnalyzer _lexicAnalyzer = new LexicAnalyzer();

        [TestMethod]
        public void IfTest()
        {
            var ifString = "if a == 0 " +
                             "a = 3 " +
                             "b = 5 "  +
                             "1+2 " +
                             "end";

            var stringCod = string.Empty;
            _lexicAnalyzer.Analyze(ifString);
            _parserAnalyzer.Analyze(_lexicAnalyzer.Tokens, ref stringCod);

            Assert.IsTrue(_parserAnalyzer.SyntaticResults.First().ToString().Equals("Success"));
        }

        [TestMethod]
        public void WhileTest()
        {
            var whileString = "while a > 0 do end";

            var stringCod = string.Empty;

            _lexicAnalyzer.Analyze(whileString);
            _parserAnalyzer.Analyze(_lexicAnalyzer.Tokens, ref stringCod);

            Assert.IsTrue(_parserAnalyzer.SyntaticResults.First().ToString().Equals("Success"));
        }
    }
}
