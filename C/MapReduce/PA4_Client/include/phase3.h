#ifndef PHASE3_H
#define PHASE3_H
#define _BSD_SOURCE
#define _DEFAULT_SOURCE
#define DIRNULL NULL
#define FILENULL NULL

#include <stdio.h>
#include <dirent.h>
#include <ctype.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <sys/stat.h>
#include <sys/socket.h>
#include <unistd.h>
#include <fcntl.h>
#include <string.h>
#include <pthread.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <errno.h>
#include "protocol.h"

void phase_three(char * sIp, int sPort);

int checkinM(int mapper_id, int sockfd);
int updateAZM(int mapper_id, int sockfd);
int * getAZM(int mapper_id, int sockfd);
int getAllUpdatesM(int mapper_id, int sockfd);
int getMapperUpdatesM(int mapper_id, int sockfd);
int checkoutM(int mapper_id, int sockfd);

int * d;
int * req;
int * resp;
int * longResp;
int * temp;

int requests;

#endif