using System;
using AngleSharp.Dom;
using Bunit;
using Fluxor;
using Fluxor.Blazor.Web.Middlewares.Routing;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using Sudoku.Board;
using Xunit;
using Xunit.Abstractions;
using Store = Sudoku.Store.Game;

namespace Sudoku.Pages.Tests
{

    [Collection("Sequential")]
    public class ConfigTests : TestContext, IDisposable
    {
        private readonly ITestOutputHelper output;
        private readonly IStore store;
        private readonly IActionSubscriber actionSubscriber;
        private readonly IDispatcher dispatcher;

        public ConfigTests(ITestOutputHelper output)
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

        [Fact(DisplayName = "Should load 'Config' page")]
        public void should_load_config_page()
        {
            // Act
            var cut = RenderComponent<Page_Config>();

            // Assert that create new board is present
            cut.Dispose();
        }

        [Fact(DisplayName = "Should have 1 button and 3 checkbox")]
        public void should_have_1_button_and_3_checkbox()
        {

            // Arrange

            // Act
            var cut = RenderComponent<Page_Config>();
            var buttons = cut.FindAll("button");
            var checkboxes = cut.FindAll("input");

            // Assert that create new board is present
            Assert.Equal(1, buttons.Count);
            Assert.Equal(3, checkboxes.Count);

            cut.Dispose();
        }

        // TODO wait for the correct back behavior, back should bring to the previous screen.
        // which is not always Home
        [Fact(DisplayName = "Should navigate back when clicking on 'Back'")]
        public void should_navigate_back_when_clicking_on_back()
        {
            // retrieve the button
            var cut = RenderComponent<Page_Config>();

            // url before
            var routingFeature = store.Features["@routing"];
            var routingState1 = routingFeature.GetState() as RoutingState;
            var initialUri = routingState1.Uri;

            // Act
            var buttonBack = cut.Find(PageConfigHelper.BackButtonId);
            buttonBack.Click();

            // url after
            var routingState2 = routingFeature.GetState() as RoutingState;
            var uriAfterClick = routingState2.Uri;

            // Assert 

            /// navigation done
            Assert.NotEqual(uriAfterClick, initialUri);

            /// navigate to Home
            Assert.EndsWith("", uriAfterClick);

            cut.Dispose();
        }


        ///////
        /////// ShowIdenticalNumber
        ///////
        [Fact(DisplayName = "Should have checkbox showIdenticalNumber checked when state is true")]
        public void should_have_checkbox_showIdenticalNumber_checked_when_state_is_true()
        {
            BeSureStateIsTrue();

            var cut = RenderComponent<Page_Config>();

            var ShowIdenticalCheckBoxElt = cut.Find(PageConfigHelper.ShowIdenticalNumberId);

            Assert.True(Helpers.IsCheckBoxChecked(ShowIdenticalCheckBoxElt));
            cut.Dispose();

            void BeSureStateIsTrue()
            {
                var gameFeature = store.Features["StateGame"];
                var gameState = gameFeature.GetState() as Store::StateGame;

                bool StateIsFalse = !gameState.wizardConfiguration.showIdenticalNumber;
                if (StateIsFalse) dispatcher.Dispatch(new Store::Actions.ToggleShowIdenticalNumber() { });
            }
        }

        [Fact(DisplayName = "Should have checkbox showIdenticalNumber unchecked when state is false")]
        public void should_have_checkbox_showIdenticalNumber_unchecked_when_state_is_false()
        {

            BeSureStateIsFalse();

            var cut = RenderComponent<Page_Config>();

            var ShowIdenticalcheckBoxElt = cut.Find(PageConfigHelper.ShowIdenticalNumberId);

            Assert.False(Helpers.IsCheckBoxChecked(ShowIdenticalcheckBoxElt));
            cut.Dispose();

            void BeSureStateIsFalse()
            {
                var gameFeature = store.Features["StateGame"];
                var gameState = gameFeature.GetState() as Store::StateGame;

                if (gameState.wizardConfiguration.showIdenticalNumber) dispatcher.Dispatch(new Store::Actions.ToggleShowIdenticalNumber() { });
            }
        }

        [Fact(DisplayName = "Should have state toggled when clicked on ShowIdenticalNumber checkbox")]
        public void should_have_state_toggled_when_clicked_on_showidenticalnumber_checkbox()
        {

            var gameFeature = store.Features["StateGame"];

            var initialValue = CurrentStateValueToEvaluate(gameFeature);

            var cut = RenderComponent<Page_Config>();

            var ShowIdenticalcheckBoxElt = cut.Find(PageConfigHelper.ShowIdenticalNumberId);
            Helpers.ToggleTheCheckBox(ShowIdenticalcheckBoxElt);

            var newValue = CurrentStateValueToEvaluate(gameFeature);

            Assert.Equal(!initialValue, newValue);
            cut.Dispose();

            static bool CurrentStateValueToEvaluate(IFeature gameFeature)
            {
                var gameState = gameFeature.GetState() as Store::StateGame;
                return gameState.wizardConfiguration.showIdenticalNumber;
            }

        }

        ///////
        /////// ShowErrornousCell
        ///////
        [Fact(DisplayName = "Should have checkbox ShowErrornousCell checked when state is true")]
        public void should_have_checkbox_ShowErrornousCell_checked_when_state_is_true()
        {
            BeSureStateIsTrue();

            var cut = RenderComponent<Page_Config>();

            var ShowErrornousCellCheckBoxElt = cut.Find(PageConfigHelper.ShowErrornousCellId);

            Assert.True(Helpers.IsCheckBoxChecked(ShowErrornousCellCheckBoxElt));
            cut.Dispose();

            void BeSureStateIsTrue()
            {
                var gameFeature = store.Features["StateGame"];
                var gameState = gameFeature.GetState() as Store::StateGame;

                bool StateIsFalse = !gameState.wizardConfiguration.showErrornousCells;
                if (StateIsFalse) dispatcher.Dispatch(new Store::Actions.ToggleShowErrornousCells() { });
            }
        }

        [Fact(DisplayName = "Should have checkbox ShowErrornousCell unchecked when state is false")]
        public void should_have_checkbox_ShowErrornousCell_unchecked_when_state_is_false()
        {

            BeSureStateIsFalse();

            var cut = RenderComponent<Page_Config>();

            var ShowErrornousCellcheckBoxElt = cut.Find(PageConfigHelper.ShowErrornousCellId);

            Assert.False(Helpers.IsCheckBoxChecked(ShowErrornousCellcheckBoxElt));
            cut.Dispose();

            void BeSureStateIsFalse()
            {
                var gameFeature = store.Features["StateGame"];
                var gameState = gameFeature.GetState() as Store::StateGame;

                if (gameState.wizardConfiguration.showErrornousCells) dispatcher.Dispatch(new Store::Actions.ToggleShowErrornousCells() { });
            }
        }

        [Fact(DisplayName = "Should have state toggled when clicked on ShowErrornousCell checkbox")]
        public void should_have_state_toggled_when_clicked_on_ShowErrornousCell_checkbox()
        {

            var gameFeature = store.Features["StateGame"];

            var initialValue = CurrentStateValueToEvaluate(gameFeature);

            var cut = RenderComponent<Page_Config>();

            var ShowErrornousCellCheckBoxElt = cut.Find(PageConfigHelper.ShowErrornousCellId);
            Helpers.ToggleTheCheckBox(ShowErrornousCellCheckBoxElt);

            var newValue = CurrentStateValueToEvaluate(gameFeature);

            Assert.Equal(!initialValue, newValue);
            cut.Dispose();

            static bool CurrentStateValueToEvaluate(IFeature gameFeature)
            {
                var gameState = gameFeature.GetState() as Store::StateGame;
                return gameState.wizardConfiguration.showErrornousCells;
            }
        }
        ///////
        /////// HighlightUniqueCandidateInRowColZone
        ///////
        [Fact(DisplayName = "Should have checkbox HighlightUniqueCandidateInRowColZone checked when state is true")]
        public void should_have_checkbox_HighlightUniqueCandidateInRowColZone_checked_when_state_is_true()
        {
            BeSureStateIsTrue();

            var cut = RenderComponent<Page_Config>();

            var HighlightUniqueCandidateInRowColZoneCheckBoxElt = cut.Find(PageConfigHelper.HighlightUniqueCandidateInRowColZoneId);

            Assert.True(Helpers.IsCheckBoxChecked(HighlightUniqueCandidateInRowColZoneCheckBoxElt));
            cut.Dispose();

            void BeSureStateIsTrue()
            {
                var gameFeature = store.Features["StateGame"];
                var gameState = gameFeature.GetState() as Store::StateGame;

                bool StateIsFalse = !gameState.wizardConfiguration.showUniquePossibleValueInRowOrColumn;
                if (StateIsFalse) dispatcher.Dispatch(new Store::Actions.ToggleHighlightCellWithUniqueCandidate() { });
            }
        }

        [Fact(DisplayName = "Should have checkbox HighlightUniqueCandidateInRowColZone unchecked when state is false")]
        public void should_have_checkbox_HighlightUniqueCandidateInRowColZone_unchecked_when_state_is_false()
        {

            BeSureStateIsFalse();

            var cut = RenderComponent<Page_Config>();

            var HighlightUniqueCandidateInRowColZoneCheckBoxElt = cut.Find(PageConfigHelper.HighlightUniqueCandidateInRowColZoneId);

            Assert.False(Helpers.IsCheckBoxChecked(HighlightUniqueCandidateInRowColZoneCheckBoxElt));
            cut.Dispose();

            void BeSureStateIsFalse()
            {
                var gameFeature = store.Features["StateGame"];
                var gameState = gameFeature.GetState() as Store::StateGame;

                if (gameState.wizardConfiguration.showUniquePossibleValueInRowOrColumn) dispatcher.Dispatch(new Store::Actions.ToggleHighlightCellWithUniqueCandidate() { });
            }
        }

        [Fact(DisplayName = "Should have state toggled when clicked on HighlightUniqueCandidateInRowColZone checkbox")]
        public void should_have_state_toggled_when_clicked_on_HighlightUniqueCandidateInRowColZone_checkbox()
        {

            var gameFeature = store.Features["StateGame"];

            var initialValue = CurrentStateValueToEvaluate(gameFeature);

            var cut = RenderComponent<Page_Config>();

            var ShowErrornousCellCheckBoxElt = cut.Find(PageConfigHelper.HighlightUniqueCandidateInRowColZoneId);
            Helpers.ToggleTheCheckBox(ShowErrornousCellCheckBoxElt);

            var newValue = CurrentStateValueToEvaluate(gameFeature);

            Assert.Equal(!initialValue, newValue);
            cut.Dispose();

            static bool CurrentStateValueToEvaluate(IFeature gameFeature)
            {
                var gameState = gameFeature.GetState() as Store::StateGame;
                return gameState.wizardConfiguration.showUniquePossibleValueInRowOrColumn;
            }

        }

    }
}
