/*************************************************************/
/*                                                           */
/*  Course: CIS 430 - Artificial Intelligence                */
/*                                                           */
/*  Project: Ch4Prb2.CSPrj                                   */
/*                                                           */
/*  Source File: ConnectFour.CS                              */
/*                                                           */
/*  Programmer: Rocky Lowery                                 */
/*                                                           */
/*  Classes: 1. Partial Ch4Smp1Form : Form                   */
/*           2. BoardElement                                 */
/*           3. Player                                       */
/*           4. Location                                     */
/*                                                           */
/*************************************************************/

using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Media;

/************************************************************/
/*  Begin partial namespace Ch4Prb2                         */
/************************************************************/
namespace Ch4Prb2
{
  /************************************************************/
  /*  1. Begin main form partial class Ch4Prb2Form : Form     */
  /************************************************************/
  public partial class Ch4Prb2 : Form
  {

    /************************************************************/
    /*  Partial Auxillary Methods                               */
    /************************************************************/
    private bool IsAWin(char token)
    {
      bool meetsTest = false;

      int row, col, numOfMatches, tracker;

      row = 0;  // Check each row
      while (!meetsTest && ++row <= 7)
      {
        numOfMatches = 0;
        col = 0;
        while (++col <= 7)
        {
          if (board[row, col].Token == token)
            numOfMatches++;
          else if (numOfMatches < 4)
            numOfMatches = 0;
        }
        meetsTest = (numOfMatches >= 4);
      }
      col = 0;  // Check each column
      while (!meetsTest && ++col <= 7)
      {
        numOfMatches = 0;
        row = 0;
        while (++row <= 7)
        {
          if (board[row, col].Token == token)
            numOfMatches++;
          else if (numOfMatches < 4)
            numOfMatches = 0;
        }
        meetsTest = (numOfMatches >= 4);
      }
      if (!meetsTest)  // Check lower-left to upper-right diagonal
      {
        numOfMatches = 0;
        row = 0;
        col = 0;
        tracker = 0;
        while (!meetsTest && row <=4)
        {
          while (!meetsTest && ++row <= 7 && ++col <= 7)
          {
            if (board[row, col].Token == token)
              numOfMatches++;
            else if (numOfMatches < 4)
              numOfMatches = 0;
          }
          meetsTest = (numOfMatches >= 4);
          tracker++;
          row = tracker;
          col = 0;
          numOfMatches = 0;
        }
        numOfMatches = 0;
        row = 0;
        col = 1;
        tracker = 0;
        while (!meetsTest && col <=4)
        {
          while (!meetsTest && ++row <= 7 && ++col <= 7)
          {
            if (board[row, col].Token == token)
              numOfMatches++;
            else if (numOfMatches < 4)
              numOfMatches = 0;
          }
          meetsTest = (numOfMatches >= 4);
          tracker++;
          row = 0;
          col = tracker;
          numOfMatches = 0;
        }
      }
      if (!meetsTest)   // Check lower-right to upper-left diagonal
      {
        numOfMatches = 0;
        row = 0;
        col = 8;
        tracker = 0;
        while (!meetsTest && row <= 4)
        {
          while (!meetsTest && ++row <= 7 && --col >= 1)
          {
            if (board[row, col].Token == token)
              numOfMatches++;
            else if (numOfMatches < 4)
              numOfMatches = 0;
          }
          meetsTest = (numOfMatches >= 4);
          tracker++;
          row = tracker;
          col = 8;
          numOfMatches = 0;
        }
        numOfMatches = 0;
        row = 0;
        col = 8;
        tracker = 8;
        while (!meetsTest && col >= 4)
        {
          while (!meetsTest && ++row <= 7 && --col >= 1)
          {
            if (board[row, col].Token == token)
              numOfMatches++;
            else if (numOfMatches < 4)
              numOfMatches = 0;
          }
          meetsTest = (numOfMatches >= 4);
          tracker--;
          row = 0;
          col = tracker;
          numOfMatches = 0;
        }
      }
      return meetsTest;
    }

    private bool IsATie()
    {
      bool meetsTest = true;

      int row, col;

      row = 0;
      while (meetsTest && ++row <= 7)
      {
        col = 0;
        while (meetsTest && ++col <= 7)
          if (board[row, col].Token == ' ')
            meetsTest = false;
      }
      return meetsTest;
    }

    private int EvaluateBoard(char token) 
    {
      int numOfXs, numOfOs, numOfBlanks, row, col;
      int boardValue = 0;

      for (row = 1; row <= 7; row++)  // Process rows
      {
        numOfXs = 0;
        numOfOs = 0;
        numOfBlanks = 0;
        for (col = 1; col <= 7; col++)
          switch (board[row, col].Token)
          {
            case 'X':
              numOfXs++;
              if (board[row, col].Token != board[row - 1, col].Token)
                numOfXs = 1;
              break;
            case 'O':
              numOfOs++;
              if (board[row, col].Token != board[row - 1, col].Token)
                numOfOs = 1;
              break;
            case ' ': numOfBlanks++; break;
          }
        if (numOfXs >= 4)
          numOfXs = 3;
        if (numOfOs >= 4)
          numOfOs = 3;
        if (numOfXs >= 1 && numOfXs + numOfBlanks >= 4)
          boardValue += weightArray[numOfXs];
        else if (numOfOs >= 1 && numOfOs + numOfBlanks >= 4)
          boardValue -= weightArray[numOfOs];
      }
      for (col = 1; col <= 7; col++)  // Process columns
      {
        numOfXs = 0;
        numOfOs = 0;
        numOfBlanks = 0;
        for (row = 1; row <= 7; row++)
          switch (board[row, col].Token)
          {
            case 'X':
              numOfXs++;
              if (board[row, col].Token != board[row, col - 1].Token)
                numOfXs = 1;
              break;
            case 'O':
              numOfOs++;
              if (board[row, col].Token != board[row, col - 1].Token)
                numOfOs = 1;
              break;
            case ' ': numOfBlanks++; break;
          }
        if (numOfXs >= 4)
          numOfXs = 3;
        if (numOfOs >= 4)
          numOfOs = 3;
        if (numOfXs >= 1 && numOfXs + numOfBlanks >= 4)
          boardValue += weightArray[numOfXs];
        else if (numOfOs >= 1 && numOfOs + numOfBlanks >= 4)
          boardValue -= weightArray[numOfOs];
      }
      numOfXs = 0;
      numOfOs = 0;
      numOfBlanks = 0;  // Process lower-left to upper-right diagonal
      row = 0;
      col = 0;
      while (++row <= 7 && ++col <= 7)
        switch (board[row, col].Token)
        {
          case 'X':
            numOfXs++;
            if (board[row, col].Token != board[row - 1, col - 1].Token)
              numOfXs = 1;
            break;
          case 'O':
            numOfOs++;
            if (board[row, col].Token != board[row - 1, col - 1].Token)
              numOfOs = 1; break;
          case ' ': numOfBlanks++; break;
        }
      if (numOfXs >= 4)
        numOfXs = 3;
      if (numOfOs >= 4)
        numOfOs = 3;
      if (numOfXs >= 1 && numOfXs + numOfBlanks >= 4)
        boardValue += weightArray[numOfXs];
      else if (numOfOs >= 1 && numOfOs + numOfBlanks >= 4)
        boardValue -= weightArray[numOfOs];
      numOfXs = 0;
      numOfOs = 0;
      numOfBlanks = 0;  // Process lower-right to upper-left diagonal
      row = 0;
      col = 6;
      while (++row <= 7 && --col >= 1)
        switch (board[row, col].Token)
        {
          case 'X':
            numOfXs++;
            if (board[row, col].Token != board[row - 1, col + 1].Token)
              numOfXs = 1; break;
          case 'O':
            numOfOs++;
            if (board[row, col].Token != board[row - 1, col + 1].Token)
              numOfOs = 1; break;
          case ' ': numOfBlanks++; break;
        }
      if (numOfXs >= 4)
        numOfXs = 3;
      if (numOfOs >= 4)
        numOfOs = 3;
      if (numOfXs >= 1 && numOfXs + numOfBlanks >= 4)
        boardValue += weightArray[numOfXs];
      else if (numOfOs >= 1 && numOfOs + numOfBlanks >= 4)
        boardValue -= weightArray[numOfOs];
      return boardValue;
    }

    private void ProcessMove(int col)
    {
      bool foundIt = false;
      int moveLocationIndex = 0;
      SoundPlayer soundPlayer = new SoundPlayer("Dispense.wav");

      if (this.ElapsedTime <= currentPlayer.MaxTime)
      {
        soundPlayer.Play();
        if (currentPlayer == computerPlayer) // simulate button click
        {
          tokenButtons[col].Height -= 2;
          Sleep(500);
          tokenButtons[col].Height += 2;
        }
        while ((!foundIt) && (++moveLocationIndex <= 7))
        {
          foundIt = (moveLocationArray[moveLocationIndex].Col == col);
        }
        row = moveLocationArray[moveLocationIndex].Row;
        board[row, col].Token = currentPlayer.Token;
        moveLocationArray[moveLocationIndex].Row++;
        DispenseToken(row, col);
        if (IsAWin(currentPlayer.Token))
          ProcessWin(row, col);
        else if (IsATie())
          ProcessTie();
        else
          SwitchPlayers();
      }
      else  // Current player timed out.  Game goes to opponent.
        ProcessExceededMaxTime();
    }

    private void ProcessWin(int moveRow, int moveCol)
    {
      SoundPlayer soundPlayer = new SoundPlayer("Win.wav");

      elapsedTimeTimer.Enabled = false;
      FindWinPositions(moveRow, moveCol);
      soundPlayer.Play();
      FlashWinPositions();
      MessageBox.Show("Winner: " + currentPlayer.Name, "Game", MessageBoxButtons.OK,
                      MessageBoxIcon.Exclamation);
      soundPlayer.Play();
      FlashWinPositions();
    }

    private void FindWinPositions(int moveRow, int moveCol)
    {
      bool foundIt = false;
      int row, col, offset, startRow, startCol, count;
      int moveLeftDiag, moveRightDiag;

      row = 1;
      while ((!foundIt) && (row <= 4))
      {
        count = 0;
        for (offset = 0; offset <= 3; offset++)
        {
          winLocationArray[offset + 1].Row = row + offset;
          winLocationArray[offset + 1].Col = moveCol;
          if (board[row + offset, moveCol].Token == currentPlayer.Token)
            count++;
        }
        foundIt = (count == 4);
        row++;
      }
      col = 1;
      while (!foundIt && col <= 4)
      {
        count = 0;
        for (offset = 0; offset <= 3; offset++)
        {
          winLocationArray[offset + 1].Row = moveRow;
          winLocationArray[offset + 1].Col = col + offset;
          if (board[moveRow, col + offset].Token == currentPlayer.Token)
            count++;
        }
        foundIt = (count == 4);
        col++;
      }
      moveLeftDiag = leftDiagMember[moveRow, moveCol];
      startRow = leftDiagLocationArray[moveLeftDiag].Row;
      startCol = leftDiagLocationArray[moveLeftDiag].Col;
      row = startRow; col = startCol;
      while (!foundIt && row <= 4 && col <= 4)
      {
        count = 0;
        for (offset = 0; offset <= 3; offset++)
        {
          winLocationArray[offset + 1].Row = row + offset;
          winLocationArray[offset + 1].Col = col + offset;
          if (board[row + offset, col + offset].Token == currentPlayer.Token)
            count++;
        }
        foundIt = (count == 4);
        row++; col++;
      }
      moveRightDiag = rightDiagMember[moveRow, moveCol];
      startRow = rightDiagLocationArray[moveRightDiag].Row;
      startCol = rightDiagLocationArray[moveRightDiag].Col;
      row = startRow; col = startCol;
      while (!foundIt && row <= 4 && col >= 4)
      {
        count = 0;
        for (offset = 0; offset <= 3; offset++)
        {
          winLocationArray[offset + 1].Row = row + offset;
          winLocationArray[offset + 1].Col = col - offset;
          if (board[row + offset, col - offset].Token == currentPlayer.Token)
            count++;
        }
        foundIt = (count == 4);
        row++; col--;
      }
    }

    private void FlashWinPositions()
    {
      int i, j, row, col;
      Rectangle cellRect;
      Graphics graphicsObj;
      SolidBrush brush;

      for (i = 1; i <= 3; i++)
      {
        for (j = 1; j <= 4; j++)
        {
          row = winLocationArray[j].Row;
          col = winLocationArray[j].Col;
          brush = new SolidBrush(SystemColors.Window);
          cellRect = board[row, col].Cell.ClientRectangle;
          graphicsObj = board[row, col].Cell.CreateGraphics();
          graphicsObj.FillRectangle(brush, 0, 0, cellRect.Width, cellRect.Height);
        }
        Sleep(250);
        for (j = 1; j <= 4; j++)
        {
          row = winLocationArray[j].Row;
          col = winLocationArray[j].Col;
          brush = new SolidBrush(board[row, col].Cell.BackColor);
          cellRect = board[row, col].Cell.ClientRectangle;
          graphicsObj = board[row, col].Cell.CreateGraphics();
          graphicsObj.FillRectangle(brush, 0, 0, cellRect.Width, cellRect.Height);
        }
        Sleep(250);
      }
    }

    private void ProcessTie()
    {
      SoundPlayer soundPlayer = new SoundPlayer("Tie.wav");

      elapsedTimeTimer.Enabled = false;
      soundPlayer.Play();
      MessageBox.Show("Tie", "Game", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
    }

    private void ProcessExceededMaxTime()
    {
      string message;
      SoundPlayer soundPlayer = new SoundPlayer("TimeExceeded.wav");

      elapsedTimeTimer.Enabled = false;
      if (currentPlayer == computerPlayer)
        message = "Winner: " + humanPlayer.Name;
      else
        message = "Winner: " + computerPlayer.Name;
      soundPlayer.Play();
      MessageBox.Show(message, "Exceeded Time Limit", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private void SwitchPlayers()
    {
      int i;

      if (currentPlayer == computerPlayer)
        currentPlayer = humanPlayer;
      else
        currentPlayer = computerPlayer;
      playerNameDisplay.Text = currentPlayer.Name;
      tokenDisplay.BackColor = currentPlayer.PlayerColor;
      maxTimeDisplay.Text = String.Format("{0:f0}.0 sec", currentPlayer.MaxTime);
      startDateTime = DateTime.Now;
      elapsedTimeTimer.Enabled = true;
      elapsedTimeDisplay.Text = "0.0 sec";
      for (i = 1; i <= 7; i++)
        tokenButtons[i].BackColor = currentPlayer.PlayerColor;
      if (currentPlayer == computerPlayer)
        ProcessComputerPly();
    }

    private void DispenseToken(int row, int col)
    {
      int i, j;
      Rectangle cell1Rect, cell2Rect;
      Graphics graphics1, graphics2;
      SolidBrush brush1, brush2;

      brush1 = new SolidBrush(SystemColors.Window);
      brush2 = new SolidBrush(currentPlayer.PlayerColor);
      cell2Rect = board[7, col].Cell.ClientRectangle;
      graphics2 = board[7, col].Cell.CreateGraphics();
      for (j = 1; j <= 20; j++)
      {
        graphics2.FillRectangle(brush2, 0, (j - 1), cell2Rect.Width, j);
      }
      Sleep(4);
      for (i = 7; i > row; i--)
      {
        cell1Rect = board[i, col].Cell.ClientRectangle;
        cell2Rect = board[i - 1, col].Cell.ClientRectangle;
        graphics1 = board[i, col].Cell.CreateGraphics();
        graphics2 = board[i - 1, col].Cell.CreateGraphics();
        for (j = 1; j <= 4; j++)
        {
          graphics1.FillRectangle(brush1, 0, (j - 1), cell1Rect.Width, j);
        }
        Sleep(4);
        for (j = 1; j <= 16; j++)
        {
          graphics1.FillRectangle(brush1, 0, (j + 3), cell1Rect.Width, (j + 4));
          graphics2.FillRectangle(brush2, 0, (j - 1), cell2Rect.Width, j);
        }
        Sleep(4);
        for (j = 16; j <= 20; j++)
        {
          graphics2.FillRectangle(brush2, 0, (j - 1), cell2Rect.Width, j);
        }
        Sleep(4);
      }
      board[row, col].Cell.BackColor = currentPlayer.PlayerColor;
    }

    private double ElapsedTime
    {
      get
      {
        TimeSpan timeSpan = DateTime.Now - startDateTime;

        return timeSpan.Hours * 3600.0 + timeSpan.Minutes * 60.0 +
               timeSpan.Seconds * 1.0 + timeSpan.Milliseconds / 1000.0;
      }
    }

    private void Sleep(int timeInterval)
    {
      int elapsedTime = 0;
      DateTime startDateTime;
      Timer elapsedTimeTimer;
      TimeSpan timeSpan;

      elapsedTimeTimer = new Timer();
      elapsedTimeTimer.Interval = 1;
      startDateTime = DateTime.Now;
      elapsedTimeTimer.Enabled = true;
      while (elapsedTime < timeInterval)
      {
        timeSpan = DateTime.Now - startDateTime;
        elapsedTime = timeSpan.Seconds * 1000 + timeSpan.Milliseconds;
      }
      elapsedTimeTimer.Enabled = false;
    }

  }  // End main form partial class Ch4Prb2Form

  /************************************************************/
  /*  2. Class BoardElement                                   */
  /************************************************************/
  public class BoardElement
  {
    Label cell;
    char token;

    public BoardElement()
    {
      cell = null;
      token = ' ';
    }

    public Label Cell
    {
      get
      {
        return cell;
      }
      set
      {
        cell = value;
      }
    }

    public char Token
    {
      get
      {
        return token;
      }
      set
      {
        token = value;
      }
    }
  }  // End class BoardElement

  /************************************************************/
  /*  3. Begin class Player                                   */
  /************************************************************/
  public class Player
  {
    private string name;
    private Color playerColor;
    private char token;
    private double maxTime;

    public Player()
    {
      name = "";
      playerColor = Color.White;
      token = ' ';
      maxTime = 0.0;
    }

    public string Name
    {
      get
      {
        return name;
      }
      set
      {
        name = value;
      }
    }

    public Color PlayerColor
    {
      get
      {
        return playerColor;
      }
      set
      {
        playerColor = value;
      }
    }

    public char Token
    {
      get
      {
        return token;
      }
      set
      {
        token = value;
      }
    }

    public double MaxTime
    {
      get
      {
        return maxTime;
      }
      set
      {
        maxTime = value;
      }
    }
  }  // End class Player

  /************************************************************/
  /*  4. Begin class Location                                 */
  /************************************************************/
  public class Location
  {
    private int row, col;

    public Location()
    {
      row = 0; col = 0;
    }

    public int Row
    {
      get
      {
        return row;
      }
      set
      {
        row = value;
      }
    }

    public int Col
    {
      get
      {
        return col;
      }
      set
      {
        col = value;
      }
    }
  }  // End class Location

}  // End partial namespace Ch4Prb2
