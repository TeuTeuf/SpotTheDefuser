using SpotTheDefuser_Unity.Assets.Scripts.Main.Domain;
using SpotTheDefuser_Unity.Assets.Scripts.Main.UseCases;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour {

    public PlayerController prefabPlayer;

    [Inject]
    readonly DiContainer container;

    [Inject]
    readonly AddNewPlayer addNewPlayer;

    [Inject]
    readonly RemovePlayer removePlayer;

    [Inject]
    readonly GetAllPlayers getAllPlayers;

    Player player;

    void Start () 
    {
        player = new Player(gameObject.name);
        addNewPlayer.Execute(player);

        Debug.Log("New Player Added! Nb players: " + getAllPlayers.Get().Count);
	}

	void Update()
	{
        if(Input.GetMouseButtonDown(0)) {
            container.InstantiatePrefab(prefabPlayer);
        }
	}

	void OnDestroy()
    {
        removePlayer.execute(player);

        Debug.Log("Player removed! Nb players: " + getAllPlayers.Get().Count);
    }
}
