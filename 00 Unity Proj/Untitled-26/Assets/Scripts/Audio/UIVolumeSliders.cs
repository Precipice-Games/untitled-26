using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class UIVolumeSliders : MonoBehaviour
{
    [Header("Mixer")]
    [SerializeField] private AudioMixer mixer;

    [Header("Sliders")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider dialogueSlider;

    [Header("Volume Percentage Labels")]
    [SerializeField] private TMP_Text masterLabel;
    [SerializeField] private TMP_Text sfxLabel;
    [SerializeField] private TMP_Text bgmLabel;
    [SerializeField] private TMP_Text dialogueLabel;

    //AudioMixer parameter names
    private const string MASTER_PARAM = "MasterVolume";
    private const string SFX_PARAM = "SFXVolume";
    private const string BGM_PARAM = "BGMVolume";
    private const string DIALOGUE_PARAM = "DialogueVolume";

    private const string MASTER_KEY = "vol_master";
    private const string SFX_KEY = "vol_sfx";
    private const string BGM_KEY = "vol_bgm";
    private const string DIALOGUE_KEY = "vol_dialogue";

    private void Awake()
    {
        SetupSlider(masterSlider);
        SetupSlider(sfxSlider);
        SetupSlider(bgmSlider);
        SetupSlider(dialogueSlider);

        masterSlider.value = PlayerPrefs.GetFloat(MASTER_KEY, 100f);
        sfxSlider.value = PlayerPrefs.GetFloat(SFX_KEY, 100f);
        bgmSlider.value = PlayerPrefs.GetFloat(BGM_KEY, 100f);
        dialogueSlider.value = PlayerPrefs.GetFloat(DIALOGUE_KEY, 100f);

        ApplyAll();

        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        dialogueSlider.onValueChanged.AddListener(SetDialogueVolume);
    }

    private void SetupSlider(Slider slider)
    {
        if (slider == null) return;

        slider.minValue = 0f;
        slider.maxValue = 100f;
        slider.wholeNumbers = true;
    }

    private void ApplyAll()
    {
        SetMasterVolume(masterSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetBGMVolume(bgmSlider.value);
        SetDialogueVolume(dialogueSlider.value);
    }

    private void UpdateLabel()
    {
        if (masterLabel) masterLabel.text = masterSlider.value.ToString("0") + "%";
        if (sfxLabel) sfxLabel.text = sfxSlider.value.ToString("0") + "%";
        if (bgmLabel) bgmLabel.text = bgmSlider.value.ToString("0") + "%";
        if (dialogueLabel) dialogueLabel.text = dialogueSlider.value.ToString("0") + "%";
    }
    public void SetMasterVolume(float value)
    {
        SetMixerVolume(MASTER_PARAM, value);
        PlayerPrefs.SetFloat(MASTER_KEY, value);
        UpdateLabel();
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        SetMixerVolume(SFX_PARAM, value);
        PlayerPrefs.SetFloat(SFX_KEY, value);
        UpdateLabel();
        PlayerPrefs.Save();
    }

    public void SetBGMVolume(float value)
    {
        SetMixerVolume(BGM_PARAM, value);
        PlayerPrefs.SetFloat(BGM_KEY, value);
        UpdateLabel();
        PlayerPrefs.Save();
    }

    public void SetDialogueVolume(float value)
    {
        SetMixerVolume(DIALOGUE_PARAM, value);
        PlayerPrefs.SetFloat(DIALOGUE_KEY, value);
        UpdateLabel();
        PlayerPrefs.Save();
    }

    private void SetMixerVolume(string parameterName, float sliderValue)
    {
        float normalized = Mathf.Clamp(sliderValue / 100f, 0.0001f, 1f);
        float dB = Mathf.Log10(normalized) * 20f;

        if (sliderValue <= 0.01f)
        {
            dB = -80f;
        }

        mixer.SetFloat(parameterName, dB);
    }
}