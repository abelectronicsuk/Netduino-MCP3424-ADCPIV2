using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
// demo for Netduino 2 to read ADC board with pair of MCP3424 18 bit ADC chips on ADC Pi board from
//  http://www.abelectronics.co.uk/products/3/Raspberry-Pi/17/ADC-Pi-V2---Raspberry-Pi-Analogue-to-Digital-converter
//
// Version 1.0  - 22/08/2013
// Version History:
// 1.0 - Initial Release
//
//
namespace NetduinoMCP3424
{
    public class Program
    {
        public static void Main()
        {
            // create MCP3424 object
            MCP3424 adc = new MCP3424();
            // initalise ADC addresses on I2C bus
            adc.Init();


            // loop forever
            while (true)
            {
                // set channel to input 1 and read to debug screen
                adc.SetChannel(1);
                Debug.Print("Read 1: " + adc.ReadADC().ToString());
                // set channel to input 2 and read to debug screen
                //Thread.Sleep(500);
                adc.SetChannel(2);
                Debug.Print("Read 2: " + adc.ReadADC().ToString());
                //Thread.Sleep(500);
                adc.SetChannel(3);
                Debug.Print("Read 3: " + adc.ReadADC().ToString());
                //Thread.Sleep(500);
                adc.SetChannel(4);
                Debug.Print("Read 4: " + adc.ReadADC().ToString());
                //Thread.Sleep(500);
                adc.SetChannel(5);
                Debug.Print("Read 5: " + adc.ReadADC().ToString());
                //Thread.Sleep(500);
                adc.SetChannel(6);
                Debug.Print("Read 6: " + adc.ReadADC().ToString());
                //Thread.Sleep(500);
                adc.SetChannel(7);
                Debug.Print("Read 7: " + adc.ReadADC().ToString());
                //Thread.Sleep(500);
                adc.SetChannel(8);
                Debug.Print("Read 8: " + adc.ReadADC().ToString());
                //Thread.Sleep(500);
                
            }
        }

    }
}
