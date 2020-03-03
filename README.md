# OC61domeManager
ASCOM dome server for OC61

The situation at the OC61 observatory required an extension of the standard ASCOM dome driver:
  - Integrate an external mirror cover that should open after the dome shutter finished opening
    and close before the shutter started closing. There was no way to script for these events in the ACP control software, so
    this driver was conceived to pass all commands and comm through to the working dome driver (MaxDomeII), but 
    also monitor and act when it saw shutter open and close commands.
  - The mirror cover is driven by custom electronics (the Peripheral Controller, aka PC) directed via a serial port. 
    There exists a VB6 application to talk to the PC via that serial port and make accessible all the 
    functionality of the PC via a GUI. To make the PC accessible to external scripts and the ACP control software
    a new application is included in this project, the PeripheralControllerApp. This provides the same manual GUI
    access to the PC that the old PCapp did, but also offers access to itself via serial port COM30. 
    A pair of virtual comports are installed on the scope control computer that tie COM30 to COM31. The new PCapp
    repeats anything it hears from the PC out COM31 and any command comming in that serial port will be passed 
    on to the PC. So an external application, like the new dome driver, can connect to PC by connecting to COM30.
  - The PCapp can also augment data coming from the PC. For instance, when it sees the HA/DEC readings from the PC
    it can compute the ALT/AZ and pass this upstream.  
  - The code for the existing Peripheral Controller VB6 app is included in the repo, along with the firmware of the controller itself.     
  - The project also includes a PC emulator for testing. The computer has a second pair of virtual com ports installed:
    COM20:COM21. The PCemul connects to COM20. So you can exercise the PCapp by having it connect to COM21. The PCEmul 
    is not a full emulation, but it can return any of the strings the real PC might send.
  - There are lots of configurations for this software!
    - PCapp connect to COM21 to the PCemul
    - PCapp connect to the real PC (COM7 on the OC61 control computer)
    - PCapp to the ASCOM dome driver....
          Here the 
