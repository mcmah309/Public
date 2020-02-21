#include "../include/phase3.h"

FILE * lPtr;

void phase_three(char * sIp, int sPort){
    //master id
    int mapper_id = -1;

    FILE * cmdFile = fopen("./PA4_Client/commands.txt", "r");
    int cmds[100]; 
    char buf[20];
    int n = 0; 
    while (fgets(buf, 20, cmdFile)){
        cmds[n++] = atoi(buf);
    }
    
    requests = 0;
    
    //opening log file
    char logBuffer[1024];//logBuffer is used for the log filepath aswell as the buffer for log printing
    
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

    for(int i = 0; i < n; i++){
        
        //---------------------------------------------------------------------------------------------------------------Client
        // Create a TCP socket.
        int sockfd = socket(AF_INET , SOCK_STREAM , 0);

        // Specify an address to connect to (we use the local host or 'loop-back' address).
        struct sockaddr_in address;
        address.sin_family = AF_INET;
        address.sin_port = htons(sPort);//4061
        address.sin_addr.s_addr = inet_addr(sIp);//"127.0.0.1"

        // Connect it.
        printf("Client %i attmepting to connect to Port: %i IP: %d\n", mapper_id, address.sin_port, address.sin_addr.s_addr);
        if (connect(sockfd, (struct sockaddr *) &address, sizeof(address)) == 0){
            sprintf(logBuffer, "[-1] open connection\n");
            if(fputs(logBuffer, lPtr)<=0){
                printf("Error in writing to log_client.txt\n");
                exit(0);
            }
        }else{ 
            perror("Connection failed!");
            exit(0);
        }

        //client is now checked in~~
        //---------------------------------------------------------------------------------------------------------------Client

        switch(cmds[i]){
            case 1:
                checkinM(-1, sockfd);
                break;
            case 2:
                updateAZM(-1, sockfd);
                break;
            case 3:
                getAZM(-1, sockfd);
                break;
            case 4:
                getAllUpdatesM(-1, sockfd);
                break;
            case 5:
                getMapperUpdatesM(-1, sockfd);
                break;
            case 6:
                checkoutM(-1, sockfd);
                break;
            default:
                fprintf(lPtr, "wrong command\n");
                break;
        }
        
        sprintf(logBuffer, "[-1] close connection\n");
            if(fputs(logBuffer, lPtr)<=0){
                printf("Error in writing to log_client.txt\n");
                exit(0);
            }
        close(sockfd);
    }
        
        fclose(lPtr);
        exit(1);
}

int checkinM(int mapper_id, int sockfd){
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

int updateAZM(int mapper_id, int sockfd){
	char logBuffer[1024];
	//building message
	req[RQS_COMMAND_ID] = htonl(UPDATE_AZLIST);//first element is request code
	req[RQS_MAPPER_PID] = htonl(mapper_id);//second element is mapper id

	//sending
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

int * getAZM(int mapper_id, int sockfd){
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
		sprintf(logBuffer, "[%d] GET_AZLIST: %d%s\n", mapper_id, longResp[1], tempBuf);//logging if correct

		if(fputs(logBuffer, lPtr)<=0){
			printf("Error in writing to log_client.txt\n");
			exit(0);
		}else{
			return (longResp + 2);
		}	
	}else{//logging if incorrect
		requests++;
		sprintf(logBuffer, "(E)[%d] GET_AZLIST: %d %d\n", mapper_id, longResp[1], resp[2]);
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

int getMapperUpdatesM(int mapper_id, int sockfd){
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

int getAllUpdatesM(int mapper_id, int sockfd){
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

int checkoutM(int mapper_id, int sockfd){
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

