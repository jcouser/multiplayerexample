using UnityEngine;
using UnityEngine.Networking;

public class DedicatedServer : MonoBehaviour {
	
	private NetworkManager _netManager;

	// Use this for initialization
	void Start () {	
		if (SystemInfo.graphicsDeviceID == 0)
		{
			_netManager = GetComponent<NetworkManager>();
			Debug.Log("Creating Server on port '" + _netManager.networkPort + "'...");
			_netManager.StartServer();
		}
	}

}
