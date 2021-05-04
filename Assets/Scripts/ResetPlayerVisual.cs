using UnityEngine;
using UnityEngine.UI;

public class ResetPlayerVisual : MonoBehaviour
{
    public Image image;

    public float alphaFadeInSeconds = 0.5f;
    public AnimationCurve alphaFadeIn = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float alphaFadeOutSeconds = 0.5f;
    public AnimationCurve alphaFadeOut = AnimationCurve.EaseInOut(0, 1, 1, 0);

    [Header("Debug")]
    public float playback = 0;
    public bool isPlaying = false;

    [SerializeField]
    private Component onFadedTarget;

    [SerializeField]
    private string onFadedMessage;

    public void StartFading(Component onFadedTarget, string onFadedMessage)
    {
        this.onFadedTarget = onFadedTarget;
        this.onFadedMessage = onFadedMessage;
        isPlaying = true;
        playback = 0;
    }

    void Reset()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (!isPlaying)
        {
            return;
        }

        if (!image)
        {
            Debug.LogWarning($"The '{nameof(image)}' field must be set.", this);
            isPlaying = false;
            return;
        }

        var prevPlayback = playback;
        playback += Time.deltaTime;

        if (playback <= alphaFadeInSeconds)
        {
            SetAlpha(alphaFadeIn.Evaluate(playback / alphaFadeInSeconds));
            return;
        }

        if (prevPlayback <= alphaFadeInSeconds)
        {
            onFadedTarget.SendMessage(onFadedMessage);
        }

        var fadeOutPlayback = playback - alphaFadeInSeconds;
        if (fadeOutPlayback <= alphaFadeOutSeconds)
        {
            SetAlpha(alphaFadeOut.Evaluate(playback / alphaFadeOutSeconds));
            return;
        }

        SetAlpha(alphaFadeOut.Evaluate(1));
        isPlaying = false;
    }

    void SetAlpha(float alpha)
    {
        var color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
