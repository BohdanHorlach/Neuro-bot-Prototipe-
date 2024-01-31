using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitcher : MonoBehaviour
{
    [SerializeField] private Animator levelSwithAnimator;

    private int indexScene;


    private void Start()
    {
        levelSwithAnimator.SetTrigger("Start");
    }


    public void PlaySwith(int index)
    {
        indexScene = index;
        levelSwithAnimator.SetTrigger("isSwitch");
    }


    public void LoadNextScene()
    {
        SceneManager.LoadScene(indexScene);
    }
}
