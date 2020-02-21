Optimization 1: When I iterated through the matrix in the same way the data is stored. This would make it faster since the momery would only have to move over one spot instead of constantly jumping around the matrix to find the information. This utilization localization and maximizes cache hits

Optimization 2: I unrolled the inner loop four times. This made it faster since the program will be able to compute multiple computations in parallel and the number of comparisons the function needs to make is reduced by a factor of 4. This utilizes the number of registers.

Optimization 3: I Only called the functions once and set the pointers equal to the addresses of the retrieved information. This way, whenever I want the next information I can just increment the pointer instead of using another function

Test Data:
  mcmah309@csel-apollo:/home/mcmah309/2021/a4-code $ ./mult_benchmark
  SIZE       BASE       NORM        OPT BSPDUP NSPDUP
   512 2.0750e-03 1.0780e-03 5.8700e-04   3.53   1.84
  1024 4.0105e-02 4.6730e-03 2.9700e-03  13.50   1.57
  2048 3.0467e-01 1.8965e-02 1.2553e-02  24.27   1.51
  4096 1.3069e+00 7.3414e-02 4.2822e-02  30.52   1.71
  8192 5.4643e+00 2.9789e-01 2.2396e-01  24.40   1.33
