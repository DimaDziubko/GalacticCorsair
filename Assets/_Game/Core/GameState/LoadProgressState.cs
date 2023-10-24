namespace _Game.Core.GameState
{
    public class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        // private readonly IPersistentProgressService _progressService;
        // private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(
            GameStateMachine stateMachine) 
            // IPersistentProgressService progressService, 
            // ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            // _progressService = progressService;
            // _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            //LoadProgressOrInitNew();
            //_stateMachine.Enter<LoadLevelState, string>();
        }

        public void Exit()
        {

        }

        // private void LoadProgressOrInitNew() =>
        //     _progressService.Progress = 
        //         _saveLoadService.LoadProgress() 
        //         ?? NewProgress();
        //
        // private PlayerProgress NewProgress()
        // {
        //     var progress = new PlayerProgress("Main");
        //
        //     progress.HeroState.MaxHP = 50;
        //     progress.HeroStats.Damage = 1;
        //     progress.HeroStats.DamageRadius = 0.5f;
        //     progress.HeroState.ResetHP();
        //
        //     return progress;
        // }
    }
}