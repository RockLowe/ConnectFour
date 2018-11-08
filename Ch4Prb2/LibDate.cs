/****************************************************************/
/*                                                              */
/*  Course: CIS 430 - Artificial Intelligence                   */
/*                                                              */
/*  Program: LibDate.CS                                         */
/*                                                              */
/*  Programmer: Dr. Oakes                                       */
/*                                                              */
/*  Purpose: Create LibDate class library (DLL).                */
/*                                                              */
/*  Class: Date                                                 */
/*                                                              */
/****************************************************************/

using System;
using System.Windows.Forms;
using System.Text.RegularExpressions;

/****************************************************************/
/*  Begin namespace LibDate                                     */
/****************************************************************/
namespace LibDate
{
  
  /****************************************************************/
  /*  Begin class Date                                            */
  /****************************************************************/
  public class Date
  {
    private static string[] monthNameArray = 
    {
      "","January","February","March","April","May","June",
      "July","August","September","October","November","December"
    };

    private static string[] monthNameAbbrevArray = 
    {
      "","Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec"
    };

    private static string[] dayNameArray = 
    {
      "","Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"
    };

    private int year;   // Data member
    private int month;  // Data member
    private int day;    // Data member

    public Date()  // Default constructor
    {
      this.Set(1, 1, 1);
    }

    public Date(int yearValue, int monthValue, int dayValue) // Initializing constructor
    {
      this.Set(yearValue, monthValue, dayValue);
    }

    public Date(Date sourceDate) // Copy constructor
    {
      this.Copy(sourceDate);
    }

    public void Copy(Date sourceDate) // Copy method
    {
      this.Set(sourceDate.year, sourceDate.month, sourceDate.day);
    }

    public Date Clone()
    {
      return new Date(this);
    }

    public void Set(int yearValue, int monthValue, int dayValue)
    {
      if ((yearValue>=1) && (yearValue<=2100) && (monthValue>=1) && (monthValue<=12) && 
          (dayValue>=1) && (dayValue<=CalcDaysInMonth(yearValue,monthValue)))
      {
        this.year  = yearValue;
        this.month = monthValue;  
        this.day   = dayValue;  
      }
      else
        throw new Exception(String.Format("{0}, {1}, and {2} can not be assigned to the year, month, and day \n" + 
                                          "data members, respectively, of a Date object",yearValue,monthValue,dayValue));
    }

    public int Year  // Define read-only Year property
    {
      get
      {
        return this.year;
      }
    }

    public int Month // Define read-only Month property
    {
      get
      {
        return this.month;
      }
    }

    public int Day // Define read-only Day property
    {
      get
      {
        return this.day;
      }
    }

    public string DayOfWeek  // Define result DayOfWeek property
    {
      get
      {
        return dayNameArray[((int)this)%7 + 1];  // 1/1/1 was a Monday == dayNameArray[2]
      }
    }

    public static Date Today  // Define static Today result property
    {
      get
      {
        DateTime today = DateTime.Now;

        return new Date(today.Year,today.Month, today.Day);
      }
    }

    public string LongString // Characteristic propery
    {
      get
      {
        return monthNameArray[this.month] + " " + String.Format("{0}",this.day) +
               ", " + String.Format("{0}",this.year);
      }
    }

    public string ShortString  // Characteristic propery
    {
      get
      {
        return String.Format("{0:d2}/{1:d2}/{2:d2}",month,day,year%100);
      }
    }

    public string ToLongString() // Accessor method
    {
      return monthNameArray[this.month] + " " + this.day.ToString() +
             ", " + this.year.ToString("d4");
    }

    public override string ToString() // Accessor method
    {
      return String.Format("{0:d2}/{1:d2}/{2:d4}", this.month, this.day, this.year);
    }

    public string ToString(string formatString)
    {
      DateTime dateTimeValue;

      dateTimeValue = new DateTime(this.Year, this.Month, this.Day);
      return dateTimeValue.ToString(formatString);
    }

    public static implicit operator int(Date date) // "Date" to "int" type conversion method
    {
      int yr, mth;
      int intEquiv;

      intEquiv = 0;
      for (yr=1; yr<date.year; yr++)
        intEquiv += CalcDaysInYear(yr);
      for (mth=1; mth<date.month; mth++)
        intEquiv += CalcDaysInMonth(date.year,mth);
      intEquiv += date.day;
      return intEquiv;
    }

    public static implicit operator Date(int intValue) // "int" to "Date" type conversion constructor
    {
      Date dateEquiv = new Date();

      dateEquiv.year = 1;
      while (intValue>=CalcDaysInYear(dateEquiv.year))
      {
        intValue -= CalcDaysInYear(dateEquiv.year);
        dateEquiv.year++;
      }
      dateEquiv.month = 1;
      while (intValue>=CalcDaysInMonth(dateEquiv.year,dateEquiv.month))
      {
        intValue -= CalcDaysInMonth(dateEquiv.year, dateEquiv.month);
        dateEquiv.month++;
      }
      dateEquiv.day = intValue;
      return dateEquiv;
    }

    public static string[] SplitDateString(string stringValue)
    {
      bool     foundMatch=false;
      int      i=1;
      string   tempString = stringValue;
      string[] words;
      
      stringValue = stringValue.Trim();
      stringValue = stringValue.ToUpper();
      stringValue = stringValue.Replace("/", " ");
      stringValue = stringValue.Replace("-", " ");
      stringValue = stringValue.Replace(",", " ");
      stringValue = stringValue.Replace(".", " ");
      while (Regex.IsMatch(stringValue,"  "))
        stringValue = stringValue.Replace("  "," ");
      words = stringValue.Split(' ');
      if (words.Length!=3) 
        throw new Exception(tempString + " is an invalid date format");
      // Put in MonthNum_DayNum_YearNum format
      while ((i<=12) && (! foundMatch))  // Check for precence of month abbreviation
      {
        if (words[0].Substring(0,(int)Math.Min(words[0].Length,3)).CompareTo(monthNameAbbrevArray[i].ToUpper())==0)  // MonthName_DayNum_YearNum format
        {
          foundMatch = true;
          words[0]   = i.ToString();
        }
        else if (words[1].Substring(0,(int)Math.Min(words[1].Length,3)).CompareTo(monthNameAbbrevArray[i].ToUpper())==0)  // DayNum_MonthName_YearNum format
        {                                                                                                                 // or YearNum_MonthName_DayNum format
          foundMatch = true;
          if (words[0].Length<words[2].Length)  // DayNum_MonthName_YearNum format
          {
            words[1]   = words[0];
            words[0]   = i.ToString();
          }
          else  // YearNum_MonthName_DayNum format
          {
            tempString = words[0];
            words[0]   = i.ToString();
            words[1]   = words[2];
            words[2]   = tempString;
          }
        }
        i++;
      }
      if (words[0].Length>words[2].Length)  // YearNum_MonthNum_DayNum format
      {
        tempString = words[0];
        words[0]   = words[1];
        words[1]   = words[2];
        words[2]   = tempString;
      }
      return words;
    }

    public static Date Parse(string stringValue)
    {
      int      month=1,day=1,year=1;
      string[] words;
      Date     returnDate = new Date(1,1,1);

      try
      { 
        words = SplitDateString(stringValue);
      }
      catch (Exception exception)
      {
        throw exception;
      }
      try
      {
        month = Int32.Parse(words[0]);
        day   = Int32.Parse(words[1]);
        year  = Int32.Parse(words[2]);
        returnDate.Set(year,month,day);
      }
      catch
      {
        throw new Exception(String.Format("{0}, {1}, and {2} can not be assigned to the year, month, and day \n" + 
                                          "data members, respectively, of a Date object",words[2],words[0],words[1]));
      }
      return returnDate;
    }
    
    public static bool IsALeapYear(int yearValue)
    {
      return (((yearValue%4==0) && (yearValue%100!=0))||(yearValue%400==0));
    }

    public static int CalcDaysInYear(int yearValue)
    {
      if (IsALeapYear(yearValue))
        return 366;
      else
        return 365;
    }

    public static int CalcDaysInMonth(int yearValue, int monthValue)
    {
      if (monthValue == 2)
        if (IsALeapYear(yearValue))
          return 29;
        else
          return 28;
      else if ((monthValue==4) || (monthValue==6) || (monthValue==9) || (monthValue==11))
        return 30;
      else
        return 31;
    }

    private static void ProcessError(string message)
    {
      DialogResult result;

      result = MessageBox.Show(message, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error,
                               MessageBoxDefaultButton.Button1);
      if (result == DialogResult.Yes)
        Application.Exit();
    }
    
  }  // End class Date

}  // End namespace LibDate
