TcpCapture
==========

Simple utility to capture a TCP stream and dump the output to console (stdout).
Made as a personal test, not a production ready product of high quality.

Works like a TCP server, may be used as a [netcat (nc)](https://en.wikipedia.org/wiki/Netcat)
alternative for incoming TCP streams.

Written in C# using dotnet 7. Heavily inspired by Microsofts official documentation on the
[TcpListener Class](https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener?view=net-7.0#examples)
(at 2023-03-18).

## Usage

```
Usage:
TcpCapture [--port Int32] [--ip String] [--help]

	-i, --ip	 - Specify listening IP. Default 127.0.0.1
			   Use 0.0.0.0 to listen on all interfaces.
	-p, --port	 - Specify listening TCP port. Default 8210
	-h, --help	 - Print this help text
```

## Example

Listen on local loopback interface only, port 8000:

```
dotnet run -- --port 8000
```

Listen on all intefaces default port 8210:

```
dotnet run -- --ip 0.0.0.0
```

IPv6 loopback:

```
dotnet run -- -i "::1"
```

## Build for debug

```
dotnet build
```

## Build for release

```
dotnet publish -c "Release"
```

## License

[MIT License](./LICENSE)
