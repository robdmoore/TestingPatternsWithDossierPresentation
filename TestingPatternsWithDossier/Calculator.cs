namespace TestingPatternsWithDossier
{
    public class Calculator
    {
        private int _currentResult = 0;

        public int Add(int x)
        {
            _currentResult += x;
            return _currentResult;
        }

        public int Subtract(int x)
        {
            _currentResult -= x;
            return _currentResult;
        }

        public int Multiply(int x)
        {
            _currentResult *= x;
            return _currentResult;
        }

        public int Divide(int x)
        {
            _currentResult *= x;
            return _currentResult;
        }

        public int Clear()
        {
            _currentResult = 0;
            return _currentResult;
        }
    }
}
