﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace LuaFramework {
	public class NetworkManager : Manager {
		private SocketClient socket;
		static readonly object m_lockObject = new object();
		static Queue<KeyValuePair<int, ByteBuffer>> mEvents = new Queue<KeyValuePair<int, ByteBuffer>>();

		SocketClient SocketClient {
			get { 
				if (socket == null)
					socket = new SocketClient();
				return socket;                    
			}
		}

		void Awake() {
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
			lock (m_lockObject) {
				mEvents.Enqueue(new KeyValuePair<int, ByteBuffer>(_event, data));
			}
		}

		/// <summary>
		/// 交给Command，这里不想关心发给谁。
		/// </summary>
		void Update() {
			if (mEvents.Count > 0) {
				while (mEvents.Count > 0) {
					KeyValuePair<int, ByteBuffer> _event = mEvents.Dequeue();
					facade.SendMessageCommand(NotiConst.DISPATCH_MESSAGE, _event);
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