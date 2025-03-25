using Services.Levels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Game
{
    public class LevelDisplayer : MonoBehaviour
    {
        [SerializeField] private Text _textLevel;
        
        private ILevelService _levelService;
        
        [Inject]
        private void Construct(ILevelService levelService)
        {
            _levelService = levelService;
        }
    
        private void Start()
        {
            _textLevel.text = "Level " + _levelService.GetCurrentLevel();
        }
    }
}