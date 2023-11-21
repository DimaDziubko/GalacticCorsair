using _Game.Core.GameState;
using _Game.Core.Services.Camera;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Button _startButton;

    private IGameStateMachine _stateMachine;
    public void Initialize(
        IGameStateMachine stateMachine,
        IWorldCameraService cameraService)
    {
        _stateMachine = stateMachine;

        _canvas.worldCamera = cameraService.UICamera;
        _startButton.onClick.AddListener(OnStartBtnClicked);
    }

    private void OnStartBtnClicked()
    {
        _stateMachine.Enter<LoadLevelState>();
    }
}
