#include <stdio.h>
#include <stdlib.h>
#include <time.h>

#define SIZE 100000

void generateBestCase(const char *filename) {
    FILE *fp = fopen("File_Best.txt", "w");
    for (int i = 1; i <= SIZE; i++) {
        fprintf(fp, "%d\n", i);
    }
    fclose(fp);
}

void generateWorstCase(const char *filename) {
    FILE *fp = fopen(filename, "w");
    for (int i = SIZE; i >= 1; i--) {
        fprintf(fp, "%d\n", i);
    }
    fclose(fp);
}

void generateAverageCase(const char *filename) {
    FILE *fp = fopen(filename, "w");
    int *arr = (int *)malloc(SIZE * sizeof(int));
    for (int i = 0; i < SIZE; i++) {
        arr[i] = i + 1;
    }
// No like i give format in middle add 
    // Shuffle the array using Fisher-Yates
    srand(time(NULL));
    for (int i = SIZE - 1; i > 0; i--) {
        int j = rand() % (i + 1);
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }

    for (int i = 0; i < SIZE; i++) {
        fprintf(fp, "%d\n", arr[i]);
    }
    fclose(fp);
    free(arr);
}

int main() {
    printf("Generating input files...\n");

    generateBestCase("best.txt");
    printf("Best case generated in 'best.txt'\n");

    generateAverageCase("average.txt");
    printf("Average case generated in 'average.txt'\n");

    generateWorstCase("worst.txt");
    printf("Worst case generated in 'worst.txt'\n");

    printf("All files generated successfully!\n");
    return 0;
}
