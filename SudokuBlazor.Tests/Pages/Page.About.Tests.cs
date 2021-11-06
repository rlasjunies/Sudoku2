using Bunit;
using Fluxor;
using Fluxor.Blazor.Web.Middlewares.Routing;
using Microsoft.AspNetCore.Components;
using Sudoku.Board;
using Sudoku.Store.Game;
using Sudoku.Store.Game.Actions;
using System;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Sudoku.Pages.Tests
{

    public class AboutTests : TestContext, IDisposable
    {
        private readonly ITestOutputHelper output;
        private readonly IStore store;
        private readonly IActionSubscriber actionSubscriber;
        private readonly IDispatcher dispatcher;

        public AboutTests(ITestOutputHelper output)
        {
            this.output = output;

            // return the assembly of the tested program.
            // Take care of the using, that should return an namespace with at least one Program
            var assembly = typeof(Program).Assembly;

            // Store initialization
            this.Services.AddFluxor(config =>
            {
                config
                    .ScanAssemblies(assembly)
                    .UseRouting();
                    //.AddMiddleware<LoggingMiddleware>();
            });
            this.Services.AddLogging(builder => builder
                //.AddBrowserConsole()
                .SetMinimumLevel(LogLevel.Trace)
             );
            this.store = this.Services.GetService(typeof(Fluxor.Store)) as IStore;
            this.actionSubscriber = this.Services.GetService(typeof(Fluxor.IActionSubscriber)) as IActionSubscriber;
            this.dispatcher = this.Services.GetService(typeof(Fluxor.IDispatcher)) as IDispatcher;
            this.store.InitializeAsync();
        }

        // public void Dispose()
        // {
        //     this.store = null;
        //     base.Dispose();
        // }

        [Fact(DisplayName = "Should load about page")]
        public void ShouldLoadAboutPage()
        {
            // Act
            var cut = RenderComponent<Page_About>();

            // Assert that create new board is present
            cut.Dispose();
        }

        [Fact(DisplayName = "Should have only back button")]
        public void ShouldHaveOnlyBackButton()
        {

            // Act
            var cut = RenderComponent<Page_About>();

            // Assert
            var buttons = cut.FindAll("button");
            Assert.Equal(1, buttons.Count);
     
            cut.Dispose();
        }

        [Fact(DisplayName = "Should navigate to 'Home' when clicking on 'Back'")]
        public void ShouldNavigateToHomeWhenClickingOnBack()
        {
            // Arrange
            var routingFeature = store.Features["@routing"];
            
            // url before
            var routingState1 = routingFeature.GetState() as RoutingState;
            var initialUri = routingState1.Uri;

            // retrieve the button
            var cut = RenderComponent<Page_About>();
            var button = PageAboutHelper.BackButton(cut);

            // Act
            button.Click();

            // url after
            var routingState2 = routingFeature.GetState() as RoutingState;
            var uriAfterClick = routingState2.Uri;

            // Assert 

            /// navigation done
            Assert.NotEqual(uriAfterClick, initialUri);

            /// navigate to Home
            Assert.EndsWith(Sudoku.Pages.Pages.Home, uriAfterClick);

            cut.Dispose();
        }
    }
}
