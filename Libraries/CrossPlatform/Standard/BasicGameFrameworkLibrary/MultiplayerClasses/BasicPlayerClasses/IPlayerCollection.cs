using BasicGameFrameworkLibrary.DIContainers;
using CommonBasicStandardLibraries.BasicDataSettingsAndProcesses;
using CommonBasicStandardLibraries.CollectionClasses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace BasicGameFrameworkLibrary.MultiplayerClasses.BasicPlayerClasses
{
    public interface IPlayerCollection<P> : IEnumerable<P> where P : IPlayerItem, new()
    {
        P this[int iD] { get; }
        P this[string nickName] { get; }
        IGamePackageResolver? MainContainer { get; set; }
        void AddPlayer(P thisPlayer);
        CustomBasicList<P> AllPlayersExceptForCurrent();
        void AutoSaved(IPlayOrder thisOrder);
        Task<int> CalculateOtherTurnAsync(bool useCurrentPlayer = false, bool includeOutPlayers = true);
        Task<int> CalculateWhoTurnAsync(bool useCurrentPlayer = false, bool includeOutPlayers = false);
        void ClearTempPlayers();
        void FinishLoading(bool needsToShufflePlayers = true);
        void ForEach(Action<P> action);
        Task ForEachAsync(BasicDataFunctions.ActionAsync<P> action);
        CustomBasicList<P> GetAllComputerPlayers(bool excludeCurrent = true);
        CustomBasicList<P> GetAllNonComputerPlayers(bool excludeCurrent = true);
        CustomBasicList<P> GetAllPlayersStartingWithSelf();
        P GetEnabledPlayer();
        P GetOtherPlayer();
        P GetSelf();
        P GetWhoPlayer();
        P GetOnlyOpponent();
        void LoadPlayers(int humans = 0, int computers = 0, bool reverse = false);
        void PerformActionOnConditional(Predicate<P> predicate, Action<P> action);
        void ResetReady();
        int Count { get; }
    }
}