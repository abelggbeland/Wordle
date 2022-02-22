using System.Linq;

using Wordle.Shared;
using Blazored.LocalStorage;
using System.Globalization;

namespace Wordle.Client.Services.GameService
{
    public class GameService : IGameService
    {
        private readonly ILocalStorageService _localStorage;

        public GameService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<int> GetRowCount()
        {
            if (await _localStorage.ContainKeyAsync("RowNumber"))
            {
                return await _localStorage.GetItemAsync<int>("RowNumber");
            }
            else
            {
                await _localStorage.SetItemAsync<int>("RowNumber", 0);
                return await _localStorage.GetItemAsync<int>("RowNumber");
            }
        }

        public async Task IncrementRowCount()
        {
            if (_localStorage != null)
            {
                int row = await _localStorage.GetItemAsync<int>("RowNumber");
                await _localStorage.SetItemAsync<int>("RowNumber", row + 1);
            }
        }

        public async Task<List<Row>> GetRows()
        {
            if (await _localStorage.ContainKeyAsync("Row"))
            {
                List<Row> row = await _localStorage.GetItemAsync<List<Row>>("Row");
                return row;
            }
            else
            {
                List<Row> rows = new List<Row>
                {
                    new Row(),
                    new Row(),
                    new Row(),
                    new Row(),
                    new Row(),
                    new Row()
                };
                await _localStorage.SetItemAsync<List<Row>>("Row", rows);
                List<Row> row = await _localStorage.GetItemAsync<List<Row>>("Row");
                return row;
            }
        }

        public async Task<Row> GetRow(int rowNumber)
        {
            List<Row> row = await _localStorage.GetItemAsync<List<Row>>("Row");
            return row[rowNumber];
        }

        public async Task SetRowValue(int rowNumber, string rowValue)
        {
            List<Row> rows = await _localStorage.GetItemAsync<List<Row>>("Row");
            rows[rowNumber].RowValue = rowValue;
            await _localStorage.SetItemAsync<List<Row>>("Row", rows);
        }

        public async Task SetRowAnimation(int rowNumber, string animation)
        {
            List<Row> rows = await _localStorage.GetItemAsync<List<Row>>("Row");
            rows[rowNumber].Animation = animation;
            await _localStorage.SetItemAsync<List<Row>>("Row", rows);
        }

        public async Task SetRowSubmit(int rowNumber, string rowSubmit)
        {
            List<Row> rows = await _localStorage.GetItemAsync<List<Row>>("Row");
            rows[rowNumber].RowSubmit = rowSubmit;
            await _localStorage.SetItemAsync<List<Row>>("Row", rows);
        }

        public async Task SetRowSloved(int rowNumber, bool rowSloved)
        {
            List<Row> rows = await _localStorage.GetItemAsync<List<Row>>("Row");
            rows[rowNumber].Sloved = rowSloved;
            await _localStorage.SetItemAsync<List<Row>>("Row", rows);
        }

        public async Task SetRowLoaded(int rowNumber, bool rowLoaded)
        {
            List<Row> rows = await _localStorage.GetItemAsync<List<Row>>("Row");
            rows[rowNumber].Loaded = rowLoaded;
            await _localStorage.SetItemAsync<List<Row>>("Row", rows);
        }

        public async Task SetEnd(bool End)
        {
            await _localStorage.SetItemAsync<bool>("End", End);
        }

        public async Task<bool> GetEnd()
        {
            if (await _localStorage.ContainKeyAsync("End"))
            {
                return await _localStorage.GetItemAsync<bool>("End"); ;
            }
            else
            {
                await _localStorage.SetItemAsync<bool>("End", false);
                return await _localStorage.GetItemAsync<bool>("End");
            }
        }

        public async Task<bool> GetFirstLoad()
        {
            if (await _localStorage.ContainKeyAsync("FirstLoad"))
            {
                await _localStorage.SetItemAsync<bool>("FirstLoad", false);
                return await _localStorage.GetItemAsync<bool>("FirstLoad");
            }
            else
            {
                await _localStorage.SetItemAsync<bool>("FirstLoad", true);
                return await _localStorage.GetItemAsync<bool>("FirstLoad");
            }
        }

        public async Task<int> GetPlayed()
        {
            if (await _localStorage.ContainKeyAsync("Played"))
            {
                return await _localStorage.GetItemAsync<int>("Played"); ;
            }
            else
            {
                await _localStorage.SetItemAsync<int>("Played", 0);
                return await _localStorage.GetItemAsync<int>("Played");
            }
        }

        public async Task IncrementPlayed()
        {
            int number = await _localStorage.GetItemAsync<int>("Played");
            await _localStorage.SetItemAsync<int>("Played", number + 1);
        }

        public async Task<int> GetWinCount()
        {
            if (await _localStorage.ContainKeyAsync("WinCount"))
            {
                return await _localStorage.GetItemAsync<int>("WinCount");
            }
            else
            {
                await _localStorage.SetItemAsync<int>("WinCount", 0);
                return await _localStorage.GetItemAsync<int>("WinCount");
            }
        }

        public async Task IncrementWinCount()
        {
            int number = await _localStorage.GetItemAsync<int>("WinCount");
            await _localStorage.SetItemAsync<int>("WinCount", number + 1);
        }

        public async Task<decimal> GetWinPercent()
        {
            var WinCount = await GetWinCount();
            var Played = await GetPlayed();
            if (Played == 0)
            {
                await _localStorage.SetItemAsync<decimal>("WinPercent", 0);
                var responce = await _localStorage.GetItemAsync<int>("WinPercent");
                return responce;
            }
            else
            {
                decimal winPercent = WinCount / Played;
                await _localStorage.SetItemAsync<decimal>("WinPercent", winPercent);
                var responce = await _localStorage.GetItemAsync<int>("WinPercent");
                return responce;
            }
        }

        public async Task<int> GetWinStreak()
        {
            if (await _localStorage.ContainKeyAsync("WinStreak"))
            {
                return await _localStorage.GetItemAsync<int>("WinStreak");
            }
            else
            {
                await _localStorage.SetItemAsync<int>("WinStreak", 0);
                return await _localStorage.GetItemAsync<int>("WinStreak");
            }
        }

        public async Task IncrementWinStreak()
        {
            int number = await _localStorage.GetItemAsync<int>("WinStreak");
            await _localStorage.SetItemAsync<int>("WinStreak", number + 1);
            int winStreak = await _localStorage.GetItemAsync<int>("WinStreak");
            if (winStreak > await GetMaxStreak())
            {
                int maxStreak = await _localStorage.GetItemAsync<int>("MaxStreak");
                await _localStorage.SetItemAsync<int>("MaxStreak", maxStreak + 1);
            }
        }

        public async Task ResetWinStreak()
        {
            await _localStorage.SetItemAsync<int>("WinStreak", 0);
        }
        public async Task<int> GetMaxStreak()
        {
            if (await _localStorage.ContainKeyAsync("MaxStreak"))
            {
                return await _localStorage.GetItemAsync<int>("MaxStreak");
            }
            else
            {
                await _localStorage.SetItemAsync<int>("MaxStreak", 0);
                return await _localStorage.GetItemAsync<int>("MaxStreak");
            }
        }

        public async Task<int> GetWinDistribution(int index)
        {
            var distribution = await _localStorage.GetItemAsync<List<int>>("WinDistribution");
            return distribution[index];
        }

        public async Task<List<int>> GetWinDistributionList()
        {
            if (await _localStorage.ContainKeyAsync("WinDistribution"))
            {
                var distribution = await _localStorage.GetItemAsync<List<int>>("WinDistribution");
                return distribution;
            }
            else
            {
                List<int> WinDistribution = new List<int>()
                {
                    0,
                    0,
                    0,
                    0,
                    0,
                    0,
                };
                await _localStorage.SetItemAsync<List<int>>("WinDistribution", WinDistribution);
                var distribution = await _localStorage.GetItemAsync<List<int>>("WinDistribution");
                return distribution;
            }
        }

        public async Task ResetGame()
        {
            await _localStorage.RemoveItemsAsync(new List<string> {
                "End",
                "Row",
                "RowShow",
                "checkString",
                "RowNumber",
                "Keys",
            });
        }

        public async Task SetWinDistribution(int index)
        {
            var distribution = await _localStorage.GetItemAsync<List<int>>("WinDistribution");
            distribution[index]++;
            await _localStorage.SetItemAsync<List<int>>("WinDistribution", distribution);
        }

        public async Task<bool> GetHardMode()
        {
            if (await _localStorage.ContainKeyAsync("HardMode"))
            {
                return await _localStorage.GetItemAsync<bool>("HardMode");
            }
            else
            {
                await _localStorage.SetItemAsync<bool>("HardMode", false);
                return await _localStorage.GetItemAsync<bool>("HardMode");
            }
        }

        public async Task SetHardMode(bool value)
        {
            await _localStorage.SetItemAsync<bool>("HardMode", value);
        }

        public async Task<bool> GetDarkTheme()
        {
            if (await _localStorage.ContainKeyAsync("DarkTheme"))
            {
                return await _localStorage.GetItemAsync<bool>("DarkTheme");
            }
            else
            {
                await _localStorage.SetItemAsync<bool>("DarkTheme", false);
                return await _localStorage.GetItemAsync<bool>("DarkTheme");
            }
        }

        public async Task SetDarkTheme(bool value)
        {
            await _localStorage.SetItemAsync<bool>("DarkTheme", value);
        }

        public async Task<bool> GetHighContrast()
        {
            if (await _localStorage.ContainKeyAsync("HighContrast"))
            {
                return await _localStorage.GetItemAsync<bool>("HighContrast");
            }
            else
            {
                await _localStorage.SetItemAsync<bool>("HighContrast", false);
                return await _localStorage.GetItemAsync<bool>("HighContrast");
            }
        }

        public async Task SetHighContrast(bool value)
        {
            await _localStorage.SetItemAsync<bool>("HighContrast", value);
        }

        public async Task<List<List<string>>> GetRowShow()
        {
            var test = await _localStorage.ContainKeyAsync("RowShow");

            if (await _localStorage.ContainKeyAsync("RowShow"))
            {
                List<List<string>> rowShow = await _localStorage.GetItemAsync<List<List<string>>>("RowShow");
                return rowShow;
            }
            else
            {
                List<List<string>> RowShows = new();
                for (int i = 0; i < 6; i++)
                {
                    RowShows.Add(new List<string>()
                    {
                        "",
                        "",
                        "",
                        "",
                        "",
                    });
                }
                await _localStorage.SetItemAsync<List<List<string>>>("RowShow", RowShows);
                List<List<string>> rowShow = await _localStorage.GetItemAsync<List<List<string>>>("RowShow");
                return rowShow;
            }

        }

        public async Task<TimeSpan> GetTime()
        {
            
            if (await _localStorage.ContainKeyAsync("Time"))
            {
                return await _localStorage.GetItemAsync<TimeSpan>("Time");
            }
            else
            {
                TimeSpan untilMidnight = DateTime.Today.AddDays(1.0) - DateTime.Now;
                await _localStorage.SetItemAsync<TimeSpan>("Time", untilMidnight);
                return await _localStorage.GetItemAsync<TimeSpan>("Time");
            }
        }

        public async Task SetTime()
        {
            TimeSpan untilMidnight = DateTime.Today.AddDays(1.0) - DateTime.Now;
            await _localStorage.SetItemAsync<TimeSpan>("Time", untilMidnight);
        }

        public async Task SetRowShow(int row, int colum, string State)
        {
            List<List<string>> rowShow = await _localStorage.GetItemAsync<List<List<string>>>("RowShow");
            rowShow[row][colum] = State;
            await _localStorage.SetItemAsync<List<List<string>>>("RowShow", rowShow);
        }

        public async Task<List<Key>> GetKeys()
        {
            if (await _localStorage.ContainKeyAsync("Keys"))
            {
                return await _localStorage.GetItemAsync<List<Key>>("Keys");
            }
            else
            {
                List<Key> Keys = new();
                await _localStorage.SetItemAsync<List<Key>>("Keys", Keys);
                return await _localStorage.GetItemAsync<List<Key>>("Keys");
            }
        }

        public async Task SetKey(Key key)
        {
            var Keys = await _localStorage.GetItemAsync<List<Key>>("Keys");
            if (Keys.Find(k => k.Letter == key.Letter) == null)
            {
                Keys.Add(key);
            }
            if (Keys.Find(k => k.Letter == key.Letter) != null)
            {
                var letter = Keys.Find(k => k.Letter == key.Letter);
                if (letter.State != "correct")
                {
                    Keys.Find(k => k.Letter == key.Letter).State = key.State;
                }
            }

            await _localStorage.SetItemAsync<List<Key>>("Keys", Keys);
        }

        public async Task<string> GetcheckString()
        {
            return await _localStorage.GetItemAsync<string>("checkString");
        }

        public async Task SetcheckString(string checkString)
        {
            await _localStorage.SetItemAsync<string>("checkString", checkString);
        }
        public string Word { get; set; } = "cunts";

        public async Task<bool> checkWord(int row)
        {
            string checkString = await GetcheckString();
            await GetRowShow();
            bool sloved = true;

            List<char> found = new List<char>();
            for (int i = 0; i < checkString.Length; i++)
            {
                if (checkString[i] == Word[i])
                {
                    found.Add(Word[i]);
                    await SetRowShow(row, i, "correct");
                    List<List<string>> StateRowShow = await GetRowShow();
                    SetKey(new Key { Letter = checkString[i].ToString(), State = StateRowShow[row][i] });
                }
            }
            for (int i = 0; i < checkString.Length; i++)
            {
                var amountOfLetterinFound = found.FindAll(l => l == checkString[i]).Count;
                var amountOfLetterinWord = Word.ToList().FindAll(l => l == checkString[i]).Count;
                List<List<string>> StateRowShow = await GetRowShow();
                if (StateRowShow[row][i] != "correct")
                {
                    if (Word.Contains(checkString[i]) && (!found.Contains(checkString[i]) || amountOfLetterinFound < amountOfLetterinWord))
                    {
                        found.Add(checkString[i]);
                        await SetRowShow(row, i, "present");
                        List<List<string>> StateRowShowpresent = await GetRowShow();
                        var test = StateRowShowpresent[row][i];
                        SetKey(new Key { Letter = checkString[i].ToString(), State = test });
                        sloved = false;
                    }
                    else
                    {
                        await SetRowShow(row, i, "absent");
                        List<List<string>> StateRowShowabsent = await GetRowShow();
                        var test = StateRowShowabsent[row][i];
                        SetKey(new Key { Letter = checkString[i].ToString(), State = test });
                        sloved = false;
                    }
                }
            }
            return sloved;
        }
    }
}
