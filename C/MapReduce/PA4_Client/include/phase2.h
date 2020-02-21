#ifndef PHASE2_H
#define PHASE2_H
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

void phase_two(int mappers, char * sIp, int sPort);

int checkin(int mapper_id, int sockfd);
int updateAZ(int mapper_id, int sockfd);
int * getAZ(int mapper_id, int sockfd);
int getAllUpdates(int mapper_id, int sockfd);
int getMapperUpdates(int mapper_id, int sockfd);
int checkout(int mapper_id, int sockfd);

int * d;
int * req;
int * resp;
int * longResp;
int * temp;

int requests;

#endif