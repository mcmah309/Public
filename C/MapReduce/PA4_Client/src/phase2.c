#include "../include/phase2.h"

/* 	Map Function
	1)	Each mapper selects a Mapper_i.txt to work with
	2)	Creates a list of letter, wordcount from the text files found in the Mapper_i.txt
	3)	Send the list to Reducer via pipes with proper closing of ends
*/
FILE * lPtr;

void phase_two(int mappers, char * sIp, int sPort){ //Takes in # of mappers and pipes
	int status = 0; 
	//lPtr = logfp;/*fopen(logBuffer,"a");*/

	//Big for loop makes (int mappers) # of processes, each one takes its respective "Mapper_i.txt"
	//opens files found inside, reads and sorts by first letter which is counted in dictionary (d[]).
	//Sends dictionary to a waiting server, following protocol and logging all occurances while doing so
	for (int m = 0; m < mappers; m++){
		if(fork() == 0){
			
			//mapper process specific id
			int mapper_id = m + 1; 
			requests = 0;
			
			//opening log file
			char logBuffer[1024];//logBuffer is used for the log filepath aswell as the buffer for log printing
			char current_dir[1024];
			
			lPtr = fopen("./PA4_Client/log/log.txt", "a");
			if(!lPtr){
				printf("Log file failed to open\n");
			}

			//intializing dictionary and request/response arrays
			d = calloc(ALPHABETSIZE,sizeof(int));
			req = calloc(LONG_RESPONSE_MSG_SIZE, sizeof(int));
			resp = calloc(LONG_RESPONSE_MSG_SIZE, sizeof(int));
			longResp = calloc(LONG_RESPONSE_MSG_SIZE, sizeof(int));
			temp = calloc(LONG_RESPONSE_MSG_SIZE, sizeof(int));

			//---------------------------------------------------------------------------------------------------------------Client
				// Create a TCP socket.
				int sockfd = socket(AF_INET , SOCK_STREAM , 0);

				// Specify an address to connect to (we use the local host or 'loop-back' address).
				struct sockaddr_in address;
				address.sin_family = AF_INET;
				address.sin_port = htons(sPort);//4061
				address.sin_addr.s_addr = inet_addr(sIp);//"127.0.0.1"

				// Connect it.
				printf("Client %i attmepting to connect to Port: %i IP: %s\n", mapper_id, sPort, sIp);
				if (connect(sockfd, (struct sockaddr *) &address, sizeof(address)) == 0){
					sprintf(logBuffer, "[%d] open connection\n", mapper_id);
					if(fputs(logBuffer, lPtr)<=0){
						printf("Error in writing to log_client.txt\n");
						exit(0);
					}
				}else{ 
					perror("Connection failed!");
					exit(0);
				}

				if(!checkin(mapper_id, sockfd)){
					exit(0);
				}
			//client is now checked in~~
			//---------------------------------------------------------------------------------------------------------------Client


			
			//Strings for "Mapper_I.txt" file path(mFilePath), testcase paths inside mapper_i (wordFilePath)
			//Each word found in testcase txt files(word), and a temp string for cwd and anything else(current_dir)
			char mFilePath[1024];
			char wordFilePath[1024]; 
			char word[1024];

			FILE *mPtr; //mapper_i file stream
			FILE *fPtr; //testcase txt file stream

			sprintf(mFilePath, "%s/MapperInput/Mapper_%i.txt", getcwd(current_dir, 1024), mapper_id);

			mPtr = fopen(mFilePath,"r");
			
			if(!mPtr){
				printf("File failed to open: <%s>\n", mFilePath);
				exit(0);
			}
			
			while (fgets(wordFilePath, 1024, mPtr)){ 		
				
				wordFilePath[strlen(wordFilePath) - 1] = '\0';
				fPtr = fopen(wordFilePath, "r");

				if (!fPtr) {
					printf("File failed to open: <%s>\n", wordFilePath);
					exit(0);
				}
				
				while(fgets(word, 39, fPtr)){
					switch(toupper(word[0])){
							case 'A':
								d[0]++; 
								break;
							case 'B':
								d[1]++; 
								break;
							case 'C':
								d[2]++;
								break; 
							case 'D':
								d[3]++; 
								break;
							case 'E':
								d[4]++; 
								break;
							case 'F':
								d[5]++; 
								break;
							case 'G':
								d[6]++; 
								break;
							case 'H':
								d[7]++; 
								break; 
							case 'I':
								d[8]++; 
								break;
							case 'J':
								d[9]++; 
								break;
							case 'K':
								d[10]++;
								break;
							case 'L':
								d[11]++; 
								break;
							case 'M':
								d[12]++; 
								break; 
							case 'N':
								d[13]++; 
								break;
							case 'O':
								d[14]++; 
								break;
							case 'P':
								d[15]++;
								break; 
							case 'Q':
								d[16]++; 
								break;
							case 'R':
								d[17]++; 
								break;
							case 'S':
								d[18]++; 
								break;
							case 'T':
								d[19]++; 
								break;
							case 'U':
								d[20]++; 
								break; 
							case 'V':
								d[21]++; 
								break;
							case 'W':
								d[22]++; 
								break;
							case 'X':
								d[23]++;
								break;
							case 'Y':
								d[24]++; 
								break;
							case 'Z':
								d[25]++; 
								break; 						
					}

				} 
				fclose(fPtr);
				// --send to server and clear dictionary

				//Message structure
				if(!updateAZ(mapper_id, sockfd)){
					exit(0);
				}

				memset(d, 0, sizeof(int) * 26); 
			}
			//close connection
			sprintf(logBuffer, "[%d] UPDATE_AZLIST: %d\n", mapper_id, requests);//logging if correct
			if(fputs(logBuffer, lPtr)<=0){
				printf("Error in writing to log_client.txt\n");
			}

			getAZ(mapper_id, sockfd);
			getMapperUpdates(mapper_id, sockfd);
			getAllUpdates(mapper_id, sockfd);
			if(checkout(mapper_id, sockfd)){
				close(sockfd);
				
				sprintf(logBuffer, "[%d] close connection\n", mapper_id);
				if(fputs(logBuffer, lPtr)<=0){
					printf("Error in writing to log_client.txt\n");
					exit(0);
				}
				
				fclose(lPtr);
				fclose(mPtr);
				exit(1);
			}else{
				fprintf(stderr,"Mapper %i bad exit\n", mapper_id);
			}
		}
	}
	while(wait(&status)>0); //Parent waits for all children to finish
}

int checkin(int mapper_id, int sockfd){
	char logBuffer[1024];
	//building message
	req[RQS_COMMAND_ID] = htonl(CHECKIN);//first element is request code
	req[RQS_MAPPER_PID] = htonl(mapper_id);//second element is mapper id
	//this is where the data field would be converted to network byte order, each element needs to be seperatly htonl()'d
	
	//sending
	write(sockfd, req, sizeof(int) * LONG_RESPONSE_MSG_SIZE);
	
	//response
	read(sockfd, temp, sizeof(int) * LONG_RESPONSE_MSG_SIZE);//getting response from server
	for(int i = 0; i < LONG_RESPONSE_MSG_SIZE; i++){//converting back to host byte order
		resp[i] = ntohl(temp[i]);
	}

	//checking and logging
	if(resp[RSP_COMMAND_ID] == CHECKIN && resp[RSP_CODE] == RSP_OK && resp[2] == mapper_id){//assuring response is as expected
		requests++;
		sprintf(logBuffer, "[%d] CHECKIN: %d %d\n", mapper_id, resp[1], resp[2]);//logging if correct
		if(fputs(logBuffer, lPtr)<=0){
			printf("Error in writing to log_client.txt\n");
			exit(0);
		}else{
			return 1;
		}
	}else{//logging if incorrect
		requests++;
		sprintf(logBuffer, "(E) [%d] CHECKIN: %d %d\n", mapper_id, resp[1], resp[2]);
		if(fputs(logBuffer, lPtr)<=0){
			fprintf(stderr,"Error in writing to log_client.txt\n");
			exit(0);
		}
		fprintf(stderr,"Mapper %i recieved an unexpected response-\n\treq: %i\n\tresp:%i\n\tmapper: %i\n\treq#: %i\n", mapper_id, resp[0], resp[1], resp[2], requests);
		return 0;
	}
}

int updateAZ(int mapper_id, int sockfd){
	char logBuffer[1024];
	//building message
	req[RQS_COMMAND_ID] = htonl(UPDATE_AZLIST);//first element is request code
	req[RQS_MAPPER_PID] = htonl(mapper_id);//second element is mapper id
	for(int i = 2; i < LONG_RESPONSE_MSG_SIZE; i++){//data
		req[i] = htonl(d[i-2]);
	}

	//sendinghtonlhtonlhtonlhtonlhtonlhtonlhtonl
	write(sockfd, req, sizeof(int) * LONG_RESPONSE_MSG_SIZE);//SENDING
	
	//response
	while(1){
		read(sockfd, temp, sizeof(int) * LONG_RESPONSE_MSG_SIZE);//RESPONSE
		for(int i = 0; i < LONG_RESPONSE_MSG_SIZE; i++){//converting back to host byte order
			resp[i] = ntohl(temp[i]);
			//printf("%i", resp[i]);
		}
		//printf(" In UpdatAZ| Client %i\n", mapper_id);
		if(resp[0] != 0){
			break;
		}
		//sleep(3);
	}
	
	//checking and logging
	if(resp[RSP_COMMAND_ID] == UPDATE_AZLIST && resp[RSP_CODE] == RSP_OK && resp[2] == mapper_id){//assuring response is as expected
		requests++;
		return 1; 
	}else{//logging if incorrect
		sprintf(logBuffer, "(E)[%d] UPDATE_AZLIST: %d\n", mapper_id, requests);
		if(fputs(logBuffer, lPtr)<=0){
			fprintf(stderr,"Error in writing to log_client.txt\n");
			exit(0);
		}
		fprintf(stderr,"Mapper %i recieved an unexpected response-\n\treq: %i\n\tresp:%i\n\tmapper: %i\n\treq#: %i\n", mapper_id, resp[0], resp[1], resp[2], requests);
		return 0;
	}
}

int * getAZ(int mapper_id, int sockfd){
	char logBuffer[1024] = "";
	char tempBuf[1024] = "";
	//building message
	req[RQS_COMMAND_ID] = htonl(GET_AZLIST);//first element is request code
	req[RQS_MAPPER_PID] = htonl(mapper_id);//second element is mapper id
	
	//sending
	write(sockfd, req, sizeof(int) * LONG_RESPONSE_MSG_SIZE);
	
	//response
	while(1){
		read(sockfd, temp, sizeof(int) * LONG_RESPONSE_MSG_SIZE);//getting response from server
		for(int i = 0; i < LONG_RESPONSE_MSG_SIZE; i++){//converting back to host byte order
			longResp[i] = ntohl(temp[i]);
		}
		if(resp[0] != 0){
			break;
		}
	}

	//checking and logging
	if(longResp[RSP_COMMAND_ID] == GET_AZLIST && longResp[RSP_CODE] == RSP_OK){//assuring response is as expected
		requests++;
		
		for(int i = 0; i < ALPHABETSIZE; i++){
			sprintf(logBuffer, " %d", longResp[i + 2]);
			strcat(tempBuf, logBuffer);	
		}

		sprintf(logBuffer, "[%d] GET_AZLIST: %d%s\n", mapper_id, resp[1], tempBuf);//logging if correct

		if(fputs(logBuffer, lPtr)<=0){
			printf("Error in writing to log_client.txt\n");
			exit(0);
		}else{
			return (longResp + 2);
		}	
	}else{//logging if incorrect
		requests++;
		sprintf(logBuffer, "(E)[%d] GET_AZLIST: %d %d\n", mapper_id, resp[1], resp[2]);
		if(fputs(logBuffer, lPtr)<=0){
			fprintf(stderr,"Error in writing to log_client.txt\n");
			exit(0);
		}
		if(requests > 0){
			fprintf(stderr,"Mapper %i recieved an unexpected response-\n\treq: %i\n\tresp:%i\n\tmapper: %i\n\treq#: %i\n", mapper_id, resp[0], resp[1], resp[2], requests);
			return 0;
		}
	}
}

int getMapperUpdates(int mapper_id, int sockfd){
	char logBuffer[1024];
	//building message
	req[RQS_COMMAND_ID] = htonl(GET_MAPPER_UPDATES);//first element is request code
	req[RQS_MAPPER_PID] = htonl(mapper_id);//second element is mapper id
	
	//sending
	write(sockfd, req, sizeof(int) * LONG_RESPONSE_MSG_SIZE);
	
	//response
	while(1){
		read(sockfd, temp, sizeof(int) * LONG_RESPONSE_MSG_SIZE);//RESPONSE
		for(int i = 0; i < LONG_RESPONSE_MSG_SIZE; i++){//converting back to host byte order
			resp[i] = ntohl(temp[i]);
		}
		if(resp[0] != 0){
			break;
		}
	}

	//checking and logging
	if(resp[RSP_COMMAND_ID] == GET_MAPPER_UPDATES && resp[RSP_CODE] == RSP_OK){//assuring response is as expected
		requests++;
		sprintf(logBuffer, "[%d] GET_MAPPER_UPDATES: %d %d\n", mapper_id, resp[1], resp[2]);//logging if correct
		if(fputs(logBuffer, lPtr)<=0){
			printf("Error in writing to log_client.txt\n");
			exit(0);
		}else{
			return resp[2];
		}
	}else{//logging if incorrect
		requests++;
		sprintf(logBuffer, "(E)[%d] GET_MAPPER_UPDATES: %d %d\n", mapper_id, resp[1], resp[2]);
		if(fputs(logBuffer, lPtr)<=0){
			fprintf(stderr,"Error in writing to log_client.txt\n");
			exit(0);
		}
		if(requests > 0){
			fprintf(stderr,"Mapper %i recieved an unexpected response-\n\treq: %i\n\tresp:%i\n\tmapper: %i\n\treq#: %i\n", mapper_id, resp[0], resp[1], resp[2], requests);
			return 0;
		}
	}

}

int getAllUpdates(int mapper_id, int sockfd){
	char logBuffer[1024];
	//building message
	req[RQS_COMMAND_ID] = htonl(GET_ALL_UPDATES);//first element is request code
	req[RQS_MAPPER_PID] = htonl(mapper_id);//second element is mapper id
	
	//sending
	write(sockfd, req, sizeof(int) * LONG_RESPONSE_MSG_SIZE);
	
	//response
	while(1){
		read(sockfd, temp, sizeof(int) * LONG_RESPONSE_MSG_SIZE);//RESPONSE
		for(int i = 0; i < LONG_RESPONSE_MSG_SIZE; i++){//converting back to host byte order
			resp[i] = ntohl(temp[i]);
		}
		if(resp[0] != 0){
			break;
		}
	}

	//checking and logging
	if(resp[RSP_COMMAND_ID] == GET_ALL_UPDATES && resp[RSP_CODE] == RSP_OK){//assuring response is as expected
		requests++;
		sprintf(logBuffer, "[%d] GET_ALL_UPDATES: %d %d\n", mapper_id, resp[1], resp[2]);//logging if correct
		if(fputs(logBuffer, lPtr)<=0){
			printf("Error in writing to log_client.txt\n");
			exit(0);
		}else{
			return resp[2];
		}
	}else{//logging if incorrect
		requests++;
		sprintf(logBuffer, "(E)[%d] GET_ALL_UPDATES: %d %d\n", mapper_id, resp[1], resp[2]);
		if(fputs(logBuffer, lPtr)<=0){
			fprintf(stderr,"Error in writing to log_client.txt\n");
			exit(0);
		}
		if(requests > 0){
			fprintf(stderr,"Mapper %i recieved an unexpected response-\n\treq: %i\n\tresp:%i\n\tmapper: %i\n\treq#: %i\n", mapper_id, resp[0], resp[1], resp[2], requests);
			return 0;
		}
	}
}

int checkout(int mapper_id, int sockfd){
	char logBuffer[1024];
	//building message
	req[RQS_COMMAND_ID] = htonl(CHECKOUT);//first element is request code
	req[RQS_MAPPER_PID] = htonl(mapper_id);//second element is mapper id
	//this is where the data field would be converted to network byte order, each element needs to be seperatly htonl()'d
	
	//sending
	write(sockfd, req, sizeof(int) * LONG_RESPONSE_MSG_SIZE);
	
	//response
	while(1){
		read(sockfd, temp, sizeof(int) * LONG_RESPONSE_MSG_SIZE);//RESPONSE
		for(int i = 0; i < LONG_RESPONSE_MSG_SIZE; i++){//converting back to host byte order
			resp[i] = ntohl(temp[i]);
		}
		if(resp[0] != 0){
			break;
		}
	}

	//checking and logging
	if(resp[RSP_COMMAND_ID] == CHECKOUT && resp[RSP_CODE] == RSP_OK && resp[2] == mapper_id){//assuring response is as expected
		requests++;
		sprintf(logBuffer, "[%d] CHECKOUT: %d %d\n", mapper_id, resp[1], resp[2]);//logging if correct
		if(fputs(logBuffer, lPtr)<=0){
			printf("Error in writing to log_client.txt\n");
			exit(0);
		}else{
			return 1;
		}
	}else{//logging if incorrect
		requests++;
		sprintf(logBuffer, "(E)[%d] CHECKOUT: %d %d\n", mapper_id, resp[1], resp[2]);
		if(fputs(logBuffer, lPtr)<=0){
			fprintf(stderr,"Error in writing to log_client.txt\n");
			exit(0);
		}
		fprintf(stderr,"Mapper %i recieved an unexpected response-\n\treq: %i\n\tresp:%i\n\tmapper: %i\n\treq#: %i\n", mapper_id, resp[0], resp[1], resp[2], requests);
		return 0;
	}
}