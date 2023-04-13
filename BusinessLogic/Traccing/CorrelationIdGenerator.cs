using System;

namespace BusinessLogic.Traccing
{
    public class CorrelationIdGenerator
    {
        private string _correlationId;

        private static CorrelationIdGenerator _instance;

        private CorrelationIdGenerator()
        {
            
        }

        public static CorrelationIdGenerator Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new CorrelationIdGenerator();
                }
                return _instance;
            }
        }

        public string GetCorrelationId()
        {
            return _correlationId;
        }

        public void GenerateCorrelationId()
        {
            string correlationId = Guid.NewGuid().ToString();

            _correlationId = correlationId;
        }
    }
}
