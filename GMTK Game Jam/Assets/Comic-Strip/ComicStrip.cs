using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComicStrip : MonoBehaviour
{
    public List<Image> Images = new List<Image>();

    [SerializeField] private string nextSceneName;
    [SerializeField] private float fadeTime = 1f;
    [SerializeField] private float timeBetweenImages = 0.5f;

    private float fps = 30f;
    private float frameTime = 1f/30f;

    private void Awake()
    {
        
        foreach(Transform child in gameObject.transform)
        {
            if (child.TryGetComponent<Image>(out Image _image))
            {
                Images.Add(_image);
            }
        } 
    }

    private void Start()
    {
        PlayComicAnimation();
    }

    public void PlayComicAnimation()
    {
        StartCoroutine(FadeInImage());
    }

    private void HandleComicFinished() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
    }

    IEnumerator FadeInImage() 
    {
        foreach (Image image in Images)
        {
            Color c = image.color;
            for (float alpha = 0f; alpha < 1; alpha += frameTime)
            {
                c.a = alpha;
                image.color = c;
                yield return new WaitForSeconds(frameTime);
            }
            yield return new WaitForSeconds(0.2f);
        }
        HandleComicFinished();
        StopCoroutine(FadeInImage());
    }
}
