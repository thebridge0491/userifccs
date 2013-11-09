namespace Userifccs.Demo {

using System;
using System.Diagnostics;
using System.Reflection;
using IO = System.IO;
using SysCollGen = System.Collections.Generic;
using System.Linq;
using SysTextRegex = System.Text.RegularExpressions;
using NewtJson = Newtonsoft.Json;

using GtkX = global::Gtk;

using Wrappercs.Curses;
using Wrappercs.Tcltk;
using Util = Introcs.Util.Library;
using Demo = Userifccs.Demo.Library;
using UiGtk = Userifccs.Gtk;
using UiCurses = Userifccs.Curses;

struct OptsRecord {
    public string Name { get; set; }
    public string Ifc { get; set; }
    public string[] Extrav { get; set; }

    public override string ToString()
    {
		return String.Format("OptsRecord: [Name: {0}; Ifc: {1}]", Name, Ifc);
    }
}

/** @mainpage Description: 
 * <p>Brief comment.</p> */
/// <summary>App class.</summary>
public static class App {
	//private static readonly log4net.ILog log = 
	//	log4net.LogManager.GetLogger(
	//	System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	private static readonly log4net.ILog log = 
		log4net.LogManager.GetLogger("root");
	
	static Type fromType = typeof(App);
	static Assembly assembly = Assembly.GetAssembly(fromType);
    static bool showHelp = false;
    static string progName = IO.Path.GetFileName(
		Environment.GetCommandLineArgs()[0]);
    
    static void RunDemo(string name, string rsrcPath = "resources")
    {
        DateTime time1 = DateTime.Now;
        string greetStr, dateStr, greetPath = "greet.txt";
        TimeZone tz1 = TimeZone.CurrentTimeZone;
        string tzStr = tz1.IsDaylightSavingTime(time1) ? tz1.DaylightName
        	: tz1.StandardName;

        SysTextRegex.Regex re = new SysTextRegex.Regex(@"(?i)^quit$");
        SysTextRegex.Match m = re.Match(name);
        Console.WriteLine("{0} match: {1} to {2}\n",
			m.Success ? "Good" : "Does not", name, re);

	    dateStr = time1.ToString("ddd MMM dd HH:mm:ss yyyy zzz");

        greetStr = Demo.Greeting(rsrcPath + "/" + greetPath, name);
        Console.WriteLine("{0} {1}\n{2}!", dateStr, tzStr, greetStr);
    }

    static void RunDemoGtk(string name, string rsrcPath = "resources")
    {
        DateTime time1 = DateTime.Now;
        string greetStr, dateStr, greetPath = "greet.txt";
        TimeZone tz1 = TimeZone.CurrentTimeZone;
        string tzStr = tz1.IsDaylightSavingTime(time1) ? tz1.DaylightName
        	: tz1.StandardName;

        SysTextRegex.Regex re = new SysTextRegex.Regex(@"(?i)^quit$");
        SysTextRegex.Match m = re.Match(name);

		dateStr = time1.ToString("ddd MMM dd HH:mm:ss yyyy zzz");

        string pretext = string.Format("{0} match: {1} to {2}\n(C# {3}.{4}) Gtk# {5}.{6}\n{7} {8}\n",
        	m.Success ? "Good" : "Does not", name, re,
        	Environment.Version.Major, Environment.Version.Minor,
        	GtkX.Global.MajorVersion, GtkX.Global.MinorVersion, dateStr, tzStr);

        GtkX.Application.Init();
		var uicontroller = new UiGtk.HelloController("greet.txt", assembly,
			rsrcPath);
				((GtkX.TextView)uicontroller.View.widgets["textview1"]).Buffer.Text =
			pretext;
        GtkX.Application.Run();
    }

    static void RunDemoCurses(string name, string rsrcPath = "resources")
    {
        DateTime time1 = DateTime.Now;
        string greetStr, dateStr, greetPath = "greet.txt";
        TimeZone tz1 = TimeZone.CurrentTimeZone;
        string tzStr = tz1.IsDaylightSavingTime(time1) ? tz1.DaylightName
        	: tz1.StandardName;

        SysTextRegex.Regex re = new SysTextRegex.Regex(@"(?i)^quit$");
        SysTextRegex.Match m = re.Match(name);

		dateStr = time1.ToString("ddd MMM dd HH:mm:ss yyyy zzz");

        string pretext = string.Format("{0} match: {1} to {2}\n(C# {3}.{4}) Curses {5} TUI\n{6} {7}\n",
        	m.Success ? "Good" : "Does not", name, re,
        	Environment.Version.Major, Environment.Version.Minor,
        	"???", dateStr, tzStr);

        var uicontroller = new UiCurses.HelloController("greet.txt", assembly,
			rsrcPath);
		CursesC.wattron(uicontroller.View.stdscr,
		    (Int32)UiCurses.HelloView.Keys.A_REVERSE);
        CursesC.mvwaddstr(uicontroller.View.stdscr, 1, 1, pretext);
        CursesC.wattroff(uicontroller.View.stdscr,
            (Int32)UiCurses.HelloView.Keys.A_REVERSE);
        uicontroller.run();
    }

    static int tk_AppInit(SWIGTYPE_p_Tcl_Interp interp) {
        if (TcltkC.Tcl_Init(interp) != 0) {
            Console.Error.WriteLine("Error: Initializing Tcl!\n");
            return 1;
        }
        if (TcltkC.Tk_Init(interp) != 0) {
            Console.Error.WriteLine("Error: Initializing Tk!\n");
            return 1;
        }
        return 0;
    }

    static void RunDemoTcltk(string name, string rsrcPath = "resources",
            string[] extrav = null)
    {
        DateTime time1 = DateTime.Now;
        string greetStr, dateStr, greetPath = "greet.txt";
        TimeZone tz1 = TimeZone.CurrentTimeZone;
        string tzStr = tz1.IsDaylightSavingTime(time1) ? tz1.DaylightName
        	: tz1.StandardName;

        SysTextRegex.Regex re = new SysTextRegex.Regex(@"(?i)^quit$");
        SysTextRegex.Match m = re.Match(name);

		dateStr = time1.ToString("ddd MMM dd HH:mm:ss yyyy zzz");

        string pretext = string.Format("{0} match: {1} to {2}\n(C# {3}.{4}) Curses {5} TUI\n{6} {7}\n",
        	m.Success ? "Good" : "Does not", name, re,
        	Environment.Version.Major, Environment.Version.Minor,
        	"???", dateStr, tzStr);
        
        string uiform = (null == extrav || 1 > extrav.Length) ?
            rsrcPath + "/" + "tcltk/helloForm-tk.tcl" : 
            rsrcPath + "/" + extrav[0];
        string[] defaultArgs = {"--", uiform};
        string[] tkArgs = new string[(null == extrav || 1 > extrav.Length) ? 3 : extrav.Length+1];
        Array.Copy(defaultArgs, 0, tkArgs, 0, 2);
        
        if (null == extrav || 1 > extrav.Length)
            tkArgs[2] = "-name=helloForm-tk";
        else
            Array.Copy(extrav, 1, tkArgs, 2, extrav.Length-1);
        
        SWIGTYPE_p_Tcl_Interp interp = TcltkC.Tcl_CreateInterp();
        tk_AppInit(interp);
        //string[] buf_setArgv = new string[tkArgs.Length-2];
        //Array.Copy(tkArgs, 2, buf_setArgv, 0, tkArgs.Length-2);
        string[] buf_setArgv = tkArgs.Skip(2).Take(tkArgs.Length-2).ToArray();
        TcltkC.Tcl_Eval(interp, String.Format("set argv {{ {0} }}",
            String.Join(" ", buf_setArgv)));
        //TcltkC.Tcl_EvalFile(interp, tkArgs[1]);
        TcltkC.Tcl_Eval(interp, String.Format("source {0}", tkArgs[1]));
        TcltkC.Tcl_Eval(interp, "if {![winfo exists .frame1]} {lib_main}");
        TcltkC.Tk_MainLoop();
        //TcltkC.Tk_Main(tkArgs.Length, tkArgs, tk_AppInit); // ???
    }
    
    static string[] ParseCmdopts(string[] args, Mono.Options.OptionSet options)
    {
        SysCollGen.List<string> extra = new SysCollGen.List<string>();
        log.Info("parseCmdopts()");
        try {
            extra = options.Parse(args);
        } catch (Mono.Options.OptionException exc) {
            Console.Error.WriteLine("{0}: {1}", progName, exc.Message);
            Environment.Exit(1);
        }
        if (0 < extra.Count)
            Console.Error.WriteLine("Extra args: {0}", extra.Count);
        if (showHelp) {
		    Console.WriteLine("Usage: {0} [options][extrafile [extraopts ..]]\n\nOptions:", progName);
	    	options.WriteOptionDescriptions(Console.Out);
		    Environment.Exit(1);
	    }
	    return extra.ToArray();
    }
    
    struct YamlConfig
    {
		public string hostname { get; set; }
		public string domain { get; set; }
		public SysCollGen.Dictionary<string, object> file1 { get; set; }
		public SysCollGen.Dictionary<string, object> user1 { get; set; }
	}
    
    /** DocComment: 
     * <p>Brief description.</p>
     * @param args - array of command-line arguments */
	/// <summary>Main entry point.</summary>
    /// <param name="args">An array</param>
    /// <returns>The exit code.</returns>
    static int Main(string[] args)
    {
        OptsRecord opts = new OptsRecord() {Name = "World", Ifc = "Term",
            Extrav = null};
        var options = new Mono.Options.OptionSet() {
            {"u|user=", "user name", (string v) => opts.Name = v},
            {"i|ifc=", "interface", (string v) => opts.Ifc = v},
            { "h|help",  "show this message", v => showHelp = true },
            //{v != null },
        };

        IO.Stream traceOut = IO.File.Create("trace.log");
        TraceListener[] lstnrs = {new ConsoleTraceListener(true),
        	new TextWriterTraceListener(traceOut)};
        foreach (var lstnr in lstnrs)
        	Debug.Listeners.Add(lstnr);	// /define:[TRACE|DEBUG]
        
        opts.Extrav = ParseCmdopts(args, options);
        
        string envRsrcPath = Environment.GetEnvironmentVariable("RSRC_PATH");
        string rsrcPath = null != envRsrcPath ? envRsrcPath : "resources";
        
        string iniStr = String.Empty, jsonStr = String.Empty,
			yamlStr = String.Empty;
        try {
			//iniStr = (new IO.StreamReader(rsrcPath + "/prac.conf")).ReadToEnd();
			iniStr = IO.File.ReadAllText(rsrcPath + "/prac.conf");
			//jsonStr = IO.File.ReadAllText(rsrcPath + "/prac.json");
			//yamlStr = IO.File.ReadAllText(rsrcPath + "/prac.yaml");
		} catch (Exception exc0) {
			Console.Error.WriteLine("(exc: {0}) Bad env var RSRC_PATH: {1}\n",
				exc0, rsrcPath);
			try {
				iniStr = Demo.GetFromResources("prac.conf", assembly);
				//jsonStr = Demo.GetFromResources("prac.json", assembly);
				//yamlStr = Demo.GetFromResources("prac.yaml", assembly);
			} catch (Exception exc1) {
				throw;
				Environment.Exit(1);
			}
		}
        
        //var cfgIni = new KeyFile.GKeyFile();
        //cfgIni.LoadFromData(iniStr);
        var cfgIni = new IniParser.StringIniParser().ParseString(iniStr);
        
        /*var defn1 = new {hostname = String.Empty, domain = String.Empty, 
			file1 = new {path = String.Empty, ext = String.Empty}, 
			user1 = new {name = String.Empty, age = 0}};
		var anonType = NewtJson.JsonConvert.DeserializeAnonymousType(
			jsonStr, defn1);
        var dictRootJson = NewtJson.JsonConvert.DeserializeObject<
			SysCollGen.Dictionary<string, object>>(jsonStr);
        var dictUserJson = NewtJson.JsonConvert.DeserializeObject<
			SysCollGen.Dictionary<string, object>>(
			String.Format("{0}", dictRootJson["user1"]));*/
        
		/*var deserializer = new YamlDotNet.Serialization.Deserializer();
		var objRootYaml = deserializer.Deserialize<YamlConfig>(
			new IO.StringReader(yamlStr));*/
        
        Tuple<string, string, string>[] arrTups = {
			//Tuple.Create(Library.IniCfgToStr(cfgIni), 
			//	cfgIni.GetValue("default", "domain"),
			//	cfgIni.GetValue("user1", "name")),
			Tuple.Create(Util.IniCfgToStr(cfgIni), 
				cfgIni["default"]["domain"],
				cfgIni["user1"]["name"])/*,
			Tuple.Create(anonType.ToString(), 
				String.Format("{0}", anonType.domain),
				String.Format("{0}", anonType.user1.name)),
			Tuple.Create(Library.MkString(dictRootJson.ToArray(), beg: "{", 
				stop: "}"),
				String.Format("{0}", dictRootJson["domain"]), 
				String.Format("{0}", dictUserJson["name"])),
			Tuple.Create(yamlStr,
				String.Format("{0}", objRootYaml.domain), 
				String.Format("{0}", objRootYaml.user1["name"]))*/
		};
		foreach (Tuple<string, string, string> tup in arrTups) {
			Console.WriteLine("config: {0}", tup.Item1);
			Console.WriteLine("domain: {0}", tup.Item2);
			Console.WriteLine("user1Name: {0}\n", tup.Item3);
		}
        
        /*var switcher = new SysCollGen.Dictionary<string, Action<string, string>> {
        	//{"term", RunDemo },
        	{"gtk", RunDemoGtk }
        };
        var func = switcher.ContainsKey(opts.Ifc.ToLower()) ?
        	switcher[opts.Ifc.ToLower()] : RunDemo;
        func(opts.Name, rsrcPath);*/
        var switcher = new SysCollGen.Dictionary<Func<string, bool>, Action<string, string>> {
        	{s => (String.Equals("gtk", s)), (u, r) => RunDemoGtk(u, r) },
        	{s => (String.Equals("curses", s)), (u, r) => RunDemoCurses(u, r) },
        	{s => (String.Equals("tcltk", s)), (u, r) => RunDemoTcltk(u, r, opts.Extrav) },
        	//{s => (String.Equals("term", s)), (u, r) => RunDemo(u, r) },
        	{s => true, (u, r) => RunDemo(u, r) }
        };
        var func = switcher.First(sw => sw.Key(opts.Ifc.ToLower()));
        func.Value(opts.Name, rsrcPath);
        //RunDemo(opts.Name, rsrcPath);
        
        //Trace.Fail("Trace example");
        Trace.Flush(); //Debug.Flush();
        traceOut.Close();
        return 0;
    }
}

}
