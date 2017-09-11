﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class networkController : MonoBehaviour {
	public int maxConnections;
	private int hostID;
	private int myReliableChannelID;
	private int myUnreliableChannelID;

	// Use this for initialization
	void Start () {
		// Initializing the Transport Layer with no arguments (default settings)
		NetworkTransport.Init();
		// An example of initializing the Transport Layer with custom settings
		GlobalConfig gConfig = new GlobalConfig();
		gConfig.MaxPacketSize = 500;
		NetworkTransport.Init(gConfig);

		ConnectionConfig config = new ConnectionConfig();
		myReliableChannelID  = config.AddChannel(QosType.Reliable);
		myUnreliableChannelID = config.AddChannel(QosType.Unreliable);

		HostTopology topology = new HostTopology(config, maxConnections);

		this.hostID = NetworkTransport.AddHost(topology, 8888);
	}
	
	// Update is called once per frame
	void Update () {
		int recHostId; 
		int connectionId; 
		int channelId; 
		byte[] recBuffer = new byte[1024]; 
		int bufferSize = 1024;
		int dataSize;
		byte error;
		NetworkEventType recData = NetworkTransport.Receive(out recHostId, out connectionId, out channelId, recBuffer, bufferSize, out dataSize, out error);
		switch (recData)
		{
		case NetworkEventType.Nothing:         //1
			//Debug.Log("nothing");
			break;
		case NetworkEventType.ConnectEvent:    //2
			Debug.Log("Connection");
			break;
		case NetworkEventType.DataEvent:       //3
			Debug.Log("Data");
			break;
		case NetworkEventType.DisconnectEvent: //4
			Debug.Log("Disconnection");
			break;
		}
	}
}
