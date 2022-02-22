using Wordle.Shared;

namespace Wordle.Client.Services.GameService
{
    public interface IGameService
    {
        string Word { get; set; }
        Task<bool> checkWord(int row);
        Task<string> GetcheckString();
        Task<bool> GetEnd();
        Task<List<Key>> GetKeys();
        Task<Row> GetRow(int rowNumber);
        Task<int> GetRowCount();
        Task<List<Row>> GetRows();
        Task<List<List<string>>> GetRowShow();
        Task IncrementRowCount();
        Task SetcheckString(string checkString);
        Task SetEnd(bool End);
        Task SetKey(Key key);
        Task SetRowAnimation(int rowNumber, string animation);
        Task SetRowLoaded(int rowNumber, bool rowLoaded);
        Task SetRowShow(int row, int colum, string State);
        Task SetRowSloved(int rowNumber, bool rowSloved);
        Task SetRowSubmit(int rowNumber, string rowSubmit);
        Task SetRowValue(int rowNumber, string rowValue);
        Task<bool> GetFirstLoad();
        Task<int> GetPlayed();
        Task IncrementPlayed();
        Task<int> GetWinCount();
        Task IncrementWinCount();
        Task<decimal> GetWinPercent();
        Task IncrementWinStreak();
        Task ResetWinStreak();
        Task<int> GetWinDistribution(int index);
        Task SetWinDistribution(int index);
        Task<int> GetWinStreak();
        Task<int> GetMaxStreak();
        Task<List<int>> GetWinDistributionList();
        Task ResetGame();
        Task<bool> GetHardMode();
        Task SetHardMode(bool value);
        Task<bool> GetDarkTheme();
        Task SetDarkTheme(bool value);
        Task<bool> GetHighContrast();
        Task SetHighContrast(bool value);
        Task SetTime();
        Task<DateTime> GetTime();
    }
}
