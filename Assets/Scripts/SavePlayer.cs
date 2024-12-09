using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavePlayer : MonoBehaviour
{
    public static SavePlayer instance;

    [ContextMenu("Test Generate")]
    public void ChangeScene(string sceneName)
    {
        SavePlayers();
        SceneManager.LoadScene(sceneName);
    }

    private void Awake()
    {
        // if (instance == null)
        // {
        //     playerWeapons = new WeaponDataSO[2];
        //     instance = this;
        // }
        instance = this;
    }

    private void Start()
    {
        LoadPlayers();
    }

    private static void SavePlayers()
    {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PlayerInfo.playerWeapon1 = player.Weapons[0].Data;
        PlayerInfo.playerWeapon2 = player.Weapons[1].Data;
        Debug.Log(PlayerInfo.playerWeapon1);
        Debug.Log(PlayerInfo.playerWeapon2);
    }

    private static void LoadPlayers()
    {
        if (PlayerInfo.playerWeapon1 == null)
        {
            Debug.Log("Player weapons not loaded");
            return;
        }
        Debug.Log(PlayerInfo.playerWeapon1);
        Debug.Log(PlayerInfo.playerWeapon2);
        
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        player.LoadWeapons(PlayerInfo.playerWeapon1, PlayerInfo.playerWeapon2);
        
        // player.Weapons[0].Data = PlayerInfo.playerWeapons[0];
        // player.Weapons[1].Data = PlayerInfo.playerWeapons[1];
        // Debug.Log(player.Weapons[0].Data, player.Weapons[0].gameObject);
        // Debug.Log(player.Weapons[1].Data, player.Weapons[1].gameObject);
        //
        // player.PickUpWeapon(PlayerInfo.playerWeapons[0]);
        // player.EquipedWeapon = 1;
        // player.PickUpWeapon(PlayerInfo.playerWeapons[1]);
        // player.EquipedWeapon = 0;
        // player.Weapons[0].GenerateWeapon();
        // player.Weapons[1].GenerateWeapon();
    }
}
