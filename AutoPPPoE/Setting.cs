namespace AutoPPPoE
{
    public class Setting
    {
        private const int ALLOW_MAX_FAST_PING_WAIT = 600000;
        private const int ALLOW_MAX_SLOW_PING_WAIT = 3600000;
        private const int ALLOW_MAX_AUTOMATIC_START_WAIT_TIME = 3600;

        public string adapter
        {
            get;
            private set;
        }

        public string name
        {
            get;
            private set;
        }

        public string account
        {
            get;
            private set;
        }

        public string password
        {
            get;
            private set;
        }

        public int fastPing
        {
            get;
            private set;
        }

        public int slowPing
        {
            get;
            private set;
        }

        public bool automaticStart
        {
            get;
            private set;
        }

        public int automaticStartWaitTime
        {
            get;
            private set;
        }

        public Setting(string adapter, string name, string account, string password, int fastPing, int slowPing, bool automaticStart, int automaticStartWaitTime)
        {
            AdapterManager adapterManager = Program.adapter;
            if (!adapterManager.adapterName.Contains(adapter))
            {
                throw new EWException("網路卡名稱不存在");
            }
            if (!adapterManager.rasName.Contains(name))
            {
                throw new EWException("PPPoE 介面卡名稱不存在");
            }

            if (fastPing >= slowPing || fastPing < 1 || fastPing > ALLOW_MAX_FAST_PING_WAIT || slowPing < 2 || slowPing > ALLOW_MAX_SLOW_PING_WAIT)
            {
                throw new EWException("Ping 等待時間參數錯誤");
            }
            if (automaticStartWaitTime < 0 || automaticStartWaitTime > ALLOW_MAX_AUTOMATIC_START_WAIT_TIME)
            {
                throw new EWException("啟動等待時間參數錯誤");
            }
            this.adapter = adapter;
            this.name = name;
            this.account = account;
            this.password = password;
            this.fastPing = fastPing;
            this.slowPing = slowPing;
            this.automaticStart = automaticStart;
            this.automaticStartWaitTime = automaticStartWaitTime;
        }
    }
}