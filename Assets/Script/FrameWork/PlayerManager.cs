using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager :Singleton<PlayerManager> {

    List<BasePlayer> players = new List<BasePlayer>();

    public void AddPlayer(BasePlayer p)
    {
        players.Add(p);
    }

    public void RemovePlayer(BasePlayer p)
    {
        players.Remove(p);
    }

    public BasePlayer GetPlayer(int id)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].id==id)
            {
                return players[i];
            }
        }
        return null;
    }

    public List<BasePlayer> GetAllPlayer()
    {
        return players;
    }

	// Update is called once per frame
	public void Update () {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].Update();
        }
	}
}
