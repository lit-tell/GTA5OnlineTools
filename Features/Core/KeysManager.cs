using System;
using System.Threading;
using System.Collections.Generic;

namespace GTA5OnlineTools.Features.Core
{
    public class KeysManager
    {
        // Keys holder 按键持有者
        private Dictionary<int, MyKey> keys;

        // Update thread 更新线程
        public Thread thread = null;
        private int interval = 20; // 20 ms

        // Keys events 按键事件
        public delegate void KeyHandler(int Id, string Name);
        public event KeyHandler KeyUpEvent;
        public event KeyHandler KeyDownEvent;

        // Init 初始化
        public KeysManager()
        {
            keys = new Dictionary<int, MyKey>();
            thread = new Thread(new ParameterizedThreadStart(Update));
            thread.IsBackground = true;
            thread.Start();
        }

        // Key Up 键弹起
        protected void OnKeyUp(int Id, string Name)
        {
            if (KeyUpEvent != null)
            {
                KeyUpEvent(Id, Name);
            }
        }

        // Key Down 键按下
        protected void OnKeyDown(int Id, string Name)
        {
            if (KeyDownEvent != null)
            {
                KeyDownEvent(Id, Name);
            }
        }

        // Add key 增加键
        public void AddKey(int keyId, string keyName)
        {
            if (!keys.ContainsKey(keyId))
            {
                keys.Add(keyId, new MyKey(keyId, keyName));
            }
        }

        // Add key 增加键
        public void AddKey(WinVK key)
        {
            int keyId = (int)key;
            if (!keys.ContainsKey(keyId))
            {
                keys.Add(keyId, new MyKey(keyId, key.ToString()));
            }
        }

        // Is Key Down 键是否按下
        public bool IsKeyDown(int keyId)
        {
            MyKey value;
            if (keys.TryGetValue(keyId, out value))
            {
                return value.IsKeyDown;
            }
            return false;
        }

        // Update Thread 更新线程
        private void Update(object sender)
        {
            while (true)
            {
                if (keys.Count > 0)
                {
                    List<MyKey> keysData = new List<MyKey>(keys.Values);
                    if (keysData != null && keysData.Count > 0)
                    {
                        foreach (MyKey key in keysData)
                        {
                            if (Convert.ToBoolean(WinAPI.GetKeyState(key.Id) & WinAPI.KEY_PRESSED))
                            {
                                if (!key.IsKeyDown)
                                {
                                    key.IsKeyDown = true;
                                    OnKeyDown(key.Id, key.Name);
                                }
                            }
                            else
                            {
                                if (key.IsKeyDown)
                                {
                                    key.IsKeyDown = false;
                                    OnKeyUp(key.Id, key.Name);
                                }
                            }
                        }
                    }
                }

                Thread.Sleep(interval);
            }
        }
    }

    public class MyKey
    {
        private string keyName;
        private int keyId;
        private bool keyDown;

        public MyKey(int keyId, string keyName)
        {
            this.keyId = keyId;
            this.keyName = keyName;
        }

        public string Name
        {
            get { return keyName; }
        }

        public int Id
        {
            get { return keyId; }
        }

        public bool IsKeyDown
        {
            get { return keyDown; }
            set { keyDown = value; }
        }
    }
}
