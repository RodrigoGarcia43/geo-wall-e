using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSharp.Compiling.SyntaxAnalysis
{
    public class TokenStream : IEnumerable<Token>
    {
        private List<Token> _tokens;
        private int _position;

        public TokenStream(IEnumerable<Token> tokens)
        {
            _tokens = new List<Token>(tokens);
            _position = 0;
        }

        public bool End => _position == _tokens.Count;

        public bool Next()
        {
            if (_position < _tokens.Count)
            {
                _position++;
            }

            return _position < _tokens.Count;
        }

        public bool CanLookAhead(int k = 0)
        {
            return _tokens.Count - _position > k;
        }

        public Token LookAhead(int k = 0)
        {
            return _tokens[_position + k];
        }

        public IEnumerator<Token> GetEnumerator()
        {
            for (int i = _position; i < _tokens.Count; i++)
                yield return _tokens[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
