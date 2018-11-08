/***************************************************************/
/*                                                             */
/*  Course: CIS 430 - Artificial Intelligence                  */
/*                                                             */
/*  Project: Ch4Prb2.CSPrj                                     */
/*                                                             */
/*  Source File: Ch4Prb2.CS                                    */
/*                                                             */
/*  Programmer: Rocky Lowery                                   */
/*                                                             */
/*  Purpose: Implement the game of Connect Four. The computer  */
/*           plays a human using the AlphaBeta search algorithm. */
/*                                                             */
/*  Classes: 1. Partial Ch4Prb2Form : Form                     */
/*           2. Ch4Prb2App                                     */
/*                                                             */
/******************************************************##*******/

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
    const string REPORT_FILE_NAME = "Ch4Prb2Rpt.Txt";

    private IContainer        components;
    private MenuStrip         mainMenuStrip;
    private ToolStripMenuItem fileMenuItem;
    private ToolStripMenuItem exitMenuItem;
    private ToolStripMenuItem playerMenuItem;
    private ToolStripMenuItem humanMenuItem;
    private ToolStripMenuItem helpMenuItem;
    private ToolStripMenuItem aboutMenuItem;
    private Label             playerNameLabel, playerNameDisplay;
    private Label             tokenLabel, tokenDisplay;
    private Label             maxTimeLabel, maxTimeDisplay;
    private Label             elapsedTimeLabel, elapsedTimeDisplay;
    private Button            startButton;
    private Button            token1Button, token2Button, token3Button, token4Button;
    private Button            token5Button, token6Button, token7Button;
    private Label             cell_11, cell_12, cell_13, cell_14, cell_15, cell_16, cell_17;
    private Label             cell_21, cell_22, cell_23, cell_24, cell_25, cell_26, cell_27;
    private Label             cell_31, cell_32, cell_33, cell_34, cell_35, cell_36, cell_37;
    private Label             cell_41, cell_42, cell_43, cell_44, cell_45, cell_46, cell_47;
    private Label             cell_51, cell_52, cell_53, cell_54, cell_55, cell_56, cell_57;
    private Label             cell_61, cell_62, cell_63, cell_64, cell_65, cell_66, cell_67;
    private Label             cell_71, cell_72, cell_73, cell_74, cell_75, cell_76, cell_77;
    private Timer             elapsedTimeTimer;
    private BackgroundWorker  bgWorker;
    private bool              gameStarted, bgWorkCancelled;
    private char              nextToken;
    private int               maxDepth, row, col, boardValue, bestBoardValue;
    private int[]             weightArray;
    private int[,]            leftDiagMember, rightDiagMember;
    private Button[]          tokenButtons;
    private BoardElement[,]   board;
    private Location[]        moveLocationArray, winLocationArray;
    private Location[]        leftDiagLocationArray, rightDiagLocationArray;
    private Player            currentPlayer, computerPlayer, humanPlayer;
    private DateTime          startDateTime;

    public Ch4Prb2()
    {
      InitializeComponent();
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Ch4Prb2));
      this.playerNameLabel = new System.Windows.Forms.Label();
      this.elapsedTimeLabel = new System.Windows.Forms.Label();
      this.elapsedTimeTimer = new System.Windows.Forms.Timer(this.components);
      this.tokenLabel = new System.Windows.Forms.Label();
      this.maxTimeLabel = new System.Windows.Forms.Label();
      this.tokenDisplay = new System.Windows.Forms.Label();
      this.maxTimeDisplay = new System.Windows.Forms.Label();
      this.startButton = new System.Windows.Forms.Button();
      this.elapsedTimeDisplay = new System.Windows.Forms.Label();
      this.cell_11 = new System.Windows.Forms.Label();
      this.cell_12 = new System.Windows.Forms.Label();
      this.cell_14 = new System.Windows.Forms.Label();
      this.cell_13 = new System.Windows.Forms.Label();
      this.cell_24 = new System.Windows.Forms.Label();
      this.cell_23 = new System.Windows.Forms.Label();
      this.cell_22 = new System.Windows.Forms.Label();
      this.cell_21 = new System.Windows.Forms.Label();
      this.cell_44 = new System.Windows.Forms.Label();
      this.cell_43 = new System.Windows.Forms.Label();
      this.cell_42 = new System.Windows.Forms.Label();
      this.cell_41 = new System.Windows.Forms.Label();
      this.cell_34 = new System.Windows.Forms.Label();
      this.cell_33 = new System.Windows.Forms.Label();
      this.cell_32 = new System.Windows.Forms.Label();
      this.cell_31 = new System.Windows.Forms.Label();
      this.playerNameDisplay = new System.Windows.Forms.Label();
      this.cell_54 = new System.Windows.Forms.Label();
      this.cell_53 = new System.Windows.Forms.Label();
      this.cell_52 = new System.Windows.Forms.Label();
      this.cell_51 = new System.Windows.Forms.Label();
      this.cell_55 = new System.Windows.Forms.Label();
      this.cell_45 = new System.Windows.Forms.Label();
      this.cell_35 = new System.Windows.Forms.Label();
      this.cell_25 = new System.Windows.Forms.Label();
      this.cell_15 = new System.Windows.Forms.Label();
      this.cell_56 = new System.Windows.Forms.Label();
      this.cell_46 = new System.Windows.Forms.Label();
      this.cell_36 = new System.Windows.Forms.Label();
      this.cell_26 = new System.Windows.Forms.Label();
      this.cell_16 = new System.Windows.Forms.Label();
      this.cell_57 = new System.Windows.Forms.Label();
      this.cell_47 = new System.Windows.Forms.Label();
      this.cell_37 = new System.Windows.Forms.Label();
      this.cell_27 = new System.Windows.Forms.Label();
      this.cell_17 = new System.Windows.Forms.Label();
      this.cell_77 = new System.Windows.Forms.Label();
      this.cell_67 = new System.Windows.Forms.Label();
      this.cell_76 = new System.Windows.Forms.Label();
      this.cell_66 = new System.Windows.Forms.Label();
      this.cell_75 = new System.Windows.Forms.Label();
      this.cell_65 = new System.Windows.Forms.Label();
      this.cell_74 = new System.Windows.Forms.Label();
      this.cell_73 = new System.Windows.Forms.Label();
      this.cell_72 = new System.Windows.Forms.Label();
      this.cell_71 = new System.Windows.Forms.Label();
      this.cell_64 = new System.Windows.Forms.Label();
      this.cell_63 = new System.Windows.Forms.Label();
      this.cell_62 = new System.Windows.Forms.Label();
      this.cell_61 = new System.Windows.Forms.Label();
      this.token1Button = new System.Windows.Forms.Button();
      this.token2Button = new System.Windows.Forms.Button();
      this.token4Button = new System.Windows.Forms.Button();
      this.token3Button = new System.Windows.Forms.Button();
      this.token7Button = new System.Windows.Forms.Button();
      this.token6Button = new System.Windows.Forms.Button();
      this.token5Button = new System.Windows.Forms.Button();
      this.bgWorker = new System.ComponentModel.BackgroundWorker();
      this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
      this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.playerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.humanMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.helpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mainMenuStrip.SuspendLayout();
      this.SuspendLayout();
      // 
      // playerNameLabel
      // 
      this.playerNameLabel.Location = new System.Drawing.Point(48, 28);
      this.playerNameLabel.Name = "playerNameLabel";
      this.playerNameLabel.Size = new System.Drawing.Size(67, 24);
      this.playerNameLabel.TabIndex = 1;
      this.playerNameLabel.Text = "Player";
      this.playerNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // elapsedTimeLabel
      // 
      this.elapsedTimeLabel.Location = new System.Drawing.Point(41, 174);
      this.elapsedTimeLabel.Name = "elapsedTimeLabel";
      this.elapsedTimeLabel.Size = new System.Drawing.Size(80, 24);
      this.elapsedTimeLabel.TabIndex = 7;
      this.elapsedTimeLabel.Text = "Elapsed Time";
      this.elapsedTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // elapsedTimeTimer
      // 
      this.elapsedTimeTimer.Interval = 1000;
      this.elapsedTimeTimer.Tick += new System.EventHandler(this.elapsedTimeTimer_Tick);
      // 
      // tokenLabel
      // 
      this.tokenLabel.Location = new System.Drawing.Point(56, 76);
      this.tokenLabel.Name = "tokenLabel";
      this.tokenLabel.Size = new System.Drawing.Size(50, 24);
      this.tokenLabel.TabIndex = 3;
      this.tokenLabel.Text = "Token";
      this.tokenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // maxTimeLabel
      // 
      this.maxTimeLabel.Location = new System.Drawing.Point(48, 126);
      this.maxTimeLabel.Name = "maxTimeLabel";
      this.maxTimeLabel.Size = new System.Drawing.Size(67, 24);
      this.maxTimeLabel.TabIndex = 5;
      this.maxTimeLabel.Text = "Max Time";
      this.maxTimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tokenDisplay
      // 
      this.tokenDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.tokenDisplay.Location = new System.Drawing.Point(74, 100);
      this.tokenDisplay.Name = "tokenDisplay";
      this.tokenDisplay.Size = new System.Drawing.Size(15, 20);
      this.tokenDisplay.TabIndex = 4;
      this.tokenDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // maxTimeDisplay
      // 
      this.maxTimeDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.maxTimeDisplay.Location = new System.Drawing.Point(49, 150);
      this.maxTimeDisplay.Name = "maxTimeDisplay";
      this.maxTimeDisplay.Size = new System.Drawing.Size(65, 20);
      this.maxTimeDisplay.TabIndex = 6;
      this.maxTimeDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // startButton
      // 
      this.startButton.Location = new System.Drawing.Point(118, 236);
      this.startButton.Name = "startButton";
      this.startButton.Size = new System.Drawing.Size(75, 22);
      this.startButton.TabIndex = 0;
      this.startButton.Text = "Start";
      this.startButton.Click += new System.EventHandler(this.startButton_Click);
      // 
      // elapsedTimeDisplay
      // 
      this.elapsedTimeDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.elapsedTimeDisplay.Location = new System.Drawing.Point(49, 198);
      this.elapsedTimeDisplay.Name = "elapsedTimeDisplay";
      this.elapsedTimeDisplay.Size = new System.Drawing.Size(65, 20);
      this.elapsedTimeDisplay.TabIndex = 8;
      this.elapsedTimeDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // cell_11
      // 
      this.cell_11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_11.Enabled = false;
      this.cell_11.Location = new System.Drawing.Point(139, 198);
      this.cell_11.Name = "cell_11";
      this.cell_11.Size = new System.Drawing.Size(15, 20);
      this.cell_11.TabIndex = 11;
      this.cell_11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_12
      // 
      this.cell_12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_12.Enabled = false;
      this.cell_12.Location = new System.Drawing.Point(158, 198);
      this.cell_12.Name = "cell_12";
      this.cell_12.Size = new System.Drawing.Size(15, 20);
      this.cell_12.TabIndex = 12;
      this.cell_12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_14
      // 
      this.cell_14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_14.Enabled = false;
      this.cell_14.Location = new System.Drawing.Point(196, 198);
      this.cell_14.Name = "cell_14";
      this.cell_14.Size = new System.Drawing.Size(15, 20);
      this.cell_14.TabIndex = 14;
      this.cell_14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_13
      // 
      this.cell_13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_13.Enabled = false;
      this.cell_13.Location = new System.Drawing.Point(177, 198);
      this.cell_13.Name = "cell_13";
      this.cell_13.Size = new System.Drawing.Size(15, 20);
      this.cell_13.TabIndex = 13;
      this.cell_13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_24
      // 
      this.cell_24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_24.Enabled = false;
      this.cell_24.Location = new System.Drawing.Point(196, 174);
      this.cell_24.Name = "cell_24";
      this.cell_24.Size = new System.Drawing.Size(15, 20);
      this.cell_24.TabIndex = 18;
      this.cell_24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_23
      // 
      this.cell_23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_23.Enabled = false;
      this.cell_23.Location = new System.Drawing.Point(177, 174);
      this.cell_23.Name = "cell_23";
      this.cell_23.Size = new System.Drawing.Size(15, 20);
      this.cell_23.TabIndex = 17;
      this.cell_23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_22
      // 
      this.cell_22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_22.Enabled = false;
      this.cell_22.Location = new System.Drawing.Point(158, 174);
      this.cell_22.Name = "cell_22";
      this.cell_22.Size = new System.Drawing.Size(15, 20);
      this.cell_22.TabIndex = 16;
      this.cell_22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_21
      // 
      this.cell_21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_21.Enabled = false;
      this.cell_21.Location = new System.Drawing.Point(139, 174);
      this.cell_21.Name = "cell_21";
      this.cell_21.Size = new System.Drawing.Size(15, 20);
      this.cell_21.TabIndex = 15;
      this.cell_21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_44
      // 
      this.cell_44.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_44.Enabled = false;
      this.cell_44.Location = new System.Drawing.Point(196, 126);
      this.cell_44.Name = "cell_44";
      this.cell_44.Size = new System.Drawing.Size(15, 20);
      this.cell_44.TabIndex = 26;
      this.cell_44.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_43
      // 
      this.cell_43.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_43.Enabled = false;
      this.cell_43.Location = new System.Drawing.Point(177, 126);
      this.cell_43.Name = "cell_43";
      this.cell_43.Size = new System.Drawing.Size(15, 20);
      this.cell_43.TabIndex = 25;
      this.cell_43.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_42
      // 
      this.cell_42.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_42.Enabled = false;
      this.cell_42.Location = new System.Drawing.Point(158, 126);
      this.cell_42.Name = "cell_42";
      this.cell_42.Size = new System.Drawing.Size(15, 20);
      this.cell_42.TabIndex = 24;
      this.cell_42.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_41
      // 
      this.cell_41.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_41.Enabled = false;
      this.cell_41.Location = new System.Drawing.Point(139, 126);
      this.cell_41.Name = "cell_41";
      this.cell_41.Size = new System.Drawing.Size(15, 20);
      this.cell_41.TabIndex = 23;
      this.cell_41.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_34
      // 
      this.cell_34.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_34.Enabled = false;
      this.cell_34.Location = new System.Drawing.Point(196, 150);
      this.cell_34.Name = "cell_34";
      this.cell_34.Size = new System.Drawing.Size(15, 20);
      this.cell_34.TabIndex = 22;
      this.cell_34.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_33
      // 
      this.cell_33.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_33.Enabled = false;
      this.cell_33.Location = new System.Drawing.Point(177, 150);
      this.cell_33.Name = "cell_33";
      this.cell_33.Size = new System.Drawing.Size(15, 20);
      this.cell_33.TabIndex = 21;
      this.cell_33.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_32
      // 
      this.cell_32.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_32.Enabled = false;
      this.cell_32.Location = new System.Drawing.Point(158, 150);
      this.cell_32.Name = "cell_32";
      this.cell_32.Size = new System.Drawing.Size(15, 20);
      this.cell_32.TabIndex = 20;
      this.cell_32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_31
      // 
      this.cell_31.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_31.Enabled = false;
      this.cell_31.Location = new System.Drawing.Point(139, 150);
      this.cell_31.Name = "cell_31";
      this.cell_31.Size = new System.Drawing.Size(15, 20);
      this.cell_31.TabIndex = 19;
      this.cell_31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // playerNameDisplay
      // 
      this.playerNameDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.playerNameDisplay.Location = new System.Drawing.Point(49, 52);
      this.playerNameDisplay.Name = "playerNameDisplay";
      this.playerNameDisplay.Size = new System.Drawing.Size(65, 20);
      this.playerNameDisplay.TabIndex = 2;
      this.playerNameDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // cell_54
      // 
      this.cell_54.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_54.Enabled = false;
      this.cell_54.Location = new System.Drawing.Point(196, 102);
      this.cell_54.Name = "cell_54";
      this.cell_54.Size = new System.Drawing.Size(15, 20);
      this.cell_54.TabIndex = 31;
      this.cell_54.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_53
      // 
      this.cell_53.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_53.Enabled = false;
      this.cell_53.Location = new System.Drawing.Point(177, 102);
      this.cell_53.Name = "cell_53";
      this.cell_53.Size = new System.Drawing.Size(15, 20);
      this.cell_53.TabIndex = 30;
      this.cell_53.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_52
      // 
      this.cell_52.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_52.Enabled = false;
      this.cell_52.Location = new System.Drawing.Point(158, 102);
      this.cell_52.Name = "cell_52";
      this.cell_52.Size = new System.Drawing.Size(15, 20);
      this.cell_52.TabIndex = 29;
      this.cell_52.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_51
      // 
      this.cell_51.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_51.Enabled = false;
      this.cell_51.Location = new System.Drawing.Point(139, 102);
      this.cell_51.Name = "cell_51";
      this.cell_51.Size = new System.Drawing.Size(15, 20);
      this.cell_51.TabIndex = 28;
      this.cell_51.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_55
      // 
      this.cell_55.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_55.Enabled = false;
      this.cell_55.Location = new System.Drawing.Point(215, 102);
      this.cell_55.Name = "cell_55";
      this.cell_55.Size = new System.Drawing.Size(15, 20);
      this.cell_55.TabIndex = 36;
      this.cell_55.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_45
      // 
      this.cell_45.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_45.Enabled = false;
      this.cell_45.Location = new System.Drawing.Point(215, 126);
      this.cell_45.Name = "cell_45";
      this.cell_45.Size = new System.Drawing.Size(15, 20);
      this.cell_45.TabIndex = 35;
      this.cell_45.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_35
      // 
      this.cell_35.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_35.Enabled = false;
      this.cell_35.Location = new System.Drawing.Point(215, 150);
      this.cell_35.Name = "cell_35";
      this.cell_35.Size = new System.Drawing.Size(15, 20);
      this.cell_35.TabIndex = 34;
      this.cell_35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_25
      // 
      this.cell_25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_25.Enabled = false;
      this.cell_25.Location = new System.Drawing.Point(215, 174);
      this.cell_25.Name = "cell_25";
      this.cell_25.Size = new System.Drawing.Size(15, 20);
      this.cell_25.TabIndex = 33;
      this.cell_25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_15
      // 
      this.cell_15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_15.Enabled = false;
      this.cell_15.Location = new System.Drawing.Point(215, 198);
      this.cell_15.Name = "cell_15";
      this.cell_15.Size = new System.Drawing.Size(15, 20);
      this.cell_15.TabIndex = 32;
      this.cell_15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_56
      // 
      this.cell_56.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_56.Enabled = false;
      this.cell_56.Location = new System.Drawing.Point(234, 102);
      this.cell_56.Name = "cell_56";
      this.cell_56.Size = new System.Drawing.Size(15, 20);
      this.cell_56.TabIndex = 41;
      this.cell_56.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_46
      // 
      this.cell_46.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_46.Enabled = false;
      this.cell_46.Location = new System.Drawing.Point(234, 126);
      this.cell_46.Name = "cell_46";
      this.cell_46.Size = new System.Drawing.Size(15, 20);
      this.cell_46.TabIndex = 40;
      this.cell_46.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_36
      // 
      this.cell_36.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_36.Enabled = false;
      this.cell_36.Location = new System.Drawing.Point(234, 150);
      this.cell_36.Name = "cell_36";
      this.cell_36.Size = new System.Drawing.Size(15, 20);
      this.cell_36.TabIndex = 39;
      this.cell_36.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_26
      // 
      this.cell_26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_26.Enabled = false;
      this.cell_26.Location = new System.Drawing.Point(234, 174);
      this.cell_26.Name = "cell_26";
      this.cell_26.Size = new System.Drawing.Size(15, 20);
      this.cell_26.TabIndex = 38;
      this.cell_26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_16
      // 
      this.cell_16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_16.Enabled = false;
      this.cell_16.Location = new System.Drawing.Point(234, 198);
      this.cell_16.Name = "cell_16";
      this.cell_16.Size = new System.Drawing.Size(15, 20);
      this.cell_16.TabIndex = 37;
      this.cell_16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_57
      // 
      this.cell_57.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_57.Enabled = false;
      this.cell_57.Location = new System.Drawing.Point(253, 102);
      this.cell_57.Name = "cell_57";
      this.cell_57.Size = new System.Drawing.Size(15, 20);
      this.cell_57.TabIndex = 46;
      this.cell_57.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_47
      // 
      this.cell_47.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_47.Enabled = false;
      this.cell_47.Location = new System.Drawing.Point(253, 126);
      this.cell_47.Name = "cell_47";
      this.cell_47.Size = new System.Drawing.Size(15, 20);
      this.cell_47.TabIndex = 45;
      this.cell_47.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_37
      // 
      this.cell_37.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_37.Enabled = false;
      this.cell_37.Location = new System.Drawing.Point(253, 150);
      this.cell_37.Name = "cell_37";
      this.cell_37.Size = new System.Drawing.Size(15, 20);
      this.cell_37.TabIndex = 44;
      this.cell_37.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_27
      // 
      this.cell_27.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_27.Enabled = false;
      this.cell_27.Location = new System.Drawing.Point(253, 174);
      this.cell_27.Name = "cell_27";
      this.cell_27.Size = new System.Drawing.Size(15, 20);
      this.cell_27.TabIndex = 43;
      this.cell_27.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_17
      // 
      this.cell_17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_17.Enabled = false;
      this.cell_17.Location = new System.Drawing.Point(253, 198);
      this.cell_17.Name = "cell_17";
      this.cell_17.Size = new System.Drawing.Size(15, 20);
      this.cell_17.TabIndex = 42;
      this.cell_17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_77
      // 
      this.cell_77.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_77.Enabled = false;
      this.cell_77.Location = new System.Drawing.Point(253, 54);
      this.cell_77.Name = "cell_77";
      this.cell_77.Size = new System.Drawing.Size(15, 20);
      this.cell_77.TabIndex = 60;
      this.cell_77.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_67
      // 
      this.cell_67.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_67.Enabled = false;
      this.cell_67.Location = new System.Drawing.Point(253, 78);
      this.cell_67.Name = "cell_67";
      this.cell_67.Size = new System.Drawing.Size(15, 20);
      this.cell_67.TabIndex = 59;
      this.cell_67.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_76
      // 
      this.cell_76.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_76.Enabled = false;
      this.cell_76.Location = new System.Drawing.Point(234, 54);
      this.cell_76.Name = "cell_76";
      this.cell_76.Size = new System.Drawing.Size(15, 20);
      this.cell_76.TabIndex = 58;
      this.cell_76.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_66
      // 
      this.cell_66.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_66.Enabled = false;
      this.cell_66.Location = new System.Drawing.Point(234, 78);
      this.cell_66.Name = "cell_66";
      this.cell_66.Size = new System.Drawing.Size(15, 20);
      this.cell_66.TabIndex = 57;
      this.cell_66.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_75
      // 
      this.cell_75.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_75.Enabled = false;
      this.cell_75.Location = new System.Drawing.Point(215, 54);
      this.cell_75.Name = "cell_75";
      this.cell_75.Size = new System.Drawing.Size(15, 20);
      this.cell_75.TabIndex = 56;
      this.cell_75.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_65
      // 
      this.cell_65.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_65.Enabled = false;
      this.cell_65.Location = new System.Drawing.Point(215, 78);
      this.cell_65.Name = "cell_65";
      this.cell_65.Size = new System.Drawing.Size(15, 20);
      this.cell_65.TabIndex = 55;
      this.cell_65.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_74
      // 
      this.cell_74.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_74.Enabled = false;
      this.cell_74.Location = new System.Drawing.Point(196, 54);
      this.cell_74.Name = "cell_74";
      this.cell_74.Size = new System.Drawing.Size(15, 20);
      this.cell_74.TabIndex = 54;
      this.cell_74.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_73
      // 
      this.cell_73.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_73.Enabled = false;
      this.cell_73.Location = new System.Drawing.Point(177, 54);
      this.cell_73.Name = "cell_73";
      this.cell_73.Size = new System.Drawing.Size(15, 20);
      this.cell_73.TabIndex = 53;
      this.cell_73.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_72
      // 
      this.cell_72.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_72.Enabled = false;
      this.cell_72.Location = new System.Drawing.Point(158, 54);
      this.cell_72.Name = "cell_72";
      this.cell_72.Size = new System.Drawing.Size(15, 20);
      this.cell_72.TabIndex = 52;
      this.cell_72.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_71
      // 
      this.cell_71.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_71.Enabled = false;
      this.cell_71.Location = new System.Drawing.Point(139, 54);
      this.cell_71.Name = "cell_71";
      this.cell_71.Size = new System.Drawing.Size(15, 20);
      this.cell_71.TabIndex = 51;
      this.cell_71.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_64
      // 
      this.cell_64.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_64.Enabled = false;
      this.cell_64.Location = new System.Drawing.Point(196, 78);
      this.cell_64.Name = "cell_64";
      this.cell_64.Size = new System.Drawing.Size(15, 20);
      this.cell_64.TabIndex = 50;
      this.cell_64.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_63
      // 
      this.cell_63.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_63.Enabled = false;
      this.cell_63.Location = new System.Drawing.Point(177, 78);
      this.cell_63.Name = "cell_63";
      this.cell_63.Size = new System.Drawing.Size(15, 20);
      this.cell_63.TabIndex = 49;
      this.cell_63.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_62
      // 
      this.cell_62.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_62.Enabled = false;
      this.cell_62.Location = new System.Drawing.Point(158, 78);
      this.cell_62.Name = "cell_62";
      this.cell_62.Size = new System.Drawing.Size(15, 20);
      this.cell_62.TabIndex = 48;
      this.cell_62.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // cell_61
      // 
      this.cell_61.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.cell_61.Enabled = false;
      this.cell_61.Location = new System.Drawing.Point(139, 78);
      this.cell_61.Name = "cell_61";
      this.cell_61.Size = new System.Drawing.Size(15, 20);
      this.cell_61.TabIndex = 47;
      this.cell_61.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // token1Button
      // 
      this.token1Button.BackColor = System.Drawing.SystemColors.Control;
      this.token1Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
      this.token1Button.ForeColor = System.Drawing.Color.White;
      this.token1Button.Location = new System.Drawing.Point(139, 30);
      this.token1Button.Name = "token1Button";
      this.token1Button.Size = new System.Drawing.Size(15, 20);
      this.token1Button.TabIndex = 61;
      this.token1Button.Text = "1";
      this.token1Button.UseVisualStyleBackColor = false;
      this.token1Button.Click += new System.EventHandler(this.ProcessHumanPly);
      // 
      // token2Button
      // 
      this.token2Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
      this.token2Button.ForeColor = System.Drawing.Color.White;
      this.token2Button.Location = new System.Drawing.Point(158, 30);
      this.token2Button.Name = "token2Button";
      this.token2Button.Size = new System.Drawing.Size(15, 20);
      this.token2Button.TabIndex = 62;
      this.token2Button.Text = "2";
      this.token2Button.Click += new System.EventHandler(this.ProcessHumanPly);
      // 
      // token4Button
      // 
      this.token4Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
      this.token4Button.ForeColor = System.Drawing.Color.White;
      this.token4Button.Location = new System.Drawing.Point(196, 30);
      this.token4Button.Name = "token4Button";
      this.token4Button.Size = new System.Drawing.Size(15, 20);
      this.token4Button.TabIndex = 64;
      this.token4Button.Text = "4";
      this.token4Button.Click += new System.EventHandler(this.ProcessHumanPly);
      // 
      // token3Button
      // 
      this.token3Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
      this.token3Button.ForeColor = System.Drawing.Color.White;
      this.token3Button.Location = new System.Drawing.Point(177, 30);
      this.token3Button.Name = "token3Button";
      this.token3Button.Size = new System.Drawing.Size(15, 20);
      this.token3Button.TabIndex = 63;
      this.token3Button.Text = "3";
      this.token3Button.Click += new System.EventHandler(this.ProcessHumanPly);
      // 
      // token7Button
      // 
      this.token7Button.BackColor = System.Drawing.SystemColors.Control;
      this.token7Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
      this.token7Button.ForeColor = System.Drawing.Color.White;
      this.token7Button.Location = new System.Drawing.Point(253, 30);
      this.token7Button.Name = "token7Button";
      this.token7Button.Size = new System.Drawing.Size(15, 20);
      this.token7Button.TabIndex = 67;
      this.token7Button.Text = "7";
      this.token7Button.UseVisualStyleBackColor = false;
      this.token7Button.Click += new System.EventHandler(this.ProcessHumanPly);
      // 
      // token6Button
      // 
      this.token6Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
      this.token6Button.ForeColor = System.Drawing.Color.White;
      this.token6Button.Location = new System.Drawing.Point(234, 30);
      this.token6Button.Name = "token6Button";
      this.token6Button.Size = new System.Drawing.Size(15, 20);
      this.token6Button.TabIndex = 66;
      this.token6Button.Text = "6";
      this.token6Button.Click += new System.EventHandler(this.ProcessHumanPly);
      // 
      // token5Button
      // 
      this.token5Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
      this.token5Button.ForeColor = System.Drawing.Color.White;
      this.token5Button.Location = new System.Drawing.Point(215, 30);
      this.token5Button.Name = "token5Button";
      this.token5Button.Size = new System.Drawing.Size(15, 20);
      this.token5Button.TabIndex = 65;
      this.token5Button.Text = "5";
      this.token5Button.Click += new System.EventHandler(this.ProcessHumanPly);
      // 
      // bgWorker
      // 
      this.bgWorker.WorkerSupportsCancellation = true;
      this.bgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgWorker_DoWork);
      // 
      // mainMenuStrip
      // 
      this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileMenuItem,
            this.playerMenuItem,
            this.helpMenuItem});
      this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
      this.mainMenuStrip.Name = "mainMenuStrip";
      this.mainMenuStrip.Size = new System.Drawing.Size(309, 24);
      this.mainMenuStrip.TabIndex = 68;
      this.mainMenuStrip.Text = "menuStrip1";
      // 
      // fileMenuItem
      // 
      this.fileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMenuItem});
      this.fileMenuItem.Name = "fileMenuItem";
      this.fileMenuItem.Size = new System.Drawing.Size(37, 20);
      this.fileMenuItem.Text = "&File";
      // 
      // exitMenuItem
      // 
      this.exitMenuItem.Name = "exitMenuItem";
      this.exitMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
      this.exitMenuItem.Size = new System.Drawing.Size(133, 22);
      this.exitMenuItem.Text = "E&xit";
      this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
      // 
      // playerMenuItem
      // 
      this.playerMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.humanMenuItem});
      this.playerMenuItem.Name = "playerMenuItem";
      this.playerMenuItem.Size = new System.Drawing.Size(51, 20);
      this.playerMenuItem.Text = "&Player";
      // 
      // humanMenuItem
      // 
      this.humanMenuItem.Name = "humanMenuItem";
      this.humanMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
      this.humanMenuItem.Size = new System.Drawing.Size(157, 22);
      this.humanMenuItem.Text = "&Human";
      this.humanMenuItem.Click += new System.EventHandler(this.humanMenuItem_Click);
      // 
      // helpMenuItem
      // 
      this.helpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutMenuItem});
      this.helpMenuItem.Name = "helpMenuItem";
      this.helpMenuItem.Size = new System.Drawing.Size(44, 20);
      this.helpMenuItem.Text = "&Help";
      // 
      // aboutMenuItem
      // 
      this.aboutMenuItem.Name = "aboutMenuItem";
      this.aboutMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
      this.aboutMenuItem.Size = new System.Drawing.Size(126, 22);
      this.aboutMenuItem.Text = "&About";
      this.aboutMenuItem.Click += new System.EventHandler(this.aboutMenuItem_Click);
      // 
      // Ch4Prb2
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(6, 16);
      this.ClientSize = new System.Drawing.Size(309, 269);
      this.Controls.Add(this.token7Button);
      this.Controls.Add(this.token6Button);
      this.Controls.Add(this.token5Button);
      this.Controls.Add(this.token4Button);
      this.Controls.Add(this.token3Button);
      this.Controls.Add(this.token2Button);
      this.Controls.Add(this.token1Button);
      this.Controls.Add(this.cell_77);
      this.Controls.Add(this.cell_67);
      this.Controls.Add(this.cell_76);
      this.Controls.Add(this.cell_66);
      this.Controls.Add(this.cell_75);
      this.Controls.Add(this.cell_65);
      this.Controls.Add(this.cell_74);
      this.Controls.Add(this.cell_73);
      this.Controls.Add(this.cell_72);
      this.Controls.Add(this.cell_71);
      this.Controls.Add(this.cell_64);
      this.Controls.Add(this.cell_63);
      this.Controls.Add(this.cell_62);
      this.Controls.Add(this.cell_61);
      this.Controls.Add(this.cell_57);
      this.Controls.Add(this.cell_47);
      this.Controls.Add(this.cell_37);
      this.Controls.Add(this.cell_27);
      this.Controls.Add(this.cell_17);
      this.Controls.Add(this.cell_56);
      this.Controls.Add(this.cell_46);
      this.Controls.Add(this.cell_36);
      this.Controls.Add(this.cell_26);
      this.Controls.Add(this.cell_16);
      this.Controls.Add(this.cell_55);
      this.Controls.Add(this.cell_45);
      this.Controls.Add(this.cell_35);
      this.Controls.Add(this.cell_25);
      this.Controls.Add(this.cell_15);
      this.Controls.Add(this.cell_54);
      this.Controls.Add(this.cell_53);
      this.Controls.Add(this.cell_52);
      this.Controls.Add(this.cell_51);
      this.Controls.Add(this.playerNameDisplay);
      this.Controls.Add(this.cell_44);
      this.Controls.Add(this.cell_43);
      this.Controls.Add(this.cell_42);
      this.Controls.Add(this.cell_41);
      this.Controls.Add(this.cell_34);
      this.Controls.Add(this.cell_33);
      this.Controls.Add(this.cell_32);
      this.Controls.Add(this.cell_31);
      this.Controls.Add(this.cell_24);
      this.Controls.Add(this.cell_23);
      this.Controls.Add(this.cell_22);
      this.Controls.Add(this.cell_21);
      this.Controls.Add(this.cell_14);
      this.Controls.Add(this.cell_13);
      this.Controls.Add(this.cell_12);
      this.Controls.Add(this.cell_11);
      this.Controls.Add(this.elapsedTimeDisplay);
      this.Controls.Add(this.startButton);
      this.Controls.Add(this.maxTimeDisplay);
      this.Controls.Add(this.tokenDisplay);
      this.Controls.Add(this.maxTimeLabel);
      this.Controls.Add(this.tokenLabel);
      this.Controls.Add(this.elapsedTimeLabel);
      this.Controls.Add(this.playerNameLabel);
      this.Controls.Add(this.mainMenuStrip);
      this.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mainMenuStrip;
      this.MaximizeBox = false;
      this.Name = "Ch4Prb2";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Ch4Prb2: Connect Four";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ch4Smp1Form_FormClosing);
      this.Load += new System.EventHandler(this.Ch4Prb2_Load);
      this.mainMenuStrip.ResumeLayout(false);
      this.mainMenuStrip.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }
    #endregion

    /************************************************************/
    /*  Message Handlers                                        */
    /************************************************************/
    private void Ch4Prb2_Load(object sender, EventArgs e)
    {
      int i, j, row, col;

      humanPlayer            = new Player();
      computerPlayer         = new Player();
      computerPlayer.Name    = "Computer";
      computerPlayer.MaxTime = 30.0;
      humanPlayer.MaxTime    = 60.0;
      gameStarted            = false;
      tokenButtons    = new Button[8];
      tokenButtons[1] = token1Button;  tokenButtons[2] = token2Button;
      tokenButtons[3] = token3Button;  tokenButtons[4] = token4Button;
      tokenButtons[5] = token5Button;  tokenButtons[6] = token6Button;
      tokenButtons[7] = token7Button; 
      board = new BoardElement[8,8];
      for (i=0; i<=7; i++)
        for (j=0; j<=7; j++)
          board[i,j] = new BoardElement();
      board[1,1].Cell = cell_11; board[1,2].Cell = cell_12; board[1,3].Cell = cell_13; board[1,4].Cell = cell_14;
      board[1,5].Cell = cell_15; board[1,6].Cell = cell_16; board[1,7].Cell = cell_17;
      board[2,1].Cell = cell_21; board[2,2].Cell = cell_22; board[2,3].Cell = cell_23; board[2,4].Cell = cell_24;
      board[2,5].Cell = cell_25; board[2,6].Cell = cell_26; board[2,7].Cell = cell_27;
      board[3,1].Cell = cell_31; board[3,2].Cell = cell_32; board[3,3].Cell = cell_33; board[3,4].Cell = cell_34;
      board[3,5].Cell = cell_35; board[3,6].Cell = cell_36; board[3,7].Cell = cell_37;
      board[4,1].Cell = cell_41; board[4,2].Cell = cell_42; board[4,3].Cell = cell_43; board[4,4].Cell = cell_44;
      board[4,5].Cell = cell_45; board[4,6].Cell = cell_46; board[4,7].Cell = cell_47;
      board[5,1].Cell = cell_51; board[5,2].Cell = cell_52; board[5,3].Cell = cell_53; board[5,4].Cell = cell_54;
      board[5,5].Cell = cell_55; board[5,6].Cell = cell_56; board[5,7].Cell = cell_57;
      board[6,1].Cell = cell_61; board[6,2].Cell = cell_62; board[6,3].Cell = cell_63; board[6,4].Cell = cell_64;
      board[6,5].Cell = cell_65; board[6,6].Cell = cell_66; board[6,7].Cell = cell_67;
      board[7,1].Cell = cell_71; board[7,2].Cell = cell_72; board[7,3].Cell = cell_73; board[7,4].Cell = cell_74;
      board[7,5].Cell = cell_75; board[7,6].Cell = cell_76; board[7,7].Cell = cell_77;
      weightArray = new int[4];
      for (i=1; i<=3; i++)
        weightArray[i] = (int) Math.Pow(10, i);
      moveLocationArray      = new Location[8];
      leftDiagLocationArray = new Location[8];
      rightDiagLocationArray = new Location[8];
      winLocationArray       = new Location[5];
      for (i=0; i<=7; i++)
      {
        moveLocationArray[i]      = new Location();
        leftDiagLocationArray[i] = new Location();
        rightDiagLocationArray[i] = new Location();
      }
      for (i=0; i<=4; i++)
        winLocationArray[i] = new Location();
      moveLocationArray[1].Row = 1;  moveLocationArray[1].Col = 1;
      moveLocationArray[2].Row = 1;  moveLocationArray[2].Col = 2;
      moveLocationArray[3].Row = 1;  moveLocationArray[3].Col = 3;
      moveLocationArray[4].Row = 1;  moveLocationArray[4].Col = 4;
      moveLocationArray[5].Row = 1;  moveLocationArray[5].Col = 5;
      moveLocationArray[6].Row = 1;  moveLocationArray[6].Col = 6;
      moveLocationArray[7].Row = 1;  moveLocationArray[7].Col = 7;

      leftDiagLocationArray[1].Row = 4; leftDiagLocationArray[1].Col = 1;
      leftDiagLocationArray[2].Row = 3; leftDiagLocationArray[2].Col = 1;
      leftDiagLocationArray[3].Row = 2; leftDiagLocationArray[3].Col = 1;
      leftDiagLocationArray[4].Row = 1; leftDiagLocationArray[4].Col = 1;
      leftDiagLocationArray[5].Row = 1; leftDiagLocationArray[5].Col = 2;
      leftDiagLocationArray[6].Row = 1; leftDiagLocationArray[6].Col = 3;
      leftDiagLocationArray[7].Row = 1; leftDiagLocationArray[7].Col = 4;

      rightDiagLocationArray[1].Row = 1; rightDiagLocationArray[1].Col = 4;
      rightDiagLocationArray[2].Row = 1; rightDiagLocationArray[2].Col = 5;
      rightDiagLocationArray[3].Row = 1; rightDiagLocationArray[3].Col = 6;
      rightDiagLocationArray[4].Row = 1; rightDiagLocationArray[4].Col = 7;
      rightDiagLocationArray[5].Row = 2; rightDiagLocationArray[5].Col = 7;
      rightDiagLocationArray[6].Row = 3; rightDiagLocationArray[6].Col = 7;
      rightDiagLocationArray[7].Row = 4; rightDiagLocationArray[7].Col = 7;
      leftDiagMember  = new int[8,8];
      rightDiagMember = new int[8,8];
      for (row=1; row<=7; row++)
        for (col = 1; col <= 7; col++)
        {
          leftDiagMember[row,col]  = 0;
          rightDiagMember[row,col] = 0;
        }
      for (i = 1; i <= 7; i++)
      {
        row = leftDiagLocationArray[i].Row;
        col = leftDiagLocationArray[i].Col;
        while ((row <= 7) && (col <= 7))
        {
          leftDiagMember[row, col] = i;
          row++; col++;
        }
      }
      for (i = 1; i <= 7; i++)
      {
        row = rightDiagLocationArray[i].Row;
        col = rightDiagLocationArray[i].Col;
        while ((row <= 7) && (col >= 1))
        {
          rightDiagMember[row, col] = i;
          row++; col--;
        }
      }
    }

    private void exitMenuItem_Click(object sender, System.EventArgs e)
    {
      Application.Exit();
    }

    private void humanMenuItem_Click(object sender, EventArgs e)
    {
      PlayerDialog playerDialog = new PlayerDialog();

      playerDialog.HumanPlayer = this.humanPlayer;
      if (playerDialog.ShowDialog(this) == DialogResult.OK)
      {
        if (humanPlayer.PlayerColor == Color.Red)
        {
          humanPlayer.Token          = 'X';
          computerPlayer.Token       = 'O';
          computerPlayer.PlayerColor = Color.Blue;
          currentPlayer = humanPlayer;
        }
        else
        {
          humanPlayer.Token          = 'O';
          computerPlayer.Token       = 'X';
          computerPlayer.PlayerColor = Color.Red;
          currentPlayer = computerPlayer;
        }
      }
    }

    private void aboutMenuItem_Click(object sender, System.EventArgs e)
    {
      AboutDialog aboutDialog = new AboutDialog();

      aboutDialog.ShowDialog(this);
    }

    private void startButton_Click(object sender, System.EventArgs e)
    {
      SoundPlayer soundPlayer;

      if (currentPlayer!=null)
      {
        startButton.Visible = false;
        this.Focus();
        gameStarted = true;
        soundPlayer = new SoundPlayer("Start.wav");
        soundPlayer.Play();
        Initialize();
        if (currentPlayer==computerPlayer)
          ProcessComputerPly();
      }
      else
      {
        soundPlayer = new SoundPlayer("RejectMove.wav");
        soundPlayer.Play();
        MessageBox.Show("Must identify Human player", "Missing Information", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private void ProcessHumanPly(object sender, System.EventArgs e)
    {
      int    col = 0;
      Button tokenButton;

      if (currentPlayer==humanPlayer && gameStarted)
      {
        tokenButton = (Button) sender;
        col = Int32.Parse(tokenButton.Name[5].ToString());
        if (board[7, col].Token == ' ')
          ProcessMove(col);
        else
          Console.Beep(300, 500);
      }
      else
        Console.Beep(300, 500);
    }

    private void elapsedTimeTimer_Tick(object sender, System.EventArgs e)
    {
      double elapsedTime = this.ElapsedTime;

      elapsedTimeDisplay.Text = elapsedTime.ToString("f0") + ".0 sec";
      if (currentPlayer == computerPlayer && elapsedTime > computerPlayer.MaxTime)
        bgWorkCancelled = true;
    }

    private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
    {
      AlphaBeta(1, -42000, 42000, nextToken);
    }

    private void Ch4Smp1Form_FormClosing(object sender, FormClosingEventArgs e)
    {
      bgWorkCancelled = true;
    }

    /************************************************************/
    /*  Partial Auxillary Methods                               */
    /************************************************************/
    private void Initialize()
    {
      int row, col;

      playerNameDisplay.Text  = currentPlayer.Name;
      tokenDisplay.BackColor  = currentPlayer.PlayerColor;
      maxTimeDisplay.Text     = currentPlayer.MaxTime.ToString("n1") + " sec";
      elapsedTimeDisplay.Text = "0.0 sec";
      for (row=1; row<=7; row++)
      {
        for (col=1; col<=7; col++)
        {
          board[row, col].Cell.Enabled   = true;
          board[row, col].Cell.BackColor = SystemColors.Window;
          board[row, col].Token          = ' ';
        }
        tokenButtons[row].BackColor = currentPlayer.PlayerColor;
      }
      startDateTime            = DateTime.Now;
      elapsedTimeTimer.Enabled = true;
    }

    private void ProcessComputerPly()  // Uses AlphaBeta
    {
      int i, bestCol = 0;

      boardValue     = 0;
      bestBoardValue = 0;
      nextToken      = ' ';
      maxDepth       = 7;
      switch (currentPlayer.Token)
      {
        case 'X': bestBoardValue = -42000; nextToken = 'O'; break;
        case 'O': bestBoardValue = 42000; nextToken = 'X'; break;
      }
      i = 0;
      while (++i <= 7)
        if (moveLocationArray[i].Row <= 7)
        {
          // Simulate move
          switch (currentPlayer.Token)
          {
            case 'X': boardValue = -42000; break;
            case 'O': boardValue =  42000; break;
          }
          row = moveLocationArray[i].Row;
          col = moveLocationArray[i].Col;
          board[row, col].Token = currentPlayer.Token;
          moveLocationArray[i].Row++;
          bgWorker.RunWorkerAsync();  // Execute AlphaBeta() method in the background.
          while (bgWorker.IsBusy)     // Continue responding to UI events while waiting for   
            Application.DoEvents();   // the background work to finish.
          moveLocationArray[i].Row--;
          board[row, col].Token = ' ';
          if ((currentPlayer.Token == 'X' && boardValue > bestBoardValue) ||
              (currentPlayer.Token == 'O' && boardValue < bestBoardValue))
          {
            bestBoardValue = boardValue;
            bestCol        = col;
          }
        }
      ProcessMove(bestCol);
    }
int pruned = 0; 
    private void AlphaBeta(int depth, int alpha, int beta, char token)
    {
      int  i=0, row, col, bestBoardValue = 0;
      char nextToken = ' ';
      
      if (!bgWorkCancelled)
      {
        switch (token)
        {
          case 'X': bestBoardValue = -42000; nextToken = 'O'; break;
          case 'O': bestBoardValue =  42000; nextToken = 'X'; break;
        }
        if (IsAWin(nextToken))  // nextToken==prevToken 
          switch (nextToken)
          {
            case 'X': boardValue =  40000 - 10 * depth; break;
            case 'O': boardValue = -40000 + 10 * depth; break;
          }
        else if (IsATie())
          boardValue = 0;
        else if (depth == maxDepth)
          boardValue = EvaluateBoard(nextToken);  // nextToken == prevToken
        else
        {
          i = 0;
          if (beta <= alpha || alpha == -42000 || beta == 42000)
          {
            while (++i <= 7)
              if (moveLocationArray[i].Row <= 7)
              {
                row = moveLocationArray[i].Row;
                col = moveLocationArray[i].Col;
                board[row, col].Token = token;
                moveLocationArray[i].Row++;
                AlphaBeta(depth + 1, alpha, beta, nextToken);
                moveLocationArray[i].Row--;
                board[row, col].Token = ' ';
                if (((token == 'X') && (boardValue > bestBoardValue)) ||
                    ((token == 'O') && (boardValue < bestBoardValue)))
                  bestBoardValue = boardValue;
                if ((token == 'X' && beta >= boardValue) || (token == 'X' && depth == 6))
                  alpha = boardValue;
                else if ((token == 'O' && alpha <= boardValue) || (token == 'O' && depth == 6))
                  beta = boardValue;
              }
            boardValue = bestBoardValue;

          }
          //else if (token == 'X')
          //{
          //  boardValue = alpha;
          //  pruned++;
          //}
          //else if (token == 'O')
          //{
          //  boardValue = beta;
          //  pruned++;
          //}
        }
      }
    }

  }  // End main form partial class Ch4Prb2Form

  /************************************************************/
  /*  2. Application class Ch4Prb2App                         */
  /************************************************************/
  static class Ch4Smp1App
  {
    static void Main()
    {
      Application.Run(new Ch4Prb2());
    }
  }  // End application class Ch4Prb2App

}  // End partial namespace Ch4Prb2

