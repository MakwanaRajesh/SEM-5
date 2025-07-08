#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#define size 10000

int LinearSearch(int arr[], int key) {
    for (int i = 0; i < size; i++) {
        if (arr[i] == key) {
            return i;
        }
    }
    return -1;
}

int main() {
    int arr[size];
    FILE* fp;
    clock_t start, end;
    double cpu_time;

    fp = fopen("Best.txt", "r");
    if (fp == NULL) {
        printf("File not found.\n");
        return 1;
    }

    for (int i = 0; i < size; i++) {
        fscanf(fp, "%d", &arr[i]);
    }
    fclose(fp);

    printf("Enter Key : ");
    int key;
    scanf("%d", &key);

    start = clock();
    int res = LinearSearch(arr, key);
    end = clock();

    cpu_time = (double)(end - start) / CLOCKS_PER_SEC;

    if (res != -1) {
        printf("%d\n", res);
    } else {
        printf("Not Found 404()\n");
    }
    printf("Time : %.2f\n", cpu_time);

    return 0;
}
