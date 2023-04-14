using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleTrackMixer : PlayableBehaviour
{
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI subtitle = playerData as TextMeshProUGUI;
        string currentSubtitle = "";
        float currentAlpha = 0f;
        if (!subtitle) { return;  }

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);

            if (inputWeight > 0f)
            {
                ScriptPlayable<SubtitleBehavior> inputPlayable = (ScriptPlayable<SubtitleBehavior>) playable.GetInput(i);
                
                SubtitleBehavior input = inputPlayable.GetBehaviour();
                currentSubtitle = input.subtitleText;
                currentAlpha = inputWeight;
            }
        }

        {
            subtitle.text = currentSubtitle;
            subtitle.color = new Color(1, 1, 1, currentAlpha);
        }
    }
}
