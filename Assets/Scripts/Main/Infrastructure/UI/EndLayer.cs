using Main.Domain.UI;
using Main.Domain.UI.Layers;
using Main.UseCases.Network;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Main.Infrastructure.UI
{
    public class EndLayer: BaseLayer, IEndLayer
    {
        public Text nbBombsDefusedText;

        private StopNetwork _stopNetwork;

        [Inject]
        public void Init(StopNetwork stopNetwork)
        {
            _stopNetwork = stopNetwork;
        }
        
        public void UpdateNbBombsDefused(int nbBombsDefused)
        {
            nbBombsDefusedText.text = nbBombsDefused.ToString();
        }

        public void OnClickOnBackHome()
        {
            _stopNetwork.Stop();
            SceneManager.LoadScene(0);
        }

        public override View GetView()
        {
            return View.End;
        }
    }
}