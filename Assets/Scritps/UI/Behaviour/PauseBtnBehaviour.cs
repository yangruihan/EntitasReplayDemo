using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Entitas;

public class PauseBtnBehaviour : MonoBehaviour
{
    [SerializeField] private Button _pauseBtn;
    [SerializeField] private Text _pauseBtnText;

    private Contexts _contexts;
    private bool _isPausing = false;

    private void Awake()
    {
        _contexts = Contexts.sharedInstance;
        _pauseBtn.onClick.AddListener(OnPauseBtnClick);
    }

    private void OnPauseBtnClick()
    {
        if (!_isPausing)
        {
            _pauseBtnText.text = "Running";
            _contexts.game.CreateEntity().AddPauseBtnClick(EnmPauseBtnStatus.Pause);
        }
        else
        {
            _pauseBtnText.text = "Pause";
            _contexts.game.CreateEntity().AddPauseBtnClick(EnmPauseBtnStatus.Running);
        }
        _isPausing = !_isPausing;
    }
}
