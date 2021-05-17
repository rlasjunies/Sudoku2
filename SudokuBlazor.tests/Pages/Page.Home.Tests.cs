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

namespace Sudoku.Pages.Tests
{

    [Collection("Sequential")]
    public class HomeTests : TestContext, IDisposable
    {
        private readonly ITestOutputHelper output;
        private readonly IStore store;
        private readonly IActionSubscriber actionSubscriber;
        private readonly IDispatcher dispatcher;

        public HomeTests(ITestOutputHelper output)
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

        [Fact(DisplayName = "Should load home page")]
        public void ShouldLoadHomePage()
        {
            // Act
            var cut = RenderComponent<Page_Home>();

            // Assert that create new board is present
            cut.Dispose();
        }

        [Fact(DisplayName = "Should have only create new board button")]
        public void ShouldHaveOnlyCreateNewBoardButton()
        {

            // Act
            var cut = RenderComponent<Page_Home>();

            // Assert
            var buttons = cut.FindAll("button");
            Assert.Equal(1, buttons.Count);
     
            cut.Dispose();
        }

        [Fact(DisplayName = "Should have 2 buttons when game is on going")]
        public void ShouldHave2ButtonsIfGameIsOnGoing()
        {

            // Arrange
            dispatcher.Dispatch(new GenerateBoard { level = SudokuLevelType.easy});

            // Act
            var cut = RenderComponent<Page_Home>();

            // Assert that create new board is present
            var buttons = cut.FindAll("button");
            Assert.Equal(2, buttons.Count);

            cut.Dispose();
        }

        [Fact(DisplayName = "Should have 2 buttons New board and continue")]
        public void ShouldHave2ButtonsNewBoardAndContinue()
        {

            // Arrange
            dispatcher.Dispatch(new GenerateBoard { level = SudokuLevelType.easy });

            // Act
            var cut = RenderComponent<Page_Home>();

            // Assert that create new board is present
            var button1 = cut.Find(PageHomeHelper.ContinueButtonId);
            var button2 = cut.Find(PageHomeHelper.CreateNewBoardButtonId);

            Assert.Equal("Continue", button1.InnerHtml);
            Assert.Equal("New Game", button2.InnerHtml);
            
            cut.Dispose();
        }

        [Fact(DisplayName = "Should navigate to new Board when clicking on 'New Game'")]
        public void ShouldNavigateToNewBoardWhenClickingOnNewBoard()
        {
            // Arrange
            var routingFeature = store.Features["@routing"];
            
            // url before
            var routingState1 = routingFeature.GetState() as RoutingState;
            var initialUri = routingState1.Uri;

            // retrieve the button
            var cut = RenderComponent<Page_Home>();
            var button = cut.Find(PageHomeHelper.CreateNewBoardButtonId);

            // Act
            button.Click();

            // url after
            var routingState2 = routingFeature.GetState() as RoutingState;
            var uriAfterClick = routingState2.Uri;

            // Assert 

            /// navigation done
            Assert.NotEqual(uriAfterClick, initialUri);

            /// navigate to NewBoard
            Assert.EndsWith(Sudoku.Pages.Pages.NewBoard, uriAfterClick);

            cut.Dispose();
        }
    }
}
