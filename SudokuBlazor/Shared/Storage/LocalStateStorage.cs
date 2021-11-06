using Blazored.LocalStorage;
using Fluxor.Persist.Storage;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sudoku.Shared.Storage
{
    public class LocalStateStorageSudoku : IStringStateStorage
    {

        private ILocalStorageService _localstorage { get; set; }
        private readonly ILogger<LocalStateStorageSudoku> _logger;

        public LocalStateStorageSudoku(ILocalStorageService localstorage, ILogger<LocalStateStorageSudoku> logger)
        {
            _localstorage = localstorage;
            _logger = logger;
        }

        public async ValueTask<string> GetStateJsonAsync(string statename)
        {
            // string readstring = await localstorage.getitemasstringasync(statename); ... bug?
            var readstring = await _localstorage.GetItemAsync<string>(statename);
            //_logger.LogDebug($"get store key:@{statename} - value@{readstring}");
            return readstring;
        }

        public async ValueTask StoreStateJsonAsync(string statename, string json)
        {
            await _localstorage.SetItemAsync(statename, json);
            _logger.LogDebug($"store key:@{statename} - value@{json}");
        }

    }
}

