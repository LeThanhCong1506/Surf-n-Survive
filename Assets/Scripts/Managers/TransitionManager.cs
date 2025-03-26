using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TurnOffVideo());
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
        //tạo một biến với string là true vào savemanager
        SaveManager.Instance.SaveBool("PlayedStoryScene", true);
    }

    IEnumerator TurnOffVideo()
    {
        yield return new WaitForSeconds(37.7f);
        GameObject.Find("Screen").SetActive(false);
    }

    public void PlayButtonSound()
    {
        AudioManager.Instance.PlayButtonSound();
    }
}
