using UnityEngine;
using LuaFramework;
using System.Collections.Generic;

public class AppView : View {
	private string message;

	///<summary>
	/// 监听的消息
	///</summary>
	List<string> MessageList {
		get {
			return new List<string>()
			{ 
				NotiConst.UPDATE_MESSAGE,
				NotiConst.UPDATE_EXTRACT,
				NotiConst.UPDATE_DOWNLOAD,
				NotiConst.UPDATE_PROGRESS,
			};
		}
	}

	void Awake() {
		RemoveMessage(this, MessageList);
		RegisterMessage(this, MessageList);
	}

	/// <summary>
	/// 处理View消息
	/// </summary>
	/// <param name="message"></param>
	public override void OnMessage(IMessage message) {
		string name = message.Name;
		object body = message.Body;
		switch (name) {
		case NotiConst.UPDATE_MESSAGE:      //更新消息
			UpdateMessage(body.ToString());
			break;
		case NotiConst.UPDATE_EXTRACT:      //更新解压
			UpdateExtract(body.ToString());
			break;
		case NotiConst.UPDATE_DOWNLOAD:     //更新下载
			UpdateDownload(body.ToString());
			break;
		case NotiConst.UPDATE_PROGRESS:     //更新下载进度
			UpdateProgress(body.ToString());
			break;
		}
	}

	public void UpdateMessage(string data) {
		this.message = data;
	}

	public void UpdateExtract(string data) {
		this.message = data;
	}

	public void UpdateDownload(string data) {
		this.message = data;
	}

	public void UpdateProgress(string data) {
		this.message = data;
	}

	void OnGUI() {
		GUI.Label(new Rect(10, 120, 960, 50), message);

	}
}
