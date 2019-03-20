using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using UnityEngine.UI;

public class Player_Handler : Photon.MonoBehaviour {

	private PhotonView pv;

	public bool IsMaster;
	public string PlayerName;

	void Start(){
		pv = GetComponent<PhotonView>();

		transform.SetParent(GameObject.Find("Players").transform);
		transform.localScale = new Vector3(1,1,1);

		if(PhotonNetwork.isMasterClient){
			if(pv.isMine){
				IsMaster = true;
				transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.player.NickName;
			}else{
				transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.otherPlayers[0].NickName;
			}
		}else{
			if(pv.isMine){
				transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.player.NickName;
			}else{
				IsMaster = true;
				transform.GetChild(0).GetComponent<Text>().text = PhotonNetwork.masterClient.NickName;
			}
		}
	}

}
