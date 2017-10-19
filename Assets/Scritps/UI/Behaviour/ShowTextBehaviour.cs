using UnityEngine;
using UnityEngine.UI;

public class ShowTextBehaviour : MonoBehaviour
{
    public Text _showText;

    private Contexts _contexts;

    private void Awake()
    {
        _contexts = Contexts.sharedInstance;
        _showText = GetComponent<Text>();

        _contexts.game.CreateEntity().AddShowText(this);
    }

    public void Show(string content)
    {
        _showText.text = content;
    }
}
