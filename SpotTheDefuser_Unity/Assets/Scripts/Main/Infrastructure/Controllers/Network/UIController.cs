using UnityEngine;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
	public class UIController : MonoBehaviour {

		[Inject] public AllPlayerControllers AllPlayerControllers { get; set; }
		
		public void ClickOnNewDefuseAttempt()
		{
			AllPlayerControllers.SetNewDefuseAttemptOnServer();
		}

		public void ClickOnTryToDefuse()
		{
			Debug.Log("ClickOnTryToDefuse");
		}
	}
}
