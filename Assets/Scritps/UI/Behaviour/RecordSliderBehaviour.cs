using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecordSliderBehaviour : MonoBehaviour
{
    private Slider _slider;

    private Contexts _contexts;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _contexts = Contexts.sharedInstance;

        _slider.interactable = false;
        _slider.onValueChanged.AddListener(OnSliderValueChanged);

        _contexts.game.CreateEntity().AddRecordSlider(this);
    }

    public void SetSlider(int maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = maxValue;
    }

    public void EnableSlider()
    {
        _slider.interactable = true;
    }

    public void DisableSlider()
    {
        _slider.interactable = false;
        _slider.maxValue = 1;
    }

    private void OnSliderValueChanged(float value)
    {
        var toTick = (int)value;
        if (toTick < _contexts.game.lastTick.Value)
        {
            var entity = _contexts.game.CreateEntity();
            entity.AddReplay(toTick);
        }
    }
}
