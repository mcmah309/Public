#include <stdio.h>
#include <netdb.h>
#include <netinet/in.h>
#include <stdlib.h>
#include <string.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <zconf.h>
#include <pthread.h>
#include <signal.h>
#include <arpa/inet.h>
#include <semaphore.h> 
#include "../include/protocol.h"

void checkin(int id, int clientfd);
void update_azlist(int* buffer, int clientfd);
void get_azlist(int id, int clientfd);
void get_mapper_updates(int id, int clientfd);
void get_all_updates(int id, int clientfd);
void checkout(int id, int clientfd);
void * reducer(void * args);

int * azList;
int updateStatus;
pthread_mutex_t azlist;
pthread_mutex_t table;
sem_t sem;
//pthread_mutex_t clientInfo;


typedef struct UpdateStatus UpdateStatus;
struct UpdateStatus{
    int MapperID[MAX_CONCURRENT_CLIENTS];
    int NumOfUpdates[MAX_CONCURRENT_CLIENTS];
    int Check[MAX_CONCURRENT_CLIENTS];
};
UpdateStatus* status;

/*
typedef struct Connection Connection;
struct Connection{
    int short port;
    char address[INET_ADDRSTRLEN];
};
Connection connection[MAX_CONCURRENT_CLIENTS];
*/

int AZList[]={0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};


int main(int argc, char *argv[]) {
    printf("Server running\n");

    status = calloc(1, sizeof(UpdateStatus));
    azList = calloc(ALPHABETSIZE, sizeof(int));
    //initalizing server data
    pthread_mutex_init(&azlist, NULL);
    pthread_mutex_init(&table, NULL);
    sem_init(&sem, 0, 0);
    //pthread_mutex_init(&clientInfo, NULL);

    int server_port;

    if (argc == 2) { // 1 arguments
        server_port = atoi(argv[1]);
    } else {
        printf("Invalid or less number of arguments provided\n");
        printf("./server <server Port>\n");
        exit(0);
    }

    // Server (Reducer) code
	int sock = socket(AF_INET , SOCK_STREAM , 0);
	
    //Binding to a local address
	struct sockaddr_in servAddress;
	servAddress.sin_family = AF_INET;
	servAddress.sin_port = htons(server_port);
	servAddress.sin_addr.s_addr = htonl(INADDR_ANY);
	bind(sock, (struct sockaddr *) &servAddress, sizeof(servAddress));

    //a few vars for incoming connections and threads for reducer threads
    pthread_t threads[MAX_CONCURRENT_CLIENTS];
    int reducers = 0; 
    int clientfd = 0;

    //listen on the port
    printf("Server listening on IP: %i Port: %i\n", servAddress.sin_addr.s_addr, servAddress.sin_port);
	listen(sock, MAX_CONCURRENT_CLIENTS);


    //server runs till stopped by user
	while (1) {

		//Accept the incoming connections.
		struct sockaddr_in clientAddress;

		socklen_t size = sizeof(struct sockaddr_in);
		clientfd = accept(sock, (struct sockaddr*) &clientAddress, &size);
        if(clientfd != 0){
            char address[INET_ADDRSTRLEN];
            int port=0;
            inet_ntop(AF_INET, &(clientAddress.sin_addr),  address, INET_ADDRSTRLEN); 
            port=ntohs(clientAddress.sin_port);
            printf("open connection from %s:%d\n", address, port);
            
            pthread_create(&threads[reducers], NULL, reducer, &clientfd );
            sem_wait(&sem);
            clientfd = 0; 
            reducers++;
        }
        //printf("Connections detected: %i\n", reducers);
	
	}
} 

void * reducer(void * args){
    //printf("Reducer running\n");
    int* temp = (int*) args;
    int clientfd = (*temp);
    sem_post(&sem);
    int* buffer = calloc(LONG_RESPONSE_MSG_SIZE, sizeof(int));
    int* temp2 = calloc(LONG_RESPONSE_MSG_SIZE, sizeof(int));
    while(1){
        read(clientfd, temp2, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
        for(int i =0; i<LONG_RESPONSE_MSG_SIZE; i++){
            buffer[i]=ntohl(temp2[i]);
            //printf("(s)%i", buffer[i]);
        }
        //printf("\n");

        switch(buffer[0]){
            case 1:
                checkin(buffer[1], clientfd);
                break;
            case 2:
                update_azlist( buffer, clientfd);
                break;
            case 3:
                get_azlist(buffer[1], clientfd);
                break;
            case 4:
                get_mapper_updates(buffer[1], clientfd);
                break;
            case 5:
                get_all_updates(buffer[1], clientfd);
                break;
            case 6:
                checkout(buffer[1], clientfd);
                pthread_exit(NULL);
                break;
            default:
                break;
        }
        if(buffer[0] == -1){
            pthread_exit(NULL);
        }
        int buffersize = sizeof(buffer);
        memset(buffer, '\0',buffersize);
        int temp2size = sizeof(temp2);
        memset(temp2, '\0', temp2size);
    }


    
}

void checkin(int id, int clientfd){// check in client
    if(id != -1){
        pthread_mutex_lock(&table);
        status->Check[id - 1]=1;
        pthread_mutex_unlock(&table);
    }
    int* temp = calloc(1, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
    temp[0]=1;
    if(id == -1){
        temp[1] = RSP_NOK; 
    }
    temp[2]=id;
    for(int i =0; i<LONG_RESPONSE_MSG_SIZE; i++){
        temp[i]=htonl(temp[i]);
    }
    write(clientfd, temp, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
    free(temp);
    printf("[%d] CHECKIN\n", id);  
    return;
}

void update_azlist(int* buffer, int clientfd){// return list
    pthread_mutex_lock(&azlist);
    for(int i=0; i<26;i++){
        AZList[i] += buffer[2+i];
    }
    pthread_mutex_unlock(&azlist);
    pthread_mutex_lock(&table);
    status->NumOfUpdates[buffer[1]] +=1;
    pthread_mutex_unlock(&table);
    int* temp = calloc(1, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
    temp[0]=2;
    if(buffer[1] == -1){
        temp[1] = RSP_NOK; 
    }
    temp[2]=buffer[1];
    for(int i =0; i<LONG_RESPONSE_MSG_SIZE; i++){
        //printf("%i", temp[i]);
        temp[i]=htonl(temp[i]);
    }        
    //printf("sever sent\n");
    write(clientfd, temp, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
    free(temp);
    return;
}

void get_azlist(int id, int clientfd){// get list
    if(id !=-1){
        pthread_mutex_lock(&table);
        if(status->Check[id-1] == 0){// is already checked in? No, send back error
            pthread_mutex_unlock(&table);
            int* temp = calloc(1, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
            temp[0]=3;
            temp[1]=1;
            for(int i =0; i<LONG_RESPONSE_MSG_SIZE; i++){
                temp[i]=htonl(temp[i]);
            }            
            write(clientfd, temp, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
            free(temp);
            printf("[%d] GET_AZLIST\n", id);
            return;
        }
        else{// Yes send back list
            pthread_mutex_unlock(&table);
            pthread_mutex_lock(&azlist);
            int* temp = calloc(1, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
            temp[0]=3;
            temp[1]=0;
            for(int i =2; i<LONG_RESPONSE_MSG_SIZE; i++){
                temp[i]=AZList[i-2];
            } 
            pthread_mutex_unlock(&azlist);       
            for(int i =0; i<LONG_RESPONSE_MSG_SIZE; i++){
                temp[i]=htonl(temp[i]);
            }   
            write(clientfd, temp, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
            free(temp);
            printf("[%d] GET_AZLIST\n", id);
            return;
        }
    }
    else{
        pthread_mutex_lock(&azlist);
        int* temp = calloc(1, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
        temp[0]=3;
        temp[1]=0;
        for(int i =2; i<LONG_RESPONSE_MSG_SIZE; i++){
            temp[i]=AZList[i-2];
        } 
        pthread_mutex_unlock(&azlist);       
        for(int i =0; i<LONG_RESPONSE_MSG_SIZE; i++){
            temp[i]=htonl(temp[i]);
        }   
        write(clientfd, temp, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
        free(temp);
        printf("[%d] GET_AZLIST\n", id);
        return;
    }
}

void get_mapper_updates(int id, int clientfd){// return update  number for client
    int* temp = calloc(1, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
    temp[0]=4;
    pthread_mutex_lock(&table);
    temp[2]=status->NumOfUpdates[id-1];
    pthread_mutex_unlock(&table);
    if(id == -1){
        temp[1] = RSP_NOK; 
    }
    for(int i =0; i<LONG_RESPONSE_MSG_SIZE; i++){
        temp[i]=htonl(temp[i]);
    }
    write(clientfd, temp, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
    free(temp);
    printf("[%d] GET_MAPPER_UPDATES\n", id);
}

void get_all_updates(int id, int clientfd){// get  the sum of number of updates in the table
    pthread_mutex_lock(&table);
    int sum=0;
    for(int i=0; i<MAX_CONCURRENT_CLIENTS; i++){
        sum += status->NumOfUpdates[i];
    }
    pthread_mutex_unlock(&table);
    int* temp = calloc(1, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
    temp[0]=5;
    temp[2]=sum;
    for(int i =0; i<LONG_RESPONSE_MSG_SIZE; i++){
        temp[i]=htonl(temp[i]);
    }
    write(clientfd, temp, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
    free(temp);
    printf("[%d] GET_ALL_UPDATES\n", id);
}

void checkout(int id, int clientfd){// sets check in status to 0
    if(id != -1){
        pthread_mutex_lock(&table);
        status->Check[id-1] = 0;
        pthread_mutex_unlock(&table);
    }
    int* temp = calloc(1, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
    temp[0]=6;
    if(id == -1){
        temp[1] = RSP_NOK; 
    }
    temp[2]=id;      
    for(int i =0; i<LONG_RESPONSE_MSG_SIZE; i++){
        temp[i]=htonl(temp[i]);
    }
    write(clientfd, temp, sizeof(int)*LONG_RESPONSE_MSG_SIZE);
    free(temp);
    printf("[%d] CHECKOUT\n", id);
    
}