using UnityEngine;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
	public class UIController : MonoBehaviour {

		[Inject] public NetworkControllers NetworkControllers { get; set; }
		
		public void ClickOnNewDefuseAttempt()
		{
			NetworkControllers.SetNewDefuseAttemptOnServer();
		}

		public void ClickOnTryToDefuse()
		{
			Debug.Log("ClickOnTryToDefuse");
		}
	}
}
