# OC61domeManager
ASCOM dome server for OC61

The situation at the OC61 observatory required an extension of the standard ASCOM dome driver:
  - Integrate an external mirror cover that would open after the dome shutter finished opening
    and close before the shutter started closing. There was no way to script for these events, so
    this driver was conceived to pass all commands and comm through to the working dome driver, but 
    also monitor and act when it saw shutter open and close commands.
  - The mirror cover is driven by custom electronics directed via a serial port. Since there are 
    now two softwares trying to access the peripheral controller, the new dome driver and a 
    standalone application, the design has to  be able share that serial port. The dome driver will
    own the serial port and the dome manager will use the ASCOM local server template to allow 
    both the scope control software, ACP, and a standalone app to connect to that driver.
    
    
