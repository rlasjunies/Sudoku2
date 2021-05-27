using Fluxor;
using Sudoku.Board;
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
                boardHistory = Array.Empty<SudokuBoard>(),
                rowSolved = -1,
                colSolved = -1,
                blockSolved = -1,
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
