all: server client

server:
	gcc -std=c99 -Wall -pthread -I ./PA4_Server/include -o server ./PA4_Server/src/server.c

client:
	gcc -pthread -I ./PA4_Client/include -o client ./PA4_Client/src/client.c ./PA4_Client/src/phase1.c ./PA4_Client/src/phase2.c ./PA4_Client/src/phase3.c

clean:
	rm client
	rm -rf ./PA4_Client/log
	rm -rf MapperInput
	rm server