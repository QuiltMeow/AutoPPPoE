using System;
using System.Runtime.Serialization;

namespace AutoPPPoE
{
    [Serializable]
    public class EWException : Exception
    {
        private const int EW = 6987;
        private readonly int type;

        public EWException() : base()
        {
        }

        public EWException(string message) : base(message)
        {
        }

        public EWException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public EWException(string message, int type) : base(message)
        {
            this.type = type;
        }

        public EWException(string message, Exception innerException, int type) : base(message, innerException)
        {
            this.type = type;
        }

        protected EWException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
        {
        }

        public int getExceptionType()
        {
            return type;
        }

        public int errorInteger()
        {
            return -1;
        }

        public int defaultInteger()
        {
            return 0;
        }

        public double defaultDouble()
        {
            return 0;
        }

        public string defaultString()
        {
            return string.Empty;
        }

        public object getNull()
        {
            return null;
        }

        public int getEW()
        {
            return EW;
        }

        public int getFirstRef()
        {
            return 8093;
        }

        public double getSecondRef()
        {
            return 221;
        }

        public void forceExit()
        {
            Environment.Exit(EW);
        }

        public string stackTraceString()
        {
            return StackTrace;
        }

        public string getMessage()
        {
            return Message;
        }

        public string toString()
        {
            return "[EW 例外狀況物件] 原因 : " + getMessage();
        }
    }
}