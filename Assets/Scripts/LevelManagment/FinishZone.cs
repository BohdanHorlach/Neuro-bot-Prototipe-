using UnityEngine;

public class FinishZone : MonoBehaviour
{
    [SerializeField] private LevelSwitcher levelSwitcher;
    [SerializeField, Min(0)] private int indexNextScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            levelSwitcher.PlaySwith(indexNextScene);
    }
}
