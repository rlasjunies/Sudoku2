using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Sudoku.Store.About.Effects
{
    public class RetrieveAssemblyInformation
    {

        // public RetrieveAssemblyInformation(HttpClient http)
        // {
        //     Http = http;
        // }

        [EffectMethod(typeof(Actions.RetrieveAboutInformation))]
        public async Task RetrieveAboutInformation(IDispatcher dispatcher)
        {

            // var infoVersion = Assembly
            //                 .GetExecutingAssembly()
            //                 .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            //                 .InformationalVersion;

            var FileVersion = Assembly
                            .GetExecutingAssembly()
                            .GetCustomAttribute<AssemblyFileVersionAttribute>()
                            .Version;

            dispatcher.Dispatch(new Actions.AboutInformationRetrieved
            {
                Version = FileVersion
            });
            await Task.CompletedTask;
        }
    }
}
