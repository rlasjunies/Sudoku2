using System;
using Bunit;
using Fluxor;
using Fluxor.Blazor.Web.Middlewares.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using Sudoku.Board;
using Sudoku.Store.Game;
using Sudoku.Store.Game.Actions;
using Xunit;
using Xunit.Abstractions;

namespace Sudoku.Pages.Tests
{

    [Collection("Sequential")]
    public class NewBoardTests : TestContext, IDisposable
    {
        private readonly ITestOutputHelper output;
        private readonly IStore store;
        private readonly IActionSubscriber actionSubscriber;
        private readonly IDispatcher dispatcher;

        public NewBoardTests(ITestOutputHelper output)
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

        [Fact(DisplayName = "Should load new board page")]
        public void should_load_new_board_page()
        {
            // Act
            var cut = RenderComponent<Page_NewBoard>();

            // Assert that create new board is present
            cut.Dispose();
        }

        [Fact(DisplayName = "Should have 5 only buttons")]
        public void should_have_5_only_buttons()
        {

            // Arrange

            // Act
            var cut = RenderComponent<Page_NewBoard>();

            // Assert that create new board is present
            var buttons = cut.FindAll("button");
            Assert.Equal(5, buttons.Count);

            cut.Dispose();
        }

        [Fact(DisplayName = "Should navigate to 'Home' when clicking on 'Back'")]
        public void should_navigate_to_home_when_clicking_on_back()
        {
            // Arrange
            var routingFeature = store.Features["@routing"];

            // url before
            var routingState1 = routingFeature.GetState() as RoutingState;
            var initialUri = routingState1.Uri;

            // retrieve the button
            var cut = RenderComponent<Page_NewBoard>();
            var buttonBack = cut.Find(PageNewBoardHelper.BackButtonId);

            // Act
            buttonBack.Click();

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


        [Fact(DisplayName = "Should have 4 buttons to create New board")]
        public void should_have_4_buttons_to_create_new_board()
        {

            // Arrange

            // Act
            var cut = RenderComponent<Page_NewBoard>();

            // Assert that create new board is present
            var buttonEasy = cut.Find(PageNewBoardHelper.CreateEasyButtonId);
            var buttonMedium = cut.Find(PageNewBoardHelper.CreateMediumButtonId);
            var buttonComplex = cut.Find(PageNewBoardHelper.CreateExpertButtonId);
            var buttonVeryComplex = cut.Find(PageNewBoardHelper.CreateMasterButtonId);

            Assert.Equal("Easy", buttonEasy.InnerHtml);
            Assert.Equal("Medium", buttonMedium.InnerHtml);
            Assert.Equal("Expert", buttonComplex.InnerHtml);
            Assert.Equal("Master", buttonVeryComplex.InnerHtml);

            cut.Dispose();
        }

        [Fact(DisplayName = "Should navigate to Sudoku and level should be easy when clicking on easy")]
        public void should_navigate_to_sudoku_and_level_should_be_easy_when_clicking_on_easy()
        {
            // Arrange
            var routingFeature = store.Features["@routing"];
            var gameFeature = store.Features["StateGame"];
            // url before
            var routingState1 = routingFeature.GetState() as RoutingState;
            var initialUri = routingState1.Uri;

            // retrieve the button
            var cut = RenderComponent<Page_NewBoard>();
            var buttonEasy = cut.Find(PageNewBoardHelper.CreateEasyButtonId);

            // Act
            buttonEasy.Click();

            // url after
            var routingState2 = routingFeature.GetState() as RoutingState;
            var uriAfterClick = routingState2.Uri;

            // Game level
            var gameState = gameFeature.GetState() as StateGame;

            // Assert 

            /// navigation done
            Assert.NotEqual(uriAfterClick, initialUri);

            /// navigate to Home
            Assert.EndsWith(Sudoku.Pages.Pages.Sudoku, uriAfterClick);

            /// check level of the board
            Assert.Equal(Sudoku.Board.SudokuLevelType.easy, gameState.boardLevel);

            cut.Dispose();
        }

        [Fact(DisplayName = "Should navigate to 'Sudoku' and level should be medium when clicking on 'medium'")]
        public void should_navigate_to_sudoku_and_level_should_be_medium_when_clicking_on_medium()
        {
            // Arrange
            var routingFeature = store.Features["@routing"];
            var gameFeature = store.Features["StateGame"];
            // url before
            var routingState1 = routingFeature.GetState() as RoutingState;
            var initialUri = routingState1.Uri;

            // retrieve the button
            var cut = RenderComponent<Page_NewBoard>();
            var buttonMedium = cut.Find(PageNewBoardHelper.CreateMediumButtonId);

            // Act
            buttonMedium.Click();

            // url after
            var routingState2 = routingFeature.GetState() as RoutingState;
            var uriAfterClick = routingState2.Uri;

            // Game level
            var gameState = gameFeature.GetState() as StateGame;

            // Assert 

            /// navigation done
            Assert.NotEqual(uriAfterClick, initialUri);

            /// navigate to Home
            Assert.EndsWith(Sudoku.Pages.Pages.Sudoku, uriAfterClick);

            /// check level of the board
            Assert.Equal(Sudoku.Board.SudokuLevelType.medium, gameState.boardLevel);

            cut.Dispose();
        }


    }
}
