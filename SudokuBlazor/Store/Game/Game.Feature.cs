using Fluxor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sudoku.Store.Game
{
    public class Feature : Feature<StateGame>
    {
        public override string GetName() => nameof(StateGame);
        protected override StateGame GetInitialState() =>
            new StateGame
            {
                board = null,
                boardHistory = new List<Board.SudokuBoard>(),
                rowSolved = 0,
                colSolved = 0,
                blockSolved = 0,
                boardSolved = false,
                boardLevel = Board.SudokuLevelType.easy,
                cellSelected = -1,
                draftMode = false,
                gameOnGoing = false,
                lastCellOfTheGame = -1,
                boardJustFinish = false,
                gameInPause = false,
                timer = 0,
                timerOn = true,
                solutionsByRules = new Board.SolutionByRules()
                {
                    uniqueOccurrenceInZone = new List<Board.Solution>(),
                    uniquePossibleValue = new List<Board.Solution>(),
                },
                wizardConfiguration = new Board.SudokuWizardConfiguration
                {
                    calculatePossibleValues = false,
                    showUniquePossibleValueInZones = false,
                    showUniquePossibleValueInRowOrColumn = false,
                    showErrornousCells = true,
                    showIdenticalNumber = true
                }
			};
	}
}
