using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System.Threading;

// read ADC board with pair of MCP3424 18 bit ADC chips on ADC Pi board from
//  http://www.abelectronics.co.uk/products/3/Raspberry-Pi/17/ADC-Pi-V2---Raspberry-Pi-Analogue-to-Digital-converter
 
namespace NetduinoMCP3424
{
    class MCP3424
    {
        I2CDevice.Configuration conADCA;
        I2CDevice.Configuration conADCB;
        I2CDevice MyI2C;
        // setup channels for 18 bit mode
        Byte[] ChannelArray = { 0x9C, 0xBC, 0xDC, 0xFC };

        public void Init() {
            // setup I2C devices on addresses 0x68 and 0x69
            conADCA = new I2CDevice.Configuration(0x68, 400);
            conADCB = new I2CDevice.Configuration(0x69, 400);

            MyI2C = new I2CDevice(conADCA);
        }

        public void SetChannel(int channel)
        {
            byte[] TxBuff = new byte[1];
            int requestchannel = channel - 1;
            if (channel > 4) { requestchannel = requestchannel - 4; }
            TxBuff[0] = ChannelArray[requestchannel];
            if (channel <= 4)
            {
                MyI2C.Config = conADCA;
            }
            else
            {
                MyI2C.Config = conADCB;
            }
            I2CDevice.I2CTransaction[] WriteTran = new I2CDevice.I2CTransaction[] 
            {
                I2CDevice.CreateWriteTransaction(TxBuff)
            };

            MyI2C.Execute(WriteTran, 1000);
            Thread.Sleep(5);
        }
        public double ReadADC()
        {
            // send one byte with 0 value to request data
            byte[] Addr = new byte[1];
            Addr[0] = 0x00;
            // array for returned values
            byte[] input = new byte[4];   
    
            I2CDevice.I2CTransaction[] ReadTran = new I2CDevice.I2CTransaction[]
            {
                I2CDevice.CreateReadTransaction(input)
            };

            MyI2C.Execute(ReadTran, 1000);
            // loop until adc is ready
            while ((input[3] & 0x80) != 0)
            {
                MyI2C.Execute(ReadTran, 1000);
            }
            // combine the bits into value
            int t = ((input[0] & 00000001) << 16) | (input[1] << 8) | input[2];
            // invert if negative number
            if (input[0] > 128)
            {
                t = ~(0x020000 - t);
            }
            // convert reading to 0-5V value based on varDivisior from datasheet 
            double varDivisior = 64;
            double varMultiplier = (2.4705882 / varDivisior) / 1000;
            double result = t * varMultiplier;
            if (result >= 0.0)
            {
                return result;
            }
            else
            {
                return 0;
            }
           
        }
        
    }
}
