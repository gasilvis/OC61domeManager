// JavaScript source code

// an enum
var RunModes = {
    ACPconsole: 901  // from the ACP console, load script for test
    , StartupObs: 902  // production mode, from the scheduler startup
    , CScript: 903  // using cscript from dos or bat file
    , Nodejs: 904  // running validate from unix nodejs    
    , StandAlone: 905    // create xml files only
};
var RunMode = null;
var PlansDir;

// detect if we running in Nodejs/Linux
try {
    var FSO = new ActiveXObject("Scripting.FileSystemObject"); // establish FSO
    // must be windows if you get here
} catch (e) {
    // not windows. Hope its nodejs!
    var FS = require("fs");
    RunMode = RunModes.Nodejs;
}


// what mode are we running in? 
var Util;
if (RunMode === null) { // windows
    RunMode = RunModes.ACPconsole; // guess
    try {
        Printline("in ACP console"); // this will fault in cscript (standalone or ShellExec)
        //Util= new ActiveXObject("ACP.Util");
    } catch (e) {
        try { // ACP needs this to make WScript available
            Util = new ActiveXObject("ACP.Util"); // tbd  bomb out if this fails, as it does sometimes
        } catch (e) { } // no error needed  
        RunMode = RunModes.CScript;
        /*
        if (WScript.Arguments.length > 0) {
            switch (Number(WScript.Arguments.Item(0))) {
                case 99: RunMode = RunModes.StartupObs; break;
                case 901: RunMode = RunModes.ACPconsole; break;
                case 905: RunMode = RunModes.StandAlone; break;
                case 903:
                default:
                    RunMode = RunModes.CScript; break;
            }
        }
        */
    }
}

//PrintLine(RunMode);
//WScript.Echo(RunMode);
//debugger;



function main() { // to keep ACP happy if run in ACPconsole
    // ie, ACP has its own script engine that starts with a main() 
    //PrintLine("in main");
}

// use this for all text output; compatible for all modes
function PrintLine(s) {
    switch (RunMode) {
        case RunModes.ACPconsole: Console.PrintLine(s); break;
        case RunModes.StandAlone:
        case RunModes.CScript: WScript.Echo(s); break;
        case RunModes.StartupObs: Util.Console.PrintLine(s); break;
        case RunModes.Nodejs: console.log(s); break;
    }
}


var dome;
var ret;


dome = new ActiveXObject("ASCOM.OC61domeServer2.Dome");

var arg
arg = (WScript.Arguments.length > 0) ? (WScript.Arguments.Item(0)) : "huh?";

switch (arg) {
    case "pos":
        PrintLine("fetch the current position");
        ret = dome.Action("ScopePosition", "");
        PrintLine(ret);
        break;
    case "pots":
        PrintLine("fetch the current ADC pot values");
        ret = dome.Action("ADCvalues", "");
        PrintLine(ret);
        break;
    case "open":
        PrintLine("Open the shutter");
        dome.OpenShutter();
        do {
            arg = dome.ShutterStatus();
            PrintLine("shutter: " + arg);
            //Util.WaitForMilliseconds(500);
        } while (arg != 0 && arg != 4);
        break;
    case "close":
        PrintLine("Close the shutter");
        dome.CloseShutter();
        do {
            arg = dome.ShutterStatus();
            PrintLine("shutter: " + arg);
            //Util.WaitForMilliseconds(500);
        } while (arg != 1 && arg != 4);
        break;
    default:
        PrintLine("need an argument:  pos, pots, open or close");
        break;
}




    // close?
