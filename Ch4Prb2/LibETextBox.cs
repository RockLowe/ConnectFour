/**************************************************************/
/*                                                            */
/*  Class: CIS 430 - Artificial Intelligence                  */
/*                                                            */
/*  Library: LibETextBox.CS                                   */
/*                                                            */
/*  Programmer: Dr. Oakes                                     */
/*                                                            */
/*  Purpose: Implementation for class following classes.      */
/*                                                            */
/*           1. ETextBox  : TextBox                           */
/*           2. AETextBox : ETextBox                          */
/*           3. METextBox : ETextBox                          */
/*                                                            */
/*           ETextBox extends the TextBox class to provide    */
/*           methods to read and validate string, char,       */
/*           integer, floating point, currency, and date      */
/*           values.                                          */
/*                                                            */
/*           AETextBox extends the ETextBox class to provide  */
/*           methods to read and validate arithmetic infix    */
/*           expressions.                                     */
/*                                                            */
/*           METextBox extends the ETextBox class to provide  */
/*           methods to read and validate matrix infix        */
/*           expressions.                                     */
/*                                                            */
/**************************************************************/

using System;
using System.Windows.Forms;
using LibDate;

// Begin namespace LibETextBox
namespace LibETextBox
{
  public enum TBounds        {None=0, Lower, Upper, Both};
  public enum TValueRequired {ValueRequired=0, NoValueRequired};
  
  [System.ComponentModel.DesignerCategory("Code")]  // Specifies the the souce file is to be opened
                                                    // in "code" view rather than "design" view. 
  /************************************************************/
  /*  1. Class ETextBox : TexBox                              */
  /************************************************************/  
  public class ETextBox : TextBox
  {

    public ETextBox() : base()
    {
    }

    public bool ReadChar(out char charValue)
    {
      bool   valueOk = true;
      string message = "";

      charValue = (char) 0;
      try
      {
        charValue = Char.Parse(this.Text);
      }
      catch 
      {
        message = "Invalid character value";
        valueOk = false;
      }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadChar(out char charValue, string validChars)
    {
      bool   valueOk = true;
      string message = "";

      charValue = (char) 0;
      try
      {
        charValue = Char.Parse(this.Text);
      }
      catch 
      {
        message = "Invalid character value";
        valueOk = false;
      }
      if (valueOk)
        if (! validChars.Contains(charValue.ToString()))  
        {          
          message = String.Format("Value must be one of the following characters: {0}",validChars);
          valueOk = false;
        }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadString(out string stringValue)
    {
      bool   valueOk = true;
      int    numOfSpaces = 0;
      string message = "";

      if ((stringValue = this.Text)=="")
      {
        message = "Must enter a value";
        valueOk = false;
      }
      if (valueOk)
      {
        for(int i=0; i<stringValue.Length; i++)
          if (stringValue[i]==' ')
            numOfSpaces++;
        if (numOfSpaces==stringValue.Length)
        {
          message = "Must enter at least one non-space character in value";
          valueOk = false;
        }
      }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadString(out string stringValue, string[] validValues)
    {
      bool   valueOk = true;
      int    numOfSpaces = 0, i;
      string message = "";

      if ((stringValue = this.Text)=="")
      {
        message = "Must enter a value";
        valueOk = false;
      }
      if (valueOk)
      {
        for(i=0; i<stringValue.Length; i++)
          if (stringValue[i]==' ')
            numOfSpaces++;
        if (numOfSpaces==stringValue.Length)
        {
          message = "Must enter at least one non-space character in value";
          valueOk = false;
        }
      }
      if (valueOk)
      {
        valueOk = false;
        i = 0;
        while (! valueOk && i<validValues.Length)
        {
          if (stringValue==validValues[i])
            valueOk = true;
          i++;
        }
        if (! valueOk)
        {
          message = "Value must be one of the following:\n";
          foreach (String value in validValues)
            message += "\n  " + value;
        }
      }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }
    
    public bool ReadString(out string stringValue, TValueRequired valueRequiredDirective)
    {
      bool   valueOk = true;
      int    numOfSpaces = 0;
      string message = "";

      if ((stringValue = this.Text)=="" && valueRequiredDirective==TValueRequired.ValueRequired)
      {
        message = "Must enter a value";
        valueOk = false;
      }
      if (valueOk)
      {
        for(int i=0; i<stringValue.Length; i++)
          if (stringValue[i]==' ')
            numOfSpaces++;
        if (numOfSpaces==stringValue.Length && valueRequiredDirective==TValueRequired.ValueRequired)
        {
          message = "Must enter at least one non-space character in value";
          valueOk = false;
        }
      }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadUInt(out uint uintValue, TBounds boundsCode, uint bound1, uint bound2)
    {
      bool   valueOk = true;
      string message = "";

      uintValue = 0;
      try
      {
        uintValue = UInt32.Parse(this.Text.Replace(",",""));
      }
      catch 
      {
        message = "Invalid unsigned integer value";
        valueOk = false;
      }
      if (valueOk)
        if ((boundsCode==TBounds.Lower) && (uintValue<bound1))  
        {          
          message = String.Format("Value must be >= {0}",bound1);
          valueOk = false;
        }
        else if ((boundsCode==TBounds.Upper) && (uintValue>bound2))
        {
          message = String.Format("Value must be <= {0}",bound2);          
          valueOk = false;
        }   
        else if ((boundsCode==TBounds.Both) && 
          ((uintValue<bound1) || (uintValue>bound2)))
        { 
          message = String.Format("Value must be >= {0} and <= {1}",bound1,bound2);                    
          valueOk = false;
        }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadUInt(out uint uintValue, TBounds boundsCode, uint bound)
    {
      bool   valueOk = true;
      string message = "";

      uintValue = 0;
      try
      {
        uintValue = UInt32.Parse(this.Text.Replace(",",""));
      }
      catch 
      {
        message = "Invalid unsigned integer value";
        valueOk = false;
      }
      if (valueOk)
        if ((boundsCode==TBounds.Lower) && (uintValue<bound))  
        {          
          message = String.Format("Value must be >= {0}",bound);
          valueOk = false;
        }
        else if ((boundsCode==TBounds.Upper) && (uintValue>bound))
        {
          message = String.Format("Value must be <= {0}",bound);          
          valueOk = false;
        }   
        else if (boundsCode==TBounds.Both)
        { 
          message = String.Format("ETextBox.TBounds.Both cannot be used with this version\n" + 
                                  "of ETextBox.ReadUInt()");                    
          valueOk = false;
        }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadUInt(out uint uintValue)
    {
      bool valueOk = true;

      uintValue = 0;
      try
      {
        uintValue = UInt32.Parse(this.Text.Replace(",",""));
      }
      catch
      {
        valueOk = false;
        MessageBox.Show("Invalid unsigned integer value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
        this.Select();
      }
      return valueOk;
    }

    public bool ReadInt(out int intValue, TBounds boundsCode, int bound1, int bound2)
    {
      bool   valueOk = true;
      string message = "";

      intValue = 0;
      try
      {
        intValue = Int32.Parse(this.Text.Replace(",",""));
      }
      catch 
      {
        message = "Invalid signed integer value";
        valueOk = false;
      }
      if (valueOk)
        if ((boundsCode==TBounds.Lower) && (intValue<bound1))  
        {          
          message = String.Format("Value must be >= {0}",bound1);
          valueOk = false;
        }
        else if ((boundsCode==TBounds.Upper) && (intValue>bound2))
        {
          message = String.Format("Value must be <= {0}",bound2);          
          valueOk = false;
        }   
        else if ((boundsCode==TBounds.Both) && 
          ((intValue<bound1) || (intValue>bound2)))
        { 
          message = String.Format("Value must be >= {0} and <= {1}",bound1,bound2);                    
          valueOk = false;
        }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadInt(out int intValue, TBounds boundsCode, int bound)
    {
      bool   valueOk = true;
      string message = "";

      intValue = 0;
      try
      {
        intValue = Int32.Parse(this.Text.Replace(",",""));
      }
      catch 
      {
        message = "Invalid signed integer value";
        valueOk = false;
      }
      if (valueOk)
        if ((boundsCode==TBounds.Lower) && (intValue<bound))  
        {          
          message = String.Format("Value must be >= {0}",bound);
          valueOk = false;
        }
        else if ((boundsCode==TBounds.Upper) && (intValue>bound))
        {
          message = String.Format("Value must be <= {0}",bound);          
          valueOk = false;
        }   
        else if (boundsCode==TBounds.Both)
        { 
          message = String.Format("ETextBox.TBounds.Both cannot be used with this version\n" + 
                                  "of ETextBox.ReadInt()");                    
          valueOk = false;
        }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadInt(out int intValue)
    {
      bool valueOk = true;

      intValue = 0;
      try
      {
        intValue = Int32.Parse(this.Text.Replace(",",""));
      }
      catch
      {
        valueOk = false;
        MessageBox.Show("Invalid signed integer value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
        this.Select();
      }
      return valueOk;
    }
    
    public bool ReadDouble(out double doubleValue, TBounds boundsCode, double bound1, double bound2)
    {
      bool   valueOk = true;
      string message = "";

      doubleValue = 0.0;
      try
      {
        doubleValue = Double.Parse(this.Text);
      }
      catch 
      {
        message = "Invalid double value";
        valueOk = false;
      }
      if (valueOk)
        if ((boundsCode==TBounds.Lower) && (doubleValue<bound1))  
        {          
          message = String.Format("Value must be >= {0}",bound1);
          valueOk = false;
        }
        else if ((boundsCode==TBounds.Upper) && (doubleValue>bound2))
        {
          message = String.Format("Value must be <= {0}",bound2);          
          valueOk = false;
        }
        else if ((boundsCode==TBounds.Both) && 
          ((doubleValue<bound1) || (doubleValue>bound2)))
        { 
          message = String.Format("Value must be >= {0} and <= {1}",bound1,bound2);                    
          valueOk = false;
        }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadDouble(out double doubleValue, TBounds boundsCode, double bound)
    {
      bool   valueOk = true;
      string message = "";

      doubleValue = 0.0;
      try
      {
        doubleValue = Double.Parse(this.Text);
      }
      catch 
      {
        message = "Invalid double value";
        valueOk = false;
      }
      if (valueOk)
        if ((boundsCode==TBounds.Lower) && (doubleValue<bound))  
        {          
          message = String.Format("Value must be >= {0}",bound);
          valueOk = false;
        }
        else if ((boundsCode==TBounds.Upper) && (doubleValue>bound))
        {
          message = String.Format("Value must be <= {0}",bound);          
          valueOk = false;
        }
        else if (boundsCode==TBounds.Both)
        { 
          message = String.Format("ETextBox.TBounds.Both cannot be used with this version\n" + 
                                  "of ETextBox.ReadDouble()");                    
          valueOk = false;
        }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadDouble(out double doubleValue)
    {
      bool valueOk = true;

      doubleValue = 0.0;
      try
      {
        doubleValue = Double.Parse(this.Text);
      }
      catch 
      {
        valueOk = false;
        MessageBox.Show("Invalid double value","Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadDecimal(out decimal decimalValue, TBounds boundsCode, decimal bound1, decimal bound2)
    {
      bool   valueOk = true;
      string message = "";

      decimalValue = (decimal) 0.0;
      try
      {
        decimalValue = decimal.Parse(this.Text);
      }
      catch
      {
        message = "Invalid decimal value";
        valueOk = false;
      }
      if (valueOk)
        if ((boundsCode == TBounds.Lower) && (decimalValue < bound1))
        {
          message = String.Format("Value must be >= {0}", bound1);
          valueOk = false;
        }
        else if ((boundsCode == TBounds.Upper) && (decimalValue > bound2))
        {
          message = String.Format("Value must be <= {0}", bound2);
          valueOk = false;
        }
        else if ((boundsCode == TBounds.Both) &&
          ((decimalValue < bound1) || (decimalValue > bound2)))
        {
          message = String.Format("Value must be >= {0} and <= {1}", bound1, bound2);
          valueOk = false;
        }
      if (!valueOk)
      {
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
        this.Select();
      }
      return valueOk;
    }

    public bool ReadDecimal(out decimal decimalValue, TBounds boundsCode, decimal bound)
    {
      bool   valueOk = true;
      string message = "";

      decimalValue = (decimal) 0.0;
      try
      {
        decimalValue = decimal.Parse(this.Text);
      }
      catch
      {
        message = "Invalid decimal value";
        valueOk = false;
      }
      if (valueOk)
        if ((boundsCode == TBounds.Lower) && (decimalValue < bound))
        {
          message = String.Format("Value must be >= {0}", bound);
          valueOk = false;
        }
        else if ((boundsCode == TBounds.Upper) && (decimalValue > bound))
        {
          message = String.Format("Value must be <= {0}", bound);
          valueOk = false;
        }
        else if (boundsCode == TBounds.Both)
        {
          message = String.Format("ETextBox.TBounds.Both cannot be used with this version\n" + 
                                  "of ETextBox.ReadDecimal()");
          valueOk = false;
        }
      if (!valueOk)
      {
        MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
        this.Select();
      }
      return valueOk;
    }

    public bool ReadDecimal(out decimal decimalValue)
    {
      bool valueOk = true;

      decimalValue = (decimal) 0.0;
      try
      {
        decimalValue = decimal.Parse(this.Text);
      }
      catch
      {
        valueOk = false;
        MessageBox.Show("Invalid decimal value", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
        this.Select();
      }
      return valueOk;
    }

    public bool ReadCurrency(out double doubleValue, TBounds boundsCode, double bound1, double bound2)
    {
      bool   valueOk = true;
      string message = "";
      string textBoxContent = this.Text;

      doubleValue = 0.0;
      textBoxContent = textBoxContent.Replace("$","");
      textBoxContent = textBoxContent.Replace(",","");
      if ((textBoxContent[0]=='(') && (textBoxContent[textBoxContent.Length-1]==')'))
      {
        textBoxContent = textBoxContent.Replace("(","-");
        textBoxContent = textBoxContent.Replace(")","");
      }
      try
      {
        doubleValue = Double.Parse(textBoxContent);
      }
      catch 
      {
        message = "Invalid currency value";
        valueOk = false;
      }
      if (valueOk)
        if ((boundsCode==TBounds.Lower) && (doubleValue<bound1))  
        {          
          message = String.Format("Value must be >= {0:c}",bound1);
          valueOk = false;
        }
        else if ((boundsCode==TBounds.Upper) && (doubleValue>bound2))
        {
          message = String.Format("Value must be <= {0:c}",bound2);          
          valueOk = false;
        }
        else if ((boundsCode==TBounds.Both) && 
          ((doubleValue<bound1) || (doubleValue>bound2)))
        { 
          message = String.Format("Value must be >= {0:c} and <= {1:c}",bound1,bound2);                    
          valueOk = false;
        }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadCurrency(out double doubleValue, TBounds boundsCode, double bound)
    {
      bool   valueOk = true;
      string message = "";
      string textBoxContent = this.Text;

      doubleValue = 0.0;
      textBoxContent = textBoxContent.Replace("$","");
      textBoxContent = textBoxContent.Replace(",","");
      if ((textBoxContent[0]=='(') && (textBoxContent[textBoxContent.Length-1]==')'))
      {
        textBoxContent = textBoxContent.Replace("(","-");
        textBoxContent = textBoxContent.Replace(")","");
      }
      try
      {
        doubleValue = Double.Parse(textBoxContent);
      }
      catch 
      {
        message = "Invalid currency value";
        valueOk = false;
      }
      if (valueOk)
        if ((boundsCode==TBounds.Lower) && (doubleValue<bound))  
        {          
          message = String.Format("Value must be >= {0:c}",bound);
          valueOk = false;
        }
        else if ((boundsCode==TBounds.Upper) && (doubleValue>bound))
        {
          message = String.Format("Value must be <= {0:c}",bound);          
          valueOk = false;
        }
        else if (boundsCode==TBounds.Both)
        { 
          message = String.Format("ETextBox.TBounds.Both cannot be used with this version\n" + 
                                  "of ETextBox.ReadCurrency()");                    
          valueOk = false;
        }
      if (! valueOk)
      {
        MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadCurrency(out double doubleValue)
    {
      bool valueOk = true;
      string textBoxContent = this.Text;

      doubleValue = 0.0;
      textBoxContent = textBoxContent.Replace("$","");
      textBoxContent = textBoxContent.Replace(",","");
      if ((textBoxContent[0]=='(') && (textBoxContent[textBoxContent.Length-1]==')'))
      {
        textBoxContent = textBoxContent.Replace("(","-");
        textBoxContent = textBoxContent.Replace(")","");
      }
      try
      {
        doubleValue = Double.Parse(textBoxContent);
      }
      catch 
      {
        valueOk = false;
        MessageBox.Show("Invalid currency value","Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select(); 
      }
      return valueOk; 
    }

    public bool ReadDate(out Date dateValue, TValueRequired valueRequiredDirective)
    {
      bool valueOk = true;

      dateValue = null;
      if (this.Text!="")  
      {
        try
        {
          dateValue = Date.Parse(this.Text);
        }
        catch (Exception exception) 
        {
          valueOk = false;
          MessageBox.Show(exception.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
          this.Select();
        }
      } 
      else if (valueRequiredDirective==TValueRequired.ValueRequired) 
      {
        valueOk = false;
        MessageBox.Show("Must enter a value","Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
        this.Select();
      }
      return valueOk; 
    }

    public bool ReadDate(out Date dateValue)
    {
      bool valueOk = true;

      dateValue = null;
      if (this.Text!="")  
      {
        try
        {
          dateValue = Date.Parse(this.Text);
        }
        catch (Exception exception) 
        {
          valueOk = false;
          MessageBox.Show(exception.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
          this.Select();
        }
      } 
      else
      {
        valueOk = false;
        this.Select();
        MessageBox.Show("Must enter a value","Error",MessageBoxButtons.OK,MessageBoxIcon.Question);
      }
      return valueOk; 
    }

  } // End class ETextBox

  /************************************************************/
  /*  2. Class AETextBox : ETexBox                            */
  /************************************************************/  
  public class AETextBox : ETextBox
  {
    
    public bool ReadInfixStr(out string infixStr)
    {
      bool dataOk = false;
      
      if (this.ReadString(out infixStr))
      {
        infixStr = " " + infixStr.Replace(" ","") + " ";
        ReplaceUnaryOperators(ref infixStr);
        dataOk = this.Validate(infixStr);
        infixStr = infixStr.Trim();
      }
      return dataOk;
    }

    private static void ReplaceUnaryOperators(ref string infixStr)
    {
      int    i;
      char   prevCh, infixCh, nextCh;
      string originalInfixStr = infixStr;

      infixStr = " ";
      for (i=1; i<=originalInfixStr.Length-2; i++)
      {
        prevCh  = originalInfixStr[i-1];
        infixCh = originalInfixStr[i];
        nextCh  = originalInfixStr[i+1];
        if ((infixCh=='-') && (prevCh==' ' || prevCh=='(' || 
            IsOperator(prevCh)))
          infixCh = '!';
        infixStr += infixCh;
      }
      infixStr += " ";
    }

    private bool Validate(string infixStr)
    {
      bool   error = false;
      char   prevCh, infixCh, nextCh;
      int    i=1, leftParenCount=0;
      int    operandCount=0, binaryOperatorCount=0;
      string message = "";
      
      while ((!error) && (i<infixStr.Length-1))
      {
        prevCh  = infixStr[i-1];  infixCh = infixStr[i];  nextCh  = infixStr[i+1];
        if (infixCh=='(')
          leftParenCount++;
        else if (IsOperand(infixCh))
          operandCount++;
        else if (IsBinaryOperator(infixCh))
          binaryOperatorCount++;
        if (infixCh==')')
        {
          leftParenCount--;
          if (leftParenCount<0)
          {
            this.ProcessErrorInExpression("Error in infix expression: Missing left parenthesis");
            error = true;
          }
        }
        else if (!IsOperator(infixCh) && !IsParen(infixCh) && !IsOperand(infixCh))
        {
          message = "Error in infix expression: Invalid character.\n\n" +
                    "Valid charaters are:\n\n" +
                    "  Operands: a b c d s t\n" +
                    "  Operators: + - * / ^ !\n" +
                    "  Parentheses: ( )";
          this.ProcessErrorInExpression(message);
          error = true;
        } 
        else if (IsOperand(prevCh) && (IsOperand(infixCh) || (infixCh=='(')))
        {
          this.ProcessErrorInExpression("Error in infix expression: Succesive operands");
          error = true;  
        }
        else if (IsBinaryOperator(prevCh) && IsBinaryOperator(infixCh))
        {
          this.ProcessErrorInExpression("Error in infix expression: Succesive binary operators are not allowed");
          error = true;  
        }
        i++;
      }
      if (!error)
        if (leftParenCount>0)
        {
          this.ProcessErrorInExpression("Error in infix expression: Missing right parenthesis");
          error = true;
        }
        else if (operandCount!=binaryOperatorCount+1)
        {
          this.ProcessErrorInExpression("Error in infix expression: Must be one more operand than number\n" + 
                                        "of binary operators");
          error = true; 
        }  
      return !error;
    }

    private static bool IsOperator(char ch)
    {
      return (ch=='!' || ch=='+' || ch=='-' || ch=='*' || ch=='/' || ch=='^');
    }

    private static bool IsBinaryOperator(char ch)
    {
      return (ch=='+' || ch=='-' || ch=='*' || ch=='/' || ch=='^' );
    }

    private static bool IsParen(char ch)
    {
      return (ch=='(' || ch==')');
    }

    public static bool IsOperand(char ch)
    {
      return (ch=='a' || ch=='b' || ch=='c' || ch=='d' || ch=='s' || ch=='t');
    }

    private void ProcessErrorInExpression(string message)
    {
      MessageBox.Show(message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
      this.Select();
    }
  }  // End class AETextBox

  /************************************************************/
  /*  3. Class METextBox : ETexBox                            */
  /************************************************************/
  public class METextBox : ETextBox
  {
    
    public bool ReadInfixStr(out string infixStr)
    {
      bool dataOk = false;

      if (this.ReadString(out infixStr))
      {
        infixStr = " " + infixStr.Replace(" ", "") + " ";
        ReplaceSelectedOperators(ref infixStr);
        dataOk = this.Validate(infixStr);
        infixStr = infixStr.Trim();
      }
      return dataOk;
    }

    private static void ReplaceSelectedOperators(ref string infixStr)
    {
      int    i;
      char   prevCh, infixCh, nextCh;
      string originalInfixStr = infixStr;
      infixStr = " ";
      for (i=1; i<originalInfixStr.Length-1; i++)
      {
        prevCh = originalInfixStr[i-1];
        infixCh = originalInfixStr[i];
        nextCh = originalInfixStr[i + 1];
        if ((infixCh == '-') && (prevCh == ' ' || prevCh == '(' || IsOperator(prevCh)))
          infixCh = '!';
        else if ((infixCh=='*') &&
                 ((IsScalar(prevCh) && IsMatrix(nextCh)) ||
                  (IsScalar(prevCh) && (nextCh=='-' || nextCh=='~')) ||
                  (IsScalar(prevCh) && nextCh=='(') ||
                  (IsScalar(prevCh) && nextCh=='(' && IsBinaryMatrixOperator(infixStr[i-2])) ||
                  (IsScalar(prevCh) && nextCh=='(' && infixStr[i-2]=='(') ||
                  (IsMatrix(prevCh) && IsScalar(nextCh))))

          infixCh = '#';
        infixStr += infixCh;
      }
      infixStr += " ";
    }

    private bool Validate(string infixStr)
    {
      bool   error = false;
      char   prevCh, infixCh, nextCh;
      int    i = 1, leftParenCount = 0;
      int    operandCount = 0, binaryOperatorCount = 0;
      string message = "";

      while ((!error) && (i < infixStr.Length - 1))
      {
        prevCh = infixStr[i - 1]; infixCh = infixStr[i]; nextCh = infixStr[i + 1];
        if (infixCh == '(')
          leftParenCount++;
        else if (IsMatrix(infixCh) || IsScalar(infixCh))
          operandCount++;
        else if (IsBinaryOperator(infixCh))
          binaryOperatorCount++;
        if (infixCh == ')')
        {
          leftParenCount--;
          if (leftParenCount < 0)
          {
            this.ProcessErrorInExpression("Error in infix expression: Missing left parenthesis");
            error = true;
          }
        }
        else if (!IsOperator(infixCh) && !IsParen(infixCh) && !IsMatrix(infixCh) && !IsScalar(infixCh))
        {
          message = "Error in infix expression: Invalid character.\n\n" +
                    "Valid charaters are:\n\n" +
                    "  Operands: A B C D s t\n" +
                    "  Operators: + - * # ^ ! ~\n" +
                    "  Parentheses: ( )";
          this.ProcessErrorInExpression(message);
          error = true;
        }
        else if (IsOperand(prevCh) && (IsOperand(infixCh) || (infixCh == '(')))
        {
          this.ProcessErrorInExpression("Error in infix expression: Succesive operands");
          error = true;
        }
        else if (IsBinaryOperator(prevCh) && IsBinaryOperator(infixCh))
        {
          this.ProcessErrorInExpression("Error in infix expression: Succesive binary operators");
          error = true;
        }
        else if (infixCh == '^' && (IsScalar(prevCh) || IsMatrix(nextCh)))
        {
          this.ProcessErrorInExpression("Error in infix expression: Left operand of ^ must be a matrix and \n" +
                                        "right operand must be a scalar");
          error = true;
        }
        else if (IsBinaryMatrixOperator(infixCh) &&
                 ((IsScalar(prevCh) && IsScalar(nextCh) && !IsBinaryScalarOperator(infixStr[i - 2]) && !IsBinaryScalarOperator(infixStr[i + 2])) ||
                  (IsScalar(prevCh) && IsMatrix(nextCh) && !IsBinaryScalarOperator(infixStr[i - 2])) ||
                  (IsMatrix(prevCh) && IsScalar(nextCh) && !IsBinaryScalarOperator(infixStr[i + 2]))))
        {
          this.ProcessErrorInExpression("Error in infix expression: Binary matrix operator must have two matrix operands");
          error = true;
        }
        else if (IsUnaryMatrixOperator(infixCh) && IsScalar(nextCh))
        {
          this.ProcessErrorInExpression("Error in infix expression: At sclar operand cannot have a unary operator");
          error = true;
        }
        i++;
      }
      if (!error)
        if (leftParenCount > 0)
        {
          this.ProcessErrorInExpression("Error in infix expression: Missing right parenthesis");
          error = true;
        }
        else if (operandCount != binaryOperatorCount + 1)
        {
          this.ProcessErrorInExpression("Error in infix expression: Must be one more operand than number\n" +
                                        "of binary operators");
          error = true;
        }
      return !error;
    }

    private static bool IsOperator(char ch)
    {
      return (ch == '!' || ch == '~' || ch == '+' || ch == '-' || ch == '*' || ch == '#' || ch == '^');
    }

    private static bool IsUnaryMatrixOperator(char ch)
    {
      return (ch == '!' || ch == '~');
    }

    private static bool IsBinaryOperator(char ch)
    {
      return (ch == '+' || ch == '-' || ch == '*' || ch == '#' || ch == '^');
    }

    private static bool IsBinaryMatrixOperator(char ch)
    {
      return (ch == '+' || ch == '-' || ch == '*');
    }

    private static bool IsBinaryScalarOperator(char ch)
    {
      return (ch == '#' || ch == '^');
    }

    private static bool IsParen(char ch)
    {
      return (ch == '(' || ch == ')');
    }

    private static bool IsOperand(char ch)
    {
      return (ch == 'A' || ch == 'B' || ch == 'C' || ch == 'D' || ch == 's' || ch == 't');
    }

    private static bool IsScalar(char ch)
    {
      return (ch == 's' || ch == 't');
    }

    private static bool IsMatrix(char ch)
    {
      return (ch == 'A' || ch == 'B' || ch == 'C' || ch == 'D');
    }

    private void ProcessErrorInExpression(string message)
    {
      MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      this.Select();
    }

  }

} // End namespace LibETextBox


    //private static void ReplaceSelectedOperators(ref string infixStr)
    //{
    //  int    i;
    //  char   prevCh, infixCh, nextCh;
    //  string originalInfixStr = infixStr;

    //  infixStr = " ";
    //  for (i=1; i<originalInfixStr.Length-1; i++)
    //  {
    //    prevCh = originalInfixStr[i-1];
    //    infixCh = originalInfixStr[i];
    //    nextCh = originalInfixStr[i + 1];
    //    if ((infixCh == '-') && (prevCh == ' ' || prevCh == '(' || IsOperator(prevCh)))
    //      infixCh = '!';
    //    //else if ((infixCh=='*') &&
    //    //         ((IsScalar(prevCh) && (IsMatrix(nextCh) || (nextCh == '(')) &&
    //    //          (IsBinaryMatrixOperator(infixStr[i-2]) || infixStr[i-2]=='(')) ||
    //    //          (IsMatrix(prevCh) && IsScalar(nextCh))))

    //    //  infixCh = '#';
    //    //else if ((infixCh == '*') &&
    //    //         ((IsScalar(prevCh) && IsMatrix(nextCh)) ||
    //    //          (IsScalar(prevCh) && (IsMatrix(nextCh) || (nextCh == '(')) &&
    //    //          (IsBinaryMatrixOperator(infixStr[i - 2]) || infixStr[i - 2] == '(')) ||
    //    //          (IsMatrix(prevCh) && IsScalar(nextCh))))

    //    //  infixCh = '#';
    //    //else if ((infixCh=='*') &&
    //    //         ((IsScalar(prevCh) && IsMatrix(nextCh)) || 
    //    //          (IsScalar(prevCh) && nextCh=='(' &&
    //    //          (IsBinaryMatrixOperator(infixStr[i-2]) || infixStr[i-2]=='(')) ||
    //    //          (IsMatrix(prevCh) && IsScalar(nextCh))))

    //    //  infixCh = '#';
    //    else if ((infixCh=='*') &&
    //             ((IsScalar(prevCh) && IsMatrix(nextCh)) || 
    //              (IsScalar(prevCh) && nextCh=='(' && IsBinaryMatrixOperator(infixStr[i-2])) ||
    //              (IsScalar(prevCh) && nextCh=='(' && infixStr[i-2]=='(') ||
    //              (IsMatrix(prevCh) && IsScalar(nextCh))))

    //      infixCh = '#';
    //    infixStr += infixCh;
    //  }
    //  infixStr += " ";
    //}