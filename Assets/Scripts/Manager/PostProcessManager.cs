using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessManager : SceneSingleton<PostProcessManager>
{
    [SerializeField] private Volume _postProcessVolume;
    [SerializeField] private float _chromaticAberrationIntensity = 1.0f;
    [SerializeField] private float _lensDistortionIntensity = 1.0f;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        if (_postProcessVolume.profile.TryGet<ChromaticAberration>(out var _chromaticAberration))
        {
            _chromaticAberration.intensity.value = _chromaticAberrationIntensity;
        }

        if (_postProcessVolume.profile.TryGet<LensDistortion>(out var _lensDistortion))
        {
            _lensDistortion.intensity.value = _lensDistortionIntensity;
        }
    }
}
