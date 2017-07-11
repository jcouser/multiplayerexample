using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : NetworkBehaviour {
	
	[SyncVar]
	Vector3 realPosition = Vector3.zero;
	[SyncVar]
	Quaternion realRotation;
	private float updateInterval;

	public int updatePerSec = 9;

	void Update()
	{
		if (!isLocalPlayer)
		{
			transform.position = Vector3.Lerp(transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, 0.1f);
		}
		else
		{
			var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
			var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

			transform.Rotate(0, x, 0);
			transform.Translate(0, 0, z);
		
			// update the server with position/rotation
			updateInterval += Time.deltaTime;
			if (updateInterval > (1.0f / updatePerSec)) // 9 times per second
			{
				updateInterval = 0;
				CmdSync(transform.position, transform.rotation);
			}
		}
		
	}
	
	[Command]
	void CmdSync(Vector3 position, Quaternion rotation)
	{
		realPosition = position;
		realRotation = rotation;
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer>().material.color = Color.blue;
	}

	void OnGUI()
	{
		GUILayout.Label("Ping: " + NetworkManager.singleton.client.GetRTT() + " ms");
	}
}
