using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//消息中心
public class NotificationCenter
{
	//实力句柄
	private static NotificationCenter instance;
	
	//消息队列
	private static List<Notification> notifications = new List<Notification>();
	
	//注册消息
	public static void Register(string name,MonoBehaviour receiver)
	{
		foreach(Notification n in notifications)
		{
			if(n.GetName() == name)
			{
				n.AddReceiver(receiver);
				return;
			}
		}
		
		Notification temp = new Notification(name);
		temp.AddReceiver(receiver);
		notifications.Add(temp);
	}
	
	//派发消息
	public static void Dispatch(string name,MonoBehaviour dispatcher)
	{
		foreach(Notification n in notifications)
		{
			if(n.GetName() == name)
			{
				foreach(MonoBehaviour receiver in n.GetReceivers())
				{
					receiver.SendMessage(name,dispatcher,SendMessageOptions.DontRequireReceiver);
				}
				return;
			}
		}
	}
	
	//清空消息队列
	public static void ClearNotifications()
	{
		notifications.Clear();
	}
	
	//获取实力句柄
	private static NotificationCenter GetInstance()
	{
		if(instance == null)
		{
			instance = new NotificationCenter();
		}
		return instance;
	}
	
	//消息体
	private class Notification
	{
		//消息名
		private string name;
		
		//接收者
		private List<MonoBehaviour> receivers = new List<MonoBehaviour>();
		
		public Notification(string n)
		{
			name = n;
		}
		
		//添加接收者
		public void AddReceiver(MonoBehaviour receiver)
		{
			receivers.Add(receiver);
		}
		
		//获取消息名
		public string GetName()
		{
			return name;
		}
		
		//获取接收者列表
		public List<MonoBehaviour> GetReceivers()
		{
			return receivers;
		}
	}
}