using System;
using System.Threading;

namespace AutoPPPoE
{
    public enum Status
    {
        WAIT_NEXT_TIME,
        WAIT_RASDIAL,
        WAIT_ADAPTER,
        SHOW_WELCOME,
        SHOW_START,
        SHOW_AUTOMATIC_START,
        SHOW_ADAPTER,
        SHOW_CONNECT,
        SHOW_DISCONNECT,
        MODE_START_UP,
        MODE_WATCH_DOG
    }

    public static class Constant
    {
        public const byte FAST_PING_CHECK_TIME = 2;
        public const byte SLOW_PING_CHECK_TIME = 1;

        public const byte MAX_FETCH_ATTEMPT = 3;
        public const byte PING_BYTE = 32;

        public const int WAIT_NEXT_TIME_DELAY = 2500;
        public const int WAIT_NETWORK_STATUS_CHANGE_DELAY = 5000;
        public const int TOOL_TIP_SHOW_DURATION = 5000;

        public const string FAST_CHECK_HOST = "139.175.55.244";
        public const string SLOW_CHECK_HOST = "203.79.224.10";
        public const string IP_FETCH_URL = "https://ip4.seeip.org/";

        public const string AES_KEY = "ew.sr.x1c.quilt.meow";

        public static void wait(Status mode)
        {
            switch (mode)
            {
                case Status.WAIT_NEXT_TIME:
                    {
                        Thread.Sleep(WAIT_NEXT_TIME_DELAY);
                        break;
                    }
                case Status.WAIT_RASDIAL:
                case Status.WAIT_ADAPTER:
                    {
                        Thread.Sleep(WAIT_NETWORK_STATUS_CHANGE_DELAY);
                        break;
                    }
                default:
                    {
                        throw new ArgumentException("參數錯誤");
                    }
            }
        }
    }
}