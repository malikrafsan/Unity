using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

class ToastManager : MonoBehaviour
{
    public static ToastManager Instance { get; private set; }
    private Queue<(string, int)> toasts = new Queue<(string, int)>();
    private bool isProcessing = false;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public Text txt;

    public void ShowToast(string text,
        int duration)
    {
        //StartCoroutine(showToastCOR(text, duration));
        toasts.Enqueue((text, duration));
        if (!isProcessing)
        {
            StartCoroutine(showMultipleToast());
        }
    }

    private IEnumerator showToastCOR(string text,
        int duration)
    {
        Color orginalColor = txt.color;

        txt.text = text;
        txt.enabled = true;

        //Fade in
        yield return fadeInAndOut(txt, true, 0.5f);

        //Wait for the duration
        float counter = 0;
        while (counter < duration)
        {
            counter += Time.deltaTime;
            yield return null;
        }

        //Fade out
        yield return fadeInAndOut(txt, false, 0.5f);

        txt.enabled = false;
        txt.color = orginalColor;
    }

    IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = targetText.color;
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);

            yield return null;
        }
    }

    IEnumerator showMultipleToast()
    {
        isProcessing = true;
        while (toasts.Count > 0)
        {
            var toast = toasts.Dequeue();
            yield return showToastCOR(toast.Item1, toast.Item2);
        }
        if (toasts.Count == 0)
        {
            isProcessing = false;
        }
    }
}
