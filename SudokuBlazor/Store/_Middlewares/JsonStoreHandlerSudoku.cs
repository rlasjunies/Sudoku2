//using Fluxor.Persist.Middleware;
//using Microsoft.Extensions.Logging;
//using System;
////using System.Text.Json;
////using System.Text.Json.Serialization;
//using Newtonsoft.Json;
//using System.Threading.Tasks;
//using Fluxor.Persist.Storage;

//namespace Sudoku.Store.Middlewares
//{
//    public class JsonStoreHandlerSudoku : IStoreHandler
//    {
//        private IStringStateStorage LocalStorage;
//        private ILogger<PersistMiddleware> Logger;
//        public JsonStoreHandlerSudoku(IStringStateStorage localStorage, ILogger<PersistMiddleware> logger)
//        {
//            LocalStorage = localStorage;
//            Logger = logger;
//        }

//        public async Task<object> GetState(IFeature feature)
//        {
//            Logger?.LogDebug($"Rehydrating state {feature.GetName()}");
//            string json = await LocalStorage.GetStateJsonAsync(feature.GetName());
//            if (json == null)
//            {
//                Logger?.LogDebug($"No saved state for {feature.GetName()}, skipping");
//            }
//            else
//            {
//                Logger?.LogDebug($"wwwwwwwwDeserializing type {feature.GetStateType().ToString()} from json {json}");
//                Logger?.LogDebug($"Deserializing type {feature.GetStateType().ToString()}");
//                try
//                {

//                    // var readOnlySpan = new ReadOnlySpan<byte>(json);
//                    // weatherForecast = JsonSerializer.Deserialize<WeatherForecast>(readOnlySpan);

//                    object stronglyTypedFeatureState = JsonSerializer.Deserialize(
//                        json,
//                        feature.GetStateType());
//                    if (stronglyTypedFeatureState == null)
//                    {
//                        Logger?.LogError($"Deserialize returned null");
//                    }
//                    else
//                        // Now set the feature's state to the deserialized object
//                        return stronglyTypedFeatureState;
//                }
//                catch (Exception ex)
//                {
//                    Logger?.LogError("Failed to deserialize state. Skipping. Error:" + ex.ToString());
//                }
//            }
//            return feature.GetState(); //get initial state
//        }

//        public async Task SetState(IFeature feature)
//        {
//            var state = feature.GetState();
//            //var options = new JsonSerializerOptions
//            //{
//            //    WriteIndented = false,
//            //    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
//            //};
//            //string serializedState = JsonSerializer.Serialize(state, options);
//            string serializedState = JsonSerializer.Serialize(state);
//            await LocalStorage.StoreStateJsonAsync(feature.GetName(), serializedState);
//        }
//    }
//}
