using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonManager : Photon.MonoBehaviour {

	public Button CreateOrJoinRoom_Btn;
	public string Player_Preafab_name;
	public InputField PlayerName;

	void Start(){
		CreateOrJoinRoom_Btn.interactable = false;		//disable create or join button at start..

		ConnectToPhoton();
	}

	void OnGUI(){
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	void ConnectToPhoton(){
		PhotonNetwork.ConnectUsingSettings("v0.1");
		Debug.Log("Connecting to Photon");
	}

	void OnConnectedToMaster(){
		PhotonNetwork.JoinLobby(TypedLobby.Default);
		Debug.Log("User Connected to Master");
	}

	void OnJoinedLobby(){
		Debug.Log("Lobby Joined");

		CreateOrJoinRoom_Btn.interactable = true;		//enable when user successfully connected to lobby
	}

	public void CreateOrJoinRoom(){
		Debug.Log("Joinning Random Room");

		PhotonNetwork.playerName = PlayerName.text;
		PhotonNetwork.JoinRandomRoom();
	}

	void OnJoinedRoom(){
		Debug.Log("Joined room successfully");

		SpawnPlayer();
	}

	void OnPhotonRandomJoinFailed(object[] msg){
		Debug.Log("Join random room Failed. ---> "+msg);

		CreateRoom();
	}

	void CreateRoom(){
		Debug.Log("Creating New room...");

		RoomOptions roomOps = new RoomOptions();
		roomOps.MaxPlayers = 2;

		string Rand_Num = Random.Range(0,100).ToString();
		PhotonNetwork.CreateRoom("Room"+Rand_Num,roomOps,TypedLobby.Default);
	}

	void OnCreatedRoom(){
		Debug.Log("Created Room --> "+PhotonNetwork.room.Name);
	}

	void OnPhotonCreateRoomFailed(object[] msg){
		Debug.Log("Create room Failed. Fuck off Bitches");
	}


	void OnPhotonPlayerConnected(PhotonPlayer player){
		Debug.Log(player.NickName);
	}

	void SpawnPlayer(){
		PhotonNetwork.Instantiate(Player_Preafab_name,Vector3.zero,Quaternion.identity,0);
	}
		
}
