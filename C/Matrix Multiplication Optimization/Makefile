CFLAGS = -Wall

CC     = gcc $(CFLAGS)

PROGRAMS = \
	program \


all : $(PROGRAMS)

clean :
	rm -f $(PROGRAMS) *.o vgcore.* 

matvec_util.o : matvec_util.c matvec.h
	$(CC) -c $<

baseline_matvec_mult.o : baseline_matvec_mult.c matvec.h
	$(CC) -c $<

optimized_matvec_mult.o : optimized_matvec_mult.c matvec.h
	$(CC) -c $<

program : mult_benchmark.o baseline_matvec_mult.o optimized_matvec_mult.o matvec_util.o
	$(CC) -o $@ $^

mult_benchmark.o : mult_benchmark.c matvec.h
	$(CC) -c $<


