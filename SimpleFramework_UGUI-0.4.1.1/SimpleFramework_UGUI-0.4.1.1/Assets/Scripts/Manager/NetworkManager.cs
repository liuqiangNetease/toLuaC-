using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;

namespace SimpleFramework.Manager {
    public class NetworkManager : View {

        private static NetworkManager _instance;
        public static NetworkManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameObject("NetworkManager").AddComponent<NetworkManager>();
                }

                return _instance;
            }
        }

        private void InstanceAwake()
        {
            if(_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        private SocketClient socket;
        static Queue<KeyValuePair<int, ByteBuffer>> sEvents = new Queue<KeyValuePair<int, ByteBuffer>>();

        SocketClient SocketClient {
            get { 
                if (socket == null)
                    socket = new SocketClient();
                return socket;                    
            }
        }

        void Awake() {
            InstanceAwake();
            Init();
        }

        void Init() {
            SocketClient.OnRegister();
        }

        public void OnInit() {
            CallMethod("Start");
        }

        public void Unload() {
            CallMethod("Unload");
        }

        /// <summary>
        /// 执行Lua方法
        /// </summary>
        public object[] CallMethod(string func, params object[] args) {
            return Util.CallMethod("Network", func, args);
        }

        ///------------------------------------------------------------------------------------
        public static void AddEvent(int _event, ByteBuffer data) {
            sEvents.Enqueue(new KeyValuePair<int, ByteBuffer>(_event, data));
        }

        /// <summary>
        /// 交给Command，这里不想关心发给谁。
        /// </summary>
        void Update() {
            if (sEvents.Count > 0) {
                while (sEvents.Count > 0) {
                    KeyValuePair<int, ByteBuffer> _event = sEvents.Dequeue();
                    Util.CallMethod("Network", "OnSocket", _event.Key, _event.Value);
                    //facade.SendMessageCommand(NotiConst.DISPATCH_MESSAGE, _event);
                }
            }
        }

        /// <summary>
        /// 发送链接请求
        /// </summary>
        public void SendConnect() {
            SocketClient.SendConnect();
        }

        /// <summary>
        /// 发送SOCKET消息
        /// </summary>
        public void SendMessage(ByteBuffer buffer) {
            SocketClient.SendMessage(buffer);
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        new void OnDestroy() {
            SocketClient.OnRemove();
            Debug.Log("~NetworkManager was destroy");
        }
    }
}