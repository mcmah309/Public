#include "matvec.h"
#include <stdlib.h>

int optimized_matrix_trans_mult_vec(matrix_t mat, vector_t vec, vector_t res){
  if(mat.cols != vec.len){
    printf("mat.cols (%ld) != vec.len (%ld)\n",mat.cols,vec.len);
    return 1;
  }
  if(mat.rows != res.len){
    printf("mat.rows (%ld) != res.len (%ld)\n",mat.rows,res.len);
    return 2;
  }
  int *veci = &(VGET(vec,0));//pointer to multiplying vector
  int *elij = &(MGET(mat,0,0));//pointer to start of matrix
  int *x= &(VGET(res,0));//pointer to new vector
  for(int j=0;j<mat.cols; j++){
    for(int i=0; i<mat.cols; i+=4){//unroll 4 times
      *x += ((*elij) * (*veci));//first entry in new vector
      elij++;//move throw matrix one
      x++;//go to next entry in new vector
      *x += ((*elij) * (*veci));
      elij++;
      x++;
      *x += ((*elij) * (*veci));
      elij++;
      x++;
      *x += ((*elij) * (*veci));
      elij++;
      x++;
    }
    veci++;// next entry in multiplying vector
    x-=mat.cols;//go back to first entry in new vector
  }
return 0;
}
