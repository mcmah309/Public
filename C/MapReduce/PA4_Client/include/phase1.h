#ifndef PHASE1_H
#define PHASE1_H
#define DIRNULL NULL
#define FILENULL NULL
#define _BSD_SOURCE
#define _DEFAULT_SOURCE

#include <stdio.h>
#include <unistd.h>
#include <dirent.h>
#include <string.h>
#include <stdlib.h>
#include <sys/stat.h>
#include <sys/wait.h>



void recursiveTraverseFS(int mappers, char *basePath, FILE *fp[], int *toInsert, int *nFiles);
void traverseFS(int mappers, char *path);

#endif