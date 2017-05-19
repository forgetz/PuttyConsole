using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuttyConsole
{
    public class SSH
    {
        private SshClient _client;

        public SshClient OpenSession(string host, string username, string password, int port = 22)
        {
            _client = new SshClient(host, port, username, password);
            _client.Connect();
            if (!_client.IsConnected)
                return null;
            return _client;
        }

        public string SendCommand(string command)
        {
            var cmd = _client.RunCommand(command);
            return cmd.Result;
        }

        public void CloseSession()
        {
            _client.Disconnect();
        }

    }
}
