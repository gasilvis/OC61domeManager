/*****************************************************


Project :
Version :
Date    : 5/12/2012
Author  : Geoff Graham
Company : UOC
Comments:


Chip type               : ATmega64
Program type            : Application
AVR Core Clock frequency: 3.686400 MHz
Memory model            : Small
External RAM size       : 0
Data Stack size         : 1024
*****************************************************/

#include <mega64.h>
#include <stdio.h>
#include <delay.h>
#include <alcd.h>
#include <stdlib.h>
#include <string.h>
#include <ctype.h>



#define true 1
#define false 0
#define LED_On 1
#define LED_Off 0
#define Detected 0
#define ButtonPressed 0
#define ZeroFound 0
#define OpenCover 1
#define CloseCover 0
#define MirrorCoverShut 0
#define MirrorCoverOpen 0
#define TurnSolenoid_On 1
#define TurnSolenoid_Off 0
#define FlipMirror_In 0
#define FlipMirror_Out 0
#define Going_In 1
#define Going_Out 0
#define Brake_On 1
#define Brake_Off 0
#define Drive 1
#define StopDrive 0
#define ReadingArraySize 20
#define TelescopeAboveHorizonLimit 0
#define LimitExceeded 1
#define FlipMirrorIn 0
#define FlipMirrorOut 1
#define FlipMirrorDriveError 1




#define DEC_Zero_PositionSensor PINE.4          //This input bit is used to detect when the DEC drive is at Zero DEC
#define HA_Zero_PositionSensor PINE.5           //This input bit is used to detect when the RA drive is at Zero HA
#define MirrorCoverOpenSenseSwitch PINE.6       //This is connected to the mirror cover "open" position sense switch
#define MirrorCoverShutSenseSwitch PINE.7       //This is connected to the mirror cover "shut" position sense switch

#define MirrorCoverOpen_LED PORTA.0
#define MirrorCoverShut_LED PORTA.1
#define FlipMirrorIn_LED PORTA.2   
#define FlipMirrorOut_LED PORTA.3                      
//#define HorizonSenseLED PORTA.3

#define FlipMirror_ErrorIn_Bit PINF.2
#define FlipMirror_Status_Bit PINF.3
#define FlipMirror_Direction_Bit PORTB.4
#define FlipMirror_StartPulse PORTB.5




#define MirrorCoverSolenoid PORTA.4             //This bit controls the air valve solenoid
#define HorizonSenseOver_RideBit PORTA.6
#define HorizonSenseStatusBit PINA.7


#define FlipMirrorOutPushButton PIND.4
#define FlipMirrorInPushButton PIND.5
#define MirrorCoverShutPushButton PIND.6
#define MirrorCoverOpenPushButton PIND.7




//Global variables.............................................................

bit CommandStringReady, Transmitting, HA_ZeroFound, DEC_ZeroFound, TimeToReadPositionPots, TimeToSendPositions;
bit SaveNextHA_ZeroRef, SaveNextDEC_ZeroRef, TimeToResetDisplay, MirrorCoverClosing, MirrorCoverOpening;
bit MirrorCoverFailedToShut, MirrorCoverFailedToOpen;
bit PC_CommsLinkEstablished, PositionSendingEnabled, PositionSendRequested;
bit SearchingForHA_Opto, SearchingForDEC_Opto, HA_ChopperFound, DEC_ChopperFound;


char TxBufferIndex, CommandStringIndex, ReadingArrayIndex, ParameterStringIndex, MirrorCoverMovementTimer;
unsigned int Timer1Counter, SecondsCounter;

float HA_Pot_AvgVal, DEC_Pot_AvgVal;
float HourAngleFromPot, DEC_AngleFromPot, HA_Pot_ZeroReference, DEC_Pot_ZeroReference;
float HA_Pot_ScaleFactor, DEC_Pot_ScaleFactor;
float HA_RelativeVal, DEC_RelativeVal;

char ParameterString[50];
char CommandString[50];
char TxBuffer[50];
float HA_PotArray[50];
float DEC_PotArray[50];

float eeprom HA_Pot_ZeroReference_EEPROM, DEC_Pot_ZeroReference_EEPROM;
float eeprom HA_Pot_ScaleFactor_EEPROM, DEC_Pot_ScaleFactor_EEPROM;
float eeprom TestByte1_EEPROM, TestByte2_EEPROM, TestByte3_EEPROM, TestByte4_EEPROM;


//Function prototypes..........................................................
void DecodeCommand();
void Send_HA_ZeroFoundMessage();
void Send_DEC_ZeroFoundMessage();
void ReadRA_And_DEC_PositionPots();
void CalcAverageValueHA();
void CalcAverageValueDEC();
void CalcHA_Angle();
void CalcDEC_Angle();
void SendPositionsToPC();
void Display_ADC_Vals_On_LCD();
void Display_HA_And_DEC_On_LCD();
void SendHardwareDetectedResponse();
void Set_HA_ZeroReference_Val(float ZeroRefVal);
void Set_DEC_ZeroReference_Val(float ZeroRefVal);
void Set_HA_ScaleFactor_Val(float ScaleFactorVal);
void Set_DEC_ScaleFactor_Val(float ScaleFactorVal);
void SendMirrorCoverStatusToPC();
void ManualOpenMirrorCover();
void ManualShutMirrorCover();
void ManualDriveFlipMirrorIn();
void ManualDriveFlipMirrorOut();
void SendFlipMirrorStatusToPC();
void CheckForSlipageOn_HA_Pot();
void CheckForSlipageOn_DEC_Pot();
void SendHA_PotSlippageMessageToPC();
void SendDEC_PotSlippageMessageToPC();
void Send_ADC_Vals_ToPC();
void Send_HA_Zero_Ref_Val();
void Send_DEC_Zero_Ref_Val();
void Send_HA_ScaleFactorToPC();
void Send_DEC_ScaleFactorToPC();
void Send_HorizonSenseStatus_ToPC();
void DisplayHorizonSenseLimitOnLCD();
void PulseFlipMirrorStartLine();



//.............................................................................

// External Interrupt 4 service routine
interrupt[EXT_INT4] void ext_int4_isr(void)
{
	if (SearchingForHA_Opto) // set by Find HA Opto cmd
	{
		HA_ZeroFound = true; // main() will signal find
		SearchingForHA_Opto = false; // clear
	}

	HA_ChopperFound = true; // main() will check for slippage
}

//.............................................................................

// External Interrupt 5 service routine
interrupt[EXT_INT5] void ext_int5_isr(void)
{
	if (SearchingForDEC_Opto) // set by Find Dec Opto cmd
	{
		DEC_ZeroFound = true; // main() will signl find
		SearchingForDEC_Opto = false; // clear
	}

	DEC_ChopperFound = true; // main() will check for slippage
}

//.............................................................................


// USART0 Receiver interrupt service routine
interrupt[USART0_RXC] void usart0_rx_isr(void)
{
	char RxData;

	RxData = UDR0;

	if (RxData == 10) return;     //Ignore any line fed character
	if (RxData == 0) return;      //Ignore any null character
	if (RxData == ' ') return;    //Ignore any space character      

	if (RxData == '*') // cmd prefix; reset for receive
	{
		CommandStringIndex = 0;
		CommandString[0] = 0;
		ParameterStringIndex = 0;
		ParameterString[0] = 0;
		return;
	}

	RxData = toupper(RxData);    //Convert to upper case 

	if (RxData == 13) // cmd terminator
	{
		CommandStringReady = true;
		CommandStringIndex = 0; // reset for next
		ParameterStringIndex = 0; // "
		return;
	}

	// build strings
	// if its numeric or decimal or neg sign, it is parameter
	if ((RxData >= '0') && (RxData <= '9'))
	{
		ParameterString[ParameterStringIndex++] = RxData;
		ParameterString[ParameterStringIndex] = 0;
		return;
	}
	if ((RxData == '.') || (RxData == '-'))
	{
		ParameterString[ParameterStringIndex++] = RxData;
		ParameterString[ParameterStringIndex] = 0;
		return;
	}

	// else, its part of the command
	{
		CommandString[CommandStringIndex++] = RxData;
		CommandString[CommandStringIndex] = 0;
		return;
	}
}

//.............................................................................

// USART0 Transmitter interrupt service routine
interrupt[USART0_TXC] void usart0_tx_isr(void)
{
	char Character;

	Character = TxBuffer[++TxBufferIndex];
	if (Character != 0)
	{
		UDR0 = Character;
	}
	else
	{
		Transmitting = false;
	}
}

//.............................................................................


// Timer 0 overflow interrupt service routine
interrupt[TIM0_OVF] void timer0_ovf_isr(void)
{
	// not used
}

//.............................................................................

//Timer1 is set up as a 1ms timer
//Every 1ms we read the HA & DEC pots and every 200ms we average the readings and output the results as HA & DEC angles
// Timer1 overflow interrupt service routine
interrupt[TIM1_OVF] void timer1_ovf_isr(void)
{
	TCNT1H = 0xFE;
	TCNT1L = 0x32;      //Set timer1 for a 1 ms timebase

	TimeToReadPositionPots = true; // main(0) will read

	if (++Timer1Counter > 300)
	{
		Timer1Counter = 0;
		TimeToSendPositions = true;
	}
}

//.............................................................................

// Timer2 overflow interrupt service routine
interrupt[TIM2_OVF] void timer2_ovf_isr(void)
{
   // not used
}

//.............................................................................

//Timer3 is used as a 1 second timer. It is used as a general purpose timer for anything requiring a 1 second or multiple of 1 second timeout
// Timer3 overflow interrupt service routine
interrupt[TIM3_OVF] void timer3_ovf_isr(void)
{
	TCNT3H = 0xC7;
	TCNT3L = 0xBF;

	if (MirrorCoverClosing)
		if (++MirrorCoverMovementTimer > 20)
		{
			MirrorCoverMovementTimer = 0;
			MirrorCoverClosing = false;
			if (MirrorCoverShutSenseSwitch != MirrorCoverShut) MirrorCoverFailedToShut = true;
		}

	if (MirrorCoverOpening)
		if (++MirrorCoverMovementTimer > 20)
		{
			MirrorCoverMovementTimer = 0;
			MirrorCoverOpening = false;
			if (MirrorCoverOpenSenseSwitch != MirrorCoverOpen) MirrorCoverFailedToOpen = true;
		}


	if (++SecondsCounter > 3600)
	{
		SecondsCounter = 0;            //Every hour we do this just in case the display has been corrupted 
		TimeToResetDisplay = true;     //
	}
}

//.............................................................................


#define ADC_VREF_TYPE 0x40

// Read the AD conversion result
unsigned int read_adc(unsigned char adc_input)
{
	ADMUX = adc_input | (ADC_VREF_TYPE & 0xff);
	// Delay needed for the stabilization of the ADC input voltage
	delay_us(10);
	// Start the AD conversion
	ADCSRA |= 0x40;
	// Wait for the AD conversion to complete
	while ((ADCSRA & 0x10) == 0);
	ADCSRA |= 0x10;
	return ADCW;
}

//.............................................................................




void main(void)
{

	// Input/Output Ports initialization
	// Port A initialization
	// Func7=In Func6=Out Func5=Out Func4=Out Func3=Out Func2=Out Func1=Out Func0=Out 
	// State7=1 State6=0 State5=0 State4=0 State3=0 State2=0 State1=0 State0=0 
	PORTA = 0x80;
	DDRA = 0x7F;

	// Port B initialization
	// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
	// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
	PORTB = 0x00;
	DDRB = 0x30;

	// Port C initialization
	// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
	// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
	PORTC = 0x00;
	DDRC = 0x00;

	// Port D initialization
	// Func7=Out Func6=Out Func5=Out Func4=Out Func3=Out Func2=Out Func1=Out Func0=Out 
	// State7=0 State6=0 State5=0 State4=0 State3=0 State2=0 State1=0 State0=0 
	PORTD = 0xF0;
	DDRD = 0x00;

	// Port E initialization
	// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
	// State7=P State6=P State5=P State4=P State3=T State2=T State1=T State0=T 
	PORTE = 0x00;
	DDRE = 0x00;

	// Port F initialization
	// Func7=In Func6=In Func5=In Func4=In Func3=In Func2=In Func1=In Func0=In 
	// State7=T State6=T State5=T State4=T State3=T State2=T State1=T State0=T 
	PORTF = 0x0C;
	DDRF = 0x00;

	// Port G initialization
	// Func4=In Func3=In Func2=In Func1=In Func0=In 
	// State4=T State3=T State2=T State1=T State0=T 
	PORTG = 0x00;
	DDRG = 0x00;

	// Timer/Counter 0 initialization
	// Clock source: System Clock
	// Clock value: Timer 0 Stopped
	// Mode: Normal top=0xFF
	// OC0 output: Disconnected
	ASSR = 0x00;
	TCCR0 = 0x00;
	TCNT0 = 0x00;
	OCR0 = 0x00;

	// Timer/Counter 1 initialization
	// Clock source: System Clock
	// Clock value: Timer1 Stopped
	// Mode: Normal top=0xFFFF
	// OC1A output: Discon.
	// OC1B output: Discon.
	// OC1C output: Discon.
	// Noise Canceler: Off
	// Input Capture on Falling Edge
	// Timer1 Overflow Interrupt: On
	// Input Capture Interrupt: Off
	// Compare A Match Interrupt: Off
	// Compare B Match Interrupt: Off
	// Compare C Match Interrupt: Off
	TCCR1A = 0x00;
	TCCR1B = 0x00;
	TCNT1H = 0x00;
	TCNT1L = 0x00;
	ICR1H = 0x00;
	ICR1L = 0x00;
	OCR1AH = 0x00;
	OCR1AL = 0x00;
	OCR1BH = 0x00;
	OCR1BL = 0x00;
	OCR1CH = 0x00;
	OCR1CL = 0x00;

	// Timer/Counter 2 initialization
	// Clock source: System Clock
	// Clock value: Timer2 Stopped
	// Mode: Normal top=0xFF
	// OC2 output: Disconnected
	TCCR2 = 0x00;
	TCNT2 = 0x00;
	OCR2 = 0x00;

	// Timer/Counter 3 initialization
	// Clock source: System Clock
	// Clock value: Timer3 Stopped
	// Mode: Normal top=0xFFFF
	// OC3A output: Discon.
	// OC3B output: Discon.
	// OC3C output: Discon.
	// Noise Canceler: Off
	// Input Capture on Falling Edge
	// Timer3 Overflow Interrupt: On
	// Input Capture Interrupt: Off
	// Compare A Match Interrupt: Off
	// Compare B Match Interrupt: Off
	// Compare C Match Interrupt: Off
	TCCR3A = 0x00;
	TCCR3B = 0x00;
	TCNT3H = 0x00;
	TCNT3L = 0x00;
	ICR3H = 0x00;
	ICR3L = 0x00;
	OCR3AH = 0x00;
	OCR3AL = 0x00;
	OCR3BH = 0x00;
	OCR3BL = 0x00;
	OCR3CH = 0x00;
	OCR3CL = 0x00;

	// External Interrupt(s) initialization
	// INT0: Off
	// INT1: Off
	// INT2: Off
	// INT3: Off
	// INT4: On
	// INT4 Mode: Falling Edge
	// INT5: On
	// INT5 Mode: Falling Edge
	// INT6: Off
	// INT7: Off
	EICRA = 0x00;
	EICRB = 0x0A;
	EIMSK = 0x30;
	EIFR = 0x30;

	// Timer(s)/Counter(s) Interrupt(s) initialization
	TIMSK = 0x45; // timer 0, 1 and 2 overflow interrrupts  enabled

	ETIMSK = 0x04; // timer 3 overflow interrupt enabled

	// USART0 initialization
	// Communication Parameters: 8 Data, 1 Stop, No Parity
	// USART0 Receiver: On
	// USART0 Transmitter: On
	// USART0 Mode: Asynchronous
	// USART0 Baud Rate: 9600
	UCSR0A = 0x00;
	UCSR0B = 0xD8;
	UCSR0C = 0x06;
	UBRR0H = 0x00;
	UBRR0L = 0x17;

	// USART1 initialization
	// USART1 disabled
	UCSR1B = 0x00;

	// Analog Comparator initialization
	// Analog Comparator: Off
	// Analog Comparator Input Capture by Timer/Counter 1: Off
	ACSR = 0x80;
	SFIOR = 0x00;

	// ADC initialization
	// ADC Clock frequency: 921.600 kHz
	// ADC Voltage Reference: AVCC pin
	ADMUX = ADC_VREF_TYPE & 0xff;
	ADCSRA = 0x82;

	// SPI initialization
	// SPI disabled
	SPCR = 0x00;

	// TWI initialization
	// TWI disabled
	TWCR = 0x00;

	// Alphanumeric LCD initialization
	// Connections specified in the
	// Project|Configure|C Compiler|Libraries|Alphanumeric LCD menu:
	// RS - PORTC Bit 0
	// RD - PORTC Bit 1
	// EN - PORTC Bit 2
	// D4 - PORTC Bit 4
	// D5 - PORTC Bit 5
	// D6 - PORTC Bit 6
	// D7 - PORTC Bit 7
	// Characters/line: 20
	lcd_init(20);

	// Watchdog Timer initialization
	// Watchdog Timer Prescaler: OSC/2048k
#pragma optsize-
	WDTCR = 0x1F;
	WDTCR = 0x0F;
#ifdef _OPTIMIZE_SIZE_
#pragma optsize+
#endif

	// Global enable interrupts
	#asm("sei")


	if (TestByte1_EEPROM != 0x55)    //Initialize these EEPROM variables if not already initialized
	{
		HA_Pot_ZeroReference_EEPROM = 512;
	}

	if (TestByte2_EEPROM != 0x55)    //Initialize these EEPROM variables if not already initialized
	{
		DEC_Pot_ZeroReference_EEPROM = 512;
	}


	HA_Pot_ZeroReference = HA_Pot_ZeroReference_EEPROM;
	DEC_Pot_ZeroReference = DEC_Pot_ZeroReference_EEPROM;


	if (TestByte3_EEPROM != 0x55)
	{
		HA_Pot_ScaleFactor_EEPROM = 0.38136;   //0.3636
	}

	if (TestByte4_EEPROM != 0x55)
	{
		DEC_Pot_ScaleFactor_EEPROM = 0.39823;
	}

	HA_Pot_ScaleFactor = HA_Pot_ScaleFactor_EEPROM;
	DEC_Pot_ScaleFactor = DEC_Pot_ScaleFactor_EEPROM;





	TCNT1H = 0xFE;
	TCNT1L = 0x32;
	TCCR1B = 0x02;   //Set timer1 running with 1 ms timebase    

	TCNT3H = 0xC7;
	TCNT3L = 0xBF;
	TCCR3B = 0x04;   //Set timer3 running as a 1 second timebase



	while (1)
	{

		#asm("wdr")


			if (CommandStringReady) DecodeCommand();


		if (MirrorCoverOpenPushButton == ButtonPressed) ManualOpenMirrorCover();
		if (MirrorCoverShutPushButton == ButtonPressed) ManualShutMirrorCover();
		if (FlipMirrorInPushButton == ButtonPressed) ManualDriveFlipMirrorIn();
		if (FlipMirrorOutPushButton == ButtonPressed) ManualDriveFlipMirrorOut();

		if (MirrorCoverOpenSenseSwitch == MirrorCoverOpen)
		{
			MirrorCoverOpen_LED = LED_On;
		}
		else
		{
			MirrorCoverOpen_LED = LED_Off;
		}


		if (MirrorCoverShutSenseSwitch == MirrorCoverShut)
		{
			MirrorCoverShut_LED = LED_On;
		}
		else
		{
			MirrorCoverShut_LED = LED_Off;
		}


		if (FlipMirror_Status_Bit == FlipMirrorIn)
		{
			FlipMirrorIn_LED = LED_On;
			FlipMirrorOut_LED = LED_Off;
		}
		else
		{
			FlipMirrorIn_LED = LED_Off;
			FlipMirrorOut_LED = LED_On;
		}


		if (HA_ChopperFound) CheckForSlipageOn_HA_Pot();
		if (DEC_ChopperFound) CheckForSlipageOn_DEC_Pot();


		if (HA_ZeroFound)
		{
			HA_ZeroFound = false;
			Send_HA_ZeroFoundMessage();
		}

		if (DEC_ZeroFound)
		{
			DEC_ZeroFound = false;
			Send_DEC_ZeroFoundMessage();
			if (SaveNextDEC_ZeroRef == true)
			{
				SaveNextDEC_ZeroRef = false;
				Set_DEC_ZeroReference_Val(DEC_Pot_AvgVal);
			}
		}

		if (TimeToReadPositionPots) // every 1 ms
		{
			TimeToReadPositionPots = false;
			ReadRA_And_DEC_PositionPots();
		}

		if (TimeToSendPositions) // every 300 ms
		{
			TimeToSendPositions = false; // clear
			if ((PositionSendingEnabled) && (PC_CommsLinkEstablished)) 
				SendPositionsToPC();

			if (HorizonSenseStatusBit == LimitExceeded)
			{
				DisplayHorizonSenseLimitOnLCD();
				// no warning to serial port! You have to ask for status
			}
			else
			{
				Display_ADC_Vals_On_LCD();
				Display_HA_And_DEC_On_LCD();
			}
		}

		if (PositionSendRequested)
		{
			PositionSendRequested = false;
			if (PositionSendingEnabled == false) // send only if not being sent automatically
				SendPositionsToPC();
		}

		if (TimeToResetDisplay)
		{
			TimeToResetDisplay = false;
			lcd_init(20);
		}




	}

}


//.............................................................................

void DecodeCommand()
{
	char x, StrLength;
	float ParameterVal;


	CommandStringReady = false;

	ParameterVal = atof(ParameterString);

	StrLength = strlen(CommandString);


	if (StrLength == 0)        //If there was not command string then we send the HardwareDetected response to the PC
	{
		SendHardwareDetectedResponse();
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "SEND_POSITION", StrLength);
	if (x == 0)
	{
		PositionSendRequested = true;
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "FIND_HA_OPTO", StrLength);
	if (x == 0)
	{
		SearchingForHA_Opto = true;
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "FIND_DEC_OPTO", StrLength);
	if (x == 0)
	{
		SearchingForDEC_Opto = true;
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "OPEN_MIRROR_COVER", StrLength);
	if (x == 0)
	{
		MirrorCoverFailedToOpen = false;
		MirrorCoverSolenoid = OpenCover;
		MirrorCoverOpening = true;
		MirrorCoverMovementTimer = 0;
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "CLOSE_MIRROR_COVER", StrLength);
	if (x == 0)
	{
		MirrorCoverFailedToShut = false;
		MirrorCoverSolenoid = CloseCover;
		MirrorCoverClosing = true;
		MirrorCoverMovementTimer = 0;
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "MIRROR_COVER_STATUS", StrLength);
	if (x == 0)
	{
		SendMirrorCoverStatusToPC();
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "FLIP_MIRROR_IN", StrLength);
	if (x == 0)
	{
		PC_CommsLinkEstablished = true;
		FlipMirror_Direction_Bit = FlipMirrorIn;
		PulseFlipMirrorStartLine();
		return;
	}

	x = strncmpf(CommandString, "FLIP_MIRROR_OUT", StrLength);
	if (x == 0)
	{
		PC_CommsLinkEstablished = true;
		FlipMirror_Direction_Bit = FlipMirrorOut;
		PulseFlipMirrorStartLine();
		return;
	}

	x = strncmpf(CommandString, "FLIP_MIRROR_STATUS", StrLength);
	if (x == 0)
	{
		SendFlipMirrorStatusToPC();
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "POSITION_SEND_ON", StrLength);
	if (x == 0)
	{
		PositionSendingEnabled = true;
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "POSITION_SEND_OFF", StrLength);
	if (x == 0)
	{
		PositionSendingEnabled = false;
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "SAVE_HA_POT_ZERO_REF", StrLength);
	if (x == 0)
	{
		SaveNextHA_ZeroRef = true;
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "SAVE_DEC_POT_ZERO_REF", StrLength);
	if (x == 0)
	{
		SaveNextDEC_ZeroRef = true;
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "SET_HA_SCALE_FACTOR", StrLength);
	if (x == 0)
	{
		Set_HA_ScaleFactor_Val(ParameterVal);
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "SET_DEC_SCALE_FACTOR", StrLength);
	if (x == 0)
	{
		Set_DEC_ScaleFactor_Val(ParameterVal);
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "SEND_HA_SCALE_FACTOR", StrLength);
	if (x == 0)
	{
		Send_HA_ScaleFactorToPC();
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "SEND_DEC_SCALE_FACTOR", StrLength);
	if (x == 0)
	{
		Send_DEC_ScaleFactorToPC();
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "SET_HA_ZERO_REF_VAL", StrLength);
	if (x == 0)
	{
		Set_HA_ZeroReference_Val(ParameterVal);
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "SET_DEC_ZERO_REF_VAL", StrLength);
	if (x == 0)
	{
		Set_DEC_ZeroReference_Val(ParameterVal);
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "SEND_HA_ZERO_REF_VAL", StrLength);
	if (x == 0)
	{
		Send_HA_Zero_Ref_Val();
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "SEND_DEC_ZERO_REF_VAL", StrLength);
	if (x == 0)
	{
		Send_DEC_Zero_Ref_Val();
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "SEND_ADC_VALS", StrLength);
	if (x == 0)
	{
		Send_ADC_Vals_ToPC();
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "HORIZON_STATUS", StrLength);
	if (x == 0)
	{
		Send_HorizonSenseStatus_ToPC();
		PC_CommsLinkEstablished = true;
		return;
	}

	x = strncmpf(CommandString, "OVER_RIDE_HORZ_LIMIT", StrLength);
	if (x == 0)
	{
		HorizonSenseOver_RideBit = 1;       //Sending a pulse to this line allows the horizon sensor to be over-ridden for 5 minutes.
		delay_ms(200);
		HorizonSenseOver_RideBit = 0;
		PC_CommsLinkEstablished = true;
		return;
	}



}

//.............................................................................

void CheckForSlipageOn_HA_Pot()
{
	HA_ChopperFound = false;

	if ((HA_Pot_AvgVal < 490) || (HA_Pot_AvgVal > 530))      //Allow a margin of + or - 20 ADUs about 511 ( the centre of the range for the pot
	{
		SendHA_PotSlippageMessageToPC();
		return;
	}


	if (SaveNextHA_ZeroRef == true)
	{
		SaveNextHA_ZeroRef = false;
		Set_HA_ZeroReference_Val(HA_Pot_AvgVal);
	}
}

//.............................................................................

//Each time we detect the DEC zero reference opto, we chek the DEC_Pot_AvgVal to see if it is within + or - 30 ADUs of 511 (pot centre)
//If not we flag an error message because it will have slipped and needs to be re-aligned.

//At the same time, if SaveNextDEC_ZeroRef is true, we compare the saved zero reference ADU value with the DEC_Pot_AvgVal.
//If not equal then we save the DEC_Pot_AvgVal as the new reference value.

void CheckForSlipageOn_DEC_Pot()
{
	DEC_ChopperFound = false;

	if ((DEC_Pot_AvgVal < 490) || (DEC_Pot_AvgVal > 530))    //Allow a margin of + or - 20 ADUs about 511 ( the centre of the range for the pot
	{
		SendDEC_PotSlippageMessageToPC();
		return;
	}


	if (SaveNextDEC_ZeroRef == true)
	{
		SaveNextDEC_ZeroRef = false;
		Set_DEC_ZeroReference_Val(DEC_Pot_AvgVal);
	}
}

//.............................................................................

void ManualOpenMirrorCover()
{
	MirrorCoverFailedToOpen = false;

	delay_ms(50);

	if (MirrorCoverOpenPushButton == ButtonPressed)
	{
		MirrorCoverSolenoid = TurnSolenoid_On;
	}

}


//.............................................................................

void ManualShutMirrorCover()
{
	MirrorCoverFailedToShut = false;

	delay_ms(50);

	if (MirrorCoverShutPushButton == ButtonPressed)
	{
		MirrorCoverSolenoid = TurnSolenoid_Off;
	}

}


//.............................................................................

void ManualDriveFlipMirrorIn()
{

	if (FlipMirrorOutPushButton == ButtonPressed) return;

	delay_ms(50);

	if (FlipMirrorInPushButton == ButtonPressed)
	{
		FlipMirror_Direction_Bit = FlipMirrorIn;
		PulseFlipMirrorStartLine();
	}
}

//.............................................................................

void ManualDriveFlipMirrorOut()
{

	if (FlipMirrorInPushButton == ButtonPressed) return;

	delay_ms(50);

	if (FlipMirrorOutPushButton == ButtonPressed)
	{
		FlipMirror_Direction_Bit = FlipMirrorOut;
		PulseFlipMirrorStartLine();
	}
}

//.............................................................................

void Set_HA_ZeroReference_Val(float ZeroRefVal)
{
	HA_Pot_ZeroReference_EEPROM = ZeroRefVal;
	HA_Pot_ZeroReference = ZeroRefVal;
	TestByte1_EEPROM = 0x55;
}

//.............................................................................

void Set_DEC_ZeroReference_Val(float ZeroRefVal)
{
	DEC_Pot_ZeroReference_EEPROM = ZeroRefVal;
	DEC_Pot_ZeroReference = ZeroRefVal;
	TestByte2_EEPROM = 0x55;
}

//.............................................................................

void Set_HA_ScaleFactor_Val(float ScaleFactorVal)
{
	HA_Pot_ScaleFactor_EEPROM = ScaleFactorVal;
	HA_Pot_ScaleFactor = ScaleFactorVal;
	TestByte3_EEPROM = 0x55;
}

//.............................................................................

void Set_DEC_ScaleFactor_Val(float ScaleFactorVal)
{
	DEC_Pot_ScaleFactor_EEPROM = ScaleFactorVal;
	DEC_Pot_ScaleFactor = ScaleFactorVal;
	TestByte4_EEPROM = 0x55;
}

//.............................................................................

void SendHardwareDetectedResponse()
{
	while (Transmitting);

	strcpy(TxBuffer, "$\r");

	TxBufferIndex = 0;
	Transmitting = true;
	UDR0 = TxBuffer[0];
}

//.............................................................................

void SendMirrorCoverStatusToPC()
{
	while (Transmitting);

	if (MirrorCoverFailedToShut)
	{
		while (Transmitting);
		if (MirrorCoverOpenSenseSwitch == MirrorCoverOpen)
		{
			strcpy(TxBuffer, "MIRROR COVER FAILED TO SHUT: COVER OPEN\r");
		}

		else
		{
			strcpy(TxBuffer, "MIRROR COVER FAILED TO SHUT: COVER PARTLY OPEN\r");
		}


		TxBufferIndex = 0;
		Transmitting = true;
		UDR0 = TxBuffer[0];
		return;
	}


	if (MirrorCoverFailedToOpen)
	{
		while (Transmitting);
		if (MirrorCoverShutSenseSwitch == MirrorCoverShut)
		{
			strcpy(TxBuffer, "MIRROR COVER FAILED TO OPEN: COVER SHUT\r");
		}

		else
		{
			strcpy(TxBuffer, "MIRROR COVER FAILED TO OPEN: COVER PARTLY OPEN\r");
		}


		TxBufferIndex = 0;
		Transmitting = true;
		UDR0 = TxBuffer[0];
		return;
	}



	if ((MirrorCoverShutSenseSwitch == MirrorCoverShut) && (MirrorCoverOpenSenseSwitch == MirrorCoverOpen))
	{
		strcpy(TxBuffer, "MIRROR COVER SENSOR ERROR\r");
		TxBufferIndex = 0;
		Transmitting = true;
		UDR0 = TxBuffer[0];
		return;
	}


	if (MirrorCoverShutSenseSwitch == MirrorCoverShut)
	{
		strcpy(TxBuffer, "SHUT\r");
		TxBufferIndex = 0;
		Transmitting = true;
		UDR0 = TxBuffer[0];
		return;
	}


	if (MirrorCoverOpenSenseSwitch == MirrorCoverOpen)
	{
		strcpy(TxBuffer, "OPEN\r");
		TxBufferIndex = 0;
		Transmitting = true;
		UDR0 = TxBuffer[0];
		return;
	}

	else

	{
		strcpy(TxBuffer, "PARTLY OPEN\r");
		TxBufferIndex = 0;
		Transmitting = true;
		UDR0 = TxBuffer[0];
		return;
	}

}

//.............................................................................


void SendFlipMirrorStatusToPC()
{
	while (Transmitting);


	if (FlipMirror_ErrorIn_Bit == FlipMirrorDriveError)
	{
		strcpy(TxBuffer, "FLIP MIRROR DRIVE ERROR\r");
		TxBufferIndex = 0;
		Transmitting = true;
		UDR0 = TxBuffer[0];
		return;
	}


	if (FlipMirror_Status_Bit == FlipMirrorIn)
	{
		strcpy(TxBuffer, "MIRROR IN\r");
		TxBufferIndex = 0;
		Transmitting = true;
		UDR0 = TxBuffer[0];
		return;
	}


	if (FlipMirror_Status_Bit == FlipMirrorOut)
	{
		strcpy(TxBuffer, "MIRROR OUT\r");
		TxBufferIndex = 0;
		Transmitting = true;
		UDR0 = TxBuffer[0];
		return;
	}


}


//.............................................................................

void Send_HorizonSenseStatus_ToPC()
{

	if (HorizonSenseStatusBit == TelescopeAboveHorizonLimit)
	{
		strcpy(TxBuffer, "HORIZON OK\r");
		TxBufferIndex = 0;
		Transmitting = true;
		UDR0 = TxBuffer[0];
	}

	else
	{
		strcpy(TxBuffer, "AT HORIZON LIMIT\r");
		TxBufferIndex = 0;
		Transmitting = true;
		UDR0 = TxBuffer[0];
	}

}

//.............................................................................

void Send_HA_ZeroFoundMessage()
{

	HA_ZeroFound = false;

	while (Transmitting);

	strcpy(TxBuffer, "HA_ZERO\r");

	TxBufferIndex = 0;
	Transmitting = true;
	UDR0 = TxBuffer[0];
}

//.............................................................................

void Send_DEC_ZeroFoundMessage()
{

	DEC_ZeroFound = false;

	while (Transmitting);

	strcpy(TxBuffer, "DEC_ZERO\r");

	TxBufferIndex = 0;
	Transmitting = true;
	UDR0 = TxBuffer[0];
}

//.............................................................................

void SendPositionsToPC()
{
	char HA_String[10];
	char DEC_String[10];
	char TextString[10];
	char PadString[6];
	char StrLength;
	float AbsVal;

	AbsVal = fabs(HourAngleFromPot);
	ftoa(AbsVal, 0, TextString);
	StrLength = strlen(TextString);
	if (HourAngleFromPot < 0) strcpyf(PadString, "-"); else strcpyf(PadString, "+");
	if (StrLength == 1) strcatf(PadString, "00");
	if (StrLength == 2) strcatf(PadString, "0");
	strcpy(HA_String, PadString);
	strcat(HA_String, TextString);

	AbsVal = fabs(DEC_AngleFromPot);
	ftoa(AbsVal, 0, TextString);
	StrLength = strlen(TextString);
	if (DEC_AngleFromPot < 0) strcpyf(PadString, "-"); else strcpyf(PadString, "+");
	if (StrLength == 1) strcatf(PadString, "00");
	if (StrLength == 2) strcatf(PadString, "0");
	strcpy(DEC_String, PadString);
	strcat(DEC_String, TextString);



	while (Transmitting);

	TxBufferIndex = 0;

	strcpyf(TxBuffer, "HA");
	strcat(TxBuffer, HA_String);
	strcatf(TxBuffer, " DEC");
	strcat(TxBuffer, DEC_String);
	strcatf(TxBuffer, "\r");

	TxBufferIndex = 0;
	Transmitting = true;
	UDR0 = TxBuffer[0];
}

//.............................................................................

void SendHA_PotSlippageMessageToPC()
{
	while (Transmitting);

	TxBufferIndex = 0;

	strcpyf(TxBuffer, "HA_POT_SLIPPAGE_ERROR\r");

	TxBufferIndex = 0;
	Transmitting = true;
	UDR0 = TxBuffer[0];
}

//.............................................................................

void SendDEC_PotSlippageMessageToPC()
{
	while (Transmitting);

	TxBufferIndex = 0;

	strcpyf(TxBuffer, "DEC_POT_SLIPPAGE_ERROR\r");

	TxBufferIndex = 0;
	Transmitting = true;
	UDR0 = TxBuffer[0];
}

//.............................................................................

void ReadRA_And_DEC_PositionPots()
{
	unsigned int HA_Reading, DEC_Reading;

	HA_Reading = read_adc(0);     //Read the HA pot adc
	DEC_Reading = read_adc(1);    //Read the DEC pot adc

	HA_PotArray[ReadingArrayIndex] = (float)HA_Reading;
	DEC_PotArray[ReadingArrayIndex] = (float)DEC_Reading;
	if (++ReadingArrayIndex > ReadingArraySize)
	{
		ReadingArrayIndex = 0;
		CalcAverageValueHA();
		CalcAverageValueDEC();
		CalcHA_Angle();
		CalcDEC_Angle();
	}

}

//.............................................................................

void CalcAverageValueHA()
{
	float Sum, NumberOfReadings;
	char i;

	Sum = 0;
	NumberOfReadings = ReadingArraySize;

	for (i = 0; i < ReadingArraySize; i++)
	{
		Sum = Sum + HA_PotArray[i];
	}

	HA_Pot_AvgVal = Sum / NumberOfReadings;

}

//.............................................................................

void CalcAverageValueDEC()
{
	float Sum, NumberOfReadings;
	char i;

	Sum = 0;
	NumberOfReadings = ReadingArraySize;

	for (i = 0; i < ReadingArraySize; i++)
	{
		Sum = Sum + DEC_PotArray[i];
	}

	DEC_Pot_AvgVal = Sum / NumberOfReadings;
}

//.............................................................................

void CalcHA_Angle()
{

	HA_RelativeVal = HA_Pot_AvgVal - HA_Pot_ZeroReference;

	#asm("cli")
		HourAngleFromPot = HA_RelativeVal * HA_Pot_ScaleFactor;
	#asm("sei")
}

//.............................................................................

void CalcDEC_Angle()
{

	DEC_RelativeVal = DEC_Pot_AvgVal - DEC_Pot_ZeroReference;

	#asm("cli")
		DEC_AngleFromPot = DEC_RelativeVal * DEC_Pot_ScaleFactor;
	#asm("sei")
}

//.............................................................................

void DisplayHorizonSenseLimitOnLCD()
{

	lcd_gotoxy(0, 0);
	lcd_putsf("AT HORIZON LIMIT    ");

	lcd_gotoxy(0, 1);
	lcd_putsf("PRESS BUTTON ON HORZ");

	lcd_gotoxy(0, 2);
	lcd_putsf("SENSE BOX TO OVERIDE");

	lcd_gotoxy(0, 3);
	lcd_putsf("OVER-RIDE FOR 5 MINS");

}

//.............................................................................

void Display_ADC_Vals_On_LCD()
{
	char TextString[10];
	char DisplayString[20];


	strcpyf(DisplayString, "HA  abs     rel    ");
	lcd_gotoxy(0, 0);
	lcd_puts(DisplayString);

	ltoa(HA_Pot_AvgVal, TextString);
	lcd_gotoxy(7, 0);
	lcd_puts(TextString);

	ftoa(HA_RelativeVal, 0, TextString);
	lcd_gotoxy(15, 0);
	if (HA_RelativeVal >= 0) lcd_putchar('+');
	lcd_puts(TextString);


	strcpyf(DisplayString, "DEC abs     rel    ");
	lcd_gotoxy(0, 1);
	lcd_puts(DisplayString);

	ltoa(DEC_Pot_AvgVal, TextString);
	lcd_gotoxy(7, 1);
	lcd_puts(TextString);

	ftoa(DEC_RelativeVal, 0, TextString);
	lcd_gotoxy(15, 1);
	if (DEC_RelativeVal >= 0) lcd_putchar('+');
	lcd_puts(TextString);

}

//.............................................................................

void Display_HA_And_DEC_On_LCD()
{
	char TextString[10];
	char DisplayString[20];



	strcpyf(DisplayString, "HA  Degrees: ");
	lcd_gotoxy(0, 2);
	lcd_puts(DisplayString);

	ftoa(HourAngleFromPot, 0, TextString);
	lcd_gotoxy(13, 2);
	if (HourAngleFromPot >= 0) lcd_putchar('+');
	lcd_puts(TextString);
	lcd_putsf("   ");




	strcpyf(DisplayString, "DEC Degrees: ");
	lcd_gotoxy(0, 3);
	lcd_puts(DisplayString);

	ftoa(DEC_AngleFromPot, 0, TextString);
	lcd_gotoxy(13, 3);
	if (DEC_AngleFromPot >= 0) lcd_putchar('+');
	lcd_puts(TextString);
	lcd_putsf("   ");

}

//.............................................................................

void Send_ADC_Vals_ToPC()
{
	char TextString[10];


	while (Transmitting);

	TxBufferIndex = 0;

	strcpyf(TxBuffer, "HA_ABS: ");
	ltoa(HA_Pot_AvgVal, TextString);
	strcat(TxBuffer, TextString);

	strcatf(TxBuffer, " HA_REL: ");
	ltoa(HA_RelativeVal, TextString);
	strcat(TxBuffer, TextString);


	strcatf(TxBuffer, " DEC_ABS: ");
	ltoa(DEC_Pot_AvgVal, TextString);
	strcat(TxBuffer, TextString);

	strcatf(TxBuffer, " DEC_REL: ");
	ltoa(DEC_RelativeVal, TextString);
	strcat(TxBuffer, TextString);
	strcatf(TxBuffer, "\r");

	TxBufferIndex = 0;
	Transmitting = true;
	UDR0 = TxBuffer[0];

}

//.............................................................................

void Send_HA_Zero_Ref_Val()
{
	char TextString[10];
	float ZeroReference_Copy;

	ZeroReference_Copy = HA_Pot_ZeroReference_EEPROM;

	while (Transmitting);

	TxBufferIndex = 0;

	strcpyf(TxBuffer, "HA ZERO REF VAL: ");
	ftoa(ZeroReference_Copy, 0, TextString);
	strcat(TxBuffer, TextString);
	strcatf(TxBuffer, "\r");

	TxBufferIndex = 0;
	Transmitting = true;
	UDR0 = TxBuffer[0];
}

//.............................................................................

void Send_DEC_Zero_Ref_Val()
{
	char TextString[10];
	float ZeroReference_Copy;

	ZeroReference_Copy = DEC_Pot_ZeroReference_EEPROM;

	while (Transmitting);

	TxBufferIndex = 0;

	strcpyf(TxBuffer, "DEC ZERO REF VAL: ");
	ftoa(ZeroReference_Copy, 0, TextString);
	strcat(TxBuffer, TextString);
	strcatf(TxBuffer, "\r");

	TxBufferIndex = 0;
	Transmitting = true;
	UDR0 = TxBuffer[0];
}

//.............................................................................

void Send_HA_ScaleFactorToPC()
{
	char TextString[10];
	float ScaleFactor_Copy;

	ScaleFactor_Copy = HA_Pot_ScaleFactor_EEPROM;

	while (Transmitting);

	TxBufferIndex = 0;

	strcpyf(TxBuffer, "HA SCALE FACTOR VAL: ");
	ftoa(ScaleFactor_Copy, 3, TextString);
	strcat(TxBuffer, TextString);
	strcatf(TxBuffer, "\r");

	TxBufferIndex = 0;
	Transmitting = true;
	UDR0 = TxBuffer[0];
}

//............................................................................. 

void Send_DEC_ScaleFactorToPC()
{
	char TextString[10];
	float ScaleFactor_Copy;

	ScaleFactor_Copy = DEC_Pot_ScaleFactor_EEPROM;

	while (Transmitting);

	TxBufferIndex = 0;

	strcpyf(TxBuffer, "DEC SCALE FACTOR VAL: ");
	ftoa(ScaleFactor_Copy, 3, TextString);
	strcat(TxBuffer, TextString);
	strcatf(TxBuffer, "\r");

	TxBufferIndex = 0;
	Transmitting = true;
	UDR0 = TxBuffer[0];
}

//............................................................................. 

void PulseFlipMirrorStartLine()
{
	delay_ms(1);

	FlipMirror_StartPulse = 1;
	#asm("wdr")
		delay_ms(120);
	FlipMirror_StartPulse = 0;
	delay_ms(120);
}

//.............................................................................