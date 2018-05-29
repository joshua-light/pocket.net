namespace Pocket.Common
{
    public struct OneTimeFalse
    {
        private OneTime<bool> _current;
        
        public bool Value
        {
            get
            {
                if (_current == null)
                    _current = new OneTime<bool>(false, true);

                return _current.Value;
            }
        }
    }
}