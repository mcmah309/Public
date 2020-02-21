Optimization 1: Since each matrix of size nxm can be viewed as and an array with size nxm, computation is done by iterat tnghrough the matrix in the same way the data is stored. This is faster since it utilizes localization and maximizes cache hits, instead of constantly jumping around the stack and recieving cache misses and possible page faults.

Optimization 2: The inner loop is unrolled four times. This made it faster since the program will be able to compute multiple computations in parallel once compilation optimization is done and the number of comparisons the function needs to make is reduced by a factor of 4. Addtionally, this utilizes ll the registers of the cpu. Any more loop unrolling leads to no performance improvement.

Optimization 3: Function calls were minimized to one case per function and the retrieved data is stored. This way, whenever the next information is needed, the retrieved pointer is incremented within bounds, instead of using another function.

Run "make" to compile the program and "make clean" to erase the executables.

Test Data:
  mcmah309:/ $ ./program
  SIZE       BASE       NORM        OPT BSPDUP NSPDUP
   512 2.0750e-03 1.0780e-03 5.8700e-04   3.53   1.84
  1024 4.0105e-02 4.6730e-03 2.9700e-03  13.50   1.57
  2048 3.0467e-01 1.8965e-02 1.2553e-02  24.27   1.51
  4096 1.3069e+00 7.3414e-02 4.2822e-02  30.52   1.71
  8192 5.4643e+00 2.9789e-01 2.2396e-01  24.40   1.33
