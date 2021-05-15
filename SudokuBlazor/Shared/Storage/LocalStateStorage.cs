using Blazored.LocalStorage;
using Fluxor.Persist.Storage;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sudoku.Shared.Storage
{
    public class LocalStateStorageSudoku : IStringStateStorage
    {

        private ILocalStorageService LocalStorage { get; set; }
        private readonly ILogger<LocalStateStorageSudoku> _logger;
        public LocalStateStorageSudoku(ILocalStorageService localStorage, ILogger<LocalStateStorageSudoku> logger)
        {
            LocalStorage = localStorage;
            _logger = logger;
        }

        public async ValueTask<string> GetStateJsonAsync(string statename)
        {
            // string readstring = await LocalStorage.GetItemAsStringAsync(statename); ... bug?
            var readstring = await LocalStorage.GetItemAsync<string>(statename);
            _logger.LogDebug($"get store key:@{statename} - value@{readstring}");
            return readstring;
        }

        public async ValueTask StoreStateJsonAsync(string statename, string json)
        {
            await LocalStorage.SetItemAsync(statename, json);
            _logger.LogDebug($"store key:@{statename} - value@{json}");
        }
    }
}

