using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] private GameSession session;
    [SerializeField] private PlayerInput playerInput;

    [Header("HUD")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Slider healthSlider;

    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text resultTitle;
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private GameObject resumeButton;
    [SerializeField] private GameObject restartButton;

    private bool paused;

    private void OnEnable()
    {
        session.ScoreChanged += UpdateScore;
        session.HealthChanged += UpdateHealth;
        session.TimeChanged += UpdateTime;
        session.Finished += ShowResult;
    }

    private void Start()
    {
        pausePanel.SetActive(false);
        endPanel.SetActive(false);
        Time.timeScale = 1f;
        session.PublishState();

        Debug.Log($"Start: currentActionMap = {playerInput.currentActionMap.name}", this);
    }

    private void OnDisable()
    {
        session.ScoreChanged -= UpdateScore;
        session.HealthChanged -= UpdateHealth;
        session.TimeChanged -= UpdateTime;
        session.Finished -= ShowResult;
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        Debug.Log($"OnPause: performed = {context.performed}", this);
        if (context.performed && !session.IsFinished) SetPaused(true);
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
        Debug.Log($"OnCancel: performed = {context.performed}, paused = {paused}", this);
        if (context.performed && paused) SetPaused(false);
    }

    public void ResumeGame()
    {
        if (paused) SetPaused(false);
    }

    private void SetPaused(bool value)
    {
        Debug.Log($"SetPaused: {value}", this);
        paused = value;
        pausePanel.SetActive(value);
        Time.timeScale = value ? 0f : 1f;

        if (playerInput != null && playerInput.actions != null)
        {
            try
            {
                playerInput.SwitchCurrentActionMap(value ? "UI" : "Gameplay");
                Debug.Log($"SwitchCurrentActionMap: OK ? {(value ? "UI" : "Gameplay")}", this);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning($"SwitchCurrentActionMap failed: {e.Message}", this);
            }
        }

        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;

        if (value && UnityEngine.EventSystems.EventSystem.current != null)
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    private void UpdateScore(int value)
    {
        scoreText.text = $"Счёт: {value}";
    }

    private void UpdateHealth(int current, int maximum)
    {
        healthSlider.maxValue = maximum;
        healthSlider.value = current;
        healthText.text = $"{current} / {maximum}";
    }

    private void UpdateTime(float seconds)
    {
        int total = Mathf.CeilToInt(seconds);
        timerText.text = $"{total / 60:00}:{total % 60:00}";
    }

    private void ShowResult(bool victory, int score)
    {
        Debug.Log($"ShowResult: victory = {victory}, score = {score}", this);
        paused = false;
        pausePanel.SetActive(false);
        endPanel.SetActive(true);
        resultTitle.text = victory ? "Победа" : "Поражение";
        finalScoreText.text = $"Итоговый счёт: {score}";
        Time.timeScale = 0f;
        playerInput.SwitchCurrentActionMap("UI");
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(restartButton);
    }
}