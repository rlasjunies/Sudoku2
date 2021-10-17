using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sudoku.Board;

namespace Sudoku.Store.Game
{
    public static class Const
    {
        // TODO candidate to move to OneOf<NotCelleSelected, or [0-80]
        public const int NoCellSelected = -1;
        public const int NoValueInTheCell = 0;
        public const int NothingSolved = -1;
    }
    public record StateGame
    {
        public SudokuBoard board;
        public SudokuBoard[] boardHistory;
        public SudokuLevelType boardLevel;
        public int cellSelected;
        public bool boardJustFinish;
        public bool draftMode;
        public int rowSolved;
        public int colSolved;
        public int blockSolved;
        public bool boardSolved;
        public bool gameOnGoing;
        public int lastCellOfTheGame;
        public bool gameInPause;
        public int timer; //seconds
        public bool timerOn;
        public SolutionByRules solutionsByRules;
        public SudokuWizardConfiguration wizardConfiguration;
    }
}


