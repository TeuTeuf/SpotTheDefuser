using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;
using SpotTheDefuser_Unity.Assets.Scripts.Main.UseCases;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour {

    [Inject]
    public AddNewPlayer addNewPlayer;

    [Inject]
    public RemovePlayer removePlayer;

    [Inject]
    public GetAllPlayers getAllPlayers;

    Player player;

    public void Start () 
    {
        player = new Player("Player");
        addNewPlayer.Execute(player);

        Debug.Log("New Player Added!");
	}

	public void OnDestroy()
    {
        //removePlayer.execute(player);

        //Debug.Log("Player removed! Nb players: " + getAllPlayers.Get().Count);
    }
}
