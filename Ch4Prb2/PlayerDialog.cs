/*************************************************************/
/*                                                           */
/*  Course: CIS 430 - Artificial Intelligence                */
/*                                                           */
/*  Project: Ch4Prb2.CSPrj                                   */
/*                                                           */
/*  Source File: PlayerDialog.CS                             */
/*                                                           */
/*  Programmer: Dr. Oakes                                    */
/*                                                           */
/*  Class: PlayerDialog : Form                               */
/*                                                           */
/*************************************************************/

using System;
using System.Drawing;
using System.Windows.Forms;
using LibETextBox;

/************************************************************/
/*  Begin partial namespace Ch4Smp1                         */
/************************************************************/
namespace Ch4Prb2
{

  /************************************************************/
  /*  Begin form class PlayerDialog : Form                    */
  /************************************************************/
  public class PlayerDialog : Form
  {
    private Label    nameLabel;
    private Label    tokenLabel;
    private Label    tokenDisplay;
    private Button   okButton;
    private Button   cancelButton;
    private ETextBox nameETextBox;
    private Player   humanPlayer;
    
    public PlayerDialog()
    {
      InitializeComponent();
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
      this.nameLabel = new System.Windows.Forms.Label();
      this.tokenLabel = new System.Windows.Forms.Label();
      this.tokenDisplay = new System.Windows.Forms.Label();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.nameETextBox = new LibETextBox.ETextBox();
      this.SuspendLayout();
      // 
      // nameLabel
      // 
      this.nameLabel.Location = new System.Drawing.Point(94, 6);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(49, 20);
      this.nameLabel.TabIndex = 0;
      this.nameLabel.Text = "Name";
      this.nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tokenLabel
      // 
      this.tokenLabel.Location = new System.Drawing.Point(86, 55);
      this.tokenLabel.Name = "tokenLabel";
      this.tokenLabel.Size = new System.Drawing.Size(49, 20);
      this.tokenLabel.TabIndex = 2;
      this.tokenLabel.Text = "Token";
      this.tokenLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // tokenDisplay
      // 
      this.tokenDisplay.BackColor = System.Drawing.Color.Red;
      this.tokenDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.tokenDisplay.Location = new System.Drawing.Point(103, 76);
      this.tokenDisplay.Name = "tokenDisplay";
      this.tokenDisplay.Size = new System.Drawing.Size(15, 20);
      this.tokenDisplay.TabIndex = 3;
      this.tokenDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.tokenDisplay.Click += new System.EventHandler(this.tokenDisplay_Click);
      // 
      // okButton
      // 
      this.okButton.Location = new System.Drawing.Point(32, 121);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 22);
      this.okButton.TabIndex = 4;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.Location = new System.Drawing.Point(118, 121);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 22);
      this.cancelButton.TabIndex = 5;
      this.cancelButton.Text = "Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      // 
      // nameETextBox
      // 
      this.nameETextBox.Location = new System.Drawing.Point(60, 28);
      this.nameETextBox.Name = "nameETextBox";
      this.nameETextBox.Size = new System.Drawing.Size(100, 20);
      this.nameETextBox.TabIndex = 1;
      // 
      // PlayerDialog
      // 
      this.AcceptButton = this.okButton;
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.cancelButton;
      this.ClientSize = new System.Drawing.Size(224, 153);
      this.Controls.Add(this.nameETextBox);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.tokenDisplay);
      this.Controls.Add(this.tokenLabel);
      this.Controls.Add(this.nameLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
      this.Name = "PlayerDialog";
      this.ShowIcon = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Human Player";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private void tokenDisplay_Click(object sender, EventArgs e)
    {
      if (tokenDisplay.BackColor == Color.Red)
        tokenDisplay.BackColor = Color.Blue;
      else
        tokenDisplay.BackColor = Color.Red;
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      string playerName = "";

      if (nameETextBox.ReadString(out playerName))
      {
        humanPlayer.Name        = playerName;
        humanPlayer.PlayerColor = tokenDisplay.BackColor;
        this.DialogResult       = DialogResult.OK;
      }
    }

    public Player HumanPlayer
    {
      get
      {
        return humanPlayer;
      }
      set
      {
        humanPlayer = value;
      }
    }
  }  // End class PlayerDialog

}