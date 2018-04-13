using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSocket
{
   public class SocketClient : IDisposable
    {

        private TcpClient tcpClient;
        private NetworkStream networkStream;
        byte[] SendMessageBuffer;
        byte[] ReceiveMessageBuffer;
        internal ConnectionStatus _status;
        private bool isDisposed = false;
    

        string IPAdress;
        int portnumber;

        ~SocketClient()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="PortNumber"></param>
        public SocketClient(string IP, int PortNumber)
        {
            tcpClient = new TcpClient();
            tcpClient.ReceiveTimeout = 1;
            this.IPAdress = IP;
            this.portnumber = PortNumber;
        }

        /// <summary>
        /// Asynchronously connect to the TCP connection
        /// </summary>
        public async Task ConnectAsync()
        {
            try
            {
                Console.WriteLine("\n -- Connecting --");
                await tcpClient.ConnectAsync(IPAdress, portnumber);
                Console.WriteLine("Saying HELLO! to the server/n /n");
                _status = ConnectionStatus.Connected;
                await SendAsync("HELLO there!");
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        /// <summary>
        /// Send the message to the server using async await pattern
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendAsync(string message)
        {
            try
            {
                networkStream = tcpClient.GetStream();
                ASCIIEncoding asciiData = new ASCIIEncoding();
                byte[] array = asciiData.GetBytes(message);
                Console.WriteLine("-- Transmitting data --");
                await networkStream.WriteAsync(array, 0, array.Length);
            }
            catch (Exception)
            {
                throw;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task ReceiveAsync()
        {

            byte[] buffer = new byte[1024];
            int iRx = networkStream.Read(buffer, 0, buffer.Length);
            char[] chars = new char[iRx];
            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
            int charLen = d.GetChars(buffer, 0, iRx, chars, 0);
            System.String szData = new System.String(chars);

        }

        public void Dispose()
        {
            try
            {
                this.Dispose(true);
                GC.SuppressFinalize(true);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {

            }
        }

        private void Dispose(bool Desposing)
        {
            try
            {
                if (Desposing && !this.isDisposed)
                {
                    //cleanup unmanaged code
                    if (this.networkStream !=null)
                    {
                        networkStream.Close();
                        networkStream.Dispose();
                    }
                    
                    if (this.tcpClient !=null)
                    {
                        tcpClient.Close();
                        tcpClient.Dispose();
                    }
                    this._status = ConnectionStatus.Disconnected;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Some error occured while desposing:{0}",ex.Message);
            }
            finally
            {
                this.isDisposed = true;
            }
        }
    }


    enum ConnectionStatus
    {
        Connected,
        connecting,
        Disconnected

    }
}
