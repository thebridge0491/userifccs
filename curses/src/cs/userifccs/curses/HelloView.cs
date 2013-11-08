namespace Userifccs.Curses {

using System;
using IO = System.IO;
using SysCollGen = System.Collections.Generic;

using Wrappercs.Curses;

/// <summary>HelloView class.</summary>
public class HelloView : System.IEquatable<HelloView>, Userifccs.Aux.IObserver {
    public SysCollGen.Dictionary<string, SWIGTYPE_p_PANEL> panels =
      new SysCollGen.Dictionary<string, SWIGTYPE_p_PANEL>();
    public SWIGTYPE_p_WINDOW stdscr = null;

    const int diffCtrl = '\0' - '@';   // -64
    const int diffUprLwr = 'A' - 'a';  // -32
    const int attr_shift = 8;          // #define NCURSES_ATTR_SHIFT 8

    public enum Keys {
        KEY_ENTER = 'E' + diffCtrl,      // Ctrl+E -- Enter (0527)
        KEY_ESC = 'X' + diffCtrl,        // Ctrl+X -- Exit  (0551)
        KEY_RUN = 'R' + diffCtrl,        // Ctrl+R -- Run   (???)
        A_REVERSE = 1 << 10+attr_shift,  //#define A_REVERSE NCURSES_BITS(1U,10)
        A_STANDOUT = 1 << 8+attr_shift   //#define A_STANDOUT NCURSES_BITS(1U,8)
    }

    /// <summary>Constructor for HelloView.</summary>
    /// <param name="screen">A Ncurses WINDOW</param>
    public HelloView(SWIGTYPE_p_WINDOW screen = null)
    {
        stdscr = null == screen ? CursesC.initscr() : screen;
        stdscr = StdscrSetup();
        
        //var (orig_hgt, orig_wid) = (0, 0);
        //CursesC.getmaxyx(stdscr, orig_hgt, orig_wid);
        int orig_hgt = CursesC.getmaxy(stdscr), 
            orig_wid = CursesC.getmaxx(stdscr);
        
        var panel_output = CursesC.new_panel(CursesC.newwin(orig_hgt - 5,
            orig_wid - 2, 1, 1));
        var panel_input = CursesC.new_panel(CursesC.newwin(3,
            (int)(orig_wid / 2), 7, 20));
        var panel_commands = CursesC.new_panel(CursesC.newwin(4, orig_wid - 2,
            orig_hgt - 5, 1));
        
        CursesC.wattron(this.stdscr, (Int32)Keys.A_REVERSE);
        CursesC.waddstr(this.stdscr, string.Format("'{0, -32}'", 
            "curseshello"));
        CursesC.wattroff(this.stdscr, (Int32)Keys.A_REVERSE);
        
        CursesC.werase(CursesC.panel_window(panel_output));
        CursesC.werase(CursesC.panel_window(panel_input));
        CursesC.werase(CursesC.panel_window(panel_commands));
        CursesC.box(CursesC.panel_window(panel_output), '|', '-');
        CursesC.box(CursesC.panel_window(panel_input), '|', '-');
        CursesC.box(CursesC.panel_window(panel_commands), '|', '-');
        CursesC.wattron(CursesC.panel_window(panel_commands),
            (Int32)Keys.A_STANDOUT);
        CursesC.mvwaddch(CursesC.panel_window(panel_commands), 1, 1,
            (Int32)Keys.KEY_RUN);
        CursesC.wattroff(CursesC.panel_window(panel_commands),
            (Int32)Keys.A_STANDOUT);
        CursesC.wprintw(CursesC.panel_window(panel_commands),
            string.Format("'{0, -11}'", " Run"));
        
        CursesC.wattron(CursesC.panel_window(panel_commands),
          (Int32)Keys.A_STANDOUT);
        CursesC.waddch(CursesC.panel_window(panel_commands),
            (Int32)Keys.KEY_ENTER);
        CursesC.wattroff(CursesC.panel_window(panel_commands),
            (Int32)Keys.A_STANDOUT);
        CursesC.wprintw(CursesC.panel_window(panel_commands),
            string.Format("'{0, -11}'", " Enter Name"));
        
        CursesC.wattron(CursesC.panel_window(panel_commands),
            (Int32)Keys.A_STANDOUT);
        CursesC.mvwaddch(CursesC.panel_window(panel_commands), 2, 1,
            (Int32)Keys.KEY_ESC);
        CursesC.wattroff(CursesC.panel_window(panel_commands),
            (Int32)Keys.A_STANDOUT);
        CursesC.wprintw(CursesC.panel_window(panel_commands),
            string.Format("'{0, -11}'", " Exit"));
        
        panels.Add("panel_output", panel_output);
        panels.Add("panel_input", panel_input);
        panels.Add("panel_commands", panel_commands);
        CursesC.wrefresh(stdscr);
    }
    
    private SWIGTYPE_p_WINDOW StdscrSetup()
    {
        CursesC.noecho();
        CursesC.cbreak();
        CursesC.keypad(this.stdscr, true);
        return this.stdscr;
    }
    
    //destructor
    /// <summary>Destructor for HelloView.</summary>
    ~HelloView()
    {
        //Console.WriteLine("destructing {0}", GetType());
        CursesC.nocbreak();
        CursesC.keypad(this.stdscr, false);
        CursesC.echo();
        CursesC.endwin();
    }
    
    /// <value>Gets|Sets the HelloView's data.</value>
    public string Data { get; set;
    }

    /*/// <inheritdoc/>
    public override bool Equals(object obj)
    {   //if (null == obj || !(obj is HelloView))
        if (null == obj || GetType() != obj.GetType())
            return false;
        HelloView p = obj as HelloView; //HelloView p = (HelloView)obj;
        return (null == p) ? false : (Data == p.Data) && (panels == p.panels);
    }*/
    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return Equals(obj as HelloView);
    }

    /// <summary>Compares HelloView instance equality.</summary>
    /// <param name="other">An HelloView</param>
    /// <returns>The truth result of equality comparison.</returns>
    public bool Equals(HelloView other)
    {   //if (null == obj || !(obj is HelloView))
        return null != other && Data == other.Data && panels == other.panels;
    }

    /// <summary>Operator for HelloView instance equality.</summary>
    /// <param name="lhs">A HelloView</param>
    /// <param name="rhs">A HelloView</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator ==(HelloView lhs, HelloView rhs)
    {
        if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
            return true;
        return Object.ReferenceEquals(lhs, null) ? false : lhs.Equals(rhs);
    }

    /// <summary>Operator for HelloView instance inequality.</summary>
    /// <param name="lhs">A HelloView</param>
    /// <param name="rhs">A HelloView</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator !=(HelloView lhs, HelloView rhs)
    {
        return !(lhs == rhs);
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return string.Format("{0}: [Data: {1}]", GetType(), Data);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        //return this.ToString().GetHashCode();
        int prime = -65336, hashCode = 17;
        hashCode = prime * hashCode + ((null != Data) ? 
            Data.GetHashCode() : 0);
        hashCode = prime * hashCode + ((null != panels) ? 
            panels.GetHashCode() : 0);
        return hashCode;
    }

    /// <inheritdoc/>
    public void Update(string newData)
    {
        CursesC.echo();
        Data = newData;
        var cur_y = CursesC.getcury(CursesC.panel_window(
            panels["panel_output"]));
        CursesC.mvwprintw(CursesC.panel_window(panels["panel_output"]),
            cur_y+1, 1, Data);
        CursesC.noecho();
    }
}

}
