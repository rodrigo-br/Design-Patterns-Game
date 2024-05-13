using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public InputObserver InputObserver { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InputObserver = new InputObserver();
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
