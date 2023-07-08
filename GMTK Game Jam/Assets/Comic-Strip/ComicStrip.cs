using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComicStrip : MonoBehaviour
{
    public List<Image> Images = new List<Image>();

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

    /*
    private void Start()
    {
        PlayComicAnimation();
    }
    */

    public void PlayComicAnimation()
    {
        StartCoroutine(FadeInImage());
    }

    IEnumerator FadeInImage() 
    {
        foreach (Image image in Images)
        {
            Color c = image.color;
            for (float alpha = 0f; alpha < 1; alpha += 0.005f)
            {
                c.a = alpha;
                image.color = c;
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.2f);
        }
        StopCoroutine(FadeInImage());
    }
}
