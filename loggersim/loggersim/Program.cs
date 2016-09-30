

namespace loggersim
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            logger.createListener();

            Client client = new Client();
            client.Connect();





            //OPEN TCP/IP END POINT 

            //CREATE STREAM OF DATA

            //TRANSMIT DATA OVER THAT PORT

            //CRATE A TCP/IP FOR CONSUMER

            //CONSUMER MUST OBTAIN A TCP/IP ENDPOINT WHERE DATA IS PUBLISHED

            //CONSUMER MUST READ THE INCOMING DATA STREAM

            //SAVE THE DATA TO A FILE

        }
    }
    
}
