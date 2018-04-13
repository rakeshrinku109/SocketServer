using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerSocket
{
    public class Server
    {
        private IPAddress IPAddr;
        private Socket m_socListener;
        private EndPoint ServerEndPoint;
        private Socket m_socWorker;
        public Server()
        {

        }


        public void CreateSocket()
        {
            try
            {
                ServerEndPoint = new IPEndPoint(IPAddress.Any, 9298);
                m_socListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_socListener.Bind(ServerEndPoint);
                m_socListener.Listen(10);
                m_socListener.BeginAccept(new AsyncCallback(OnClientConnect), null);
                Console.WriteLine("-- Server activated and listening --");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {

            }

        }

        /// <summary>
        /// Invloke if client tries to conect
        /// </summary>
        /// <param name="ar"></param>
        private void OnClientConnect(IAsyncResult ar)
        {
            try
            {
                m_socListener = m_socListener.EndAccept(ar);
                WaitForData();

            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                
            }
        }

        private void WaitForData()
        {
            try
            {
                SocketPackets SockPack = new SocketPackets();
                SockPack.CurrentSocket = m_socListener;
                m_socListener.BeginReceive(SockPack.DataBuffer, 0, SockPack.DataBuffer.Length, SocketFlags.None, new AsyncCallback(OnDataReceived), SockPack);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void OnDataReceived(IAsyncResult ar)
        {
            SocketPackets theSockID = (SocketPackets)ar.AsyncState;

            int Irx = 0;
            Irx = theSockID.CurrentSocket.EndReceive(ar);
            char[] chars = new char[Irx + 1];
            Decoder decoder = Encoding.UTF8.GetDecoder();
            int charlen = decoder.GetChars(theSockID.DataBuffer, 0, Irx, chars, 0);
            string sZData = new string(chars);
            Console.Write(sZData);
            WaitForData();
            SendData();
        }

        private void SendData()
        {
            try
            {
                Object objData = "ramu";
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());
                m_socListener.Send(byData);
            }
            catch (SocketException se)
            {
                Console.WriteLine(se.Message);
            }
        }


    }
}
