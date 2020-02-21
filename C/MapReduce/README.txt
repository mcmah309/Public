Compilation: In folder p4, run "make" to compile. This will produce the executables “client” and “server” located in folder p4.

In terminal, inside file p4, run "make clean" to remove all the executables files, MapperInput, and log

Additional assumptions: Arguments when running the program will be in this format- 
	For Server:
		./server [port number]&
	For Client:
		./client ./PA4_Client/Testcases/[name of testcase] [# of clients] [IP] [port number] 

What it does: The server runs and connects to the specified port number. When a client tries to the connect to the server, it will spawn a thread on which the client 
will communicate with the sever. Once connection is established, the client is checked in and will begin to read through files in order to determine the amount each first 
letter of each word occurs in each file. The client then sends requests and information about the letter count to the sever over the established connection. Based on the 
requests and information passed to the server, the sever can update a list of the total amount of each letter and send the client back the requested information 
stored on the server. The server also keeps track of which clients are checked in. The server prints the specified client requests to the terminal and the 
client prints the specified server responses to a log.txt.

Extra credit is being attempted.
