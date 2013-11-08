namespace Userifccs.Curses {

using System;
using System.Reflection;
using Threading = System.Threading;

using Wrappercs.Curses;

/// <summary>HelloController class.</summary>
public class HelloController : System.IEquatable<HelloController> {
    private HelloModel _model1;
    private HelloView _view1;

    /// <summary>Constructor default for HelloController.</summary>
    public HelloController() : this("greet.txt")
    { }

    /// <summary>Constructor for HelloController.</summary>
    /// <param name="greetPath">A string</param>
    /// <param name="assy">An assembly or null.</param>
    /// <param name="rsrcPath">A string</param>
    public HelloController(string greetPath, Assembly assy = null,
    		string rsrcPath = "resources")
    {
        _model1 = new HelloModel(greetPath, assy, rsrcPath);
        var win = CursesC.initscr();
        _view1 = new HelloView(win);

        _model1.AttachObserver(_view1);
        //CursesC.wrefresh(_view1.stdscr);
    }
    /*
    //destructor
    /// <summary>Destructor for HelloController.</summary>
    ~HelloController()
    {
		Console.WriteLine("destructing {0}", GetType());
	}
	*/
	/// <value>Gets the HelloController's model.</value>
    public HelloModel Model { get { return _model1; }
    }

	/// <value>Gets the HelloController's view.</value>
    public HelloView View { get { return _view1; }
    }

    /*/// <inheritdoc/>
    public override bool Equals(object obj)
    {	//if (null == obj || !(obj is HelloController))
        if (null == obj || GetType() != obj.GetType())
            return false;
        HelloController p = obj as HelloController;	//HelloController p = (HelloController)obj;
        return (null == p) ? false : (Model == p.Model) && (View == p.View);
    }*/
    /// <inheritdoc/>
    public override bool Equals(object obj)
    {
        return Equals(obj as HelloController);
    }

	/// <summary>Compares HelloController instance equality.</summary>
    /// <param name="other">A HelloController</param>
    /// <returns>The truth result of equality comparison.</returns>
    public bool Equals(HelloController other)
    {	//if (null == obj || !(obj is HelloController))
        return null != other && Model == other.Model && View == other.View;
    }

	/// <summary>Operator for HelloController instance equality.</summary>
    /// <param name="lhs">A HelloController</param>
    /// <param name="rhs">A HelloController</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator ==(HelloController lhs, HelloController rhs)
    {
		if (Object.ReferenceEquals(lhs, null) && Object.ReferenceEquals(rhs, null))
			return true;
		return Object.ReferenceEquals(lhs, null) ? false : lhs.Equals(rhs);
	}

	/// <summary>Operator for HelloController instance inequality.</summary>
    /// <param name="lhs">A HelloController</param>
    /// <param name="rhs">A HelloController</param>
    /// <returns>The truth result of equality comparison.</returns>
    public static bool operator !=(HelloController lhs, HelloController rhs)
    {
		return !(lhs == rhs);
	}
	/*
    /// <inheritdoc/>
    public override string ToString()
    {
        return string.Format("{0}", GetType());
    }
	*/
    /// <inheritdoc/>
    public override int GetHashCode()
    {
        //return this.ToString().GetHashCode();
        int prime = -65336, hashCode = 17;
        hashCode = prime * hashCode + ((null != Model) ? Model.GetHashCode() : 0);
        hashCode = prime * hashCode + ((null != View) ? View.GetHashCode() : 0);
        return hashCode;
    }
    
    public bool step_virtualscr()
    {
        bool isRunning = true;
        
        CursesC.werase(CursesC.panel_window(View.panels["panel_input"]));
        CursesC.box(CursesC.panel_window(View.panels["panel_input"]),
            '|', '-');
        CursesC.hide_panel(View.panels["panel_input"]);
        //var ch = CursesC.getch();
        var ch = CursesC.wgetch(CursesC.panel_window(
            View.panels["panel_commands"]));
    
        if ((Int32)HelloView.Keys.KEY_ENTER == ch)
            on_key_enter();
        else if ((Int32)HelloView.Keys.KEY_ESC == ch)
            isRunning = false;
        else if (!((Int32)HelloView.Keys.KEY_RUN == ch))
            on_key_unmapped(ch);
        
        CursesC.wrefresh(CursesC.panel_window(View.panels["panel_output"]));
        CursesC.wrefresh(CursesC.panel_window(View.panels["panel_input"]));
        CursesC.wrefresh(CursesC.panel_window(View.panels["panel_commands"]));
        
        return isRunning;
    }
    
    public void run()
    {
        CursesC.noecho();
        CursesC.wrefresh(View.stdscr);
        //CursesC.refresh();
        
        while (step_virtualscr()) {
            //CursesC.update_panels();
            CursesC.doupdate();
        }
    }
    
    public void on_key_unmapped(Int32 ch)
    {
        CursesC.mvwprintw(CursesC.panel_window(View.panels["panel_input"]),
            1, 1, string.Format("Error! Un-mapped key: {0}. Retrying.",
            CursesC.unctrl((uint)(int)ch)));
        CursesC.wrefresh(CursesC.panel_window(View.panels["panel_input"]));
        CursesC.flash();
        Threading.Thread.Sleep(2000);
    }
    
    public void on_key_enter()
    {
        CursesC.echo();
        var data = "";
        //CursesC.mvwgetstr(CursesC.panel_window(View.panels["panel_input"]),
        //    1, 1, data);
        for (int i = (int)0, ch = (int)'\0'; '\n' != ch; ++i) {
            data = data + ('\0' == ch ? "" : Char.ToString((char)ch));
            ch = CursesC.mvwgetch(CursesC.panel_window(
                View.panels["panel_input"]), 1, i+1);
        }
        CursesC.mvwprintw(CursesC.panel_window(View.panels["panel_input"]),
            1, 1, data);
        CursesC.wrefresh(CursesC.panel_window(View.panels["panel_input"]));
        var cur_y = CursesC.getcury(CursesC.panel_window(
            View.panels["panel_output"]));
        var max_y = CursesC.getmaxy(CursesC.panel_window(
            View.panels["panel_output"]));
        if ((max_y - 3) < cur_y) {
            CursesC.werase(CursesC.panel_window(View.panels["panel_output"]));
            CursesC.box(CursesC.panel_window(View.panels["panel_output"]),
                '|', '-');
        }
        cur_y = CursesC.getcury(CursesC.panel_window(
            View.panels["panel_output"]));
        Model.NotifyObservers(data);
        CursesC.noecho();
    }
}

}
