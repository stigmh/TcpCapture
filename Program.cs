using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

Int32 port = 8210;
uint connectionsCounter = 0;
IPAddress localAddr = IPAddress.Parse("127.0.0.1");

TcpListener? server = null;

int i;

// Simple argument parser
for (i = 0; i < args.Length; ++i) {
	int next = i + 1;

	if (next != args.Length) {
		if (args[i] == "-p" || args[i] == "--port") {
			port = Int32.Parse(args[next]);
			i = next;
			continue;
		}

		if (args[i] == "-i" || args[i] == "--ip") {
			localAddr = IPAddress.Parse(args[next]);
			i = next;
			continue;
		}
	}

	int ret = 0;

	// Check if help i specified
	if (args[i] != "-h" && args[i] != "--help" && args[i] != "help") {
		ret = 1;
		Console.SetOut(Console.Error); // use stderr
		Console.WriteLine("One or more unsupported arguments!");
	}

	String progName = System.AppDomain.CurrentDomain.FriendlyName;

	Console.WriteLine("Usage:\n{0} [--port Int32] [--ip String] [--help]\n", progName);
	Console.WriteLine("\t-i, --ip\t - Specify listening IP. Default 127.0.0.1");
	Console.WriteLine("\t\t\t   Use 0.0.0.0 to listen on all interfaces.");
	Console.WriteLine("\t-p, --port\t - Specify listening TCP port. Default 8210");
	Console.WriteLine("\t-h, --help\t - Print this help text");

	Environment.Exit(ret);
}

Console.WriteLine("Waiting for connections on {0}:{1}", localAddr, port);
Console.WriteLine("Press [Ctrl]+[c] to terminate.");

// Start listening for connections
try {
	server = new TcpListener(localAddr, port);
	server.Start();

	Byte[] bytes = new Byte[256];

	while(true) {
		// Block-wait for client connection
		using TcpClient client = server.AcceptTcpClient();
		NetworkStream stream = client.GetStream();

		// Connection established, retrieve and print data
		Console.WriteLine("########## CONNECTION {0} ##########", ++connectionsCounter);

		using Stream stdout = Console.OpenStandardOutput();
		while ((i = stream.Read(bytes, 0, bytes.Length)) != 0) {
			// Write raw bytes to console
			stdout.Write(bytes, 0, i);
		}

		Console.WriteLine();
		client.Close();
	}
} catch(SocketException e) {
	Console.WriteLine("SocketException: {0}", e);
} finally {
	if (server != null) {
		server.Stop();
	}
}

return 0;
